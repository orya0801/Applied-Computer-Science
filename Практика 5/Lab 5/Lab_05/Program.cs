using System;
using System.Collections.Generic;
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

            var values1 = new Dictionary<string, FloatingPoint>
            {
                { "x1", 0.9},
                { "x2", 0.5} 
            };

            var f1 = (x1 * x1);
            var f2 = (x2 * x2 - 1);
            var f3 = (x1 * x1 * x1);
            var f4 = (- x2);

            Expr[,] functions1 = new Expr[,] { { f1, f2 }, { f3, f4 } };

            var system1 = new EquationsSystem(functions1);

            system1.Print();

            var newton1 = new Newton(system1, values1, 0.0001);

            newton1.Solve();

            var values2 = new Dictionary<string, FloatingPoint>
            {
                {"x1", 0.5 },
                {"x2", 0.5 },
                {"x3", 0.5 }
            };

            f1 = (x1 * x1);
            f2 = (x2 * x2);
            f3 = (x3 * x3 - 1);
            f4 = (2 * x1 * x1);
            var f5 = f2;
            var f6 = (-4 * x3);
            var f7 = (3 * x1 * x1);
            var f8 = (-4 * x2);
            var f9 = (x3 * x3);

            Expr[,] functions2 = new Expr[,] { { f1, f2, f3 }, { f4, f5, f6 }, {f7, f8, f9 } };

            var system2 = new EquationsSystem(functions2);

            system2.Print();

            var newton2 = new Newton(system2, values2, 0.005);

            newton2.Solve();

            Console.ReadLine();
        }
    }
}
