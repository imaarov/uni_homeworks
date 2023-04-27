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
                            userInpStr == null)
                        {
                            userInpInt = 0;     //? ۰ یعنی اون خونه تو سودوکو خالیه
                        }
                        else
                        {
                            userInpInt = Convert.ToInt16(userInpStr);
                        }
                        if (userInpInt > 9 || userInpInt < 0)
                        {
                            Console.WriteLine("ERR:enter number between 0-9(space or enter for skipping) !!!");
                        }
                    } while (userInpInt > 9 || userInpInt < 0);

                    // Console.WriteLine(userInp);
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
                    Console.Write(sudoku[i, j]);
                    if (j != 0 || j != SIZE)
                    {
                        Console.Write("|");
                    }

                }
                Console.Write("\n------------------\n");
            }
        }

    }
}