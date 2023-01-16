using System;
using System.Collections;

class Program
{
    public static void Main()
    {
        // var dataArray = new string[]{
        //     "republican,n,y,n,y,y,y,n,n,n,y,?,y,y,y,n,y",
        //     "republican,n,y,n,y,y,y,n,n,n,n,n,y,y,y,n,?",
        //     "democrat,?,y,y,?,y,y,n,n,n,n,y,n,y,y,n,n",
        //     "democrat,n,y,y,n,?,y,n,n,n,n,y,n,y,n,n,y",
        //     "democrat,y,y,y,n,y,y,n,n,n,n,y,?,y,y,y,y",
        //     "democrat,n,y,y,n,y,y,n,n,n,n,n,n,y,y,y,y",
        //     "democrat,n,y,n,y,y,y,n,n,n,n,n,n,?,y,y,y",
        //     "republican,n,y,n,y,y,y,n,n,n,n,n,n,y,y,?,y",
        //     "republican,n,y,n,y,y,y,n,n,n,n,n,y,y,y,n,y",
        //     "democrat,y,y,y,n,n,n,y,y,y,n,n,n,n,n,?,?"
        // };
        
        string data = System.IO.File.ReadAllText(@"G:\My Drive\Dido\UNI\Семестър_7\Интелигентни Системи\Data-Mining\5.NaiveBayes\data\house-votes-84.DATA");
        System.Console.WriteLine(data);

        var dataArray = data.Split('\n');
        var testDataCount = dataArray.Count() / 10;
        var accuracies = new List<double>();

        for(int i = 0; i < 10; i++)
        {
            var republicanCount = 0;
            var democratCount = 0;
            var republicanAttributsYes = new int[16];
            var republicanAttributsNo = new int[16];
            var democratAttributsYes = new int[16];
            var democratAttributsNo = new int[16];

            for (int j = 0; j < dataArray.Count(); j++)
            {
                if (true)
                {
                    var line = dataArray[j].Split(",");
                    var isRepublican = line[0] == "republican" ? true : false;
                    if (isRepublican) republicanCount++;
                    else democratCount++;

                    for (int k = 1; k < 17; k++)
                    {
                        if (isRepublican)
                        {
                            if (line[k] == "y")
                                republicanAttributsYes[k - 1]++;
                                
                            else if (line[k] == "n")
                                republicanAttributsNo[k - 1]++;
                        }
                        else
                        {
                            if (line[k] == "y")
                                democratAttributsYes[k - 1]++;
                            
                            else if (line[k] == "n")
                                democratAttributsNo[k - 1]++;
                        }
                    }
                }
            }

            SetEmptyAnswers(dataArray, republicanAttributsYes, republicanAttributsNo, democratAttributsYes, democratAttributsNo);

            var testDataArray = dataArray.Skip(i*testDataCount).Take(testDataCount).ToArray();
            var correctAnswers = 0;
            for (int j = 0; j < testDataCount; j++)
            {
                var testLine = testDataArray[j].Split(",");
                var republicatProbability = 1.0;
                var democratProbability = 1.0;

                for (int k = 1; k < 17; k++)
                {
                    if (testLine[k] == "y")
                    {
                        var repYes = republicanAttributsYes[k - 1] == 0 ? 1 : republicanAttributsYes[k - 1];
                        var demYes = democratAttributsYes[k - 1] == 0 ? 1 : democratAttributsYes[k - 1];

                        republicatProbability *= ((double)repYes / (double)republicanCount);
                        democratProbability *= ((double)demYes / (double)democratCount);
                    }
                    else
                    {
                        var repNo = republicanAttributsNo[k - 1] == 0 ? 1 : republicanAttributsNo[k - 1];
                        var demNo = democratAttributsNo[k - 1] == 0 ? 1 : democratAttributsNo[k - 1];

                        republicatProbability *= ((double)repNo / (double)republicanCount);
                        democratProbability *= ((double)demNo / (double)democratCount);
                    }
                }

                if (republicatProbability > democratProbability && testLine[0] == "republican") 
                    correctAnswers++;

                else if (republicatProbability < democratProbability && testLine[0] == "democrat") 
                    correctAnswers++;
            }
            var currAccuracy = (double)correctAnswers / (double)testDataCount * 100;
            accuracies.Add(currAccuracy);
            System.Console.WriteLine($"{i}. iteration: {currAccuracy}%");
        }
        var average = accuracies.Sum() / 10.0;
        System.Console.WriteLine($"Average accuracy: {average}");
    }

    public static void SetEmptyAnswers(string[] dataArray, int[] republicanAttributsYes, int[] republicanAttributsNo, int[] democratAttributsYes, int[] democratAttributsNo)
    {
        for(int index = 0; index < dataArray.Count(); index++)
        {
            var data = dataArray[index].Split(",");
            if (data[0] == "republican")
            {
                for (int i = 1; i < 17; i++)
                {
                    if (data[i] == "?")
                    {
                        if (republicanAttributsYes[i - 1] > republicanAttributsNo[i - 1])
                        {
                            data[i] = "y";
                            republicanAttributsYes[i - 1]++;
                        }
                        else
                        {
                            data[i] = "n";
                            republicanAttributsNo[i - 1]++;
                        }
                    }
                }
            }
            else
            {
                for (int i = 1; i < 17; i++)
                {
                    if (data[i] == "?")
                    {
                        if (democratAttributsYes[i - 1] > democratAttributsNo[i - 1])
                        {
                            data[i] = "y";
                            democratAttributsYes[i - 1]++;
                        }
                        else
                        {
                            data[i] = "n";
                            democratAttributsNo[i - 1]++;
                        }
                    }
                }
            }
            var dataString = string.Join(",", data);
            dataArray[index] = dataString;
        }
    }
}