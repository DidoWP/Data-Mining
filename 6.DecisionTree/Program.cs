using System;
using System.Collections;

class Program
{   
    public static void Main()
    {
        var dataArray = System.IO.File.ReadAllText(@"G:\My Drive\Dido\UNI\Семестър_7\Интелигентни Системи\Data-Mining\6.DecisionTree\data\breast-cancer.DATA").Split('\n');        
        var dataSet = new List<BreastCancerEntry>();

        for(int i = 0; i < dataArray.Length; i++)
        {
            dataSet.Add(new BreastCancerEntry(dataArray[i].Split(",")));
        }

        var refrectionProps = dataSet.First().GetType().GetProperties().Skip(1);
        var props = refrectionProps.Select(x => x.Name).ToList();
        var root = new Node(null, "root", false);

        CreateTree(dataSet, props, root);

        for (int i = 0; i < 10; i++)
        {
            var takeSize = dataSet.Count() / 10;
            var testData = dataSet.Skip(i * takeSize).Take(takeSize);
            var rightGuesses = 0;
            var wrongGuesses = 0;

            foreach (var test in testData)
            {
                var result = "";
                Predict(test, root.Children.First(), false, result);

                if (result == test.EntryClass)
                    rightGuesses++;
                else
                    wrongGuesses++;
            }
            var accurancy = (double)rightGuesses / takeSize * 100;
            System.Console.WriteLine($"Test {i+1}: {accurancy} %");
        }

        System.Console.WriteLine("kur");
    }

    public static void Predict(BreastCancerEntry test, Node node, bool flag, string result)
    {
        var breastCancerEntryValues = new BreastCancerEntryValues();
        if (flag) return;

        if (node.IsLeaf)
        {
            flag = true;
            result = node.Result;
            return;
        }

        var testValue = (string)test.GetType().GetProperty(node.AttributeName).GetValue(test, null);   

        var chiled = node.Children.First(x => x.AttributeName == testValue);
        var goToChiled = chiled.Children.First();
        
        Predict(test, goToChiled, flag, result);       
    }

    public static void CreateTree(List<BreastCancerEntry> dataSet, List<string> props, Node currNode)
    {
        var breastCancerEntryValues = new BreastCancerEntryValues();

        if (props.Count() == 0)
            return;

        var propsGains = GetAttributesGain(dataSet, props);
        var nextBranchName = propsGains.Aggregate((x,y) => x.Value > y.Value ? x : y);

        var nextNode = new Node(currNode, nextBranchName.Key, true);
        currNode.Children.Add(nextNode);

        props.Remove(nextNode.AttributeName);

        var values = (List<string>)breastCancerEntryValues.GetType().GetProperty(nextNode.AttributeName).GetValue(breastCancerEntryValues, null);
        foreach(var value in values)
        {
            var chield = new Node(nextNode, value, false);
            var newDataSet = new List<BreastCancerEntry>();
            var entryClassNo = 0;
            var entryClassYes = 0;

            foreach (var item in dataSet)
            {
                if ((string)item.GetType().GetProperty(nextNode.AttributeName).GetValue(item, null) == value)
                {
                    newDataSet.Add(item);
                    if (item.EntryClass == breastCancerEntryValues.EntryClass.First())
                        entryClassNo++;
                    else
                        entryClassYes++;
                }
            }

            if (dataSet.Count() < 10)
            {
                chield.IsLeaf = true;
                if (newDataSet.Count(x => x.EntryClass == breastCancerEntryValues.EntryClass.First()) >= 5)
                    chield.Result = breastCancerEntryValues.EntryClass.First();
                else
                    chield.Result = breastCancerEntryValues.EntryClass.Last();
                
                nextNode.Children.Add(chield);
                return;
            }
            else if (entryClassNo == 0)
            {
                chield.IsLeaf = true;
                chield.Result = breastCancerEntryValues.EntryClass.First();
                nextNode.Children.Add(chield);
                return;
            }
            else if (entryClassYes == 0)
            {
                chield.IsLeaf = true;
                chield.Result = breastCancerEntryValues.EntryClass.Last();
                nextNode.Children.Add(chield);
                return;
            }

            nextNode.Children.Add(chield);
            CreateTree(newDataSet, props, chield);
        }

        return;
    } 

    private static Dictionary<string, double> GetAttributesGain(List<BreastCancerEntry> dataSet, List<string> props)
    {
        var propsGains = new Dictionary<string, double>();
        var breastCancerEntryValues = new BreastCancerEntryValues();

        var entryClassNo = dataSet.Count(x => x.EntryClass == breastCancerEntryValues.EntryClass.First());
        var entryClassYes = dataSet.Count(x => x.EntryClass == breastCancerEntryValues.EntryClass.Last());

        foreach (var prop in props)
        {
            var propValues = (List<string>)breastCancerEntryValues.GetType().GetProperty(prop).GetValue(breastCancerEntryValues, null);
            var resultsWithPropValues = new Dictionary<string, KeyValuePair<int,int>>();

            foreach (var propValue in propValues)
            {
                var propValueNo = 0;
                var propValueYes = 0;              

                foreach (var item in dataSet)
                {
                    var itemValue = (string)item.GetType().GetProperty(prop).GetValue(item, null);
                    if (itemValue == propValue && item.EntryClass == breastCancerEntryValues.EntryClass.First())
                        propValueNo ++;
                    
                    else if (itemValue == propValue && item.EntryClass == breastCancerEntryValues.EntryClass.Last())
                        propValueYes ++;
                }
               
                resultsWithPropValues.Add(propValue, new KeyValuePair<int, int>(propValueNo, propValueYes));
            }
            
            var propGain = CalculateGainTX(resultsWithPropValues, entryClassNo, entryClassYes);
            propsGains.Add(prop, propGain);
        }

        return propsGains;
    }

    private static double EntropyT(int no, int yes)
    {
        if (no == 0 || yes == 0)
            return 0;

        var pYes = (double)yes / (yes + no);
        var pNo = (double)no / (yes + no);
        var resPYes = pYes != 0 ? pYes * Math.Log2(pYes) : 0;
        var resPNo = pNo != 0 ? pNo * Math.Log2(pNo) : 0; 
        
        return Math.Abs(resPYes + resPNo);
    }
    
    private static double EntropyTX(Dictionary<string, KeyValuePair<int,int>> resultsWithPropValues, int dataSetCount)
    {
        var result = 0.0;
        foreach (var resultForPropValue in resultsWithPropValues)
        {
            var propValueNo = resultForPropValue.Value.Key;
            var propValueYes = resultForPropValue.Value.Value;
            var prob = (double)(propValueNo + propValueYes) / dataSetCount;
            var entropy = EntropyT(propValueNo, propValueYes);
            result += prob * entropy;
        }

        return result;
    }

    private static double CalculateGainTX(Dictionary<string, KeyValuePair<int,int>> resultsWithPropValues, int entryClassNo, int entryClassYes)
        => EntropyT(entryClassNo, entryClassYes) - EntropyTX(resultsWithPropValues, entryClassNo + entryClassYes);
}