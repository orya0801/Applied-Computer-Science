using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Symbolics;
using Expr = MathNet.Symbolics.SymbolicExpression;

namespace Lab_05
{
    class EquationsSystem
    {
        public Expr[,] Functions { get; set; }

        public EquationsSystem(Expr[,] expr)
        {
            Functions = expr;
        }

        public void Print()
        {
            for (int i = 0; i < Functions.GetLength(0); i++)
            {
                for (int j = 0; j < Functions.GetLength(1); j++)
                {
                    Console.Write(Functions[i, j]);
                }

                Console.WriteLine(" = 0");
            }
        }

    }
}
