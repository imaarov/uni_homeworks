#include <iostream>

using namespace std;

#define SIZE 2

void getArr(int arr[SIZE][SIZE]);
int getDupCount(int[SIZE][SIZE], int);

int main()
{
    int arr[SIZE][SIZE];
    int dupNum;

    getArr(arr);

    cout << "Enter duplicate number: ";
    cin >> dupNum;

    int dupCount = getDupCount(arr, dupNum);

    printf("the number of %d its duplicate %d times", 
        dupNum, 
        dupCount
    );
}

void getArr(int arr[SIZE][SIZE])
{
    for (int i = 0; i < SIZE; i++)
    {
        for (int j = 0; j < SIZE; j++)
        {
            printf("Enter the[%d][%d]", i, j);
            cin >> arr[i][j];
        }
    }
}

int getDupCount(int arr[SIZE][SIZE], int dupNum)
{
    int dupCount = 0;
    for (int i = 0; i < SIZE; i++)
    {
        for (int j = 0; j < SIZE; j++)
        {
            if (arr[j][i] == dupNum)
                ++dupCount;
        }
    }

    return dupCount;
}