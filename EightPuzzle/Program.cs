using System;
using System.Collections;
using System.Diagnostics;
using EightPuzzle;

class Program
{
    public static void Main()
    {
        var n = Int32.Parse(Console.ReadLine());
        var arrSize = (int)Math.Sqrt(n + 1);
        var zeroFinalPosition = Int32.Parse(Console.ReadLine());
        var startBoard = new int[arrSize,arrSize]; //int[4,4]{{5,6,3,4},{8,0,1,15},{10,7,2,11},{12,9,14,13}};
        var zeroPositionI = 1;
        var zeroPositionJ = 1;

        for(int i=0; i<arrSize; i++)
        {
            var line = Console.ReadLine().Split(" "); 
            for(int j=0; j<arrSize; j++)
            {
                startBoard[i,j] = Int32.Parse(line[j]);
                if (startBoard[i,j] == 0)
                {
                    zeroPositionI = i;
                    zeroPositionJ = j;
                }
            }
        }

        var goalBoardMap = new Dictionary<int, Tuple<int,int>>();
        var numbers = 1;
        for(int i=0; i<arrSize; i++)
        {
            for(int j=0; j<arrSize; j++)
            {
                if (numbers != zeroFinalPosition && numbers != arrSize*arrSize)
                {
                    goalBoardMap.Add(numbers, new Tuple<int, int>(i,j));
                    numbers++;
                }
            }
        }

        var timer = new Stopwatch();
        timer.Start();

        var board = new Board(startBoard, goalBoardMap, arrSize, zeroPositionI, zeroPositionJ);

        var threshold = board.Manhattan;
        var result = new Stack<string>();
        var flag = false;
        while (!flag)
        {
            var newThresholdsList = new List<int>();

            flag = Play(board, 0, threshold, newThresholdsList, result);
            
            threshold = newThresholdsList.Min();
        }

        while(result.Count > 0)
        {
            System.Console.WriteLine(result.Pop());
        }

        timer.Stop();
        var time = (double)timer.ElapsedMilliseconds / 1000;
        System.Console.WriteLine(time);
    }

    private static bool Play(Board board, int currPrice, int threshold, List<int> newThresholdsList, Stack<string> result)
    {
        if (board.IsGoal)
        {   
            Console.WriteLine(currPrice);    
            return true;
        }

        if (currPrice + board.Manhattan > threshold)
        {
            newThresholdsList.Add(currPrice+board.Manhattan);
            return false;
        }


        var child = board.GenerateRightChild(); 
        if (child != null)
        {
            if (Play(child, currPrice + 1, threshold, newThresholdsList, result))
            {
                result.Push("right");
                return true;
            }    
                
        }
        
        child = board.GenerateLeftChild(); 
        if (child != null)
        {
            if (Play(child, currPrice + 1, threshold, newThresholdsList, result))
            {
                result.Push("left");
                return true;
            }    
        }
        
        child = board.GenerateUpChild();
        if (child != null)
        {
            if (Play(child, currPrice + 1, threshold, newThresholdsList, result))
            {
                result.Push("up");
                return true;
            }    
        }

        child = board.GenerateDownChild(); 
        if (child != null)
        {
            if (Play(child, currPrice + 1, threshold, newThresholdsList, result))
            {
                result.Push("down");
                return true;
            }    
        }

        return false;
    }
}