using System.Text;
using angular_project;
using angular_project.Models;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Nodes;

namespace angular_project.Models;
public class Huffman{

    private DictionaryHandmade<DictionaryNodeData> frequencies;
    private HuffmanTreeList<HuffmanNode> tree;

    private CompressionDictionary<CompressionNodeData> dictionary;

    public Huffman(){
        frequencies=new DictionaryHandmade<DictionaryNodeData>();
        tree=new HuffmanTreeList<HuffmanNode>();
        dictionary=new CompressionDictionary<CompressionNodeData>();
    }

    public string compress(string data, string userID){
        GetFrequencies(data); //works
        BuildTree(); //works
        var compressedString = BuildStringCompressed(data);
        StoreDictionary(userID);
        return compressedString;

    }

    public string decompress(string data, string userID){
        RetrieveDictionary(userID);

        string decompressedData = "";
        string tempString = "";
        foreach(char c in data){//each c is a 1 or 0
            tempString+=c;
            if(dictionary.StringInDictionary(tempString)){
                decompressedData+=dictionary.GetInString(tempString);
                tempString="";
            }

        }
        return decompressedData;
    }
    public void RetrieveDictionary(string userID){
        string dictionaryValue = string.Empty;

        if (File.Exists("compressedPasswords.json"))
        {
            string jsonData = File.ReadAllText("compressedPasswords.json");

            // Parse the JSON data
            using (JsonDocument document = JsonDocument.Parse(jsonData))
            {
                // Get the "users" array
                JsonElement usersElement = document.RootElement.GetProperty("users");
                
                foreach (JsonElement userElement in usersElement.EnumerateArray())
                {
                    // Get the user object
                    string id = userElement.GetProperty("id").GetString();
                    if (id == userID)
                    {
                        // Retrieve the dictionary value
                        dictionaryValue = userElement.GetProperty("dictionary").GetString();
                        break;
                    }
                }
            }
        }
        string tempString="";
        char currentChar='a';
        bool found=true;
        foreach(char c in dictionaryValue){
            if(found==true){
                currentChar=c;
                found=false;
            }
            if(c=='='&&found==false){
                break;
            }else if(c!=';'&&(c=='1'||c=='0')&&found==false){
                tempString+=c;
            }else if(c==';'&&found==false){
                dictionary.Add(currentChar,tempString);
                currentChar='a';
                tempString="";
                found=true;
            }
        }
    }
    public string BuildStringCompressed(string code){
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
            Left = nodes.GetNodeAtPosition(nodes.GetCount()-1).Data,
            Right = nodes.GetNodeAtPosition(nodes.GetCount()-2).Data,
            Frequency = nodes.GetNodeAtPosition(nodes.GetCount()-1).Data.Frequency + nodes.GetNodeAtPosition(nodes.GetCount()-2).Data.Frequency
        };

        // Remove the two lowest frequency nodes and add the parent node
        nodes.RemoveAt(nodes.GetCount()-1);
        nodes.RemoveAt(nodes.GetCount()-1);
        nodes.Add(parent);
        
    }

    // The last remaining node is the root of the Huffman tree
    HuffmanNode root=nodes.GetNodeAtPosition(0).Data;
    CreateDictionary(root);
    

}

    private void CreateDictionary(HuffmanNode rootBase)
    {
        HuffmanTreeList<HuffmanNode> dictNodes = new HuffmanTreeList<HuffmanNode>();
        for(int i=0;i<frequencies.getCount();i++){
         HuffmanNode dictNode = new HuffmanNode
        {
            Symbol = frequencies.getCharFromPos(i),
            Frequency = frequencies.getIntFromPos(i)
        };
        dictNodes.Add(dictNode);
    }
        dictNodes.Sort();

        for(int i=0;i<dictNodes.GetCount();i++){
            char actualChar = dictNodes.GetNodeAtPosition(i).Data.Symbol;
            dictionary.Add(actualChar,"");
            //search the tree and add the 
            TraverseTree(actualChar, rootBase);
        }
    }

    private void TraverseTree(char charToSearch,HuffmanNode rootBase)
    {   
        var tempRoot=rootBase;
        
        while(true){
            
            if(tempRoot.Right.Symbol==charToSearch){
                dictionary.setStringInChar(dictionary.GetInChar(charToSearch)+"0",charToSearch);
                //Console.WriteLine("Root check dict: "+dictionary.GetInChar(charToSearch)+"  "+charToSearch.ToString());
                break;
            }
            else if(tempRoot.Left.Symbol!=null && tempRoot.Left.Symbol==charToSearch){
                dictionary.setStringInChar(dictionary.GetInChar(charToSearch)+"1",charToSearch);
                break;
            }
            else{
                dictionary.setStringInChar(dictionary.GetInChar(charToSearch)+"1",charToSearch);
                tempRoot=tempRoot.Left;
            }
        }
        
    }


public void StoreDictionary(string userID)
    {
        string dictionaryAsString = "";
        for (int i = 0; i < dictionary.getCount(); i++)
        {
            char valueChar = dictionary.getCharFromPos(i);
            string digitsInt = dictionary.GetInChar(valueChar);
            string tempString = $"{valueChar}:{digitsInt};";
            dictionaryAsString += tempString;
        }
        dictionaryAsString+="=";
        var json = new
        {
            id = userID,
            dictionary = dictionaryAsString
        };
        string jsonData = JsonSerializer.Serialize(json);
        if (File.Exists("compressedPasswords.json"))
        {
            string existingData = File.ReadAllText("compressedPasswords.json");

        // Parse the existing JSON data
        using (JsonDocument document = JsonDocument.Parse(existingData))
        {
            // Create a new array containing the existing users and the new user
            JsonArray usersArray = new JsonArray();
            JsonElement usersElement = document.RootElement.GetProperty("users");
            foreach (JsonElement userElement in usersElement.EnumerateArray())
            {
                usersArray.Add(userElement);
            }
            usersArray.Add(json);

            // Create a new object with the updated users array
            JsonObject updatedJson = new JsonObject();
            updatedJson["users"] = usersArray;

            // Serialize the updated JSON data
            jsonData = updatedJson.ToString();
        }

        }

        File.WriteAllText("compressedPasswords.json", jsonData);
    }



}