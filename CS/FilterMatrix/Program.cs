using System;

namespace App
{
    class Project
    {
        public static int SIZE = 4; //TODO change to 50
        static void Main()
        {
            double[,] matrix = new double[SIZE,SIZE];
            //? Fill matrix with user input
            matrix = fillMatrix(matrix);
            //? Filter matrix
            matrix = filterMatrix(matrix);
            printMatrix(matrix);
        }

        private static void printMatrix(double[,] matrix)
        {
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    Console.Write(matrix[i,j] + " ");
                }
                Console.Write("\n");
            }
        }

        private static double[,] fillMatrix(double[,] matrix)
        {
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    Console.WriteLine("Enter the [" + i + "]["+ j +"] , element: ");
                    matrix[i,j] = Convert.ToInt32(Console.ReadLine());
                }
            }
            return matrix;
        }

        private static double[,] filterMatrix(double[,] matrix)
        {
            double tempAvg = 0;
            for (int i = 1; i < SIZE - 1; i++)
            {
                for (int j = 1; j < SIZE - 1; j++)
                {

                    //? پیمایش آرایه سه در سه ای به مرکز المنتی که داریم
                    for (int k = i - 1; k <= i + 1; k++)
                    {
                        for(int z = j - 1;z <= j + 1; z++)
                        {
                            Console.Write(matrix[k,z] + "-");
                            //? جمع ۹ المنت با هم و ذخیره برای میانگین گیری
                            tempAvg += matrix[k,z];
                        }
                    }
                    Console.WriteLine("sum:" + tempAvg);
                    matrix[i,j] = tempAvg / 9;
                    //? reset tempAvg
                    tempAvg = 0;
                }
            }

            return matrix;
        }

    }
}