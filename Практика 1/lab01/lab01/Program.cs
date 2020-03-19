using System;
using System.Threading.Tasks;

namespace lab01
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Матрица A: ");
            Console.Write("Введите количество строк в матрице: ");
            int rows_array1 = int.Parse(Console.ReadLine());

            Console.Write("Введите количество столбцов в матрице: ");
            int columns_array1 = int.Parse(Console.ReadLine());

            int[,] array1 = Matrix.CreateMatrix(rows_array1, columns_array1);
          
            Matrix.PrintMatrix(array1);

            //Обработка исключений
            try
            {
                Matrix.CalculateInverseMatrix(array1);
            }
            //Если матрица 
            catch(DeterminantNullException e)
            {
                Console.WriteLine(e.Message);
            }
            catch(NonSquareMatrixException e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.ReadKey();
        }
    }
}
