namespace OptimalBinarySearchTree;

using System;
using System.Collections.Generic;
using System.IO;

public class Parser
{
    public static (List<Tuple<int, string>>, int) ParseFile(string filePath)
    {
        List<Tuple<int, string>> list = new List<Tuple<int, string>>();
        int totalSum = 0;
    
        foreach (var line in File.ReadAllLines(filePath))
        {
            string[] parts = line.Split(new char[] { ' ' }, 2);
            if (parts.Length == 2 && int.TryParse(parts[0], out int number))
            {
                list.Add(Tuple.Create(number, parts[1].Trim()));
                totalSum += number;
            }
        }
        // Sort the list by the keys on binary values if not there is difference in tree and cost (aprox. 0.011)
        list.Sort((a, b) => String.Compare(a.Item2, b.Item2, StringComparison.Ordinal));

        return (list, totalSum);
    }

}
