﻿using System;
using System.Collections.Generic;

namespace Sudoku
{
    /// <summary>
    ///  Class for generating 
    ///  - fully filled sudoku (random), 
    ///  - sudoku with diagonal squares filled (random),
    ///  - setting sudoku with custom solution.
    /// </summary>
    
    class Sudoku
    {
        protected const int size = 9;
        protected int[,] matrix;        //matrix for a fully filled sudoku


        public Sudoku()
        {
            matrix = new int[size, size];
        }

        protected int GenerateRandom()  // generates random number from 1 to 9
        {
            Random rnd = new Random();
            return rnd.Next(1, 10);
        }

        private bool CheckSquare(int value, int k, int r)   //checks if value is already placed in a given square
        {
            for (int i = k; i < k + 3; i++)
            {
                for (int j = r; j < r + 3; j++)
                {
                    if (matrix[i, j] == value)
                        return false;
                }
            }
            return true;
        }

        private bool CheckRow(int value, int r)
        {
            for (int i = 0; i < size; i++)
            {
                if (matrix[r, i] == value)
                    return false;
            }
            return true;
        }
        private bool CheckColumn(int value, int k)
        {
            for (int i = 0; i < size; i++)
            {
                if (matrix[i, k] == value)
                    return false;
            }
            return true;
        }

        private bool FillSquare(int k, int r)
        {
            for (int i = k - 1; i < k + 2; i++)
            {
                for (int j = r - 1; j < r + 2; j++)
                {
                    List<int> table = new List<int>();
                    int rnd = GenerateRandom();
                    while (!(CheckSquare(rnd, k - 1, r - 1) && (CheckRow(rnd, i)) && CheckColumn(rnd, j)))
                    {
                        rnd = GenerateRandom();
                        if (!table.Contains(rnd))
                        {
                            table.Add(rnd);
                        }
                        if (table.Count >= 9)
                            return false;
                    }
                    matrix[i, j] = rnd;
                }
            }
            return true;
        }

        private void FillWithZeros()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                    matrix[i, j] = 0;
            }
        }

        private void GenerateDiagonal()
        {
            FillSquare(1, 1);
            FillSquare(4, 4);
            FillSquare(7, 7);
        }
        
        protected void GenerateDiagonalPuzzle()
        {
            FillWithZeros();
            GenerateDiagonal();
        }

        private bool GenerateNonDiagonal()
        {
            int cos = 0;
            GenerateDiagonal();
            for (int i = 1; i < size; i += 3)
            {
                for (int j = 1; j < size; j += 3)
                {
                    if (i != j)
                    {
                        bool flag = FillSquare(i, j);
                        if (!flag)
                        {
                            return false;
                        }
                        cos++;
                        if (cos == 6) //ile kwadratów jest wypełnionych (6 to wszystkie)
                            return true;
                    }
                }
            }
            return true;
        }

        public bool GenerateSudoku()
        {
            bool flag = GenerateNonDiagonal();
            while (!flag)
            {
                FillWithZeros();
                flag = GenerateNonDiagonal();
            }
            return flag;
        }

        public void SetMatrix(int[,] matrix)
        {
            if (matrix.GetLength(0) == size && matrix.GetLength(1) == size)
                this.matrix = matrix;
        }
    }
}
