using System;
using System.Collections;

class Program
{
    public static void Main()
    {
        var n = 10;
        var towns = new List<string>(){ "A", "B", "C", "D", "E", "F", "G", "H", "J", "K" };
        var positions = new List<KeyValuePair<int, int>>();
        var rand = new Random();

        for (int i = 0; i < n; i++)
        {
            var x = rand.Next(0, 100);
            var y = rand.Next(0, 100);
            positions.Add(new KeyValuePair<int, int>(x, y));
        }

        var distances = new double[10,10];

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                distances[i,j] = Math.Round(Math.Sqrt(Math.Pow(positions[i].Key - positions[j].Key, 2) + Math.Pow(positions[i].Value - positions[j].Value, 2)), 3);
                System.Console.Write(distances[i,j] + " ");
            }
            System.Console.WriteLine();
        }
        System.Console.WriteLine();

        var population = GeneratePolulation(towns, n, n*2, distances);

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                System.Console.Write(population[i].Genes[j] + " ");
            }
            System.Console.Write(population[i].PathLenght);
            System.Console.WriteLine();
        }
    }

    public static List<Individual> GeneratePolulation(List<string> towns, int townsSize, int populationSize, double[,] distances)
    {
        var population = new List<Individual>();
        var rand = new Random();
        while(populationSize > 0)
        {
            var genes = new List<int>();
            for (int i = 0; i < townsSize; i++)
            {
                var curr = rand.Next(townsSize);
                while(genes.Contains(curr))
                {
                    curr = rand.Next(townsSize);
                }
                
                genes.Add(curr);
            }
            population.Add(new Individual(genes, distances));
            populationSize--;
        }

        return population;
    }
}

