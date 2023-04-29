using System;

namespace App
{
    class Project
    {
        public static int SIZE = 9; //? size of sudoku row & col
        static void Main()
        {
            int[,] sudoku = new int[SIZE, SIZE];
            sudoku = sudokuFiller(sudoku);
        }

        private static int[,] sudokuFiller(int[,] sudoku)
        {
            int userInpInt = 0;
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
                                userInpInt = 0;     //? 0 یعنی اون خونه تو سودوکو خالیه
                            }else{
                                userInpInt = Convert.ToInt16(userInpStr);
                            }
                            if(userInpInt > 9 || userInpInt < 0) {
                                Console.WriteLine("ERR:enter number between 0-9(space or enter for skipping) !!!");
                            }
                            isExists = checkDuplicateNum(userInpInt, sudoku, i, j);
                            
                            if (isExists && userInpInt != 0)
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
    }
}