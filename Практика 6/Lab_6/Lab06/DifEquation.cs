using System;

namespace Lab06
{
    class DifEquation
    { 
        public Func<double, double, double> Function { get; set; }

        public DifEquation(Func<double, double, double> function)
        {
            Function = function;
        }

    }
}
