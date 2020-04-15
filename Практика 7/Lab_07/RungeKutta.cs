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
        double[] k1, k2, k3, k4, new_val;

        Dictionary<string, FloatingPoint> k2_var, k3_var, k4_var;

        double delta_y, new_x, new_y;

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

            k1 = new double[Equation.Functions.Count];
            k2 = new double[Equation.Functions.Count];
            k3 = new double[Equation.Functions.Count];
            k4 = new double[Equation.Functions.Count];
            new_val = new double[Equation.Functions.Count];


            Console.WriteLine("i\tx\ty\t\tk1\t\tk2\t\tk3\t\tk4\t\tdelta y");

            for (int div = 0; div <= Divisions; div++)
            {
                Console.Write($"{div}\t");

                foreach (var val in currValues.Values)
                    Console.Write($"{val.RealValue:0.00}\t");

                for (int i = 0; i < Equation.Functions.Count; i++)
                {
                    k1[i] = Step * Equation.Functions[i].Evaluate(InitialСondition).RealValue;
                }

                for (int i = 0; i < Equation.Functions.Count; i++)
                {
                    int j = 0;
                    k2_var = new Dictionary<string, FloatingPoint>();
                    foreach (var val in currValues)
                    {
                        if (val.Key == "x")
                            k2_var.Add("x", Expr.Parse((val.Value.RealValue + Step / 2)
                                .ToString().Replace(",", ".")).RealNumberValue);
                        else
                        {
                            k2_var.Add(val.Key, Expr.Parse((val.Value.RealValue + Step * k1[j] / 2)
                                .ToString().Replace(",", ".")).RealNumberValue);
                            j++;
                        }
                    }
                    k2[i] = Step * Equation.Functions[i].Evaluate(k2_var).RealValue;
                }

                for (int i = 0; i < Equation.Functions.Count; i++)
                {
                    int j = 0;
                    k3_var = new Dictionary<string, FloatingPoint>();
                    foreach (var val in currValues)
                    {
                        if (val.Key == "x")
                            k3_var.Add("x", Expr.Parse((val.Value.RealValue + Step / 2)
                                .ToString().Replace(",", ".")).RealNumberValue);
                        else
                        {
                            k3_var.Add(val.Key, Expr.Parse((val.Value.RealValue + k2[j] / 2)
                                .ToString().Replace(",", ".")).RealNumberValue);
                            j++;
                        }
                    }
                    k3[i] = Step * Equation.Functions[i].Evaluate(k3_var).RealValue;
                }

                for (int i = 0; i < Equation.Functions.Count; i++)
                {
                    int j = 0;
                    k4_var = new Dictionary<string, FloatingPoint>();
                    foreach (var val in currValues)
                    {
                        if (val.Key == "x")
                            k4_var.Add("x", Expr.Parse((val.Value.RealValue + Step)
                                .ToString().Replace(",", ".")).RealNumberValue);
                        else
                        {
                            k4_var.Add(val.Key, Expr.Parse((val.Value.RealValue + k3[j])
                                .ToString().Replace(",", ".")).RealNumberValue);
                            j++;
                        }
                    }
                    k4[i] = Step * Equation.Functions[i].Evaluate(k3_var).RealValue;
                }

                for (int i = 0; i < Equation.Functions.Count; i++)
                {
                    int j = 0;
                    k4_var = new Dictionary<string, FloatingPoint>();
                    foreach (var val in currValues)
                    {
                        if (val.Key == "x")
                            k4_var.Add("x", Expr.Parse((val.Value.RealValue + Step)
                                .ToString().Replace(",", ".")).RealNumberValue);
                        else
                        {
                            k4_var.Add(val.Key, Expr.Parse((val.Value.RealValue + k3[j])
                                .ToString().Replace(",", ".")).RealNumberValue);
                            j++;
                        }
                    }
                    k4[i] = Step * Equation.Functions[i].Evaluate(k3_var).RealValue;
                }

                for (int i = 0; i < Equation.Functions.Count; i++)
                {
                    
                }

                Console.WriteLine();
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
