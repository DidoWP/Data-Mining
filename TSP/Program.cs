using System;
using System.Collections;

class Program
{
    public static void Main()
    {
        var n = 10;
        var populationSize = n * 2;
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

        var population = GeneratePolulation(towns, n, populationSize, distances);
        
        //var generationsWithoutImprovment = 0;
        //while(generationsWithoutImprovment < 5)
        //{
            population.Sort((x,y) => x.Fitness.CompareTo(y.Fitness));

            CalculateReproductionChance(population);

            var size = populationSize / 2; 
            var stronger = population.Take(size).ToList();
            var weaker = population.TakeLast(size).ToList();

            var selection = SelectForReproduceing(population, size);

            var children = CrossingOver(selection, n, distances);

        //}


        // Prints
        System.Console.WriteLine("Stronger: ");
        Print(stronger, populationSize / 2, n);
        
        System.Console.WriteLine("Weaker: ");
        Print(weaker, populationSize / 2, n);
    
        System.Console.WriteLine();
        foreach (var item in selection)
        {
            for (int i=0; i<n; i++)
            {
                System.Console.Write(item.Key.Genes[i] + " ");
            }
            System.Console.Write(item.Key.Fitness + " ");
            System.Console.Write(item.Key.ReproductionChance);
            System.Console.WriteLine();

            for (int i=0; i<n; i++)
            {
                System.Console.Write(item.Value.Genes[i] + " ");
            }
            System.Console.Write(item.Value.Fitness + " ");
            System.Console.Write(item.Value.ReproductionChance);
            System.Console.WriteLine();
            System.Console.WriteLine();
        }
    }

    private static List<Individual> GeneratePolulation(List<string> towns, int townsSize, int populationSize, double[,] distances)
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

    private static void CalculateReproductionChance(List<Individual> population)
    {
        var totalFitness = population.Select(x => x.Fitness).Sum();
        var newTotalFitness = population.Select(x => totalFitness / x.Fitness).Sum();

        foreach(var individual in population)
        {
            individual.ReproductionChance = Math.Round(totalFitness / individual.Fitness / newTotalFitness * 100, 1);
        }
    }

    private static List<KeyValuePair<Individual, Individual>> SelectForReproduceing(List<Individual> population, int reprocuctionSize)
    {
        var selection = new List<KeyValuePair<Individual, Individual>>();
        var pickingList = new List<int>();
        var range = 0;

        for (int i=0; i<population.Count(); i++)
        {
            var count = (int)population[i].ReproductionChance * 10;
            range += count;
            while(count > 0)
            {
                pickingList.Add(i);
                count --;
            }
        }

        var countStrong = 0;
        var countWeak = 0;
        var strongLimit = reprocuctionSize / 10 * 7;
        var weakLimit = reprocuctionSize / 10 * 3;
        while(countStrong + countWeak < reprocuctionSize)
        {
            var rand = new Random(); 
            var individualIndexFirst = pickingList[rand.Next(range)];
            while (true)
            {
                if (individualIndexFirst < population.Count() / 2)
                {
                    if (countStrong < strongLimit)
                    {
                        countStrong++;
                        break;
                    }
                }
                else
                {
                    if (countWeak < weakLimit)
                    { 
                        countWeak++;
                        break;
                    }
                }
                individualIndexFirst = pickingList[rand.Next(range)];
            }

            var individualIndexSecond = pickingList[rand.Next(range)];
            while (true)
            {
                if (individualIndexSecond < population.Count() / 2)
                {
                    if (countStrong < strongLimit)
                    {
                        countStrong++;
                        break;
                    }
                }
                else
                {
                    if (countWeak < weakLimit)
                    { 
                        countWeak++;
                        break;
                    }
                }
                individualIndexSecond = pickingList[rand.Next(range)];
            }

            selection.Add(new KeyValuePair<Individual, Individual>(population[individualIndexFirst], population[individualIndexSecond]));
        }

        return selection;
    }

    private static List<Individual> CrossingOver(List<KeyValuePair<Individual, Individual>> selection, int n, double[,] distances)
    {
        var children = new List<Individual>();
        var rand = new Random();

        for (int i=0; i<selection.Count(); i++)
        {
            var x = rand.Next(n);
            var childOne = new List<int>();
            var childTwo = new List<int>();
            
            for (int j = 0; j <= x; j++)
            {
                childTwo.Add(selection[i].Key.Genes[j]);
                childOne.Add(selection[i].Value.Genes[j]);
            }
            
            var indexP = 0;
            var indexC = x + 1;
            while(indexC < n)
            {
                var geneP = selection[i].Key.Genes[indexP];

                if (!childOne.Contains(geneP))
                {
                    childOne[indexC] = geneP;
                    indexC++;
                }
                indexP++;
            }

            indexP = 0;
            indexC = x + 1;
            while(indexC < n)
            {
                var geneP = selection[i].Value.Genes[indexP];

                if (!childTwo.Contains(geneP))
                {
                    childTwo[indexC] = geneP;
                    indexC++;
                }
                indexP++;
            }
            children.Add(new Individual(childOne, distances));
            children.Add(new Individual(childTwo, distances));
        }
        
        return children;
    }

    private static void Print(List<Individual> population, int populationSize, int n)
    {
        for (int i = 0; i < populationSize; i++)
        {
            for (int j = 0; j < n; j++)
            {
                System.Console.Write(population[i].Genes[j] + " ");
            }
            System.Console.Write(population[i].Fitness + " ");
            System.Console.Write(population[i].ReproductionChance);
            System.Console.WriteLine();
        }
    }
}

