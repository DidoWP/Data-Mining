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
        public int MaxConfQueenIndex { get; set; }

        public QueensBoard(int size, int[] board, int[] queensPerRow, int[] queensPerDOne, int[] queensPerDTwo, int[] conflicts, int indexMaxConfQueen)
        {
            this.Size = size;
            this.Board = board;
            this.QueensPerRow = queensPerRow;
            this.QueensPerDOne = queensPerDOne;
            this.QueensPerDTwo = queensPerDTwo;
            this.Conflicts = conflicts;
            this.MaxConfQueenIndex = indexMaxConfQueen;
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
