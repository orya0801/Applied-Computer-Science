﻿using System;
using MathNet.Symbolics;
using Expr = MathNet.Symbolics.SymbolicExpression;

namespace Lab_08
{
    class Program
    {
        static void Main(string[] args)
        {
            //объявление переменных
            var x = Expr.Variable("x");
            var y1 = Expr.Variable("y1");
            var y2 = Expr.Variable("y2");

            #region Zadanie 1
            //Метод последовательных приближений для решения системы

            //Объявление системы
            Expr expr_dy1 = (x + y1 * y2);
            Expr expr_dy2 = (x * x - y1 * y1);
            Func<double, double, double, double> dy1 = (expr_dy1).Compile("x", "y1", "y2");
            Func<double, double, double, double> dy2 = (expr_dy2).Compile("x", "y1", "y2");

            //Первое приближение
            Expr expr_y1_1 = (1 + x * x / 2);
            Expr expr_y2_1 = (-x + x * x * x / 3);
            Func<double, double> y1_1 = expr_y1_1.Compile("x");
            Func<double, double> y2_1 = expr_y2_1.Compile("x");
            //Второе приближение
            Expr expr_y1_2 = (1 - (x.Pow(4)) / 24 + (x.Pow(6)) / 36);
            Expr expr_y2_2 = (-x - x.Pow(5) / 20);
            Func<double, double> y1_2 = expr_y1_2.Compile("x");
            Func<double, double> y2_2 = expr_y2_2.Compile("x");

            //диапозон значений x
            double[] section_x = new double[2] { 0, 1 };
            //Количество делений
            int divisions = 10;
            //Определение величны шага
            double h = (section_x[1] - section_x[0]) / divisions;

            double[] x_val = new double[divisions + 1];

            x_val[0] = section_x[0];
            for (int i = 1; i <= divisions; i++)
            {
                x_val[i] = x_val[i - 1] + h;
            }

            //массивы для хранения значений y1 и y2
            double[] y1_val = new double[divisions + 1];
            double[] y2_val = new double[divisions + 1];

            y1_val[0] = 1;
            y2_val[0] = 0;

            //Печать начальных условий
            Console.WriteLine($"y1' = {expr_dy1.ToString()}");
            Console.WriteLine($"y2' = {expr_dy2.ToString()}");
            Console.WriteLine("Начальные условия: ");
            Console.WriteLine("y1(0) = 1; y2(0) = 0;");

            //Печать приближений
            Console.WriteLine("Исходя из формулы y_n = y0 + integrate (f(x, y_(n-1)))dx from 0 to x , получаем:\n");
            Console.WriteLine($"y1_1 = 1 + integrate (x + 0)dx from 0 to x = {expr_y1_1.ToString()}");
            Console.WriteLine($"y2_1 = 1 + integrate (x^2 - 1)dx from 0 to x = {expr_y2_1.ToString()}\n");
            Console.WriteLine($"y1_2 = 1 + integrate (x + (1 + x^2/2)*(-x + x^3/3))dx from 0 to x = {expr_y1_2.ToString()}");
            Console.WriteLine($"y2_2 = 1 + integrate (x^2 + (1 + x^2 + x^4/4))dx from 0 to x = {expr_y2_2.ToString()}\n");

            Console.WriteLine("Геометрически последовательные приближения представляют " +
                "собой кривые yn = yn(x)(n = 1, 2, …),проходящие через общую точку M0(x0, y0)." +
                "\nВ таблице приведены кривые для 1-ого и 2-ого уравнения системы: \n");

            //Результат
            Console.WriteLine($"x\ty1_1\ty2_1\ty1_2\ty2_2");

            for (int i = 0; i <= divisions; i++)
            {
                Console.Write($"{x_val[i]:0.00}\t");
                Console.Write($"{y1_1(x_val[i]):0.0000}\t");
                Console.Write($"{y2_1(x_val[i]):0.0000}\t");
                Console.Write($"{y1_2(x_val[i]):0.0000}\t");
                Console.WriteLine($"{y2_2(x_val[i]):0.0000}\t");
            }

            #endregion

            #region Zadanie 2

            Expr expr_f1 = 2 * x * x + 2 * y1 + y2;
            Func<double, double, double, double> f1 = (expr_f1).Compile("x", "y1", "y2");
            Expr expr_f2 = 1 - 2 * x * x + 2 * y1 - y2;
            Func<double, double, double, double> f2 = (expr_f2).Compile("x", "y1", "y2");

            Miln(f1, f2, 10, 0, 1, 0, 0);

            #endregion
        }

