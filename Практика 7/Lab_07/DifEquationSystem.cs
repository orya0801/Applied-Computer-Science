using System;
using System.Collections.Generic;
using Expr = MathNet.Symbolics.SymbolicExpression;

namespace Lab_07
{
    class DifEquationSystem
    { 
        public List<Expr> Functions { get; set; }

        public DifEquationSystem(List<Expr> system)
        {
            Functions = system;
        }
    }
}
