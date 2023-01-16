using System;
using System.Collections;

class Program
{
    public static void Main()
    {
        var n = 12;
        var populationSize = n * 200;
        var towns = new List<string>(){ "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L" };
        var positions = new List<KeyValuePair<double, double>>();
        var rand = new Random();

        for (int i = 0; i < n; i++)
        {
            //var x = rand.Next(0, 100);
            //var y = rand.Next(0, 100);
            positions.Add(new KeyValuePair<double, double>(0.190032E-03,-0.285946E-03));
            positions.Add(new KeyValuePair<double, double>(383.458,-0.608756E-03));
            positions.Add(new KeyValuePair<double, double>(-27.0206,-282.758));
            positions.Add(new KeyValuePair<double, double>(335.751,-269.577));
            positions.Add(new KeyValuePair<double, double>(69.4331,-246.780));
            positions.Add(new KeyValuePair<double, double>(168.521,31.4012));
            positions.Add(new KeyValuePair<double, double>(320.350,-160.900));
            positions.Add(new KeyValuePair<double, double>(179.933,-318.031));
            positions.Add(new KeyValuePair<double, double>(492.671,-131.563));
            positions.Add(new KeyValuePair<double, double>(112.198,-110.561));
            positions.Add(new KeyValuePair<double, double>(306.320,-108.090));
            positions.Add(new KeyValuePair<double, double>(217.343,-447.089));
        }

        var distances = new double[12,12];

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
        population.Sort((x,y) => x.Fitness.CompareTo(y.Fitness));
        System.Console.WriteLine($"Generation {0} best: {population[0].Fitness}");

        var bestPath = double.MaxValue;
        var generationCount = 0;
        var generationsWithoutImprovment = 0;
        while(generationsWithoutImprovment < 20)
        {
            CalculateReproductionChance(population);

            var size = populationSize / 2; 

            var selection = SelectForReproduceing(population, size);

            var children = CrossingOver(selection, n, distances);

            population.AddRange(children);
            population.Sort((x,y) => x.Fitness.CompareTo(y.Fitness));
            population = population.Take(populationSize).ToList();

            Mutate(population, n);
            
            if (population[0].Fitness < bestPath)
            {
                bestPath = population[0].Fitness;
                generationsWithoutImprovment = 0;
            }
            else
                generationsWithoutImprovment++;

            generationCount++;
            System.Console.WriteLine($"Generation {generationCount} best: {bestPath}");
        }


        // Prints
        // System.Console.WriteLine("Stronger: ");
        // Print(stronger, populationSize / 2, n);
        
        // System.Console.WriteLine("Weaker: ");
        // Print(weaker, populationSize / 2, n);
    
        // System.Console.WriteLine();
        // foreach (var item in selection)
        // {
        //     for (int i=0; i<n; i++)
        //     {
        //         System.Console.Write(item.Key.Genes[i] + " ");
        //     }
        //     System.Console.Write(item.Key.Fitness + " ");
        //     System.Console.Write(item.Key.ReproductionChance);
        //     System.Console.WriteLine();

        //     for (int i=0; i<n; i++)
        //     {
        //         System.Console.Write(item.Value.Genes[i] + " ");
        //     }
        //     System.Console.Write(item.Value.Fitness + " ");
        //     System.Console.Write(item.Value.ReproductionChance);
        //     System.Console.WriteLine();
        //     System.Console.WriteLine();
        // }
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
            individual.ReproductionChance = Math.Round(totalFitness / individual.Fitness / newTotalFitness * 100, 2);
        }
    }

    private static List<KeyValuePair<Individual, Individual>> SelectForReproduceing(List<Individual> population, int reprocuctionSize)
    {
        var selection = new List<KeyValuePair<Individual, Individual>>();
        var pickingList = new List<int>();
        var range = 0;

        for (int i=0; i<population.Count(); i++)
        {
            var count = (int)(population[i].ReproductionChance * 100);
            range += count;
            while(count > 0)
            {
                pickingList.Add(i);
                count --;
            }
        }

        var countStrong = 0;
        var countWeak = 0;
        var strongLimit = reprocuctionSize * 7 / 10;
        var weakLimit = reprocuctionSize * 3 / 10 ;
        
        while (strongLimit + weakLimit < reprocuctionSize)
            strongLimit++;

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
                if (individualIndexFirst == individualIndexSecond) 
                {}
                else if (individualIndexSecond < population.Count() / 2)
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
                    childOne.Add(geneP);
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
                    childTwo.Add(geneP);
                    indexC++;
                }
                indexP++;
            }
            children.Add(new Individual(childOne, distances));
            children.Add(new Individual(childTwo, distances));
        }
        
        return children;
    }

    private static void Mutate(List<Individual> population, int n)
    {
        var rand = new Random();
        for (int i=0; i<population.Count / 10; i++)
        {
            var toMutateIndex = rand.Next(1,population.Count());
            var x = rand.Next(n);
            var y = rand.Next(n);

            var swp = population[toMutateIndex].Genes[x];
            population[toMutateIndex].Genes[x] = population[toMutateIndex].Genes[y];
            population[toMutateIndex].Genes[y] = swp;
        }
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

