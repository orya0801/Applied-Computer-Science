using MathNet.Symbolics;
using System;
using System.Collections.Generic;
using Expr = MathNet.Symbolics.SymbolicExpression;

namespace Lab_07
{
    class RungeKutta
    {
        double[,] k1, k2, k3, k4;
        FloatingPoint[] new_val;

        Dictionary<string, FloatingPoint> k2_var, k3_var, k4_var;

        Dictionary<string, FloatingPoint> currValues;

        public DifEquationSystem Equation { get; set; }

        public Dictionary<string, List<FloatingPoint>> InitialСondition { get; set; }

        public double[] Section { get; set; }

        public int Divisions { get; set; }

        public double Step { get; }

        public RungeKutta
            (DifEquationSystem equation, Dictionary<string, List<FloatingPoint>> initialCondition, double[] section, int divisions)
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
            PrintInfo();

            k1 = new double[Divisions + 1, Equation.Functions.Count];
            k2 = new double[Divisions + 1, Equation.Functions.Count];
            k3 = new double[Divisions + 1, Equation.Functions.Count];
            k4 = new double[Divisions + 1, Equation.Functions.Count];
            new_val = new FloatingPoint[currValues.Count];


            Console.Write("i\t");
            foreach( var key in InitialСondition.Keys)
            {
                Console.Write($"{key}\t");
            }
            Console.WriteLine();

            for (int div = 0; div <= Divisions; div++)
            {
                Console.Write($"{div}\t");

                currValues = new Dictionary<string, FloatingPoint>();
                foreach (var val in InitialСondition)
                {
                    currValues.Add(val.Key, val.Value[div]);
                }

                foreach (var val in currValues.Values)
                    Console.Write($"{val.RealValue:0.0000}\t");

                //Нахождение k1 для каждого уравнения
                for (int i = 0; i < Equation.Functions.Count; i++)
                {
                    k1[div, i] = Step * Equation.Functions[i].Evaluate(currValues).RealValue;
                }
                //Нахождение k2 для каждого уравнения
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
                            k2_var.Add(val.Key, Expr.Parse((val.Value.RealValue + k1[div, j] / 2)
                                .ToString().Replace(",", ".")).RealNumberValue);
                            j++;
                        }
                    }
                    k2[div, i] = Step * Equation.Functions[i].Evaluate(k2_var).RealValue;
                }
                //Нахождение k3 для каждого уравнения
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
                            k3_var.Add(val.Key, Expr.Parse((val.Value.RealValue + k2[div, j] / 2)
                                .ToString().Replace(",", ".")).RealNumberValue);
                            j++;
                        }
                    }
                    k3[div, i] = Step * Equation.Functions[i].Evaluate(k3_var).RealValue;
                }
                //Нахождение k4 для каждого уравнения
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
                            k4_var.Add(val.Key, Expr.Parse((val.Value.RealValue + k3[div, j])
                                .ToString().Replace(",", ".")).RealNumberValue);
                            j++;
                        }
                    }
                    k4[div, i] = Step * Equation.Functions[i].Evaluate(k4_var).RealValue;
                }

                int k = 0;
                foreach (var val in currValues.Values)
                {
                    new_val[k] = val;
                    k++;
                }

                //Обновление значений x, y1, y2, ...
                k = 0;
                foreach(var val in InitialСondition)
                {
                    if (val.Key == "x")
                        InitialСondition["x"].Add(Expr.Parse((new_val[k].RealValue + Step)
                            .ToString().Replace(",", ".")).RealNumberValue);
                    else
                    {
                        InitialСondition[val.Key].Add(Expr.Parse((new_val[k].RealValue + 
                            (k1[div, k-1] + 2* k2[div, k-1] + 2 * k3[div, k-1] + k4[div, k-1])/6)
                            .ToString().Replace(",", ".")).RealNumberValue);

                    }
                    k++;
                }

                Console.WriteLine();
            }


            Console.WriteLine("");
        }

        public void Miln()
        {
            for(int i = 0; i < Equation.Functions.Count; i++)
            {
                for (int div = 3; div <= Divisions; div++)
                {
                    var x = InitialСondition["x"][div + 1].RealValue;

                    currValues = new Dictionary<string, FloatingPoint>();
                    foreach (var val in InitialСondition)
                    {
                        currValues.Add(val.Key, val.Value[div]);
                    }
                }
            }
            
        }


        private void PrintInfo()
        {
            int i = 1;
            foreach(var eq in Equation.Functions)
            {
                Console.WriteLine($"y{i}' = {eq.ToString()}");
                i++;
            }

            Console.WriteLine($"Начальное условие: ");
                
            foreach(var cond in InitialСondition)
            {
                if(cond.Key == "x")
                    Console.WriteLine($"\t{cond.Key} = {cond.Value[0].RealValue}");
                else
                    Console.WriteLine($"\t{cond.Key}({InitialСondition["x"][0].RealValue}) = {cond.Value[0].RealValue}");
            }

            Console.WriteLine($"Рассматриваемый интервал: [{Section[0]}; {Section[1]}]");

            Console.WriteLine($"Количество делений: {Divisions}");

            Console.WriteLine($"Величина шага: {Step}");

            Console.WriteLine("Решение методом Рунге-Кутты:\n");
        }
    }
}
