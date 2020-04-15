using System;

namespace lab01
{
    class Matrix
    {
        //Функция, генерирующая матрицу целых чисел в интервале от (-10; 10)
        static public int[,] GenerateMatrix(int rows, int columns)
        {
            Random rnd = new Random();
            int value;
            int[,] matrix = new int[rows,columns];
            for(int i = 0; i < rows; i++)
                for(int j = 0; j < columns; j++)
                {
                    value = rnd.Next(-10, 10);
                    matrix[i, j] = value;
                }
            PrintMatrix(matrix);
            return matrix;
        }
        static public int[,] CreateMatrix(int rows, int columns)
        {
            int value;
            int[,] matrix = new int[rows, columns];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Console.Write($"a[{i + 1}, {j + 1}] = ");
                    value = int.Parse(Console.ReadLine());
                    matrix[i, j] = value;
                }
            }
            PrintMatrix(matrix);
            return matrix;
        }

        //Функция вывода матрицы на экран
        static public void PrintMatrix(int[,] matrix)
        {
            int rows = matrix.GetUpperBound(0) + 1;
            int columns = matrix.Length / rows;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        //Функция вывода матрицы дробных чисел на экран
        static public void PrintMatrix(double[,] matrix)
        {
            int rows = matrix.GetUpperBound(0) + 1;
            int columns = matrix.Length / rows;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Console.Write(Math.Round(matrix[i, j], 4) + "\t");
                }
                Console.WriteLine();
            }
        }

        static public int[,] MatrixAddition(int[,] matrix1, int[,] matrix2)
        {
            int[,] matrix;
            int rows_matrix1 = matrix1.GetUpperBound(0) + 1;
            int columns_matrix1 = matrix1.Length / rows_matrix1;
            int rows_matrix2 = matrix2.GetUpperBound(0) + 1;
            int columns_matrix2 = matrix2.Length / rows_matrix2;
            matrix = new int[rows_matrix1, columns_matrix1];
            if (isMatricesValidForAddition(rows_matrix1, columns_matrix1, rows_matrix2, columns_matrix2))
            {
                for (int i = 0; i < rows_matrix1; i++)
                    for (int j = 0; j < columns_matrix1; j++)
                    {
                        matrix[i, j] = matrix1[i, j] + matrix2[i, j];
                    }
            }
            else Console.WriteLine("Невозможно выполнить сложение, т.к. количество строк и столбцов первой матрицы не соответсвует количеству строк и столбцов второй матрицы");
            Console.WriteLine("Результат сложения: ");
            PrintMatrix(matrix);
            return matrix;
        }

        //Функция, проверяющая совпадают ли размерности складываемых матриц
        static private bool isMatricesValidForAddition(int rows1, int columns1, int rows2, int columns2)
        {
            if (rows1 == rows2 && columns1 == columns2)
                return true;
            else
                return false;
        }

        static public int[,] MatrixMultiply(int[,] matrix1,  int[,] matrix2)
        {
            int[,] matrix;
            int rows_matrix1 = matrix1.GetUpperBound(0) + 1;
            int columns_matrix1 = matrix1.Length / rows_matrix1;
            int rows_matrix2 = matrix2.GetUpperBound(0) + 1;
            int columns_matrix2 = matrix2.Length / rows_matrix2;
            matrix = new int[rows_matrix1, columns_matrix2];
            if (isMatricesValidForMultiply(columns_matrix1, rows_matrix2))
            {
                for (var i = 0; i < rows_matrix1; i++)
                {
                    for (var j = 0; j < columns_matrix2; j++)
                    {
                        matrix[i, j] = 0;

                        for (var k = 0; k < columns_matrix1; k++)
                        {
                            matrix[i, j] += matrix1[i, k] * matrix2[k, j];
                        }
                    }
                }
            }
            else Console.WriteLine("Умножение не возможно! Количество столбцов первой матрицы не равно количеству строк второй матрицы.");

            return matrix;
        }

        //Проверка совпдает ли количество столбцов первой матрицы с количеством строк второй матрицы:
        static private bool isMatricesValidForMultiply(int columns1, int rows2)
        {
            if (rows2 == columns1)
                return true;
            else
                return false;
        }

        static public void CalculateInverseMatrix(int[,] matrix)
        {
            int rows = matrix.GetUpperBound(0) + 1;
            int columns = matrix.GetUpperBound(1) + 1;

            if (!isSquare(rows, columns))
            {
                throw new NonSquareMatrixException("Матрица не квадртная!", rows, columns);
            }

            int k = 1;
            int[,] minor;
            double[,] algebraic_complement_matrix;
            
            int determinant = CalculateDeterminant(matrix, rows);

            Console.WriteLine("Определитель = " + determinant);

            if (determinant == 0)
            {
                throw new DeterminantNullException("Определитель матрицы равен нулю. Матрица вырожденная и не имеет определителя!");
            }

            minor = new int[rows - 1, rows - 1];
            matrix = FindTransparentMatrix(matrix);
            algebraic_complement_matrix = new double[rows, rows];

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < rows; j++)
                {
                    algebraic_complement_matrix[i, j] = k * CalculateDeterminant(GetMatr(matrix, minor, i, j, rows), rows - 1);
                    k = -k;
                }

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < rows; j++)
                {
                    algebraic_complement_matrix[i, j] /= (double)determinant;
                }

            Console.WriteLine("Обратная матрица: ");
            PrintMatrix(algebraic_complement_matrix);
        }


        //Проверка: является ли данная матрица квадратной?
        static private bool isSquare(int rows, int columns)
        {
            if (rows == columns)
                return true;
            else return false;
        }

        //Вычисление определителя
        static public int CalculateDeterminant(int[,] matrix, int rows)
        {
            int det = 0;
            int k = 1;
            int[,] new_matrix = new int[rows, rows];

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
                    det += k * matrix[i,0] * CalculateDeterminant(GetMatr(matrix, new_matrix, i, 0, rows), rows - 1);
                    k = -k;
                }

                return det;
            }
        }

        //Получение матрицы путем вычеркивание i-ой строки и j-ого столбца
        static public int[,] GetMatr(int[,] matrix, int[,] p, int i, int j, int m)
        {
            int ki, kj, di, dj;
            di = 0;

            for (ki = 0; ki < m - 1; ki++) { // проверка индекса строки
                if (ki == i) di = 1;
                dj = 0;
                for (kj = 0; kj < m - 1; kj++) { // проверка индекса столбца
                    if (kj == j) dj = 1;
                    p[ki,kj] = matrix[ki + di, kj + dj];
                }
            }

            return p;
        }

        //Получение транспонированной матрицы
        static public int[,] FindTransparentMatrix(int[,] matrix)
        {
            int rows = matrix.GetUpperBound(0) + 1;
            int columns = matrix.Length / rows;
            int temp;

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < columns; j++)
                {
                    if(i > j)
                    {
                        temp = matrix[i, j];
                        matrix[i, j] = matrix[j, i];
                        matrix[j, i] = temp;
                    }    
                }

            Console.WriteLine("Транспонированная матрица: ");
            PrintMatrix(matrix);

            return matrix;
        }


        static public int[] SumInRow(int[,] array)
        {
            int[] sum = new int[array.GetLength(0)];

            for(int i = 0; i < sum.Length; i++)
            {
                for(int j = 0; j < array.GetLength(1); j++)
                {
                    sum[i] += array[i, j];
                }
            }
            Console.WriteLine("R = ");

            for (int i = 0; i < sum.Length; i++)
            {
                Console.WriteLine($"{i}\t{sum[i]}");
            }

            return sum;
        }

        static public double[] GraphRang(int[] array)
        {
            double[] graphRang = new double[array.Length];
            int sum = 0;

            for(int i = 0; i < array.Length; i++)
            {
                sum += array[i];
            }

            Console.WriteLine($"sum = {sum}");

            for(int i = 0; i < graphRang.Length; i++)
            {
                graphRang[i] = (double)array[i] / sum;
            }

            for (int i = 0; i < graphRang.Length; i++)
            {
                Console.WriteLine($"{graphRang[i]}");
            }

            return graphRang;
        }
    }
}
