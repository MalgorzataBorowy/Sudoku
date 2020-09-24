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
            createCells();
            startNewGame();
        }

        SudokuCell[,] cells = new SudokuCell[9, 9];
        private SudokuPuzzle puzzle;
        private Stopwatch stopwatch = new Stopwatch();

        private void createCells()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    // Create 81 cells for with styles and locations based on the index
                    cells[i, j] = new SudokuCell();
                    cells[i, j].Font = new Font(SystemFonts.DefaultFont.FontFamily, 20);
                    cells[i, j].Size = new Size(40, 40);
                    cells[i, j].ForeColor = Color.Black;
                    cells[i, j].Location = new Point(i * 40, j * 40);
                    cells[i, j].BackColor = ((i / 3) + (j / 3)) % 2 == 0 ? SystemColors.Control : Color.LightGray;
                    cells[i, j].FlatStyle = FlatStyle.Flat;
                    cells[i, j].FlatAppearance.BorderColor = Color.Black;
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
            if (int.TryParse(e.KeyChar.ToString(), out value) && value<=9 && value>0)
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

        private void startNewGame()
        {
            // Clear the values in each cells
            foreach (var cell in cells)
            {
                cell.Value = 0;
                cell.Clear();
            }

            // Generate new game
            puzzle = new SudokuPuzzle();
            puzzle.generateGame(36);

            for(int i=0; i<9; i++)
            {
                for(int j=0; j<9; j++)
                {
                    cells[i, j].Value = puzzle.sudokuPuzzle[i, j].Value;
                    cells[i, j].IsLocked = true;
                    cells[i, j].ForeColor = Color.Black;
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
        private void btnCheck_Click(object sender, EventArgs e)
        {
            stopwatch.Stop();
            for(int i=0;i<9;i++)
            {
                for(int j=0; j<9; j++)
                {
                    if(!cells[i,j].IsLocked)
                    {
                        puzzle.makeGuess(i, j, cells[i, j].Value);
                    }
                }
            }

            bool solved = puzzle.checkPuzzle();

            if (solved)
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

        private void btnSolve_Click(object sender, EventArgs e)
        {
            puzzle.solvePuzzle();
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    cells[i, j].Value = puzzle.sudokuPuzzle[i, j].Value;
                    cells[i, j].IsLocked = !puzzle.sudokuPuzzle[i,j].Status;
                    if (!cells[i,j].IsLocked)
                        cells[i, j].ForeColor = SystemColors.ControlDarkDark;
                    cells[i, j].Text = cells[i, j].Value.ToString();
                    if (cells[i, j].Value == 0)
                    {
                        cells[i, j].Clear();
                        cells[i, j].IsLocked = false;
                    }
                }
            }
        }

        private void btnNewGame_Click(object sender, EventArgs e)
        {
            startNewGame();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            foreach (var cell in cells)
            {
                // Clear the cell only if it is not locked
                if (cell.IsLocked == false)
                    cell.Clear();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            TimeSpan ts = stopwatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}",ts.Hours, ts.Minutes, ts.Seconds);
            label1.Text = elapsedTime;
        }
    }
}
