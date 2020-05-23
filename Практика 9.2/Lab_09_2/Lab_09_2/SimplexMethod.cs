using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace Lab_09_2
{
    public class SimplexMethod
    {
        double[,] A; //симплекс таблица

        int m, n;

        List<int> basis; //список базисных переменных

        public SimplexMethod(double[,] source)
        {
            m = source.GetLength(0);
            n = source.GetLength(1);
            // Задаем размеры симплекс-таблицы
            A = new double[m, n + (m - 1)];
            basis = new List<int>();

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < A.GetLength(1); j++)
                {
                    if (j < n)
                        A[i, j] = source[i, j];
                    else
                        A[i, j] = 0;
                }
                //выставляем коэффициент 1 перед базисной переменной в строке
                if ((n + i) < A.GetLength(1))
                {
                    A[i, n + i] = 1;
                    basis.Add(n + i);
                }
            }

            n = A.GetLength(1);
        }

        public double[,] Calculate(double[] result)
        {
            int mainCol, mainRow; //ведущие столбец и строка

            while (!IsItEnd())
            {
                mainCol = FindMainCol();
                mainRow = FindMainRow(mainCol);
                basis[mainRow] = mainCol;

                double[,] new_table = new double[m, n];

                for (int j = 0; j < n; j++)
                    new_table[mainRow, j] = A[mainRow, j] / A[mainRow, mainCol];

                for (int i = 0; i < m; i++)
                {
                    if (i == mainRow)
                        continue;

                    for (int j = 0; j < n; j++)
                        new_table[i, j] = A[i, j] - A[i, mainCol] * new_table[mainRow, j];
                }
                A = new_table;
            }

            //заносим в result найденные значения X
            for (int i = 0; i < result.Length; i++)
            {
                int k = basis.IndexOf(i + 1);
                if (k != -1)
                    result[i] = A[k, 0];
                else
                    result[i] = 0;
            }

            return A;
        }

        private bool IsItEnd()
        {
            bool flag = true;

            for (int j = 1; j < n; j++)
            {
                if (A[m - 1, j] < 0)
                {
                    flag = false;
                    break;
                }
            }

            return flag;
        }

        private int FindMainCol()
        {
            int mainCol = 1;

            for (int j = 2; j < n; j++)
                if (A[m - 1, j] < A[m - 1, mainCol])
                    mainCol = j;

            return mainCol;
        }

        private int FindMainRow(int mainCol)
        {
            int mainRow = 0;

            for (int i = 0; i < m - 1; i++)
                if (A[i, mainCol] > 0)
                {
                    mainRow = i;
                    break;
                }

            for (int i = mainRow + 1; i < m - 1; i++)
                if ((A[i, mainCol] > 0) && ((A[i, 0] / A[i, mainCol]) < (A[mainRow, 0] / A[mainRow, mainCol])))
                    mainRow = i;

            return mainRow;
        }
    }
}
