using System;
using System.Collections.Generic;
using System.Text;

namespace Lab02
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
            string message = $"{Message}\nКоличество строк: {Rows}\nКоличество столбцов: {Columns}\n";

            return message;
        }
    }
}
