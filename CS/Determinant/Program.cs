using System;

namespace App
{
    class Project
    {
        static void Main()
        {
            int[,] matrix = new int [3,3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.WriteLine("Enter number of [" + i + "][" + j + "]:");
                    matrix[i,j] = Convert.ToInt32(Console.ReadLine());
                }
                Console.WriteLine("Result of matrix is : " + matrixCalc(matrix));
            }
        }

        public static int matrixCalc(int[,] matrix)
        {
            return (matrix[0,0] * ( (matrix[1,1] * matrix[2,2]) - (matrix[1,2] * matrix[2,1]) ) )
                 - (matrix[0,1] * ( (matrix[1,0] * matrix[2,2]) - (matrix[1,2] * matrix[2,0]) ) )
                 + (matrix[0,2] * ( (matrix[1,0] * matrix[2,1]) - (matrix[1,1] * matrix[2,0])));
        }
    }
}