        private static void Miln(Func<double, double, double, double> f1, Func<double, double, double, double> f2,
            int divisions, double start_point, double end_point, double y1_start, double y2_start)
        {
            Console.WriteLine("Метод Милна:");
            double k1, l1, k2, l2, k3, l3, k4, l4;

            double h = (end_point - start_point) / divisions;

            double curr_x = start_point;

            double[] x_val = new double[divisions + 1];
            double[] y1_val = new double[divisions + 1];
            double[] y2_val = new double[divisions + 1];
            double[] dy1 = new double[divisions + 1];
            double[] dy2 = new double[divisions + 1];
            x_val[0] = start_point;
            dy1[0] = y1_val[0] = y1_start;
            dy2[0] = y2_val[0] = y2_start;

            // Решение системы уравнений методом Рунге-Кутта
            for(int i = 0; i <= 3; i++)
            {
                k1 = h * f1(x_val[0], dy1[0], dy2[0]);
                l1 = h * f2(x_val[0], dy1[0], dy2[0]);
                k2 = h * f1(x_val[0] + h / 2, dy1[0] + k1 / 2, dy2[i] + l1 / 2);
                l2 = h * f2(x_val[0] + h / 2, dy1[0] + k1 / 2, dy2[i] + l1 / 2);
                k3 = h * f1(x_val[0] + h / 2, dy1[i] + k2 / 2, dy2[i] + l2 / 2);
                l3 = h * f2(x_val[0] + h / 2, dy1[i] + k2 / 2, dy2[i] + l2 / 2);
                k4 = h * f1(x_val[0] + h, dy1[i] + k3, dy2[i] + l3);
                l4 = h * f2(x_val[0] + h, dy1[i] + k3, dy2[i] + l3);

                dy1[i + 1] = y1_val[i + 1] = dy1[i] + (k1 + 2 * k2 + 2 * k3 + k4) / 6;
                dy2[i + 1] = y2_val[i + 1] = dy2[i] + (l1 + 2 * l2 + 2 * l3 + l4) / 6;

                x_val[i + 1] = x_val[i] + h;
            }

            double curr_y1, curr_y2;
            double dy1_1, dy2_1, dy1_2, dy2_2;

            

            for(int i = 3; i < divisions; i++)
            {
                x_val[i + 1] = x_val[i] + h;

                curr_y1 = y1_val[i - 3] + (4 * h) / 3 * (2 * dy1[i] - dy1[i - 1] + 2 * dy1[i - 2]);
                curr_y2 = y2_val[i - 3] + (4 * h) / 3 * (2 * dy2[i] - dy2[i - 1] + 2 * dy2[i - 2]);      

                dy1_1 = f1(x_val[i + 1], curr_y1, curr_y2);
                dy2_1 = f2(x_val[i + 1], curr_y1, curr_y2);

                int count = 0;

                while (true)
                {
                    curr_y1 = y1_val[i - 1] + (h / 3) * (dy1_1 + 4 * dy1[i] + dy1[i - 1]);
                    curr_y2 = y2_val[i - 1] + (h / 3) * (dy2_1 + 4 * dy2[i] + dy2[i - 1]);
                    dy1_2 = f1(x_val[i + 1], curr_y1, curr_y2);
                    dy2_2 = f2(x_val[i + 1], curr_y1, curr_y2);
                    if (Math.Abs(dy2_2 - dy2_1) < 1e-3 && Math.Abs(dy1_2 - dy1_1) < 1e-3
                        || count >= 20) break;
                    dy1_1 = dy1_2;
                    dy2_1 = dy2_2;
                    count++;
                }

                dy1[i + 1] = dy1_2;
                dy2[i + 1] = dy2_2;

                y1_val[i + 1] = y1_val[i - 1] + (h / 3) * (dy1[i + 1] + 4 * dy1[i] + dy1[i - 1]);
                y2_val[i + 1] = y2_val[i - 1] + (h / 3) * (dy2[i + 1] + 4 * dy2[i] + dy2[i - 1]);
            }

            Console.WriteLine("x\ty1\ty2");
            for (int i = 0; i < 11; i++)
            {
                Console.WriteLine($"{x_val[i]:0.00}\t{y1_val[i]:0.00}\t{y2_val[i]:0.00}");
            }
        }
    }
}
