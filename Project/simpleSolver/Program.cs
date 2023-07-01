using System;
using System.Security.Cryptography;
using System.Linq;

namespace App
{
    internal class Project
    {
        public static Int16 SIZE = 9;                 //? size of sudoku row & col سایز جدول سودوکو
        public static Int16 UNASSIGNED = 0;           //? نمایشگر خانه خالی سودوکو

        public static void Main()
        {
            int[,] sudoku = new int[SIZE, SIZE];
            SudokuSolver S = new SudokuSolver(UNASSIGNED, SIZE, sudoku);

            Console.WriteLine("fill the sudoku");            
            S.sudokuFiller();
            
            Console.WriteLine("The sudoku :");
            //? چاپ سودوکو اولیه
            S.printCurrentSudoku();

            S.solver();
            //? چاپ نهایی
            Console.WriteLine("Final Sudoku");
            S.printCurrentSudoku();
        }
    }
    internal class SudokuSolver
    {
        private Int16 unassigned;
        private Int16 size;
        private int[,] sudoku;

        public Int16 Unassigned
        {
            get { return unassigned; }
            set { unassigned = value; }
        }
        public Int16 Size
        {
            get { return size; }
            set { size = value; }
        }

        public int[,] Sudoku
        {
            get { return sudoku; }
            set { sudoku = value; }
        }

        public SudokuSolver(Int16 unassigned, Int16 size, int[,] sudoku)
        {
            Unassigned = unassigned;
            Size = size;
            Sudoku = sudoku;
        }
        public SudokuSolver() { }


        public int[,] sudokuFiller()
        {
            int userInpInt = unassigned;
            string? userInpStr;
            bool isExists = false;

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.WriteLine("The current sudoku:");
                    printCurrentSudoku();
                    do
                    {
                        Console.WriteLine("Enter el [" + i + "][" + j + "]:");
                        userInpStr = Console.ReadLine();
                        if (
                            userInpStr == "" ||
                            userInpStr == " " ||
                            userInpStr == null)
                        {
                            userInpInt = unassigned;     //? 0 یعنی اون خونه تو سودوکو خالیه
                        }
                        else
                        {
                            userInpInt = Convert.ToInt16(userInpStr);
                        }
                        if (userInpInt > 9 || userInpInt < 0)
                        {
                            Console.WriteLine("ERR:enter number between 0-9(space or enter for skipping) !!!");
                        }
                        isExists = checkDuplicateNum(userInpInt, i, j);

                        if (isExists && userInpInt != unassigned)
                        {
                            Console.WriteLine("dont repeat two num in row or col تو سطر یا ستون یه عدد یکسان هست");
                        }
                        else
                        {
                            isExists = false;
                        }
                    } while ((userInpInt > 9 || userInpInt < 0) || isExists);

                    sudoku[i, j] = userInpInt;
                }
            }
            return sudoku;
        }

        public void printCurrentSudoku()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.Write(sudoku[i, j]);
                    if (j != 0 || j != size)
                    {
                        Console.Write("|");
                    }

                }
                Console.Write("\n------------------\n");
            }
        }

        private bool checkDuplicateNum(int num, int x, int y)
        {
            bool isExists = false;
            for (int i = 0; i < size; i++)
            {
                // Console.WriteLine(sudoku[i, y]);
                if (sudoku[i, y] == num)
                {     //? checking for num exists in row (چک کردن در ستون)
                    isExists = true;
                }
                // Console.WriteLine(sudoku[x, i]);
                if (sudoku[x, i] == num)
                {     //? checking for num exists in col‌ (چک کردن در سطر)
                    isExists = true;
                }
            }

            return isExists;
        }

        public bool solver()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (sudoku[i, j] == unassigned)
                    {
                        for (int n = 1; n <= size; n++)
                        {
                            //? اگر عددی بین صفر تا نه هست که قوانین تکرارو نقض نکنه اون رو قرار میدیم
                            if (!checkDuplicateNum(n, i, j))
                            {
                                sudoku[i, j] = n;
                                //? اگر به پابان رسیده حل سودوکو از متود بیا بیرون
                                if (solver())
                                {
                                    return true;
                                }
                                else
                                {
                                    //? در غیر اینصورت یعنی تمامی اعداد بین صفر تا نه مرحله بعدی ممکن نبوده تو اون خانه قرار بگیره پس حانه قبلی رو خالی میکنیم
                                    sudoku[i, j] = unassigned;
                                }
                            }
                        }
                        //? تمامی  اعداد ممکن نمیتوانند قرار بگیرند پس برمیگردیم مرحله قبل تا یه عدد دیگه جایگزین کنیم
                        return false;
                    }
                }
            }

            //? حلقه اصلی تمام شد پس از متود بیا بیرون
            return true;

        }

    }
}
