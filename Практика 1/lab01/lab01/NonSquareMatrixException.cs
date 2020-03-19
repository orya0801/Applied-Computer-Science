using System;
using System.Collections.Generic;
using System.Text;

namespace lab01
{
    class NonSquareMatrixException : Exception
    {
        public int Rows { get; set; }
        public int Columns { get; set; }

        public NonSquareMatrixException(string message, int rows, int columns) : base(message)
        {
            Rows = rows;
            Columns = columns;
        }

        public override string ToString()
        {
            string message = $"{Message}\nКоличество строк: {Rows}\nКоличество столбцов: {Columns}";

            return message;
        }
    }
}
