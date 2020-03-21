using System;

namespace Lab_05
{
    static class Matrix
    {
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

        static public double[,] MatrixAddition(double[,] matrix1, double[,] matrix2)
        {
            double[,] matrix;
            int rows_matrix1 = matrix1.GetUpperBound(0) + 1;
            int columns_matrix1 = matrix1.Length / rows_matrix1;
            int rows_matrix2 = matrix2.GetUpperBound(0) + 1;
            int columns_matrix2 = matrix2.Length / rows_matrix2;

            matrix = new double[rows_matrix1, columns_matrix1];

            if (isMatricesValidForAddition(rows_matrix1, columns_matrix1, rows_matrix2, columns_matrix2))
            {
                for (int i = 0; i < rows_matrix1; i++)
                    for (int j = 0; j < columns_matrix1; j++)
                    {
                        matrix[i, j] = matrix1[i, j] + matrix2[i, j];
                    }
            }
            else Console.WriteLine("Невозможно выполнить сложение матрицы, т.к. количество строк и столбцов первой матрицы не соответсвует количеству строк и столбцов второй матрицы");

            return matrix;
        }

        static public double[,] MatrixSubtraction(double[,] matrix1, double[,] matrix2)
        {
            double[,] matrix;
            int rows_matrix1 = matrix1.GetUpperBound(0) + 1;
            int columns_matrix1 = matrix1.Length / rows_matrix1;
            int rows_matrix2 = matrix2.GetUpperBound(0) + 1;
            int columns_matrix2 = matrix2.Length / rows_matrix2;

            matrix = new double[rows_matrix1, columns_matrix1];

            if (isMatricesValidForAddition(rows_matrix1, columns_matrix1, rows_matrix2, columns_matrix2))
            {
                for (int i = 0; i < rows_matrix1; i++)
                    for (int j = 0; j < columns_matrix1; j++)
                    {
                        matrix[i, j] = matrix1[i, j] - matrix2[i, j];
                    }
            }
            else Console.WriteLine("Невозможно выполнить сложение матрицы, т.к. количество строк и столбцов первой матрицы не соответсвует количеству строк и столбцов второй матрицы");

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

        static public double[,] MatrixMultiply(double[,] matrix1, double[,] matrix2)
        {
            double[,] matrix;
            int rows_matrix1 = matrix1.GetUpperBound(0) + 1;
            int columns_matrix1 = matrix1.Length / rows_matrix1;
            int rows_matrix2 = matrix2.GetUpperBound(0) + 1;
            int columns_matrix2 = matrix2.Length / rows_matrix2;

            matrix = new double[rows_matrix1, columns_matrix2];

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
            else Console.WriteLine("Умножение матриц не возможно! Количество столбцов первой матрицы не равно количеству строк второй матрицы.");

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

        static public double[,] CalculateInverseMatrix(double[,] matrix)
        {
            int k = 1;
            double[,] algebraic_complement_matrix;

            int rows = matrix.GetUpperBound(0) + 1;
            int columns = matrix.GetUpperBound(1) + 1;

            algebraic_complement_matrix = new double[rows, rows];

            double[,] minor;

            if (isSquare(rows, columns))
            {
                double determinant = CalculateDeterminant(matrix, rows);

                if (determinant != 0)
                {
                    minor = new double[rows - 1, rows - 1];
                    matrix = FindTransparentMatrix(matrix);            

                    for (int i = 0; i < rows; i++)
                        for (int j = 0; j < rows; j++)
                        {
                            algebraic_complement_matrix[i, j] = Math.Pow(-1, (i + j)) * CalculateDeterminant(GetMatr(matrix, minor, i, j, rows), rows - 1);
                        }

                    for (int i = 0; i < rows; i++)
                        for (int j = 0; j < rows; j++)
                        {
                            algebraic_complement_matrix[i, j] /= (double)determinant;
                        }

                }
                else Console.WriteLine("Матрица вырожденная и не имеет обратной матрицы");
            }
            else Console.WriteLine("Матрица не квадратная! Только квадратная матрица имеет обратную.");


            return algebraic_complement_matrix;
        }


        //Проверка: является ли данная матрица квадратной?
        static private bool isSquare(int rows, int columns)
        {
            if (rows == columns)
                return true;
            else return false;
        }

        //Вычисление определителя
        static public double CalculateDeterminant(double[,] matrix, int rows)
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
        static public double[,] GetMatr(double[,] matrix, double[,] p, int i, int j, int m)
        {
            int ki, kj, di, dj;
            di = 0;

            for (ki = 0; ki < m - 1; ki++)
            { // проверка индекса строки
                if (ki == i) 
                    di = 1;
                dj = 0;

                for (kj = 0; kj < m - 1; kj++)
                { // проверка индекса столбца
                    if (kj == j) dj = 1;
                    p[ki, kj] = matrix[ki + di, kj + dj];
                }
            }

            return p;
        }

        //Получение транспонированной матрицы
        static public double[,] FindTransparentMatrix(double[,] matrix)
        {
            int rows = matrix.GetUpperBound(0) + 1;
            int columns = matrix.Length / rows;
            double temp;

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < columns; j++)
                {
                    if (i > j)
                    {
                        temp = matrix[i, j];
                        matrix[i, j] = matrix[j, i];
                        matrix[j, i] = temp;
                    }
                }

            return matrix;
        }
    }
}
