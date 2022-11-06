using System;
using System.Collections;

namespace EightPuzzle
{
    public class Board
    {
        public int[,] MyBoard { get; set; }
        public Dictionary<int,Tuple<int,int>> GoalBoardMap { get; set; }
        public int Size { get; set; }
        public bool IsGoal { get; set; }
        public int ZeroAtX { get; set; }
        public int ZeroAtY { get; set; }
        public int Manhattan { get; set; }
        public Board Parent { get; set; }

        public Board(int[,] board, Dictionary<int,Tuple<int,int>> goal, int size, int zeroAtX, int zeroAtY, Board parent = null)
        {
            this.MyBoard = board;
            this.GoalBoardMap = goal;
            this.Size = size;
            this.ZeroAtX = zeroAtX;
            this.ZeroAtY = zeroAtY;
            this.Parent = parent;
            this.Manhattan = CalculateManhattan();
            this.IsGoal = this.Manhattan == 0;
        }        
        
        public void ToString()
        {
            for(int i=0; i<this.Size; i++)
            {
                for(int j=0; j<this.Size; j++)
                {
                    Console.Write(this.MyBoard[i,j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine(this.Manhattan);
        }

        private int CalculateManhattan()
        {
            var result = 0;
            for(int i=0; i<this.Size; i++)
            {
                for(int j=0; j<this.Size; j++)
                {
                    if (this.MyBoard[i,j] != 0)
                    {
                        var x = this.GoalBoardMap[this.MyBoard[i,j]].Item1;
                        var y = this.GoalBoardMap[this.MyBoard[i,j]].Item2;
                        result += Math.Abs(x - i) + Math.Abs(y - j);    
                    }
                }
            }
            return result;
        }
        private bool IsEqual(int[,] a, int[,] b)
        {       
            for(int i=0; i<this.Size; i++)
            {
                for(int j=0; j<this.Size; j++)
                {
                    if (a[i,j] != b[i,j])
                        return false;
                }
            }
            return true;
        }

        public Board GenerateRightChild()
        {
            if (this.ZeroAtY != 0)
            {
                var childBoard = (int[,])this.MyBoard.Clone();
                var neighbor = childBoard[this.ZeroAtX, this.ZeroAtY-1];
                childBoard[this.ZeroAtX, this.ZeroAtY-1] = 0;
                childBoard[this.ZeroAtX, this.ZeroAtY] = neighbor;
    
                if (this.Parent == null || !this.IsEqual(childBoard, this.Parent.MyBoard))
                    return new Board(childBoard, this.GoalBoardMap, this.Size, this.ZeroAtX, this.ZeroAtY-1, this);
            }
            return null;
        }
        public Board GenerateLeftChild()
        {
            if (this.ZeroAtY != this.Size-1)
            {
                var childBoard = (int[,])this.MyBoard.Clone();
                var neighbor = childBoard[this.ZeroAtX, this.ZeroAtY+1];
                childBoard[this.ZeroAtX, this.ZeroAtY+1] = 0;
                childBoard[this.ZeroAtX, this.ZeroAtY] = neighbor;
    
                if (this.Parent == null || !this.IsEqual(childBoard, this.Parent.MyBoard))
                    return new Board(childBoard, this.GoalBoardMap, this.Size, this.ZeroAtX, this.ZeroAtY+1, this);
            }
            return null;
        }
        public Board GenerateDownChild()
        {
            if (this.ZeroAtX != 0)
            {
                var childBoard = (int[,])this.MyBoard.Clone();
                var neighbor = childBoard[this.ZeroAtX-1, this.ZeroAtY];
                childBoard[this.ZeroAtX-1, this.ZeroAtY] = 0;
                childBoard[this.ZeroAtX, this.ZeroAtY] = neighbor;
                
                if (this.Parent == null || !this.IsEqual(childBoard, this.Parent.MyBoard))
                    return new Board(childBoard, this.GoalBoardMap, this.Size, this.ZeroAtX-1, this.ZeroAtY, this);
            }
            return null;
        }
        public Board GenerateUpChild()
        {
            if (this.ZeroAtX != this.Size-1)
            {
                var childBoard = (int[,])this.MyBoard.Clone();
                var neighbor = childBoard[this.ZeroAtX+1, this.ZeroAtY];
                childBoard[this.ZeroAtX+1, this.ZeroAtY] = 0;
                childBoard[this.ZeroAtX, this.ZeroAtY] = neighbor;
                
                if (this.Parent == null || !this.IsEqual(childBoard, this.Parent.MyBoard))
                    return new Board(childBoard, this.GoalBoardMap, this.Size, this.ZeroAtX+1, this.ZeroAtY, this);
            }
            return null;
        }
    }
}
