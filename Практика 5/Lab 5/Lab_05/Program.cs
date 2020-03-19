using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Symbolics;
using Expr = MathNet.Symbolics.SymbolicExpression;


namespace Lab_05
{
    class Program
    {
        static void Main(string[] args)
        {
            var x1 = Expr.Variable("x1");
            var x2 = Expr.Variable("x2");
            var x3 = Expr.Variable("x3");
            var x4 = Expr.Variable("x4");

            var values = new Dictionary<string, FloatingPoint>
            {
                { "x1", 0.9},
                { "x2", 0.5} 
            };

            var f1 = (x1 * x1);
            var f2 = (x2 * x2 - 1);
            var f3 = (x1 * x1 * x1);
            var f4 = (- x2);

            Expr[,] functions = new Expr[,] { { f1, f2 }, { f3, f4 } };

            var system = new EquationsSystem(functions);

            system.Print();

            var newton1 = new Newton(system, values, 0.0001);

            newton1.Solve();

            Console.ReadLine();
        }
    }
}
