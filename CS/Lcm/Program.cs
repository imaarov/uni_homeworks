using System;

namespace App
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Enter num 1 : ");
            int num1 = Math.Abs(Convert.ToInt32(Console.ReadLine()));

            Console.WriteLine("Enter num 2 : ");
            int num2 = Math.Abs(Convert.ToInt32(Console.ReadLine()));

            int resGCD = calcGCD(num1, num2);   // ب.م.م
            int resLCM = calcLCM(num1, num2);   // ک.م.م
            Console.WriteLine("Result gcd is " + resGCD + ", result lcm is : " + resLCM);
        }

        static private int calcGCD(int num1, int num2)
        {
            int g = num1;   // greeter
            int l = num2;   // lower
            int gcd = 1;

            if(num1 < num2) {
                g = num2;
                l = num1;
            }

            while(l != 0) {
                if(num1 % l == 0 && num2 % l ==0) {
                    gcd = l;
                    break;
                }
                --l;
            }
            return gcd;
        }

        static private int calcLCM(int num1, int num2)
        {
            return Math.Abs(num1 * num2) / calcGCD(num1, num2);
        }
    }
}