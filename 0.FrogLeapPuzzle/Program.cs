
using System.Collections;

public class Program
{
    public static void Main()
    {
        var input = Int32.Parse(Console.ReadLine());
        var board = new char[2 * input + 1];
        var final = new char[2 * input + 1];

        SetBoard(input, board, final);

        var path = new Stack<char[]>();
        path.Push(board);

        Play(final, path);

        var revPath = new Stack<char[]>();
        var mirrorPath = new Stack<char[]>();
        while(path.Count > 0)
        {
            var curr = path.Pop();
            revPath.Push(curr);

            var currLength = curr.Length;
            var mirCurr = new char[currLength];
            for(int i=0; i<currLength; i++)
            {
                if (curr[i] == '>')
                    mirCurr[currLength-i-1] = '<';
                else if (curr[i] == '<')
                    mirCurr[currLength-i-1] = '>';
                else
                    mirCurr[currLength-i-1] = '_';
            }

            mirrorPath.Push(mirCurr);
        }

        while(revPath.Count > 0)
        {
            Console.Write(revPath.Pop());
            Console.Write("  ");
            Console.Write(mirrorPath.Pop());
            Console.WriteLine();
        }
    }

    private static void SetBoard(int size, char[] board, char[] result)
    {
        for (int i = 0; i <= size * 2; i++)
        {
            if (i < size)
            {
                board[i] = '>';
                result[i] = '<';
            }
            else if (i > size)
            {
                board[i] = '<';
                result[i] = '>';
            }
            else
            {
                board[i] = '_';
                result[i] = '_';
            }
        }
    }

    private static bool Play(char[] final, Stack<char[]> path)
    {
        var currState = path.Peek();
        if (Compare(currState, final))
            return true;
        
        var size = currState.Length;
        var i = 0;
        foreach (var frong in currState)
        {
            if (frong == '>')
            {
                if (TryMoveRight(i, path, size))
                {
                    if (Play(final, path))
                        return true;
                }
            }
            else if (frong == '<')
            {
                if (TryMoveLeft(i, path, size))
                {
                    if (Play(final, path))
                        return true;
                }
            }
        
            i++;
        }

        path.Pop();
        return false;
    }

    private static bool Compare(char[] a, char[] b)
    {
        for(int i=0; i<a.Length; i++)
        {
            if(a[i] != b[i])
                return false;
        }
        
        return true;
    }

    private static bool TryMoveLeft(int fromPosition, Stack<char[]> path, int size)
    {
        var currState = path.Peek();
        if (fromPosition - 1 >= 0 && currState[fromPosition - 1] == '_')
        {
            var newState = new char[size];
            for(int i=0; i<size; i++)
            {
                newState[i] = currState[i];
            }

            newState[fromPosition] = '_';
            newState[fromPosition - 1] = '<';
            path.Push(newState);
            return true;
        }
        else if (fromPosition - 2 >= 0 && currState[fromPosition - 2] == '_')
        {
        	var newState = new char[size];
            for(int i=0; i<size; i++)
            {
                newState[i] = currState[i];
            }

            newState[fromPosition] = '_';
            newState[fromPosition - 2] = '<';
            path.Push(newState);
            return true;
        }

        return false;
    }

    private static bool TryMoveRight(int fromPosition, Stack<char[]> path, int size)
    {
        var currState = path.Peek();
        if (fromPosition + 1 < size && currState[fromPosition + 1] == '_')
        {
            var newState = new char[size];
            for(int i=0; i<size; i++)
            {
                newState[i] = currState[i];
            }

            newState[fromPosition] = '_';
            newState[fromPosition + 1] = '>';
            path.Push(newState);
            return true;
        }
        else if (fromPosition + 2 < size && currState[fromPosition + 2] == '_')
        {
        	var newState = new char[size];
            for(int i=0; i<size; i++)
            {
                newState[i] = currState[i];
            }

            newState[fromPosition] = '_';
            newState[fromPosition + 2] = '>';
            path.Push(newState);
            return true;
        }

        return false;
    }
}