using System;

namespace App
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Enter num1:");
            string strInp1 = Console.ReadLine();

            Console.WriteLine("Enter num2:");
            string strInp2 = Console.ReadLine();

            int intInp1 = Convert.ToInt32(strInp1);
            int intInp2 = Convert.ToInt32(strInp2);

            Console.WriteLine("Sum (integer) : " + (intInp1 + intInp2));
            Console.WriteLine("Sum (string) : " + (strInp1 + strInp2));

        }
    }
}