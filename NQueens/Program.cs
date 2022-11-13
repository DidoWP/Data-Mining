using System;
using System.Diagnostics;
using NQueens;

class Program
{
    public static void Main()
    {
        var n = 10000;//Int32.Parse(Console.ReadLine());
        var queensPerRow = new int[n];
        var queensPerDOne = new int[2 * n - 1];
        var queensPerDTwo = new int[2 * n - 1];
        var queensConflicts = new int[n];
        var maxConfQueens = new List<int>();
        var StartBoard = GenerateInitialBoard(n, queensPerRow, queensPerDOne, queensPerDTwo, queensConflicts, maxConfQueens);

        // Calac maxConfQueen
        var indexMaxConfQueen = -1;
        if (maxConfQueens.Count > 1)
        {
            Random random = new Random();
            var rand = random.Next(0, maxConfQueens.Count - 1);
            indexMaxConfQueen = maxConfQueens[rand];
        }
        else
            indexMaxConfQueen = maxConfQueens[0];


        // initialize initial board
        var nQueens = new QueensBoard(n, StartBoard, queensPerRow, queensPerDOne, queensPerDTwo, queensConflicts, indexMaxConfQueen);

        // // print initial board
        // nQueens.PrintBoard();
        // // print conflicts
        // foreach (var conf in queensConflicts)
        // {
        //     System.Console.Write(conf + " ");
        // }
        // System.Console.WriteLine();

        var timer = new Stopwatch();
        timer.Start();

        //find result   
        var isResult = true;
        while (isResult)
        {
            var moves = 500;

            while (moves > 0)
            {
                isResult = MoveMaxQueen(nQueens);

                if (!isResult) break;

                //System.Console.WriteLine(moves--);    
            }
            queensPerRow = new int[n];
            queensPerDOne = new int[2 * n - 1];
            queensPerDTwo = new int[2 * n - 1];
            queensConflicts = new int[n];
            maxConfQueens = new List<int>();
            
            var newBoard = GenerateInitialBoard(n, queensPerRow, queensPerDOne, queensPerDTwo, queensConflicts, maxConfQueens);
            nQueens = new QueensBoard(n, newBoard, queensPerRow, queensPerDOne, queensPerDTwo, queensConflicts, indexMaxConfQueen);
        }
        
        timer.Stop();
        var time = (double)timer.ElapsedMilliseconds / 1000;
        System.Console.WriteLine(time);
    }

    public static int[] GenerateInitialBoard(int n, int[] queensPerRow, int[] queensPerDOne, int[] queensPerDTwo, int[] conflicts, List<int> maxConfQueen)
    {
        var board = new int[n];
        Random random = new Random();
        var randNumb = 0;
        for (int i = 0; i < n; i++)
        {
            if (i==0)
            {
                randNumb = random.Next(0, n - 1);
                board[i] = randNumb;
            }
            else
            {
                var min = int.MaxValue;
                var minQueColConfilts = new List<int>();
                for (int j=0; j<n; j++)
                {
                    var currConf = queensPerRow[j] + queensPerDOne[i - j + n - 1] + queensPerDTwo[i + j];
                    if (currConf < min)
                    {
                        min = currConf;
                        minQueColConfilts.Clear();
                        minQueColConfilts.Add(j);
                    }
                    else if (currConf == min)
                        minQueColConfilts.Add(j);
                }
                    
                randNumb = random.Next(0, minQueColConfilts.Count - 1);
                board[i] = minQueColConfilts[randNumb];
            }
            queensPerRow[board[i]]++;
            queensPerDOne[i - board[i] + n - 1]++;
            queensPerDTwo[i + board[i]]++;
        }

        var max = 0;
        for (int i = 0; i < n; i++)
        {
            var currConf = queensPerRow[board[i]] - 1 + queensPerDOne[i - board[i] + n - 1] - 1 + queensPerDTwo[i + board[i]] - 1;
            conflicts[i] = currConf;

            if (currConf > max)
            {
                max = currConf;
                maxConfQueen.Clear();
                maxConfQueen.Add(i);
            }
            else if (currConf == max)
            {
                maxConfQueen.Add(i);
            }
        }

        return board;
    }

    public static bool MoveMaxQueen(QueensBoard nQueens)
    {
        var minQueColConfilts = new List<int>();

        var min = int.MaxValue;
        for (int i = 0; i < nQueens.Size; i++)
        {
            int currConf;
            if (i == nQueens.Board[nQueens.MaxConfQueenIndex])
                currConf = nQueens.Conflicts[nQueens.MaxConfQueenIndex];
            else
                currConf = nQueens.QueensPerRow[i] +
                            nQueens.QueensPerDOne[nQueens.MaxConfQueenIndex - i + nQueens.Size - 1] +
                            nQueens.QueensPerDTwo[i + nQueens.MaxConfQueenIndex];

            if (currConf < min)
            {
                min = currConf;
                minQueColConfilts.Clear();
                minQueColConfilts.Add(i);
            }
            else if (currConf == min)
                minQueColConfilts.Add(i);
        }

        var newQueenPosition = -1;
        if (minQueColConfilts.Count > 1)
        {
            Random random = new Random();
            var rand = random.Next(0, minQueColConfilts.Count - 1);
            newQueenPosition = minQueColConfilts[rand];
        }
        else
        {
            newQueenPosition = minQueColConfilts[0];
        }

        nQueens.QueensPerRow[nQueens.Board[nQueens.MaxConfQueenIndex]]--;
        nQueens.QueensPerDOne[nQueens.MaxConfQueenIndex - nQueens.Board[nQueens.MaxConfQueenIndex] + nQueens.Size - 1]--;
        nQueens.QueensPerDTwo[nQueens.MaxConfQueenIndex + nQueens.Board[nQueens.MaxConfQueenIndex]]--;

        nQueens.Board[nQueens.MaxConfQueenIndex] = newQueenPosition;

        nQueens.QueensPerRow[newQueenPosition]++;
        nQueens.QueensPerDOne[nQueens.MaxConfQueenIndex - newQueenPosition + nQueens.Size - 1]++;
        nQueens.QueensPerDTwo[nQueens.MaxConfQueenIndex + newQueenPosition]++;

        var max = 0;
        var maxConfQueens = new List<int>();
        for (int i = 0; i < nQueens.Size; i++)
        {
            //var currConf = queensPerRow[board[i]] - 1 + queensPerDOne[i - board[i] + n - 1] - 1 + queensPerDTwo[i + board[i]] - 1;
            
            var currConf = nQueens.QueensPerRow[nQueens.Board[i]] - 1 +
                            nQueens.QueensPerDOne[i - nQueens.Board[i] + nQueens.Size - 1] - 1 +
                            nQueens.QueensPerDTwo[i + nQueens.Board[i]] - 1;

            nQueens.Conflicts[i] = currConf;

            if (currConf > max)
            {
                max = currConf;
                maxConfQueens.Clear();
                maxConfQueens.Add(i);
            }
            else if (currConf == max)
            {
                maxConfQueens.Add(i);
            }
        }

        
        // nQueens.PrintBoard();
        // foreach (var conf in nQueens.Conflicts)
        // {
        //     System.Console.Write(conf + " ");
        // }
        // System.Console.WriteLine();

        if (max == 0)
            return false;

        if (maxConfQueens.Count > 1)
        {
            Random random = new Random();
            var rand = random.Next(0, maxConfQueens.Count - 1);
            nQueens.MaxConfQueenIndex = maxConfQueens[rand];
        }
        else
            nQueens.MaxConfQueenIndex = maxConfQueens[0];

        return true;
    }
}


