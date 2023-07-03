using System;
using System.Security.Cryptography;
using System.Linq;

namespace App
{
    internal class Project
    {
        public static Int16 SIZE = 9;                 //? size of sudoku row & col سایز جدول سودوکو
        public static Int16 UNASSIGNED = 0;           //? نمایشگر خانه خالی سودوکو
        public static Int16 RANDOMFILLER = 2;         //? عدد ورودی متناسب با پر کردن رندوم
        public static Int16 USERFILLER = 1;           //? عدد ورودی متناسب با پر کردن توسط کاربر
        public static double SPEED = 0.03;                 //? سرعت حل        

        public static void Main()
        {
            int[,] sudoku = new int[SIZE, SIZE];
            SudokuSolver S = new SudokuSolver(UNASSIGNED, SIZE, RANDOMFILLER, USERFILLER, SPEED, sudoku);
            //? انتخاب پر کردن سودوکو با کاربر یا پر کردن رندوم توسط برنامه
            int opt = 0;
            Console.WriteLine("Enter the option you want: \n"
            + "1-Fill sudoku yourself\n"
            + "2-Fill sudoku random");
            opt = Convert.ToInt16(Console.ReadLine());
            if(opt == USERFILLER){
                S.sudokuFiller();
            }else{
                //? گرفتن میزان سختی سودوکو که همان تعداد خانه های پر شده را در یک ردیف مشخص میکند
                int difficulty = 0;
                Console.WriteLine("Enter Difficulty: (between 2-4 for quick result)");
                difficulty = Convert.ToInt16(Console.ReadLine());
                S.fillRandSudoku(difficulty);
            }

            //? چاپ سودوکو اولیه
            Console.WriteLine("The sudoku :");
            S.printCurrentSudoku();

            S.solver();
            S.printCurrentSudoku();
        }
    }
    internal class SudokuSolver
    {
        private Int16 unassigned;
        private Int16 size;
        private Int16 randomfiller;
        private Int16 userfiller;
        private double speed;
        private int[,] sudoku;

        public Int16 Unassigned
        {
            get{return unassigned;}
            set{unassigned = value;}
        }
        public Int16 Size
        {
            get { return size; }
            set { size = value; }
        }
        public Int16 Randomfiller
        {
            get { return randomfiller; }
            set { randomfiller = value; }
        }
        public Int16 Userfiller
        {
            get { return userfiller; }
            set { userfiller = value; }
        }
        public double Speed
        {
            get { return speed; }
            set { speed = value; }
        }
        public int[,] Sudoku
        {
            get { return sudoku; }
            set { sudoku = value; }
        }

        public SudokuSolver(Int16 unassigned, Int16 size, Int16 randomfiller, Int16 userfiller, double speed, int[,] sudoku) 
        {
            Unassigned = unassigned;
            Size = size;
            Randomfiller = randomfiller;
            Userfiller = userfiller;
            Speed = speed;
            Sudoku = sudoku;
        }
        public SudokuSolver() {}


        /// <summary>پر کردن جدول سودوکو توسط کاربر</summary>
        /// <param name="sudoku">آرایه ای که سودوکو در ان قراره پر بشه</param>
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
                            userInpStr == null){
                                userInpInt = unassigned;     //? 0 یعنی اون خونه تو سودوکو خالیه
                            }else{
                                userInpInt = Convert.ToInt16(userInpStr);
                            }
                            if(userInpInt > 9 || userInpInt < 0) {
                                Console.WriteLine("ERR:enter number between 0-9(space or enter for skipping) !!!");
                            }
                            isExists = checkDuplicateNum(userInpInt, i, j);

                            if (isExists && userInpInt != unassigned)
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

        /// <summary>چاپ سودوکو فعلی</summary>
        /// <param name="sudoku">سودوکوای که باید چاپ بشه</param>
        public void printCurrentSudoku()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.Write(sudoku[i,j]);
                    if(j != 0 || j != size) {
                        Console.Write("|");
                    }

                }
                Console.Write("\n------------------\n");
            }
        }

        /// <summary>چک کردن قوانین تکرار تو سودوکو</summary>
        /// <param name="num">عددی که قراره وارد بشه</param>
        /// <param name="sudoku">جدول سودوکو</param>
        /// <param name="x">تو چه سطری قراره وارد بشه</param>
        /// <param name="y">تو چه ستونی قراره وارد بشه</param>
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

            //? چک کردن ۳در۳
            int startRow = x - x % 3;
            int startCol = y - y % 3;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (sudoku[i + startRow, j + startCol] == num)
                    {
                        isExists = true;
                    }
                }
            }

            return isExists;
        }

        /// <summary>چک کردن پر بودن سودوکو</summary>
        /// <param name="sudoku">جدول سودوکو</param>
        private bool isFullSudoku()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if(sudoku[i, j] == unassigned) {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>پر کردن رندوم جدول سودوکو</summary>
        /// <param name="sudoku">جدول سودوکو</param>
        /// <param name="difficulty">تعدادی که باید پر بشه تو هر  خونه</param>
        public void fillRandSudoku(int difficulty)
        {
            int[] rands = new int[difficulty];
            Random r  = new Random();
            Random r2 = new Random();
            for (int i = 0; i < size; i++)
            {
                //? Get random cell PLACE in one arr
                for (int k = 0; k < difficulty; k++)
                {
                    rands[k] = r.Next(0, size);
                }


                for (int j = 0; j < size; j++)
                {
                    if(rands.Contains(j)){
                        for (int z = 0; z < size; z++)
                        {
                            if(!checkDuplicateNum(z, i, j)) {
                                sudoku[i, j] = z;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>حل سودوکو به صورت بازگشتی</summary>
        /// <param name="sudoku">پوینتر به سودوکو برای تغییر کردن مقدار پارامتر پاس داده شده</param>
        public bool solver()
        {
            //? چاپ هر مرحله از حل سودوکو
            Console.Clear();
            printCurrentSudoku();
            System.Threading.Thread.Sleep(
    (int)System.TimeSpan.FromSeconds(speed).TotalMilliseconds);
            Console.WriteLine("----------------------");


            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if(sudoku[i,j] == unassigned) {
                        for (int n = 1; n <= size; n++)
                        {
                            //? اگر عددی بین صفر تا نه هست که قوانین تکرارو نقض نکنه اون رو قرار میدیم
                            if(!checkDuplicateNum(n, i, j)) {
                                sudoku[i, j] = n;
                                //? اگر به پابان رسیده حل سودوکو از متود بیا بیرون
                                if(solver()){
                                    return true;
                                }else{
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
