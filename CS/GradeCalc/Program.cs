using System;

namespace App
{
    class Project
    {

        public const int PASS = 10; 
        static void Main()
        {
            Console.WriteLine("Enter mid term grade: ");
            int midGrade = Convert.ToInt32(Console.ReadLine());
            
            Console.WriteLine("Enter final grade: ");
            int finalGrade = Convert.ToInt32(Console.ReadLine());
            
            double grade = calcGrade(midGrade, finalGrade);

            if(grade >= PASS) {
                Console.WriteLine("Pass shodi");
            }else {
                Console.WriteLine("Pass nashodi");
            }
        }

        static double calcGrade(int mid, int final)
        {
            return (mid * 0.35) + (final * 0.65);
        }

    }
}