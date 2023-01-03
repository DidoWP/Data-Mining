using System;
using System.Collections;

class Program
{
    public static void Main()
    {
        var dataArray = new string[]{
            "republican,n,y,n,y,y,y,n,n,n,y,?,y,y,y,n,y",
            "republican,n,y,n,y,y,y,n,n,n,n,n,y,y,y,n,?",
            "democrat,?,y,y,?,y,y,n,n,n,n,y,n,y,y,n,n",
            "democrat,n,y,y,n,?,y,n,n,n,n,y,n,y,n,n,y",
            "democrat,y,y,y,n,y,y,n,n,n,n,y,?,y,y,y,y",
            "democrat,n,y,y,n,y,y,n,n,n,n,n,n,y,y,y,y",
            "democrat,n,y,n,y,y,y,n,n,n,n,n,n,?,y,y,y",
            "republican,n,y,n,y,y,y,n,n,n,n,n,n,y,y,?,y",
            "republican,n,y,n,y,y,y,n,n,n,n,n,y,y,y,n,y",
            "democrat,y,y,y,n,n,n,y,y,y,n,n,n,n,n,?,?"
        };

        var republicanCount = 0;
        var democratCount = 0;
        var republicanAttributs = new int[16];
        var democratAttributs = new int[16];

        foreach(var data in dataArray)
        {
            var attributeData = data.Split(",");
            var isRepublican = attributeData[0] == "republican" ? true : false;
            if (isRepublican) republicanCount++;
            else democratCount++;

            for (int i = 1; i < 17; i++)
            {
                if (isRepublican)
                    if (attributeData[i] == "y")
                        republicanAttributs[i-1]++;
                else
                    if (attributeData[i] == "y")
                        democratAttributs[i-1]++;
            }
        }

        System.Console.WriteLine($"Republican Count = {republicanCount}");
        System.Console.WriteLine($"Democrat Count = {democratCount}");

        foreach (var item in republicanAttributs)
        {
            System.Console.Write(item);
            System.Console.Write(" ");
        }
        System.Console.WriteLine();

        foreach (var item in democratAttributs)
        {
            System.Console.Write(item);
            System.Console.Write(" ");
        }
    }
}

