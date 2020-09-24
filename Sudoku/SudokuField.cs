using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    struct SudokuField
    {
        public int Value { get; set; }
        public bool Status { get; set; } //if the value can be changed

        public SudokuField(int value, bool status)
        {
            Status = status;
            Value = value;
        }
    }
}
