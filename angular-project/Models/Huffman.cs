using System.Text;
using Newtonsoft.Json;
using angular_project;
using angular_project.Models;

namespace angular_project.Models;

public class Huffman{

    private DictionaryHandmade<DictionaryNodeData> frequencies;
    private HuffmanTreeList<HuffmanNode> tree;

    public Huffman(){
        frequencies=new DictionaryHandmade<DictionaryNodeData>();
        tree=new HuffmanTreeList<HuffmanNode>();
    }

    string compress(string data, string username){
        GetFrequencies(data);
        BuildTree();
        var compressedString = BuildStringCompressed();
        var dictionary = CreateDictionary();
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
    public string BuildStringCompressed(){
        return "hello";
    }

    public void GetFrequencies(string data)
    {
        foreach (char c in data)
        {
            if (frequencies.charInDictionary(c))
            {
                int actualVal=frequencies.GetInChar(c)+1;
                frequencies.setIntInChar(actualVal,c);
            }
            else
            {
                frequencies.Add(c,1);
            }
        }
    }

    private void BuildTree()
{
    HuffmanTreeList<HuffmanNode> nodes = new HuffmanTreeList<HuffmanNode>();

    foreach (var pair in frequencies)
    {
        HuffmanNode node = new HuffmanNode
        {
            Symbol = frecuencies.get.Key,
            Frequency = pair.Value
        };
        nodes.Add(node);
    }

    while (nodes.Count > 1)
    {
        // Sort the nodes based on their frequencies
        nodes.Sort();

        // Create a new parent node with the two nodes having the lowest frequencies as children
        HuffmanNode parent = new HuffmanNode
        {
            Left = nodes[0],
            Right = nodes[1],
            Frequency = nodes[0].Frequency + nodes[1].Frequency
        };

        // Remove the two lowest frequency nodes and add the parent node
        nodes.RemoveLast();
        nodes.RemoveLast();
        nodes.Add(parent);
    }

    // The last remaining node is the root of the Huffman tree
    root = nodes[0];
}

    public Dictionary<char, string>? CreateDictionary()
    {

        
        return null;
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