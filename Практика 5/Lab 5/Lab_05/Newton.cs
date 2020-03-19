using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Symbolics;
using Expr = MathNet.Symbolics.SymbolicExpression;

namespace Lab_05
{
    class Newton
    {
        const int maxCounter = 20;

        double[,] numJac;

        double[,] prev_vector;

        double[,] curr_vector;

        double[,] curr_func_value;

        #region Properties
        public EquationsSystem Equations { get; set; }

        public double Eps { get; set; }

        public Dictionary<string, FloatingPoint> X { get; set; }

        public Expr[,] Jacobian { get; set; }

        public double JacobianValue { get; set; }

        public double[,] RevJacobian { get; set; }

        public int Counter { get; set; }

        #endregion

        public Newton(EquationsSystem eq, Dictionary<string, FloatingPoint> values, double e)
        {
            Equations = eq;
            Eps = e;
            X = values;
            Counter = 0;
            Jacobian = FindJacobian();
            JacobianValue = CountJacobian();
            RevJacobian = CountRevJacobian();
        }

        #region Methods

        #region JacobianSettings
        private Expr[,] FindJacobian()
        {
            Expr[,] jac = new Expr[Equations.Functions.GetLength(0), Equations.Functions.GetLength(1)];

            for (int i = 0; i < jac.GetLength(0); i++)
            {
                for (int j = 0; j < jac.GetLength(1); j++)
                {
                    jac[i, j] = Equations.Functions[i, j].Differentiate($"x{j + 1}");

                    Console.WriteLine(jac[i, j]);
                }
            }

            return jac;
        }

        private double CountJacobian()
        {
            numJac = new double[Jacobian.GetLength(0), Jacobian.GetLength(1)];

            double jacValue;

            for (int i = 0; i < Jacobian.GetLength(0); i++)
            {
                for (int j = 0; j < Jacobian.GetLength(1); j++)
                {
                    numJac[i, j] = double.Parse(Jacobian[i, j].Evaluate(X).RealValue.ToString().Replace(".", ","));
                }
            }

            jacValue = Matrix.CalculateDeterminant(numJac, numJac.GetLength(0));

            return jacValue;
        }

        private double[,] CountRevJacobian()
        {
            double[,] revJac = Matrix.CalculateInverseMatrix(numJac);

            return revJac;
        }
        #endregion

        public void Solve()
        {
            double max;

            curr_vector = new double[X.Values.Count, 1];

            prev_vector = new double[X.Values.Count, 1];

            var temp = X;

            do
            {
                Counter++;

                curr_func_value = new double[Equations.Functions.GetLength(1), 1];

                for (int i = 0; i < temp.Count; i++)
                {
                    var elem = temp.ElementAt(i);
                    prev_vector[i, 0] = double.Parse(elem.Value.RealValue.ToString().Replace(".", ","));
                }


                for (int i = 0; i < Equations.Functions.GetLength(0); i++)
                {
                    for (int j = 0; j < Equations.Functions.GetLength(1); j++)
                    {
                        curr_func_value[i, 0] += double.Parse(Equations.Functions[i, j].Evaluate(temp).RealValue.ToString().Replace(".", ","));
                    }
                }

                var delta_x = Matrix.MatrixMultiply(RevJacobian, curr_func_value);

                curr_vector = Matrix.MatrixSubtraction(prev_vector, delta_x);

                max = Math.Abs(delta_x[0, 0]);

                temp = new Dictionary<string, FloatingPoint>();

                for (int i = 0; i < X.Count; i++)
                {
                    if (max < Math.Abs(delta_x[i, 0]))
                        max = Math.Abs(delta_x[i, 0]);

                    var elem = X.ElementAt(i);
                    temp.Add(elem.Key, Expr.Parse(curr_vector[i, 0].ToString().Replace(",", ".")).RealNumberValue);
                }

                Console.WriteLine($"K = {Counter}");

                Matrix.PrintMatrix(delta_x);

            } while (max > Eps && Counter != maxCounter);

            Matrix.PrintMatrix(curr_vector);

        }

        #endregion
    }
}
