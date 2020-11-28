using System;

namespace Sudoku
{
    struct SudokuField  //single field of sudoku puzzle
    {
        public int Value { get; set; }
        public bool IsLocked { get; set; } //if the value can be changed

        public SudokuField(int value, bool status)
        {
            IsLocked = status;
            Value = value;
        }
    }
}
