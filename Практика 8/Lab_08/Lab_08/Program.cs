using System;
using System.Collections.Generic;
using Expr = MathNet.Symbolics.SymbolicExpression;


namespace lab8
{
    class Program
    {

        private static double Function1(double x)
        {
            return (x * x * x) / 3;
        }
        private static double Function2(double x)
        {
            return Function1(x) + ((x * x * x * x * x * x * x) / 63);
        }
        private static double Function3(double x)
        {
            return Function2(x) + (2 * Math.Pow(x, 11) / 2079) + (Math.Pow(x, 15) / 59535);
        }
        //первое уравнение системы
        private static float f1(float xa, float ya, float yb)
        {
            return 2 * xa * xa + 2 * ya + yb;
        }
        // Второе уравнение системы
        private static float f2(float xa, float ya, float yb)
        {
            return 1 - 2 * xa * xa + 2 * ya - yb;
        }

        private static void AdamsMethod()
        {
            //промежуток и шаг
            const float a = 0;
            const float b = 1;
            const double h = 0.1;

            //Считывание функции и её компиляция средствами библиотеки MathNet.Symbolics
            Console.WriteLine("Equation should be like: y'=f(x,y)");
            Console.WriteLine("Enter f(x,y)");
            string equation = Console.ReadLine();
            Func<double, double, double> function = Expr.Parse(equation).Compile("x", "y");

            //Ввод начальных условий
            Console.WriteLine("Enter start conditions");
            double x0, y0;
            Console.Write("x0=");
            String temp = Console.ReadLine();
            x0 = float.Parse(temp);
            Console.Write("y0=");
            temp = Console.ReadLine();
            y0 = float.Parse(temp);

            //определение колличества шагов алгоритма
            int numberOfSteps = (int)((b - a) / h);

            //массив искомых значений
            double[] values = new double[numberOfSteps];

            //алгоритм Рунге–Кутты 4–го порядка
            for (int i = 0; i < 4; i++)
            {
                double prevX = (i * h) + x0;
                double prevY;
                if (i == 0)
                    prevY = y0;
                else
                    prevY = values[i - 1];

                double k1 = function(prevX, prevY);
                double k2 = function(prevX + h / 2, prevY + h * k1 / 2);
                double k3 = function(prevX + h / 2, prevY + h * k2 / 2);
                double k4 = function(prevX + h, prevY + h * k3);

                values[i] = prevY + (h / 6 * (k1 + 2 * k2 + 2 * k3 + k4));
            }

            for (int i = 4; i < numberOfSteps; i++)
            {
                values[i] = values[i - 1] + (h / 24.0) *
                            (55 * function(((i - 1) * h) + x0, values[i - 1]) -
                             59 * function(((i - 2) * h) + x0, values[i - 2]) +
                             37 * function(((i - 3) * h) + x0, values[i - 3])
                             - 9 * function(((i - 4) * h) + x0, values[i - 4]));
            }

            //Вывод ответа
            Console.WriteLine($"x={x0}  y={y0}");
            for (int i = 0; i < numberOfSteps; i++)
            {
                Console.WriteLine($"x={(i + 1) * h + x0}  y={values[i]}");
            }
        }

        static void Main()
        {
            //метод последовательных приближений
            Console.WriteLine("y'=x^2+y^2");
            Console.WriteLine("y(0)=0");
            Console.WriteLine("-1<=x<=1");
            Console.WriteLine("-1<=y<=1");
            for (double j = -1; j <= 1; j += 0.1)
            {
                Console.WriteLine($"y={j} x={Function3(j)}");
            }
            Console.WriteLine();

            #region MilnMethod
            Console.WriteLine("Miln method");
            float h;   // Шаг
            float a, b, k1, k2, k3, k4;
            float r1, r2, r3, r4;
            float eps, abs_pogr;   // eps - точность
            float[] zkor = new float[12];
            float[] zpr = new float[12];
            float[] ypr = new float[12];
            float[] ykor = new float[12];
            float[] x = new float[12];
            float[] y1 = new float[12];
            float[] y2 = new float[12];

            // Ввод концов отрезка
            a = 0;
            b = 1;
            x[0] = a;
            // Начальные условия:
            y1[0] = 0;
            y2[0] = 0;
            // Шаг
            h = 0.1f;
            // Точность
            eps = 0.0001f;
            // Решение системы уравнений методом Рунге-Кутта
            int i = 0;
            while (i <= 3)
            {
                k1 = h * f1(x[i], y1[i], y2[i]);
                r1 = h * f2(x[i], y1[i], y2[i]);
                k2 = h * f1(x[i] + h / 2, y1[i] + k1 / 2, y2[i] + r1 / 2);
                r2 = h * f2(x[i] + h / 2, y1[i] + k1 / 2, y2[i] + r1 / 2);
                k3 = h * f1(x[i] + h / 2, y1[i] + k2 / 2, y2[i] + r2 / 2);
                r3 = h * f2(x[i] + h / 2, y1[i] + k2 / 2, y2[i] + r2 / 2);
                k4 = h * f1(x[i] + h, y1[i] + k3, y2[i] + r3);
                r4 = h * f2(x[i] + h, y1[i] + k3, y2[i] + r3);

                y1[i + 1] = y1[i] + (k1 + 2 * k2 + 2 * k3 + k4) / 6;
                y2[i + 1] = y2[i] + (r1 + 2 * r2 + 2 * r3 + r4) / 6;

                x[i + 1] = x[i] + h;
                i = i + 1;
            }
            i = 4;
            // Решение системы уравнений методом Милна
            while (x[i] <= b + h)
            {
                // Для предсказания используется первая формула Милна
                ypr[i] = y1[i - 4] + (4 * h) / 3 * (2 * f1(x[i - 3], y1[i - 3], y2[i - 3]) - f1(x[i - 2], y1[i - 2], y2[i - 2]) + 2 * f1(x[i - 1], y1[i - 1], y2[i - 1]));
                // Для уточнения - вторая формула Милна
                ykor[i] = y1[i - 2] + (h / 3) * (f1(x[i - 2], y1[i - 2], y2[i - 2]) + 4 * f1(x[i - 1], y1[i - 1], y2[i - 1]) + f1(x[i], ypr[i], y2[i]));
                // Для второго уравнения
                zpr[i] = y2[i - 4] + (4 * h) / 3 * (2 * f2(x[i - 3], y1[i - 3], y2[i - 3]) - f2(x[i - 2], y1[i - 2], y2[i - 2]) + 2 * f2(x[i - 1], y1[i - 1], y2[i - 1]));
                zkor[i] = y2[i - 2] + (h / 3) * (f2(x[i - 2], y1[i - 2], y2[i - 2]) + 4 * f2(x[i - 1], y1[i - 1], y2[i - 1]) + f2(x[i], zpr[i], y2[i]));

                abs_pogr = Math.Abs(ykor[i] - ypr[i]) / 29;
                if (abs_pogr > eps) y1[i] = ykor[i];
                else y1[i] = ypr[i];
                // Абсолютная погрешность
                abs_pogr = Math.Abs(zkor[i] - zpr[i]) / 29;
                // Контроль точности полученного результата
                if (abs_pogr > eps) y2[i] = zkor[i];
                else y2[i] = zpr[i];

                x[i + 1] = x[i] + h;
                i = i + 1;
            }

            for (i = 0; i < 11; i++)
            {
                Console.WriteLine($"x={x[i]}    y1={y1[i]}  y2={y2[i]}");
            }
            #endregion

            #region MyRegion

            AdamsMethod();

            #endregion

            Console.ReadLine();
        }

    }
}

