namespace OptimalBinarySearchTree;

public class Node
{
    public string Key { get; set; }
    public Node Left { get; set; }
    public Node Right { get; set; }

    public Node(string key)
    {
        Key = key;
    }

    public void PocetPorovnani(string word, int depth = 1)
    {
        Console.WriteLine($"Comparing with '{this.Key}' at depth {depth}.");

        // Check if the current node's key matches the word
        if (this.Key == word)
        {
            Console.WriteLine($"Word '{word}' found after {depth} comparisons.");
            return;
        }

        // If the word is less than the current node's key, go left
        if (word.CompareTo(this.Key) < 0)
        {
            if (this.Left != null)
            {
                Console.WriteLine($"Word '{word}' is less than '{this.Key}'. Going left.");
                this.Left.PocetPorovnani(word, depth + 1);
            }
            else
            {
                Console.WriteLine($"Word '{word}' not found. Reached a leaf after {depth} comparisons.");
            }
        }
        // If the word is greater than the current node's key, go right
        else
        {
            if (this.Right != null)
            {
                Console.WriteLine($"Word '{word}' is greater than '{this.Key}'. Going right.");
                this.Right.PocetPorovnani(word, depth + 1);
            }
            else
            {
                Console.WriteLine($"Word '{word}' not found. Reached a leaf after {depth} comparisons.");
            }
        }
    }
    

    public List<List<string>> GetKeysByLevel()
    {
        List<List<string>> result = new List<List<string>>();
        Queue<Node> queue = new Queue<Node>();
        queue.Enqueue(this);

        while (queue.Count > 0)
        {
            List<string> level = new List<string>();
            int levelSize = queue.Count;

            for (int i = 0; i < levelSize; i++)
            {
                Node node = queue.Dequeue();
                level.Add(node.Key);

                if (node.Left != null)
                {
                    queue.Enqueue(node.Left);
                }

                if (node.Right != null)
                {
                    queue.Enqueue(node.Right);
                }
            }

            result.Add(level);
        }

        return result;
    }
}

