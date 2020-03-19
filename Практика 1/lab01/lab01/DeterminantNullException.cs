using System;
using System.Collections.Generic;
using System.Text;

namespace lab01
{
    class DeterminantNullException : Exception
    {
        public DeterminantNullException(string message) : base(message) { }
    }
}
