using System;

public class BreastCancerEntry 
{
    public string EntryClass { get; set; }
    public string Age { get; set; }
    public string Menopause { get; set; }
    public string ToumorSize { get; set; }
    public string InvNodes { get; set; }
    public string NodeCaps { get; set; }
    public string DegMalig { get; set; }
    public string Breast { get; set; }
    public string BreastQuad { get; set; }
    public string Irradiat { get; set; }    

    public BreastCancerEntry(string[] entry)
    {
        EntryClass = entry[0];
        Age = entry[1];
        Menopause = entry[2];
        ToumorSize = entry[3];
        InvNodes = entry[4];
        NodeCaps = entry[5];
        DegMalig = entry[6];
        Breast = entry[7];
        BreastQuad = entry[8];
        Irradiat = entry[9];
    }
}

public class BreastCancerEntryValues
{
    public List<string> EntryClass { get; set; } = new List<string>(){"no-recurrence-events", "recurrence-events"};
    public List<string> Age { get; set; } = new List<string>(){"10-19", "20-29", "30-39", "40-49", "50-59", "60-69", "70-79", "80-89", "90-99"};
    public List<string> Menopause { get; set; } = new List<string>(){"lt40", "ge40", "premeno"};
    public List<string> ToumorSize { get; set; } = new List<string>(){"0-4", "5-9", "10-14", "15-19", "20-24", "25-29", "30-34", "35-39", "40-44", "45-49", "50-54", "55-59"};
    public List<string> InvNodes { get; set; } = new List<string>(){"0-2","3-5", "6-8", "9-11", "12-14", "15-17", "18-20", "21-23", "24-26","27-29", "30-32", "33-35", "36-39"};
    public List<string> NodeCaps { get; set; } = new List<string>(){"yes","no"};
    public List<string> DegMalig { get; set; } = new List<string>(){"1","2","3"};
    public List<string> Breast { get; set; } = new List<string>(){"left","right"};
    public List<string> BreastQuad { get; set; } = new List<string>(){"left_up","left_low","right_up","right_low","central"};
    public List<string> Irradiat { get; set; } = new List<string>(){"yes","no"};
    public Dictionary<string,int> AttributeIndexes { get; set; } = new Dictionary<string, int>()
    {
        {"EntryClass", 0},
        {"Age", 1},
        {"Menopause", 2},
        {"InvNodes", 3},
        {"NodeCaps", 4},
        {"DegMalig", 5},
        {"Breast", 6},
        {"BreastQuad", 7},
        {"Irradiat", 8}
    };
}