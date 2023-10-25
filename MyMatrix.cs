using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph6
{
    public class MyMatrix
    {
        public readonly int m, n;
        public float[,] matrix;
        public MyMatrix(int m, int n)
        {
            this.m = m;
            this.n = n;
            matrix = new float[m, n];
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                { matrix[i, j] = 0; }
            }

        }
        public MyMatrix(int m, int n, List<float> vals)
        {

            this.m = m;
            this.n = n;
            matrix = new float[m, n];
            for (int i = 0; i < m * n; i++)
            {
                matrix[i / n, i % n] = vals[i];
            }
        }

        public float this[int i, int j]
        {
            get { return matrix[i, j]; }
            set { matrix[i, j] = value; }
        }

        public static MyMatrix operator *(MyMatrix lhs, MyMatrix rhs)
        {
            if (lhs.n != rhs.m)
                throw new Exception("не совпадают размеры матриц для перемножения");

            MyMatrix res = new MyMatrix(lhs.m, rhs.n);
            for (int i = 0; i < lhs.m; i++)
            {
                for (int j = 0; j < rhs.n; j++)
                {
                    float temp = 0;

                    for (int k = 0; k < rhs.m; k++)
                    {
                        temp += lhs[i, k] * rhs[k, j];
                    }

                    res[i, j] = temp;
                }
            }

            return res;
        }

        public static MyMatrix operator +(MyMatrix lhs, MyMatrix rhs)
        {
            if (lhs.n != rhs.n && lhs.m == rhs.m)
                throw new Exception("не совпадают размеры матриц для перемножения");

            MyMatrix res = new MyMatrix(lhs.m, lhs.n);
            for (int i = 0; i < lhs.m; i++)
            {
                for (int j = 0; j < rhs.n; j++)
                {


                    res[i, j] = lhs[i, j] + rhs[i, j];
                }
            }

            return res;
        }

    }
}
