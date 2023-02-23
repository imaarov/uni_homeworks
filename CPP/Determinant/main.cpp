#include <iostream>

using namespace std;

#define MATRIXSIZE 3

void getMatrix(int[MATRIXSIZE][MATRIXSIZE]);
int calcMatrix(int[MATRIXSIZE][MATRIXSIZE]);
void get2X2Matrix(int[MATRIXSIZE][MATRIXSIZE], int[4], int, int);
int calc2X2Matrix(int arr[1], int num);

int main()
{
    int matrix[MATRIXSIZE][MATRIXSIZE];
    getMatrix(matrix);
    int result = calcMatrix(matrix);
    printf("\nresult is %d\n", result);
    return 0;
}

void getMatrix(int arr[MATRIXSIZE][MATRIXSIZE])
{
    for (int i = 0; i < MATRIXSIZE; i++)
    {
        for (int j = 0; j < MATRIXSIZE; j++)
        {
            printf("Enter the[%d][%d]", i, j);
            cin >> arr[i][j];
        }
    }
}

int calcMatrix(int arr[MATRIXSIZE][MATRIXSIZE])
{
    int row = 0;
    int n = 0;
    int temp2X2Matrix[4];
    int results[MATRIXSIZE];
    int result = 0;

    for (int col = 0; col < MATRIXSIZE; col++)
    {
        cout << endl
             << "calc for" << arr[row][col] << endl;
        get2X2Matrix(arr, temp2X2Matrix, row, col);
        results[n++] = calc2X2Matrix(temp2X2Matrix, arr[row][col]);
    }

    for (int i = 0; i < MATRIXSIZE; i++)
    {
        cout << endl
             << results[i] << endl;
        int n = 0;
        if (i % 2 == 0)
            n = 1;
        else
            n = -1;
        result += n * results[i];
    }
    return result;
}

void get2X2Matrix(int arr[MATRIXSIZE][MATRIXSIZE], int resArr[4], int col, int row)
{
    int k = 0;

    for (int i = 0; i < MATRIXSIZE; i++)
    {
        for (int j = 0; j < MATRIXSIZE; j++)
        {
            if (col != i && row != j)
            {
                printf("arr=%d", arr[i][j]);
                resArr[k++] = arr[i][j];
            }
        }
    }
}

int calc2X2Matrix(int arr[1], int num)
{
    return num * ((arr[0] * arr[3]) - (arr[1] * arr[2]));
}