using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using Emgu.CV;
using Emgu.CV.Structure;
using Tesseract;

namespace Sudoku
{
    public partial class SudokuForm : Form
    {
        /// <summary>
        /// UI for SudokuPuzzle using Windows Forms
        /// </summary>

        public SudokuForm()
        {
            InitializeComponent();
            CreateCells();
            StartNewGame();
            gbSolver.Visible = false;
        }

        // Fileds

        private SudokuCell[,] cells = new SudokuCell[9, 9];
        private SudokuPuzzle puzzle;
        private Stopwatch stopwatch = new Stopwatch();

        // Methods

        private void ExecuteForEvery(Action<int,int> toExecute)
        {
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    toExecute(i,j);
        }

        private void CreateCells()
        {
            ExecuteForEvery((i,j) =>
            {
                // Create 81 cells for with styles and locations based on the index
                cells[i, j] = new SudokuCell();
                cells[i, j].Font = new Font(SystemFonts.DefaultFont.FontFamily, 20);
                cells[i, j].Size = new Size(40, 40);
                cells[i, j].Location = new Point(i * 40, j * 40);
                cells[i, j].BackColor = ((i / 3) + (j / 3)) % 2 == 0 ? SystemColors.Control : Color.LightSeaGreen;
                cells[i, j].ForeColor = ((i / 3) + (j / 3)) % 2 == 0 ? Color.Black : Color.White;
                cells[i, j].FlatStyle = FlatStyle.Flat;
                cells[i, j].FlatAppearance.BorderColor = Color.Teal;
                cells[i, j].FlatAppearance.BorderSize = 1;
                cells[i, j].X = i;
                cells[i, j].Y = j;

                // Assign key press event for each cells
                cells[i, j].KeyPress += cell_keyPressed;

                panel1.Controls.Add(cells[i, j]);
            });
        }

        private void UpdateUI()
        {
            // Update the user interface
            ExecuteForEvery((i, j) =>
            {
                cells[i, j].Value = puzzle.sudokuPuzzle[i, j].Value;
                cells[i, j].ForeColor = ((i / 3) + (j / 3)) % 2 == 0 ? Color.Black : Color.White; // Set the color depending of square
                if (!cells[i, j].IsLocked)
                    cells[i, j].ForeColor = SystemColors.ControlDarkDark;   //Change the color in unlocked fields
                cells[i, j].Text = cells[i, j].Value.ToString();
                if (cells[i, j].Value == 0)
                    cells[i, j].Clear();
            });
        }

        private async Task StartNewGame() // Creates new game and starts the stopwatch
        {
            stopwatch.Reset();
            // Clear the value in every cell
            ResetUI();

            // Generate new game
            puzzle = new SudokuPuzzle();
            await Task.Run(()=>puzzle.GenerateGame(36));    //generate game with 36 blanks

            ExecuteForEvery((i, j) =>
            {
                if(puzzle.sudokuPuzzle[i, j].Value!=0)
                    cells[i, j].IsLocked = true;
            });
            UpdateUI();
            stopwatch.Restart();
        }

        private void ClearPuzzle()  // Clears the user's solution
        {
            ExecuteForEvery((i, j) =>
            {
                if (!cells[i, j].IsLocked)
                {
                    puzzle.MakeGuess(i, j, 0);
                    cells[i, j].Value = 0;
                    cells[i, j].Clear();
                }
            });
        }

        private void ResetUI()  // 
        {
            ExecuteForEvery((i, j) =>
            {
                cells[i, j].Value = 0;
                cells[i, j].Clear();
            });
        }

        private string[] ProcessSudokuImg(string fileName)  // --> Wydzielic do innej klasy!
        {
            int height = 450;
            int width = 450;
            string[] digits = new string[81];

            Image<Bgr, byte> image = new Image<Bgr, byte>(fileName);
            image = image.Resize(width, height, Emgu.CV.CvEnum.Inter.Linear);

            Image<Gray, byte> grayImage = image.Convert<Gray, byte>();
            Image<Gray, byte> buffer = grayImage.Copy();
            CvInvoke.GaussianBlur(grayImage, buffer, new System.Drawing.Size(5, 5), 1);
            grayImage = buffer;
            CvInvoke.AdaptiveThreshold(grayImage, buffer, 255, Emgu.CV.CvEnum.AdaptiveThresholdType.GaussianC, Emgu.CV.CvEnum.ThresholdType.Binary, 5, 2);
            grayImage = buffer;

            // Split image into 81 parts
            Image<Gray, byte>[] fields = new Image<Gray, byte>[81];

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    int border = 5;
                    System.Drawing.Rectangle rect = new System.Drawing.Rectangle(i * (width / 9) + border, j * (height / 9) + border, (width / 9) - 2 * border, (height / 9) - 2 * border);
                    grayImage.ROI = rect;
                    var index = i * 9 + j;
                    fields[index] = grayImage.CopyBlank();
                    grayImage.CopyTo(fields[index]);
                    grayImage.ROI = System.Drawing.Rectangle.Empty;
                }
            }

