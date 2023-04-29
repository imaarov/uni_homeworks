using System;

namespace App
{
    class Project
    {
        public static int SIZE = 9; //? size of sudoku row & col
        public static int UNASSIGNED = 0;
        static void Main()
        {
            int[,] sudoku = new int[SIZE, SIZE];
            sudoku = sudokuFiller(sudoku);
            // sudoku = solveSudoku(sudoku);
            solver(ref sudoku);
            printCurrentSudoku(sudoku);
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
    
        // private static int[,] solveSudoku(int [,] sudoku)
        // {
        //     bool flag = true;
        //     for (int i = 0; i < SIZE; i++)
        //     {
        //         for (int j = 0; j < SIZE; j++)
        //         {
        //             if(sudoku[i,j] == UNASSIGNED){      //? اگه خونه سودوکو خالی بود
        //                 for (int n = 1; n <= SIZE; n++)
        //                 {
        //                     if(!checkDuplicateNum(n, sudoku, i, j)){    //? اگه این عدده تکراری نبود تو سطر و ستون
        //                         sudoku[i, j] = n;
        //                         solveSudoku(sudoku);
        //                         if(flag) {
        //                             goto afterBreak;
        //                         }
        //                     }
        //                 }
        //                 flag = false;   //? اگه کل عدد ها امکان قرار دادنش نیست 
        //             }
        //         }
        //         flag = true;
        //     }

        //     afterBreak:
        //         Console.WriteLine("Done");

        //     return sudoku;
        // }

        private static bool solver(ref int[,] sudoku)
        {
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