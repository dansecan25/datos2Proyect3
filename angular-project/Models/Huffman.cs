using System.Text;
using Newtonsoft.Json;
using angular_project;
using angular_project.Models;

namespace angular_project.Models;

public class Huffman{

    private DictionaryHandmade<DictionaryNodeData> frequencies;
    private HuffmanTreeList<HuffmanNode> tree;
    private HuffmanListNode<HuffmanNode>? root;

     private CompressionDictionary<CompressionNodeData> dictionary;

    public Huffman(){
        frequencies=new DictionaryHandmade<DictionaryNodeData>();
        tree=new HuffmanTreeList<HuffmanNode>();
        dictionary=new CompressionDictionary<CompressionNodeData>();
    }

    public string compress(string data, string username){
        GetFrequencies(data);
        BuildTree();
        CreateDictionary();
        var compressedString = BuildStringCompressed(data);
        //StoreDictionary(username);

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
    public string BuildStringCompressed(string code){
        Console.WriteLine("Lenght of dict: "+dictionary.getCount());
        string compressedString="";
        for(int i=0;i<code.Length;i++){
            char actual=code[i];
            compressedString+=dictionary.GetInChar(actual);
        }
        return compressedString;
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

    for(int i=0;i<frequencies.getCount();i++){
         HuffmanNode node = new HuffmanNode
        {
            Symbol = frequencies.getCharFromPos(i),
            Frequency = frequencies.getIntFromPos(i)
        };
        nodes.Add(node);
    }

    while (nodes.GetCount() > 1)
    {
        // Sort the nodes based on their frequencies
        nodes.Sort();

        // Create a new parent node with the two nodes having the lowest frequencies as children
        HuffmanNode parent = new HuffmanNode
        {
            Left = nodes.GetNodeAtPosition(0).Data,
            Right = nodes.GetNodeAtPosition(1).Data,
            Frequency = nodes.GetNodeAtPosition(0).Data.Frequency + nodes.GetNodeAtPosition(1).Data.Frequency
        };

        // Remove the two lowest frequency nodes and add the parent node
        nodes.RemoveLast();
        nodes.RemoveLast();
        nodes.Add(parent);
    }

    // The last remaining node is the root of the Huffman tree
    tree = nodes;
    root=nodes.GetNodeAtPosition(0);

}

    private void CreateDictionary()
    {
        var dictionary = new Dictionary<char, string>();
        TraverseTree(root.Data, "");
    }

    private void TraverseTree(HuffmanNode node, string code)
    {
        if (node.Left == null && node.Right == null)
        {
            dictionary.setStringInChar(code,node.Symbol);
            return;
        }

        TraverseTree(node.Left, code + "0");
        TraverseTree(node.Right, code + "1");
    }

    public void StoreDictionary(string username)
    {
        var compressedPasswords = new Dictionary<string, Dictionary<char, string>>();

        if (File.Exists("compressedPasswords.json"))
        {
            var existingData = File.ReadAllText("compressedPasswords.json");
            compressedPasswords = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<char, string>>>(existingData);
        }

        //compressedPasswords[username] = dictionary;

        var jsonData = JsonConvert.SerializeObject(compressedPasswords);
        File.WriteAllText("compressedPasswords.json", jsonData);
    }


}