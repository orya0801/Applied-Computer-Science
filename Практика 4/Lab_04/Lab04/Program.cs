using System;

namespace Lab04
{
    class Program
    {
        static void Main(string[] args)
        {
            //Example 1
            double[,] matrix1 = new double[,] { { 4, -3, 2, -1 },
                                                { 3, -2, 1, -3 },
                                                { 5, -3, 1, -8} };
            double[] vector1 = new double[] { 8, 7, 1 };

            EquationsSystem system1 = new EquationsSystem(matrix1, vector1);

            system1.Solve();

            //Example 2
            double[,] matrix2 = new double[,] { { 1, 2, 2, 3 },
                                                { 6, -3, -3, -1 },
                                                { -7, 1, 1, -2},
                                                { -3, 9, 9, 10}};
            double[] vector2 = new double[] { 1, -9, 8 , 12 };

            EquationsSystem system2 = new EquationsSystem(matrix2, vector2);

            system2.Solve();

            //Example 3
            double[,] matrix3 = new double[,] { {1, 1, -1, 1 },
                                                {2, -1, 3, -2},
                                                {1, 0, -1, 2},
                                                {3, -1, 1, -1} };
            double[] vector3 = new double[] { 4, 1, 6, 0};

            EquationsSystem system3 = new EquationsSystem(matrix3, vector3 );

            system3.Solve();


            //Example 4
            double[,] matrix4 = new double[,] { {2, 3, -1, 1 },
                                                {8, 12, -9, 8},
                                                {4, 6, 3, -2},
                                                {2, 3, 9, -7} };
            double[] vector4 = new double[] { 1, 3, 3, 3 };

            EquationsSystem system4 = new EquationsSystem(matrix4, vector4);

            system4.Solve();

            Console.ReadKey();
        }
    }
}
