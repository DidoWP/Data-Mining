using System;

namespace NQueens
{
    public class QueensBoard
    {
        public int Size { get; set; }
        public int[] Board { get; set; }
        public int[] QueensPerRow { get; set; }
        public int[] QueensPerDOne { get; set; }
        public int[] QueensPerDTwo { get; set; }
        public int[] Conflicts { get; set; }

        public QueensBoard(int size, int[] board)
        {
            this.Size = size;
            this.Board = board;
            this.QueensPerRow = new int[size];
            this.QueensPerDOne = new int[size*2-1];
            this.QueensPerDTwo = new int[size*2-1];
            this.Conflicts = new int[size];
        }
        
        public void PrintBoard()
        {
            for(int i=0; i<this.Size; i++)
            {
                for(int j=0; j<this.Size; j++)
                {
                    if(this.Board[j] != i)
                        System.Console.Write("_ ");
                    else
                        System.Console.Write("* ");
                }
                System.Console.WriteLine();
            }
        }
    }
} 
