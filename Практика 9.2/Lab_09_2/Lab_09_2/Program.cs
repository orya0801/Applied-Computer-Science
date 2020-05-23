using System;

namespace Lab_09_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Условие задачи (система): ");
            Console.WriteLine("18*x1 + 15*x2 + 12*x3 <= 360");
            Console.WriteLine("6*x1 + 4*x2 + 8*x3 <= 192");
            Console.WriteLine("5*x1 + 3*x2 + 3*x3 <= 180");
            Console.WriteLine("x1 >= 0, x2 >= 0, x3 >= 0");
            Console.WriteLine("F = 9*x1 + 10*x2 + 16*x3  -->  max\n");

            double[,] table = { {360, 18, 15, 12},
                                {192, 6,  4, 8},
                                {180,  5,  3, 3},
                                { 0, -9, -10, -16} };

            double[] result = new double[3];
            double[,] table_result;
            SimplexMethod S = new SimplexMethod(table);
            table_result = S.Calculate(result);

            Console.WriteLine("Решенная симплекс-таблица:");
            for (int i = 0; i < table_result.GetLength(0); i++)
            {
                for (int j = 0; j < table_result.GetLength(1); j++)
                    Console.Write($"{table_result[i, j]:0.00}\t");
                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine("Решение:");
            for (int i = 0; i < result.Length; i++)
            {
                Console.WriteLine($"X[{i + 1}] = {result[i]}");
            }
            Console.ReadLine();
        }
    }
}
