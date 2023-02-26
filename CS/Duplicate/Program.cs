using System;

namespace App
{
    class Project
    {
        public const int SIZE = 3;
        static void Main()
        {
            int[,] arr = new int[SIZE, SIZE];
            int dupCount = 0;
            int dupNum = 0;
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    Console.WriteLine("Enter [" 
                        + i 
                        + "][" 
                        + j 
                        + "]: "
                    );
                    arr[i,j] = Convert.ToInt32(Console.ReadLine());
                }
            }

            Console.WriteLine("Enter duplicate number : ");
            dupNum = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    if(arr[j,i] == dupNum)
                        ++dupCount;
                }
            }

            Console.WriteLine("Duplicate count for " 
                + dupNum 
                + "is : " 
                + dupCount
            );
        }

    }
}