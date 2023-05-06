using System;
using System.Security.Cryptography;
using System.Linq;

namespace App
{
    class Project
    {
        public static int SIZE = 9; //? size of sudoku row & col
        public static int UNASSIGNED = 0;
        public static int RANDOMFILLER = 2;
        public static int USERFILLER = 1;

        static void Main()
        {
            int[,] sudoku = new int[SIZE, SIZE];
           
            int opt = 0;
            
            Console.WriteLine("Enter the option you want: \n"
            + "1-Fill sudoku yourself\n"
            + "2-Fill sudoku random");
            opt = Convert.ToInt16(Console.ReadLine());

            if(opt == USERFILLER){
                sudoku = sudokuFiller(sudoku);
            }else{
                int diff = 0;
                Console.WriteLine("Enter Difficulty:");
                diff = Convert.ToInt16(Console.ReadLine());
                fillRandSudoku(ref sudoku, diff);
                Console.WriteLine("The random sudoku :");
                printCurrentSudoku(sudoku);
            }
            // sudoku = solveSudoku(sudoku);
            solver(ref sudoku);
            solver(ref sudoku);
            Console.WriteLine("Answer : ");
            printCurrentSudoku(sudoku);
            if(isFullSudoku(sudoku)){
                System.Console.WriteLine("solved");
            }
        }

        private static int[,] sudokuFiller(int[,] sudoku)
        {
            int userInpInt = UNASSIGNED;
            string? userInpStr;
            bool isExists = false;

            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    Console.WriteLine("The current sudoku:");
                    printCurrentSudoku(sudoku);
                    do
                    {
                        Console.WriteLine("Enter el [" + i + "][" + j + "]:");
                        userInpStr = Console.ReadLine();
                        if (
                            userInpStr == "" ||
                            userInpStr == " " ||
                            userInpStr == null){
                                userInpInt = UNASSIGNED;     //? 0 یعنی اون خونه تو سودوکو خالیه
                            }else{
                                userInpInt = Convert.ToInt16(userInpStr);
                            }
                            if(userInpInt > 9 || userInpInt < 0) {
                                Console.WriteLine("ERR:enter number between 0-9(space or enter for skipping) !!!");
                            }
                            isExists = checkDuplicateNum(userInpInt, sudoku, i, j);
                            
                            if (isExists && userInpInt != UNASSIGNED)
                            {
                                Console.WriteLine("dont repeat two num in row or col تو سطر یا ستون یه عدد یکسان هست");
                            }else{
                                isExists = false;
                            }
                    } while (( userInpInt > 9 || userInpInt < 0 ) || isExists);
                    
                    sudoku[i, j] = userInpInt;
                }
            }
            return sudoku;
        }

        private static void printCurrentSudoku(int[,] sudoku)
        {
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    Console.Write(sudoku[i,j]);
                    if(j != 0 || j != SIZE) {
                        Console.Write("|");
                    }
                    
                }
                Console.Write("\n------------------\n");
            }
        }

        private static bool checkDuplicateNum(int num, int[,] sudoku, int x, int y)
        {
            bool isExists = false;
            for (int i = 0; i < SIZE; i++)
            {
                // Console.WriteLine(sudoku[i, y]);
                if(sudoku[i,y] == num){     //? checking for num exists in row (چک کردن در ستون)
                    isExists = true;
                }
                // Console.WriteLine(sudoku[x, i]);
                if(sudoku[x,i] == num){     //? checking for num exists in col‌ (چک کردن در سطر)
                    isExists = true;
                }
            }

            return isExists;
        }
    
        private static bool isFullSudoku(int[,] sudoku)
        {
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    if(sudoku[i, j] == UNASSIGNED) {
                        return false;
                    }
                }
            }
            return true;
        }
    
        private static void fillRandSudoku(ref int[,] sudoku, int difficulty)
        {
            int[] rands = new int[difficulty];
            Random r  = new Random();
            Random r2 = new Random();
            for (int i = 0; i < SIZE; i++)
            {
                //? Get random cell PLACE in one arr
                for (int k = 0; k < difficulty; k++)
                {
                    rands[k] = r.Next(0, SIZE);
                }


                for (int j = 0; j < SIZE; j++)
                {
                    if(rands.Contains(j)){
                        for (int z = 0; z < SIZE; z++)
                        {
                            if(!checkDuplicateNum(z, sudoku, i, j)) {
                                sudoku[i, j] = z;
                            }
                        }
                    }
                }
            }
        }

        private static bool solver(ref int[,] sudoku)
        {
            printCurrentSudoku(sudoku);
            Console.WriteLine("----------------------");
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    if(sudoku[i,j] == UNASSIGNED) {
                        for (int n = 1; n <= SIZE; n++)
                        {
                            if(!checkDuplicateNum(n, sudoku, i, j)) {
                                sudoku[i, j] = n;
                                if(solver(ref sudoku)){
                                    return true;
                                }else{
                                    sudoku[i, j] = UNASSIGNED;
                                }
                            }
                        }
                        return false;
                    }
                }
            }
            return true;

        }
    
    }
}