using MathNet.Symbolics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Expr = MathNet.Symbolics.SymbolicExpression;

namespace Lab06
{
    class RungeKutta
    {
        double k1, k2, k3, k4;

        double k2_x, k2_y, k3_x, k3_y, k4_x, k4_y, delta_y, new_x, new_y;

        Dictionary<string, double> currValues;

        public DifEquation Equation { get; set; }

        public Dictionary<string, double> InitialСondition { get; set; }

        public double[] Section { get; set; }

        public int Divisions { get; set; }

        public double Step { get; }

        public RungeKutta(DifEquation equation, Dictionary<string, double> initialCondition,
            double[] section, int divisions)
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

            PrintInfo();

            Console.WriteLine("i\tx\ty\t\tk1\t\tk2\t\tk3\t\tk4\t\tdelta y");

            for (int div = 0; div <= Divisions; div++)
            {
                Console.Write($"{div}\t");
                    
                Console.Write($"{currValues["x"]:0.00}\t");
                Console.Write($"{currValues["y"]:0.000000}\t");

                k1 = Equation.Function(currValues["x"], currValues["y"]);

                Console.Write($"{k1:0.000000}\t");

                k2_x = (currValues["x"] + Step / 2);
                k2_y = (currValues["y"] + Step * k1 / 2);

                k2 = Equation.Function(k2_x, k2_y);

                Console.Write($"{k2:0.000000}\t");

                k3_x = (currValues["x"] + Step / 2);
                k3_y = (currValues["y"] + Step * k2 / 2);

                k3 = Equation.Function(k3_x, k3_y);

                Console.Write($"{k3:0.000000}\t");

                k4_x = (currValues["x"] + Step);
                k4_y = (currValues["y"] + Step * k3);

                k4 = Equation.Function(k4_x, k4_y);

                Console.Write($"{k4:0.000000}\t");

                delta_y = Step / 6 * (k1 + k2 * 2 + k3 * 2 + k4);

                Console.WriteLine($"{delta_y:0.000000}");

                new_x = currValues["x"] + Step;
                new_y = currValues["y"] + delta_y;

                currValues = new Dictionary<string, double>()
                {
                    { "x", new_x },
                    { "y", new_y }
                };
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
