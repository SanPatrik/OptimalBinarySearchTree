
namespace OptimalBinarySearchTree;


class Program
{
    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Please specify the file path as the first argument.");
            return;
        }
        var filePath = args[0];
        
        var (list, totalSum) = Parser.ParseFile(filePath);
        var (probabilities, q, keys) = Bst.Process(list, totalSum);
        var (e,root) = Bst.OptimalBst(probabilities, q, keys.Count-1);
        Node treeRoot = Bst.BuildTree(root, keys);
        treeRoot.PocetPorovnani("day");
        List<List<string>> keysByLevel = treeRoot.GetKeysByLevel();
        for (int i = 0; i < keysByLevel.Count; i++)
        {
            Console.WriteLine($"Level {i + 1}: {string.Join(", ", keysByLevel[i])}");
        }
    }
}
         
