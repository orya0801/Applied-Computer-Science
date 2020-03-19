using System;
using System.Collections.Generic;
using System.Text;

namespace Lab02
{
    class DeterminantZeroException : Exception
    {
        public DeterminantZeroException(string message) : base(message) { }

    }
}
