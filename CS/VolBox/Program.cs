using System;

namespace App
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Enter width: ");
            int w = Convert.ToInt32(Console.ReadLine());
            
            Console.WriteLine("Enter length: ");
            int l = Convert.ToInt32(Console.ReadLine());
            
            Console.WriteLine("Enter height: ");
            int h = Convert.ToInt32(Console.ReadLine());

            double res = calc(w, l, h);
            Console.WriteLine("Vol is " + res);
        }

        static private double calc(int w, int l, int h)
        {
            return w * l * h;
        }  
    }
}