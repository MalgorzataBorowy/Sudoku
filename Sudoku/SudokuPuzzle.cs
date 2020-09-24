using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    class SudokuPuzzle : Sudoku
    {
        public SudokuField[,] sudokuPuzzle { get;  }

        public SudokuPuzzle()
        {
            sudokuPuzzle = new SudokuField[size, size];
        }

        public void generateGame(int blanks)
        {
            //this.generateSudoku();
            this.generateDiagonalPuzzle();


            /*matrix = new int[,] {{ 6, 3, 8, 2, 1, 9, 7, 5, 4}, { 9, 7, 5, 6, 3, 4, 2, 8, 1},{ 1, 2, 4, 8, 5, 7, 9, 3, 6},{ 3, 5, 1, 7, 4, 6, 8, 9, 2},
{ 4, 6, 9, 5, 8, 2, 3, 1, 7},
{ 2, 8, 7, 1, 9, 3, 6, 4, 5},
{ 8, 1, 6, 3, 7, 5, 4, 2, 9},
{ 5, 4, 2, 9, 6, 8, 1, 7, 3},
{ 7, 9, 3, 4, 2, 1, 5, 6, 8} };*/
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    sudokuPuzzle[i, j] = new SudokuField(matrix[i, j], false);
                }
            }
            solvePuzzle();

            while (blanks > 0)
            {
                int i = generateRandom() - 1;
                int j = generateRandom() - 1;
                if (sudokuPuzzle[i, j].Value != 0)
                {
                    sudokuPuzzle[i, j].Value = 0;
                    sudokuPuzzle[i, j].Status = true;
                    blanks--;
                }
            }

        }

        public void makeGuess(int x, int y, int value)
        {
            if (sudokuPuzzle[x, y].Status)
                sudokuPuzzle[x, y].Value = value;
        }

        private bool checkSquare(int value, int r, int k)
        {
            // Calculating coordinates of first element of a square
            int a = r / 3;
            int b = k / 3;
            a = a * 3;
            b = b * 3;

            for (int i = a; i < a + 3; i++)
            {
                for (int j = b; j < b + 3; j++)
                {
                    if (!(a + i % 3 == r && b + j % 3 == k))    //Checking if field coordinates are the same as input coordinates
                    {
                        if (sudokuPuzzle[i, j].Value == value)   //Checking if the field's value is equal to input value
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private bool checkRow(int value, int r, int k)
        {
            for (int i = 0; i < size; i++)
            {
                if (i != k && sudokuPuzzle[r, i].Value == value)
                    return false;
            }
            return true;
        }
        private bool checkColumn(int value, int r, int k)
        {
            for (int i = 0; i < size; i++)
            {
                if (i != r && sudokuPuzzle[i, k].Value == value)
                    return false;
            }
            return true;
        }

        public bool checkPuzzle()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (sudokuPuzzle[i, j].Value == 0)
                        return false;
                    if (!(checkSquare(sudokuPuzzle[i, j].Value, i, j) && checkRow(sudokuPuzzle[i, j].Value, i, j) &&
                        checkColumn(sudokuPuzzle[i, j].Value, i, j)))
                        return false;
                }
            }
            return true;
        }

        public bool solvePuzzle()
        {
            bool emptyField = false;
            int r = 0;
            int k = 0;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (sudokuPuzzle[i, j].Value == 0)
                    {
                        r = i;
                        k = j;
                        emptyField = true;
                    }
                }
            }
            if (!emptyField)
                return true;

            for (int value = 1; value < 10; value++)
            {
                if (checkSquare(value, r, k) && checkRow(value, r, k) && checkColumn(value, r, k))
                {
                    sudokuPuzzle[r, k].Value = value;
                    if (solvePuzzle())
                        return true;
                    sudokuPuzzle[r, k].Value = 0;
                }
            }

            return false;
        }
    }
}