            // Recognize digits
            using (TesseractEngine engine = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default))
            {
                engine.SetVariable("tessedit_char_whitelist", "0123456789");
                int i = 0;  //iterator
                foreach (var field in fields)
                {
                    Page page = engine.Process(field.ToBitmap(), PageSegMode.SingleChar);
                    string result = page.GetText();
                    page.Dispose();
                    digits[i++] = result.Trim();
                    field.Dispose();
                }
            }
            image.Dispose(); grayImage.Dispose(); buffer.Dispose();
            return digits;
        }


        // Button events     

        private void cell_keyPressed(object sender, KeyPressEventArgs e)
        {
            var cell = sender as SudokuCell;

            // Do nothing if the cell is locked
            if (cell.IsLocked)
                return;

            int value;

            // Add the pressed key value in the cell only if it is a number
            if (int.TryParse(e.KeyChar.ToString(), out value))
            {
                // Clear the cell value if pressed key is zero
                if (value == 0)
                    cell.Clear();
                else
                {
                    cell.Text = value.ToString();
                    cell.Value = value;
                }
                cell.ForeColor = SystemColors.ControlDarkDark;
            }
        }

        private void btnCheck_Click(object sender, EventArgs e) // Checks the user's solution
        {
            stopwatch.Stop();
            ExecuteForEvery((i, j) =>
            {
                if (!cells[i, j].IsLocked)
                {
                    puzzle.MakeGuess(i, j, cells[i, j].Value);
                }
            });

            bool solved = puzzle.CheckPuzzle();

            if (solved)  // Show message with result
            {
                TimeSpan ts = stopwatch.Elapsed;
                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}",ts.Hours, ts.Minutes, ts.Seconds);
                MessageBox.Show($"Congrats! Puzzle solved\n\nYour time: {elapsedTime}\n");
                stopwatch.Reset();
            }
                
            else
            {
                MessageBox.Show("Try again. Solution is not correct");
                stopwatch.Start();
            }

        }

        private void btnSolve_Click(object sender, EventArgs e) //Solves the puzzle and resets the stopwatch to 0
        {
            ClearPuzzle();  // Clear puzzle from user modifications
            puzzle.SolvePuzzle();
            UpdateUI();
            stopwatch.Reset();
        }

        private async void btnNewGame_Click(object sender, EventArgs e)
        {
            await StartNewGame();
        }

        private void btnClear_Click(object sender, EventArgs e) // Clears the puzzle and restarts the stopwatch
        {
            ClearPuzzle();
            stopwatch.Restart();
        }

        private void btnBlank_Click(object sender, EventArgs e) 
        {
            stopwatch.Reset();
            puzzle.Reset();
            ResetUI();
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            stopwatch.Reset();
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                if (ofd.ShowDialog() != DialogResult.OK)
                    throw new Exception("Cannot open a file");

                string[] digits = ProcessSudokuImg(ofd.FileName);

                // Set the cells and puzzle with detected digits
                puzzle.Reset();     // Reset the puzzle content 
                ResetUI();

                // Update puzzle with values from image
                ExecuteForEvery((i, j) =>
                {
                    if (Int32.TryParse(digits[9 * i + j], out int value))
                    {
                        puzzle.MakeGuess(i, j, value);  // Set sudoku puzzle fields
                        cells[i, j].IsLocked = true;
                        cells[i, j].Text = value.ToString();
                    }
                });
                UpdateUI();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnSolveCustom_Click(object sender, EventArgs e) // Solve user defined puzzle
        {
            // Set values in fields
            ExecuteForEvery((i, j) =>
            {
                if (cells[i, j].Value != 0)
                {
                    puzzle.MakeGuess(i, j, cells[i, j].Value);  // Set sudoku puzzle fields
                    cells[i, j].IsLocked = true;    //Change field status
                }
            });

            if (puzzle.CheckValidity()) // Check if in any column, row or square there are no duplicate values
            {
                puzzle.SolvePuzzle();
                UpdateUI();
                stopwatch.Reset();
            }
            else
            {
                ExecuteForEvery((i, j) =>
                {
                    cells[i, j].IsLocked = false;    //Change field status to allow further field modification
                });
                MessageBox.Show("Incorrect input");
            }
        }

        private void rbModeChanged(object sender, EventArgs e)    // Changes app mode between game and solver
        {
            if(rbGame.Checked)
            {
                gbGame.Visible = true;
                gbSolver.Visible = false;
                StartNewGame();
            }
            else if(rbSolver.Checked)
            {
                gbGame.Visible = false;
                gbSolver.Visible = true;
                btnBlank_Click(sender, e);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)    // Updates the label with elapsed time displayed every 1 sec
        {
            TimeSpan ts = stopwatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}",ts.Hours, ts.Minutes, ts.Seconds);
            label1.Text = elapsedTime;
        }
    }
}
