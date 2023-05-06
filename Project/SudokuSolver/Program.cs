using System;
using System.Security.Cryptography;
using System.Linq;

namespace App
{
    class Project
    {
        public static int SIZE = 9;                 //? size of sudoku row & col سایز جدول سودوکو
        public static int UNASSIGNED = 0;           //? نمایشگر خانه خالی سودوکو
        public static int RANDOMFILLER = 2;         //? عدد ورودی متناسب با پر کردن رندوم
        public static int USERFILLER = 1;           //? عدد ورودی متناسب با پر کردن توسط کاربر
        public static double SPEED;                 //? سرعت حل

        static void Main()
        {
            int[,] sudoku = new int[SIZE, SIZE];

            //? انتخاب پر کردن سودوکو با کاربر یا پر کردن رندوم توسط برنامه
            int opt = 0;
            Console.WriteLine("Enter the option you want: \n"
            + "1-Fill sudoku yourself\n"
            + "2-Fill sudoku random");
            opt = Convert.ToInt16(Console.ReadLine());
            if(opt == USERFILLER){
                sudoku = sudokuFiller(sudoku);
            }else{
                //? گرفتن میزان سختی سودوکو که همان تعداد خانه های پر شده را در یک ردیف مشخص میکند
                int difficulty = 0;
                Console.WriteLine("Enter Difficulty:");
                difficulty = Convert.ToInt16(Console.ReadLine());
                fillRandSudoku(ref sudoku, difficulty);
            }
            
            //? چاپ سودوکو اولیه
            Console.WriteLine("The sudoku :");
            printCurrentSudoku(sudoku);

            //? سرعت حل سودوکو-سرعت کم باشه مراحل پر شدن جدول رو بهتر میبینیم
            Console.WriteLine("Enter the speed for solving:(1-non stop 2-showing the steps of solving)");
            int speed = Convert.ToInt16(Console.ReadLine());
            if(speed == 1) {
                SPEED = 0;
            }else {
                SPEED = 0.03;
            }

            //? حل جدول
            solver(ref sudoku);

            //? چاپ جواب نهایی
            Console.WriteLine("Answer : ");
            printCurrentSudoku(sudoku);

            //? چک کردن پر شدن جدول سودوکو
            if(isFullSudoku(sudoku)){
                System.Console.WriteLine("solved");
            }
        }

        /// <summary>پر کردن جدول سودوکو توسط کاربر</summary>
        /// <param name="sudoku">آرایه ای که سودوکو در ان قراره پر بشه</param>
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

        /// <summary>چاپ سودوکو فعلی</summary>
        /// <param name="sudoku">سودوکوای که باید چاپ بشه</param>
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

        /// <summary>چک کردن قوانین تکرار تو سودوکو</summary>
        /// <param name="num">عددی که قراره وارد بشه</param>
        /// <param name="sudoku">جدول سودوکو</param>
        /// <param name="x">تو چه سطری قراره وارد بشه</param>
        /// <param name="y">تو چه ستونی قراره وارد بشه</param>
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
    
        /// <summary>چک کردن پر بودن سودوکو</summary>
        /// <param name="sudoku">جدول سودوکو</param>
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
    
        /// <summary>پر کردن رندوم جدول سودوکو</summary>
        /// <param name="sudoku">جدول سودوکو</param>
        /// <param name="difficulty">تعدادی که باید پر بشه تو هر  خونه</param>
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

        /// <summary>حل سودوکو به صورت بازگشتی</summary>
        /// <param name="sudoku">پوینتر به سودوکو برای تغییر کردن مقدار پارامتر پاس داده شده</param>
        private static bool solver(ref int[,] sudoku)
        {
            //? چاپ هر مرحله از حل سودوکو
            Console.Clear();
            printCurrentSudoku(sudoku);
            System.Threading.Thread.Sleep(
    (int)System.TimeSpan.FromSeconds(SPEED).TotalMilliseconds);
            Console.WriteLine("----------------------");


            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    if(sudoku[i,j] == UNASSIGNED) {
                        for (int n = 1; n <= SIZE; n++)
                        {
                            //? اگر عددی بین صفر تا نه هست که قوانین تکرارو نقض نکنه اون رو قرار میدیم
                            if(!checkDuplicateNum(n, sudoku, i, j)) {
                                sudoku[i, j] = n;
                                //? اگر به پابان رسیده حل سودوکو از متود بیا بیرون
                                if(solver(ref sudoku)){
                                    return true;
                                }else{
                                    //? در غیر اینصورت یعنی تمامی اعداد بین صفر تا نه مرحله بعدی ممکن نبوده تو اون خانه قرار بگیره پس حانه قبلی رو خالی میکنیم
                                    sudoku[i, j] = UNASSIGNED;
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