using System;

namespace App
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Enter num: ");
            int num = Convert.ToInt32(Console.ReadLine());
            
            bool isOdd = true;
            if(num % 2 == 0) {
                isOdd = false;
            }
            calc(num, isOdd);
            
        }

        private static void calc(int num, bool isOdd)
        {
            int i = isOdd 
                ? 1 
                : 2;

            int sum  = 0;
            int mult = 1;

            for (; i <= num; i++)
            {
                sum  += i;
                mult *= i;
            }

            Console.WriteLine("sum : " + sum + ", multiply : " + mult);
        }
    }
}