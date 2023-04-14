using System;

namespace App
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Enter x: ");
            int x = Convert.ToInt32(Console.ReadLine());

            calc(x);
        }

        static private void calc(int x)
        {
            double res = (x * x) + (2 * x) - 4;

            Console.WriteLine("Result : " + res);
        }
    }
}