using System;
using NQueens; 

class Program
{
    public static void Main()
    {
        var n = Int32.Parse(Console.ReadLine());
        var queensPerRow = new int[n];
        var queensPerDOne = new int[2*n-1];
        var queensPerDTwo = new int[2*n-1];
        var conflicts = new int[n];
        var StartBoard = GenerateInitialBoard(n, queensPerRow, queensPerDOne, queensPerDTwo, conflicts);
        
        var nQueens = new QueensBoard(n, StartBoard);
        
        nQueens.PrintBoard();

        foreach(var conf in conflicts)
        {
            System.Console.Write(conf + " ");
        }
    }

    public static int[] GenerateInitialBoard(int n, int[] queensPerRow, int[] queensPerDOne, int[] queensPerDTwo, int[] conflicts)
    {
        var board = new int[n];
        Random random = new Random();
        var randNumb = 0;
        for(int i=0; i<n; i++)
        {
            randNumb = random.Next(0,n-1);
            board[i] = randNumb;
            queensPerRow[randNumb] ++;
            queensPerDOne[i - randNumb + n - 1] ++;
            queensPerDTwo[i + randNumb] ++; 
        }
        
        for(int i=0; i<n; i++)
        {
            conflicts[i] = queensPerRow[i]-1 + queensPerDOne[i - board[i] + n - 1]-1 + queensPerDTwo[i + board[i]]-1;
        }
        return board;
    }
    
    // private void CalculateConflicts()
    // {
    //     for(int i=0; i<this.Size; i++)
    //     {
    //         QueensPerRow[this.Board[i]] ++;
    //         QueensPerDOne[i - this.Board[i] + this.Size - 1] ++;
    //         QueensPerDTwo[i + this.Board[i]] ++; 
    //     }
    // }
       
}

