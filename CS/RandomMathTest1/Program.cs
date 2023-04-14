using System;

namespace App
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Enter x: ");
            int x = Convert.ToInt32(Console.ReadLine());
            
            Console.WriteLine("Enter y: ");
            int y = Convert.ToInt32(Console.ReadLine());

            calc(x, y);
        }

        static private void calc(int x, int y)
        {
            double res = Math.Sqrt(
                Math.Abs(x) 
                + (y * y)
            );

            Console.WriteLine("Result : " + res);
        } 
    }
}