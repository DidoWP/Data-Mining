using System;
using System.Collections;

class Program
{
    public static void Main()
    {
        // var dataArray = new string[]{
        //     "no-recurrence-events,30-39,premeno,30-34,0-2,no,3,left,left_low,no",
        //     "no-recurrence-events,40-49,premeno,20-24,0-2,no,2,right,right_up,no",
        //     "no-recurrence-events,40-49,premeno,20-24,0-2,no,2,left,left_low,no",
        //     "no-recurrence-events,60-69,ge40,15-19,0-2,no,2,right,left_up,no",
        //     "no-recurrence-events,40-49,premeno,0-4,0-2,no,2,right,right_low,no",
        //     "no-recurrence-events,60-69,ge40,15-19,0-2,no,2,left,left_low,no",
        //     "no-recurrence-events,50-59,premeno,25-29,0-2,no,2,left,left_low,no",
        //     "no-recurrence-events,60-69,ge40,20-24,0-2,no,1,left,left_low,no",
        //     "no-recurrence-events,40-49,premeno,50-54,0-2,no,2,left,left_low,no",
        //     "no-recurrence-events,40-49,premeno,20-24,0-2,no,2,right,left_up,no",
        //     "no-recurrence-events,40-49,premeno,0-4,0-2,no,3,left,central,no",
        //     "no-recurrence-events,50-59,ge40,25-29,0-2,no,2,left,left_low,no",
        //     "no-recurrence-events,60-69,lt40,10-14,0-2,no,1,left,right_up,no"
        // };

    // GET DATA
        var dataArray = System.IO.File.ReadAllText(@"G:\My Drive\Dido\UNI\Семестър_7\Интелигентни Системи\Data-Mining\6.DecisionTree\data\breast-cancer.DATA").Split('\n');        
        var dataSet = new List<BreastCancerEntry>();

        for(int i = 0; i < dataArray.Length; i++)
        {
            dataSet.Add(new BreastCancerEntry(dataArray[i].Split(",")));
        }

    // CALCULATE GAIN OF ALL PROPERTIES
        var props = dataSet.First().GetType().GetProperties().Skip(1);
        var propsGains = new Dictionary<string, double>();
        var breastCancerEntryValues = new BreastCancerEntryValues();

        var entryClassNo = dataSet.Count(x => x.EntryClass == breastCancerEntryValues.EntryClass.First());
        var entryClassYes = dataSet.Count(x => x.EntryClass == breastCancerEntryValues.EntryClass.Last());

        foreach (var prop in props)
        {
            var propValues = (List<string>)breastCancerEntryValues.GetType().GetProperty(prop.Name).GetValue(breastCancerEntryValues, null);
            var resultsWithPropValues = new Dictionary<string, KeyValuePair<int,int>>();

            foreach (var propValue in propValues)
            {
                // var propValueNo = dataSet
                //     .Count(x => x.GetType().GetProperty(prop.Name).GetValue(x, null) == propValue &&
                //                 x.EntryClass == breastCancerEntryValues.EntryClass.First());
                // var propValueYes = dataSet
                //     .Count(x => x.GetType().GetProperty(prop.Name).GetValue(x, null) == propValue &&
                //                 x.EntryClass == breastCancerEntryValues.EntryClass.Last());

                var propValueNo = 0;
                var propValueYes = 0;              

                if (prop.Name == "Age")
                {
                    propValueNo = dataSet.Count(x => x.Age == propValue && x.EntryClass == breastCancerEntryValues.EntryClass.First());
                    propValueYes = dataSet.Count(x => x.Age == propValue && x.EntryClass == breastCancerEntryValues.EntryClass.Last());
                }
                else if (prop.Name == "Menopause")
                {
                    propValueNo = dataSet.Count(x => x.Menopause == propValue && x.EntryClass == breastCancerEntryValues.EntryClass.First());
                    propValueYes = dataSet.Count(x => x.Menopause == propValue && x.EntryClass == breastCancerEntryValues.EntryClass.Last());
                }
                else if (prop.Name == "ToumorSize")
                {
                    propValueNo = dataSet.Count(x => x.ToumorSize == propValue && x.EntryClass == breastCancerEntryValues.EntryClass.First());
                    propValueYes = dataSet.Count(x => x.ToumorSize == propValue && x.EntryClass == breastCancerEntryValues.EntryClass.Last());
                }
                else if (prop.Name == "InvNodes")
                {
                    propValueNo = dataSet.Count(x => x.InvNodes == propValue && x.EntryClass == breastCancerEntryValues.EntryClass.First());
                    propValueYes = dataSet.Count(x => x.InvNodes == propValue && x.EntryClass == breastCancerEntryValues.EntryClass.Last());
                }
                else if (prop.Name == "NodeCaps")
                {
                    propValueNo = dataSet.Count(x => x.NodeCaps == propValue && x.EntryClass == breastCancerEntryValues.EntryClass.First());
                    propValueYes = dataSet.Count(x => x.NodeCaps == propValue && x.EntryClass == breastCancerEntryValues.EntryClass.Last());
                }
                else if (prop.Name == "DegMalig")
                {
                    propValueNo = dataSet.Count(x => x.DegMalig == propValue && x.EntryClass == breastCancerEntryValues.EntryClass.First());
                    propValueYes = dataSet.Count(x => x.DegMalig == propValue && x.EntryClass == breastCancerEntryValues.EntryClass.Last());
                }
                else if (prop.Name == "Breast")
                {
                    propValueNo = dataSet.Count(x => x.Breast == propValue && x.EntryClass == breastCancerEntryValues.EntryClass.First());
                    propValueYes = dataSet.Count(x => x.Breast == propValue && x.EntryClass == breastCancerEntryValues.EntryClass.Last());
                }
                else if (prop.Name == "BreastQuad")
                {
                    propValueNo = dataSet.Count(x => x.BreastQuad == propValue && x.EntryClass == breastCancerEntryValues.EntryClass.First());
                    propValueYes = dataSet.Count(x => x.BreastQuad == propValue && x.EntryClass == breastCancerEntryValues.EntryClass.Last());
                }
                else if (prop.Name == "Irradiat")
                {
                    propValueNo = dataSet.Count(x => x.Irradiat == propValue && x.EntryClass == breastCancerEntryValues.EntryClass.First());
                    propValueYes = dataSet.Count(x => x.Irradiat == propValue && x.EntryClass == breastCancerEntryValues.EntryClass.Last());
                }

                resultsWithPropValues.Add(propValue, new KeyValuePair<int, int>(propValueNo, propValueYes));
            }
            
            var propGain = CalculateGainTX(resultsWithPropValues, entryClassNo, entryClassYes);
            propsGains.Add(prop.Name, propGain);
        }
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