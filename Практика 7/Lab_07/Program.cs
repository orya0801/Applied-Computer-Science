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
                { "x", 0 },
                { "y1", 3 },
                { "y2", 0 }
            };

            Expr func1 = (-2 * y1 + 4 * y2);
            Expr func2 = (- y1 + 3 * y2);
            var functions = new List<Expr>();
            functions.Add(func1);
            functions.Add(func2);

            DifEquationSystem equation1 = 
                new DifEquationSystem(functions);

            double[] section1 = new double[] { 0, 1 };

           
            RungeKutta rungeKutta1 = 
                new RungeKutta(equation1, initialVal1, section1, 10);

            Func <double, double> answer1 = (4 * (-x).Exp() - (2 * x).Exp()).Compile("x");
            Func <double, double> answer2= ((-x).Exp() - (2 * x).Exp()).Compile("x");
            var answers = new List<Func<double, double>>();
            answers.Add(answer1);
            answers.Add(answer2);

            PrintRealAnswer(answers, initialVal1, section1, 10);

            rungeKutta1.Solve();

            Console.ReadKey();
        }

        private static void PrintRealAnswer(List<Func<double, double>> answers,
            Dictionary<string, FloatingPoint> initVal, double[] section, int div)
        {
            double h = (section[1] - section[0]) / div;
            double curr_x = section[0];

            Console.WriteLine("Точное решение:\n");

            Console.Write("i\t");
            foreach (var key in initVal.Keys)
            {
                Console.Write($"{key}\t");
            }
            Console.WriteLine();

            for (int i = 0; i <= div; i++)
            {
                Console.Write($"{i}\t");
                Console.Write($"{curr_x:0.0000}\t");
                foreach (var answer in answers)
                    Console.Write($"{answer(curr_x):0.0000}\t");
                Console.WriteLine();

                curr_x += h;
            }

            Console.WriteLine();
        }
    }
}
