using System;
using System.Collections.Generic;
using MathNet.Symbolics;
using Expr = MathNet.Symbolics.SymbolicExpression;

namespace Lab_07
{
    class Program
    {
        static void Main(string[] args)
        {
            var x = Expr.Variable("x");
            var y1 = Expr.Variable("y1");
            var y2 = Expr.Variable("y2");

            //Example 1
            Dictionary<string, FloatingPoint> initialVal1 = new Dictionary<string, FloatingPoint>()
            {
                { "x", 2 },
                { "y1", 1 },
                { "y2", 1}
            };

            Expr func1 = (x * x - 2 * y1);
            Expr func2 = (x  - 2 * y1 + 2 * y2);
            var functions = new List<Expr>();
            functions.Add(func1);
            functions.Add(func2);

            DifEquationSystem equation1 = 
                new DifEquationSystem(functions);

            double[] section1 = new double[] { 0, 1 };

           
            RungeKutta rungeKutta1 = 
                new RungeKutta(equation1, initialVal1, section1, 10);


            rungeKutta1.Solve();

            Console.ReadKey();
        }
    }
}
