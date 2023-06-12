using System.Text;
using Newtonsoft.Json;



public class Huffman{
    public class HuffmanNode
    {
        public char Symbol { get; set; }
        public int Frequency { get; set; }
        public HuffmanNode? Left { get; set; }
        public HuffmanNode? Right { get; set; }
    }

    Huffman(){
    }

    string compress(string data, string username){
        var frequencies= GetFrequencies(data);
        var tree= BuildTree(frequencies);
        var compressedString = BuildStringCompressed(tree);
        var dictionary = CreateDictionary(tree);
        StoreDictionary(dictionary, username);
        return compressedString;

    }

    public string decompress(string data, string username){
        // //var dictionary = RetrieveDictionary(username);

        var decompressedData = new StringBuilder();
        // var tempString = "";

        // foreach (char c in data)
        // {
        //     tempString += c;

        //     if (dictionary.ContainsValue(tempString))
        //     {
        //         var matchingKey = dictionary.FirstOrDefault(x => x.Value == tempString).Key;
        //         decompressedData.Append(matchingKey);
        //         tempString = "";
        //     }
        // }

        return decompressedData.ToString();
    }
    public string BuildStringCompressed(HuffmanNode tree){
        return "hello";
    }

    public int[][] GetFrequencies(string data)
    {
        var frequencies = new Dictionary<char, int>();

        foreach (char c in data)
        {
            if (frequencies.ContainsKey(c))
            {
                frequencies[c]++;
            }
            else
            {
                frequencies[c] = 1;
            }
        }

        var result = new int[frequencies.Count][];
        int i = 0;

        foreach (var kvp in frequencies)
        {
            result[i] = new int[] { (int)kvp.Key, kvp.Value };
            i++;
        }

        return result;
    }

    public HuffmanNode BuildTree(int[][] frequencies)
    {
        var queue = new Queue<HuffmanNode>();

        foreach (var freq in frequencies)
        {
            var node = new HuffmanNode
            {
                Symbol = (char)freq[0],
                Frequency = freq[1]
            };

            queue.Enqueue(node);
        }

        while (queue.Count > 1)
        {
            var left = queue.Dequeue();
            var right = queue.Dequeue();

            var parent = new HuffmanNode
            {
                Frequency = left.Frequency + right.Frequency,
                Left = left,
                Right = right
            };

            queue.Enqueue(parent);
        }

        return queue.Dequeue();
    }
    public Dictionary<char, string> CreateDictionary(HuffmanNode tree)
    {
        var dictionary = new Dictionary<char, string>();

        void Traverse(HuffmanNode node, string path)
        {
            if (node.Left == null && node.Right == null)
            {
                dictionary[node.Symbol] = path;
                return;
            }

            if (node.Left != null)
            {
                Traverse(node.Left, path + "0");
            }

            if (node.Right != null)
            {
                Traverse(node.Right, path + "1");
            }
        }

        Traverse(tree, "");

        return dictionary;
    }

    public void StoreDictionary(Dictionary<char, string> dictionary, string username)
    {
        var compressedPasswords = new Dictionary<string, Dictionary<char, string>>();

        if (File.Exists("compressedPasswords.json"))
        {
            var existingData = File.ReadAllText("compressedPasswords.json");
            compressedPasswords = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<char, string>>>(existingData);
        }

        compressedPasswords[username] = dictionary;

        var jsonData = JsonConvert.SerializeObject(compressedPasswords);
        File.WriteAllText("compressedPasswords.json", jsonData);
    }


}