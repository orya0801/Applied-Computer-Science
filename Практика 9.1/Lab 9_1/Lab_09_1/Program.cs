using System;
using Expr = MathNet.Symbolics.SymbolicExpression;

namespace Lab_09_1
{
    class Program
    {
        static void Main(string[] args)
        {
            var _x = Expr.Variable("x");
            var _t = Expr.Variable("t");

            #region Functions

            Func<double, double, double> sigma = (3.0 * (1.1 - 0.5 * _x)).Compile("t", "x");
            Func<double, double, double> funcs = (_t.Exp() - 1.0).Compile("t", "x");

            #endregion

            double leftBorderValue = 0;
            double rightBorderValue = 0;

            double length = 1;
            double time;
            double precision = 1.0;
            double dx;
            double dt;
            double[,] u;
            double A;

            int nX = 10;
            int nT = 30;

            double max = 0;
            double s;

            dx = length / (nX);

            for(int x = 0; x < nX; x++)
            {
                s = sigma(0, dx * x);
                max = (max > s) ? max : s;
            }

            dt = dx * dx * precision / (2 * max);
            A = dt / (dx * dx);
            time = 20.0 * dt;

            nT = (int)(time / dt + 1);

            u = new double[nT, nX];

            for(int x = 0; x < nX; x++)
            {
                u[0, x] = 0.01 * (1 - dx * x) * dx * dx;
            }

            u[0, 0] = leftBorderValue;
            u[0, nX - 1] = rightBorderValue;

            double tempS, tempF, tempT, tempX;

            for(int t = 0; t< nT-1; t++)
            {
                for(int x = 0; x < nX; x++)
                {
                    tempT = dt * t;
                    tempX = dx * x;
                    tempS = sigma(tempT, tempX);
                    tempF = funcs(tempT, tempX);

                    if(x == 0)
                    {
                        u[t + 1, x] = leftBorderValue;
                    }
                    else if ( x == nX - 1)
                    {
                        u[t + 1, x] = rightBorderValue;
                    }
                    else
                    {
                        u[t + 1, x] = A * tempS * (u[t, x - 1] + u[t, x + 1]) + (1.0 - 2.0 * A * tempS)
                            * u[t, x] + dt * tempF;
                    }
                }
            }

            Console.WriteLine($"nX = {nX}");
            Console.WriteLine($"nT = {nT}");
            Console.WriteLine($"dt = {dt}");
            Console.WriteLine($"time = {time}");
            Console.WriteLine($"A = {A}");

            int xx = (int)(0.6 * (nX - 1) / length);

            Console.WriteLine("t\tU = U(0.6, t)");
            for(int t = 0; t < nT; t++)
            {
                Console.WriteLine($"{(dt * t):0.000000}\t{u[t, xx]:0.000000}");
            }
            Console.WriteLine();

            double tt = time / 10;
            xx = (int)(tt * (nT - 1) * 1 / time);

            Console.WriteLine($"t\tU = U(x, {tt * 2})");
            for (int x = 0; x < nX; x++)
            {
                Console.WriteLine($"{(dx * x):0.000000}\t{u[xx, x]:0.000000}");
            }
            Console.WriteLine();

            Console.WriteLine($"t\tU = (x, {tt * 4})");
            for (int x = 0; x < nX; x++)
            {
                Console.WriteLine($"{(dx * x):0.000000}\t{u[xx, x]:0.000000}");
            }
        }
    }
}
