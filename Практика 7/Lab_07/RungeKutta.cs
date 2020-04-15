using MathNet.Symbolics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Symbolics;
using Expr = MathNet.Symbolics.SymbolicExpression;

namespace Lab_07
{
    class RungeKutta
    {
        //double k1, k2, k3, k4;

        double k2_x, k2_y, k3_x, k3_y, k4_x, k4_y, delta_y, new_x, new_y;

        string[] str_var = new string[] { "x", "y1", "y2" };

        Dictionary<string, FloatingPoint> currValues;

        public DifEquationSystem Equation { get; set; }

        public Dictionary<string, FloatingPoint> InitialСondition { get; set; }

        public double[] Section { get; set; }

        public int Divisions { get; set; }

        public double Step { get; }

        public RungeKutta
            (DifEquationSystem equation, Dictionary<string, FloatingPoint> initialCondition, double[] section, int divisions)
        {
            Equation = equation;
            InitialСondition = initialCondition;
            Section = section;
            Divisions = divisions;
            Step = CalculateStep();
        }

        private double CalculateStep()
        {
            double step = (Section[1] - Section[0]) / Divisions;
            return step;
        }

        public void Solve()
        {
            currValues = InitialСondition;

            //PrintInfo();

            Console.WriteLine("i\tx\ty\t\tk1\t\tk2\t\tk3\t\tk4\t\tdelta y");

            for (int div = 0; div <= Divisions; div++)
            {
                foreach(var function in Equation.Functions)
                {
                    Console.Write($"{div}\t");

                    //Console.Write($"{currValues["x"]:0.00}\t");
                   // Console.Write($"{currValues["y"]:0.000000}\t");

                    var k1 = function.Evaluate(InitialСondition).RealValue;
                    Console.WriteLine($"k1 = {k1}");

                }        
            }


            Console.WriteLine("");
        }

        private void PrintInfo()
        {
            Console.WriteLine($"Начальное условие: y({InitialСondition["x"]}) = {InitialСondition["y"]}");

            Console.WriteLine($"Рассматриваемый интервал: [{Section[0]}; {Section[1]}]");

            Console.WriteLine($"Количество делений: {Divisions}");

            Console.WriteLine($"Величина шага: {Step}");
        }
    }
}
