using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;

namespace Sudoku
{
    public partial class SudokuForm : Form
    {
        public SudokuForm()
        {
            InitializeComponent();
            CreateCells();
            StartNewGame();
            gbSolver.Visible = false;
        }

        private SudokuCell[,] cells = new SudokuCell[9, 9];
        private SudokuPuzzle puzzle;
        private Stopwatch stopwatch = new Stopwatch();

        private void CreateCells()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
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
                }
            }
        }

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

        private async Task StartNewGame() // Creates new game and starts the stopwatch
        {
            stopwatch.Reset();
            // Clear the value in every cell
            foreach (var cell in cells)
            {
                cell.Value = 0;
                cell.Clear();
            }

            // Generate new game
            puzzle = new SudokuPuzzle();
            await Task.Run(()=>puzzle.GenerateGame(36));    //generate game with 36 blanks

            for(int i=0; i<9; i++)  // Set interface
            {
                for(int j=0; j<9; j++)
                {
                    cells[i, j].Value = puzzle.sudokuPuzzle[i, j].Value;
                    cells[i, j].IsLocked = true;
                    cells[i, j].ForeColor = ((i / 3) + (j / 3)) % 2 == 0 ? Color.Black : Color.White;
                    cells[i, j].Text = cells[i, j].Value.ToString();
                    if (cells[i, j].Value == 0)
                    {
                        cells[i,j].Clear();
                        cells[i, j].IsLocked = false;
                    }   
                }
            }
            stopwatch.Restart();
        }

        private void ClearPuzzle()  // Clears the user's solution
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (!cells[i, j].IsLocked)
                    {
                        puzzle.MakeGuess(i, j, 0);
                        cells[i, j].Value = 0;
                        cells[i, j].Clear();
                    }
                }
            }
        }


        // Button events        
        
        private void btnCheck_Click(object sender, EventArgs e) // Checks the user's solution
        {
            stopwatch.Stop();

            for(int i=0;i<9;i++)    // Send user solution 
            {
                for(int j=0; j<9; j++)
                {
                    if(!cells[i,j].IsLocked)
                    {
                        puzzle.MakeGuess(i, j, cells[i, j].Value);
                    }
                }
            }

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
            for (int i = 0; i < 9; i++) // Update the user interface
            {
                for (int j = 0; j < 9; j++)
                {
                    cells[i, j].Value = puzzle.sudokuPuzzle[i, j].Value;
                    if (!cells[i,j].IsLocked)
                        cells[i, j].ForeColor = SystemColors.ControlDarkDark;   //Change the color in unlocked fields
                    cells[i, j].Text = cells[i, j].Value.ToString();
                    if (cells[i, j].Value == 0)
                        cells[i, j].Clear();
                }
            }
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
            for (int i = 0; i < 9; i++)    // Send user solution 
            {
                for (int j = 0; j < 9; j++)
                {
                    cells[i,j].Value = 0;
                    cells[i,j].Clear();
                    cells[i, j].IsLocked = false;

                }
            }
        }

        private void btnSolveCustom_Click(object sender, EventArgs e) // Solve user defined puzzle
        {
            for (int i = 0; i < 9; i++) // Update puzzle with user values
            {
                for (int j = 0; j < 9; j++)
                {
                    if(cells[i,j].Value!=0)
                    {
                        puzzle.MakeGuess(i, j, cells[i, j].Value);  // Set sudoku puzzle fields
                        cells[i, j].IsLocked = true;    //Change field status
                    }
                }
            }
            puzzle.SolvePuzzle();
            for (int i = 0; i < 9; i++) // Update the user interface
            {
                for (int j = 0; j < 9; j++)
                {
                    cells[i, j].Value = puzzle.sudokuPuzzle[i, j].Value;
                    cells[i, j].ForeColor = ((i / 3) + (j / 3)) % 2 == 0 ? Color.Black : Color.White; // Set the color depending of square
                    if (!cells[i, j].IsLocked)
                        cells[i, j].ForeColor = SystemColors.ControlDarkDark;   //Change the color in unlocked fields
                    cells[i, j].Text = cells[i, j].Value.ToString();
                    if (cells[i, j].Value == 0)
                        cells[i, j].Clear();
                }
            }
            stopwatch.Reset();
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
