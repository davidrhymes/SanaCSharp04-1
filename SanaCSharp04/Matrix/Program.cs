using System.Security.Cryptography;
using System;
using System.Data.Common;

int rows, colls;
int amountOfPos = 0, k = 0, sort, temp, max = -1000, j, i, rowsWithoutZero = 0, collsWithZero = 0;
Random random = new Random();
do
{
    Console.WriteLine("Enter amount of rows:");
    rows = int.Parse(Console.ReadLine());
    Console.WriteLine("Enter amount of colls:");
    colls = int.Parse(Console.ReadLine());
} while (rows <= 0 || colls <= 0);
int[,] arr = new int[rows, colls];
for (i = 0; i < rows; i++)
{
    for (j = 0; j < colls; j++)
    {
        arr[i, j] = random.Next(-100, 101);
        Console.Write($"{arr[i, j],7}");
    }
    Console.WriteLine();
}
/*1*/
for (i = 0; i < rows; i++)
{
    for (j = 0; j < colls; j++)
    {
        if (arr[i, j] > 0) amountOfPos++;
    }
}
Console.WriteLine($"Amount of positive: {amountOfPos}");
/*2*/
int[] arr2 = new int[rows * colls];
for (i = 0; i < rows; i++)
{
    for (j = 0; j < colls; j++)
    {
        arr2[k] = arr[i, j];
        k++;
    }
}
do
{
    sort = 0;
    for (k = 0; k < rows * colls - 1; k++)
    {
        if (arr2[k] < arr2[k + 1])
        {
            temp = arr2[k];
            arr2[k] = arr2[k + 1];
            arr2[k + 1] = temp;
            sort = 1;
        }
    }
} while (sort == 1);
for (k = 0; k < rows * colls - 1; k++)
{
    if (arr2[k] == arr2[k + 1])
    {
        max = arr2[k];
        break;
    }
}
if (max == -1000)
    Console.WriteLine("There is no number that occures several times");
else
    Console.WriteLine($"Max several times:{max}");
/*3*/
for (i = 0; i < rows; i++)
{
    j = 0;
    while (j < colls - 1 && arr[i, j] != 0)
    {
        j++;
    }
    if (j == colls - 1) rowsWithoutZero++;
}
Console.WriteLine($"Rows Without Zero: {rowsWithoutZero}");
/*4*/
for (j = 0; j < colls; j++)
{
    i = 0;
    while (i < rows - 1 && arr[i, j] != 0)
    {
        i++;
    }
    if (arr[i, j] == 0) collsWithZero++;
}
Console.WriteLine($"Colls With Zero: {collsWithZero}");
/*5*/
uint[,] rowInfo = new uint[rows, 1];
for (i = 0; i < rows; i++)
{
    uint maxCountRepeat = 0;
    for (j = 0; j < colls; j++)
    {
        uint countRepeat = 0;
        for (int jTemp = 0; jTemp < colls; jTemp++)
            if (jTemp != j && arr[i, j] == arr[i, jTemp])
                countRepeat++;
        if (countRepeat > maxCountRepeat)
            maxCountRepeat = countRepeat;

    }
    rowInfo[i, 0] = maxCountRepeat;
}
uint maxSeries = 0;
for (i = 1; i < rows; i++)
    if (rowInfo[i, 0] > maxSeries)
        maxSeries = rowInfo[i, 0];
Console.Write($"Number of row that have largest series of same elements: ");
for (i = 0; i < rows; i++)
    if (rowInfo[i, 0] == maxSeries)
        Console.Write($"{i}, ");
Console.WriteLine($"count of repeat = {maxSeries}");
/*6*/
int dobWoutNeg = 1;
for (i = 0; i < rows; i++)
{
    for (j = 0; j < colls && arr[i, j] >= 0; j++)
    {
        dobWoutNeg *= arr[i, j];
    }
    if (j == colls && arr[i, j - 1] >= 0)
        Console.WriteLine($"Row {i} product = {dobWoutNeg}");
    dobWoutNeg = 1;
}
/*7*/
int diagonalAmount = colls + rows - 1;
int[] sumOfDiag = new int[diagonalAmount];
int currI = arr.GetUpperBound(0);
int currJ = 0;
bool border = false;
for (k = 0; k < diagonalAmount; k++)
{
    i = currI;
    j = currJ;
    while (i < rows && j < colls)
    {
        sumOfDiag[k] += arr[i, j];
        i++;
        j++;
    }

    if (currI == 0)
        border = true;

    if (!border)
        currI--;
    else
        currJ++;
}
int indexMaxSum = 0;
for (i = 1; i < diagonalAmount; i++)
    if (sumOfDiag[indexMaxSum] < sumOfDiag[i])
        indexMaxSum = i;
Console.WriteLine($"Maximal value diagonal sum of parallel to main diagonal: {sumOfDiag[indexMaxSum]}, have diagonal with number: {indexMaxSum}");
/*8*/
int sumWoutZero;
bool zero;
for (j = 0; j < colls; j++)
{
    zero = false;
    sumWoutZero = 0;
    for (i = 0; i < rows; i++)
    {
        sumWoutZero += arr[i, j];
        if (arr[i, j] == 0)
            zero = true;
    }
    if (!zero)
        Console.WriteLine($"Sum of coll numer {j} without zero = {sumWoutZero}");
}
/*9*/
int[] sumOfAbsDiag = new int[diagonalAmount];
currI = rows - 1;
currJ = colls - 1;
border = false;
for (k = 0; k < diagonalAmount; k++)
{
    i = currI;
    j = currJ;
    while (i < rows && j >= 0 && i + j != rows - 1)
    {
        sumOfAbsDiag[k] += Math.Abs(arr[i, j]);
        i++;
        j--;
    }
    if (currI == 0)
        border = true;
    if (!border)
        currI--;
    else
        currJ--;
}
int indexMaxAbsSum = 0;
for (k = 0; k < diagonalAmount; k++)
    if (sumOfAbsDiag[indexMaxAbsSum] < sumOfAbsDiag[k])
        indexMaxAbsSum = k;
Console.WriteLine($"Maximal value diagonal sum of parallel to minor diagonal: {sumOfAbsDiag[indexMaxAbsSum]}, have diagonal with number: {indexMaxAbsSum}");
/*10*/
bool neg;
int sumCollsWthZero = 0;
for (j = 0; j < colls; j++)
{
    neg = false;
    sumCollsWthZero = 0;
    for (i = 0; i < rows; i++)
    {
        sumCollsWthZero += arr[i, j];
        if (arr[i, j] < 0)
            neg = true;
    }
    if (neg)
        Console.WriteLine($"Sum of coll {j} with neg number = {sumCollsWthZero}");
}
/*11*/
Console.WriteLine("Transposed matrix:");
int[,] transpMartix = new int[colls, rows];
for (i = 0; i < rows; i++)
{
    for (j = 0; j < colls; j++)
    {
        transpMartix[j, i] = arr[i, j];
    }
}
for (i = 0; i < colls; i++)
{
    for (j = 0; j < rows; j++)
    {
        Console.Write($"{transpMartix[i, j],7}");
    }
    Console.WriteLine();
}