namespace OptimalBinarySearchTree;

public class Bst
{
    private const int MinimumOccurrences = 50000;
    
    public static (double[] probabilities, double[] q, List<string> filteredKeys) Process(List<Tuple<int, string>> list, int totalSum)
    {
        List<string> filteredKeys = new List<string>(){ null }; // Insert a dummy key at the beginning
        List<double> probabilities = new List<double>(){ 0 }; // Insert a dummy probability at the beginning
        List<double> q = new List<double>();
        
        int sumP = 0;

        foreach (var entry in list)
        {
            int occurrences = entry.Item1;
            string key = entry.Item2;

            // Filter out keys with less than MinimumOccurrences
            if (occurrences > MinimumOccurrences)
            {
                filteredKeys.Add(key); 
                probabilities.Add((double)occurrences / (double)totalSum);
                q.Add((double)sumP / (double)totalSum);
                sumP = 0;
            }
            // Add the occurrences to the sum
            else
            {
                sumP += occurrences;
            }
        }
        // Add the last q value
        if (sumP > 0)
            q.Add((double)sumP / (double)totalSum);
        
        return (probabilities.ToArray(), q.ToArray(), filteredKeys);
    }
    
    // Function to calculate the optimal binary search tree from pseudo code from the book
    public static (double[,] e, int[,] root) OptimalBst(double[] p, double[] q, int n)
    {
        double[,] e = new double[n + 2, n + 1];
        double[,] w = new double[n + 2, n + 1];
        int[,] root = new int[n + 1, n + 1];

        for (int i = 1; i <= n + 1; i++)
        {
            e[i, i - 1] = q[i - 1];
            w[i, i - 1] = q[i - 1];
        }
        
        for (int l = 1; l <= n; l++)
        {
            for (int i = 1; i <= n - l + 1; i++)
            {
                int j = i + l - 1;
                e[i, j] = double.MaxValue;
                w[i, j] = w[i, j - 1] + p[j] + q[j];

                for (int r = i; r <= j; r++)
                {
                    double t = e[i, r - 1] + e[r + 1, j] + w[i, j];
                    if (t < e[i, j])
                    {
                        e[i, j] = t;
                        root[i, j] = r;
                    }
                }
            }
        }
        // Cost of the optimal BST is stored in e[1, n]
        Console.WriteLine($"The cost of the optimal BST is: {e[1, n]}");
        return (e, root);
    }
    
    public static Node BuildTree(int[,] root, List<string> keyWords)
    {
        // Helper function to recursively build the tree
        Node BuildSubTree(int i, int j)
        {
            if (i > j)
            {
                return null; // No node to create, return null for this subtree
            }
            
            int rootIndex = root[i, j];
            Node node = new Node(keyWords[rootIndex]);
            
            // Recursively build left and right subtrees
            node.Left = BuildSubTree(i, rootIndex - 1);
            node.Right = BuildSubTree(rootIndex + 1, j);
            
            return node;
        }
        
        // Start the recursive tree building with the full range of keys
        return BuildSubTree(1, keyWords.Count - 1);
    }
}