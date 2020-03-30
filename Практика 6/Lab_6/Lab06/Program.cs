using System;
using System.Collections.Generic;
using Expr = MathNet.Symbolics.SymbolicExpression;

namespace Lab06
{
    class Program
    {
        static void Main(string[] args)
        {
            var x = Expr.Variable("x");
            var y = Expr.Variable("y");

            //Example 1
            Func<double, double, double> func1 = (x * x - 2 * y).Compile("x", "y");

            DifEquation equation1 = new DifEquation(func1);

            double[] section1 = new double[] { 0, 1};

            Dictionary<string, double> initialVal1 = new Dictionary<string, double>()
            {
                { "x", 0 },
                { "y", 1 }
            };

            RungeKutta rungeKutta1 = new RungeKutta(equation1, initialVal1, section1, 10);

            Console.WriteLine($"y' = {(x * x - 2 * y).ToString()}");

            rungeKutta1.Solve();

            //Example 2
            Func<double, double, double> func2 = ((y - x * y * y) / x).Compile("x", "y");

            DifEquation equation2 = new DifEquation(func2);

            double[] section2 = new double[] { 1, 2 };

            Dictionary<string, double> initialVal2 = new Dictionary<string, double>()
            {
                { "x", 1 },
                { "y", 2 }
            };

            RungeKutta rungeKutta2 = new RungeKutta(equation2, initialVal2, section2, 10);

            Console.WriteLine($"y' = {((y - x * y * y) / x).ToString()}");

            rungeKutta2.Solve();

            Console.ReadKey();
        }
    }
}
