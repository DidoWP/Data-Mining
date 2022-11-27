using System;

public class Individual
{
    public List<int> Genes { get; set; } = new List<int>();
    public double PathLenght { get; set; } = 0;

    public Individual(List<int> genes, double[,] distances)
    {
        this.Genes = genes;
        this.PathLenght = CalculatePathLenght(distances);
    }

    public double CalculatePathLenght(double[,] distances)
    {
        var result = 0.0;
        for (int i = 0; i < this.Genes.Count - 1; i++)
        {
            result += distances[this.Genes[i], this.Genes[i+1]];
        }
        return result;
    }
}