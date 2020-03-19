using System;

namespace Lab04
{
    class EquationsSystem
    {
        private double[] vector;
        public int Rows { get; set; }
        public int Columns { get; set; }
        public double[,] Matrix { get; set; }

        public EquationsSystem(double[,] matrix, double[] vector)
        {
            Rows = matrix.GetLength(0);
            Columns = matrix.GetLength(1) + 1;

            Matrix = new double[Rows, Columns];

            for(int i = 0; i < Matrix.GetLength(0); i++)
            {
                for(int j = 0; j < Columns - 1; j++)
                {
                    Matrix[i, j] = matrix[i, j];
                }
                Matrix[i, Columns - 1] = vector[i];
            }

        }

        public void PrintSystem()
        {
            for(int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    if (j != Columns - 1)
                        Console.Write($"{Matrix[i, j]:0.000}\t");
                    else Console.Write($"|\t{Matrix[i, j]:0.000}");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        private void PrintVector()
        {
            Console.WriteLine("Ответ: ");
            for (int i = 0; i < vector.Length; i++)
            {
                Console.WriteLine($"x{i+1} = {vector[i]:0.00}");
            }
            Console.WriteLine();
        }

        private bool SelectLeading(int n)
        {
            //Найдем номер строки, с наибольшим
            //элементом в столбце n
            int iMax = n;
            for (int i = n + 1; i < Rows; i++)
                if (Math.Abs(Matrix[iMax, n]) < Math.Abs(Matrix[i, n]))
                    iMax = i;
            // Переставить строки iMax и n
            if (iMax != n)
            {
                for (int i = n; i < Columns; i++)
                {
                    double t = Matrix[n, i];
                    Matrix[n, i] = Matrix[iMax, i];
                    Matrix[iMax, i] = t;
                }
                return true;
            }
            return false;
        }

        private void SubtractRow(int k)
        {
            double m = Matrix[k, k];
            for(int i = k + 1; i < Rows; i++)
            {
                double t = Matrix[i, k] / m;
                for(int j = k; j < Columns; j++)
                {
                    Matrix[i, j] = Matrix[i, j] - Matrix[k, j] * t;
                }
            }
        }

        private bool ToTriangleMatrix()
        {
            for (int i = 1; i < Rows; i++)
            {
                SelectLeading(i - 1);
                if (Math.Abs(Matrix[i - 1, i - 1]) != 0)
                {
                    SubtractRow(i - 1);
                }
                else return false;
            }

            return true;    
        }

        private int Rank(bool isExtendedMatrix)
        {
            int rang = 0;
            int q = 1;

            int rows, columns;

            rows = Rows;

            if (isExtendedMatrix)
                columns = Columns;
            else columns = Columns - 1;

            while (q <= MinValue(rows, columns))
            {
                double[,] matbv = new double[q, q];
                for (int i = 0; i < (rows - (q - 1)); i++)
                {
                    for (int j = 0; j < (columns - (q - 1)); j++)
                    {
                        for (int k = 0; k < q; k++)
                        {
                            for (int c = 0; c < q; c++)
                            {
                                matbv[k, c] = Matrix[i + k, j + c];
                            }
                        }

                        if (CalculateDeterminant(matbv, matbv.GetLength(0)) != 0)
                        {
                            rang = q;
                        }
                    }
                }
                q++;
            }

            return rang;
        }

        private int MinValue(int a, int b)
        {
            if (a >= b)
                return b;
            else
                return a;
        }

        private double CalculateDeterminant(double[,] matrix, int rows)
        {
            double det = 0;
            int k = 1;
            double[,] new_matrix = new double[rows, rows];

            if (rows == 1)
                return matrix[0, 0];
            else if (rows == 2)
            {
                det = matrix[0, 0] * matrix[1, 1] - matrix[1, 0] * matrix[0, 1];

                return det;
            }
            else
            {
                for (int i = 0; i < rows; i++)
                {
                    det += k * matrix[i, 0] * CalculateDeterminant(GetMatr(matrix, new_matrix, i, 0, rows), rows - 1);
                    k = -k;
                }

                return det;
            }
        }

        //Получение матрицы путем вычеркивание i-ой строки и j-ого столбца
        private double[,] GetMatr(double[,] matrix, double[,] p, int i, int j, int m)
        {
            int ki, kj, di, dj;
            di = 0;

            for (ki = 0; ki < m - 1; ki++)
            { // проверка индекса строки
                if (ki == i) di = 1;
                dj = 0;
                for (kj = 0; kj < m - 1; kj++)
                { // проверка индекса столбца
                    if (kj == j) dj = 1;
                    p[ki, kj] = matrix[ki + di, kj + dj];
                }
            }

            return p;
        }

        private int[] FindBaseVariables(int r)
        {
            int rows, columns;
            int[] baseVar = new int[r];

            rows = Rows;
            columns = Columns - 1;

            double[,] matbv = new double[r, r];

            for (int i = 0; i < (rows - (r - 1)); i++)
            {
                for (int j = 0; j < (columns - (r - 1)); j++)
                {
                    if (CalculateDeterminant(matbv, matbv.GetLength(0)) == 0)
                    {
                        for (int k = 0; k < r; k++)
                        {
                            for (int c = 0; c < r; c++)
                            {
                                matbv[k, c] = Matrix[i + k, j + c];
                                baseVar[c] = j+c;
                            }
                        }

                    }
                    else break;
                }
            }

            return baseVar;
        }

        public void Solve()
        {
            if (Rank(true) == Rank(false))
            {
                int rank = Rank(true);

                vector = new double[Columns - 1];

                ToTriangleMatrix();

                PrintSystem();

                Console.Write("Система совместна и ");
                if (rank == Columns - 1)
                {
                    Console.WriteLine("имеет 1 единственное решение");

                    int nb = Columns - 1;

                    for (int n = Rows - 1; n >= 0; n--)
                    {
                        double sum = 0;
                        for (int i = n + 1; i < nb; i++)
                            sum += vector[i] * Matrix[n, i];
                        vector[n] = (Matrix[n, nb] - sum) / Matrix[n, n];
                    }
                }
                else
                {
                    Console.WriteLine("имеет бесконечное множество решений");

                    int[] baseVar = FindBaseVariables(rank);
                    bool[] free = new bool[vector.Length];


                    Console.Write("Базисные переменные: ");

                    for (int i = 0; i < baseVar.Length; i++)
                            Console.Write($"x{baseVar[i] + 1} ");
                    Console.WriteLine();

                    Console.Write("Свободные переменные: ");
                    
                    for(int i = 0; i < vector.Length; i++)
                    {
                        int count = 0;

                        for(int j = 0; j < baseVar.Length; j++)
                        {
                            if(i != baseVar[j])
                            {
                                count++;
                            }
                        }

                        if (count == baseVar.Length)
                            free[i] = true;
                    }

                    for (int i = 0; i < vector.Length; i++)
                    {
                        if (free[i])
                            Console.Write($"x{i + 1} ");
                    }

                    Console.WriteLine();


                    Console.WriteLine("Введите значения свободных переменных для получения частного решения: ");
                    for (int i = 0; i < vector.Length; i++)
                    {
                        if (free[i])
                        { 
                            Console.Write($"x{i + 1} = ");
                            vector[i] = double.Parse(Console.ReadLine());
                        }                           
                    }

                    int new_rows = Rows;

                    for (int i = 0; i < Rows; i++)
                    {
                        int count = 0;

                        for (int j = 0; j < Columns - 1; j++)
                        {
                            if (Matrix[i, j] == 0) count++;
                        }

                        if (count == Columns - 1) new_rows--;
                    }

                    double[] sum = new double[new_rows];

                    for (int n = new_rows - 1; n >= 0; n--)
                    {
                        for (int i = n + 1; i < Columns - 1; i++)
                            sum[n] += vector[i] * Matrix[n, i];
                    }

                    for (int n = new_rows - 1; n >= 0; n--)
                    {
                        if (!free[n])
                        {
                            if (Matrix[n, n] != 0)
                                vector[n] = (Matrix[n, Columns - 1] - sum[n]) / Matrix[n, n];
                            else
                            {
                                for (int i = n - 1; i >= 0; i--)
                                {
                                    sum[i] = 0;
                                    for (int j = 0; j < Columns - 1; j++)
                                        sum[i] += vector[j] * Matrix[i, j];
                                    vector[n] = (Matrix[i, Columns - 1] - sum[i]) / Matrix[i, n];
                                    if (!Double.IsNaN(vector[n]))
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }        

                }


               PrintVector();
            }

            else
            {
                Console.WriteLine($"Ранг матрицы системы не равен рангу расширенной матрицы: {Rank(true)} != {Rank(false)}.\nСистема не имеет решений.\n");
            }      
        }

    }
}
