using System.Xml;
using System.IO;
namespace Server;

public class ModifyXml{

    public void CreateXml(string name, string[] attributes){
        int attributesNumber = attributes.Length;
        string path = Path.Combine("../../Documents", name+".xml");

        XmlWriterSettings configuration = new XmlWriterSettings();
        configuration.Indent = true;

        XmlWriter xmlWriter = XmlWriter.Create(path, configuration);
        xmlWriter.WriteStartDocument();
        xmlWriter.WriteStartElement(name);
        xmlWriter.WriteStartElement("object");

        foreach(string attribute in attributes){
            xmlWriter.WriteAttributeString(attribute, "");
        }
        xmlWriter.WriteEndElement();
        xmlWriter.WriteEndElement();
        xmlWriter.WriteEndDocument();
        xmlWriter.Close();
    }
    public void Insert(string name, string[] attributesForChange, string[] Elements, int contAttributes){
        XmlWriterSettings settings = new XmlWriterSettings();
        settings.Indent = true;

        XmlReader xmlReader = XmlReader.Create(name+".xml");
        XmlWriter xmlWriter = XmlWriter.Create("../../Documents/"+name+"1", settings);
        
        xmlWriter.WriteNode(xmlReader,true);
        
        for(int i=0;i<attributesForChange.Length;i++){
            xmlWriter.WriteStartElement("object"){
                
            }
        }

    }

    public void Update(string path, string attributeValue, string attributeToChange, string elementPath){
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(path);

            XmlNode elementNode = xmlDocument.SelectSingleNode(elementPath);

            if (elementNode != null)
            {
                XmlAttribute attribute = elementNode.Attributes[attributeToChange];
                attribute.Value = attributeValue;
                xmlDocument.Save(path);
            }   
    }
    public void DeleteAll(string name){
        XmlWriter xmlWriter = XmlWriter.Create(name+".xml");
        xmlWriter.WriteStartDocument();
        xmlWriter.WriteEndDocument();
        xmlWriter.Close();
    }
    public void DeleteSome(string name){
        XmlWriter xmlWriter = XmlWriter.Create(name+".xml");
        xmlWriter.WriteStartDocument();
        xmlWriter.WriteEndDocument();
        xmlWriter.Close();
    }
    public string[] SelectOneCondition(string[] script, string direction, string condition1){
        string[] Data = new string[0];
        int index = 0;
        XmlReader xmlReader = XmlReader.Create(direction+".xml");

        while(xmlReader.Read()){
            if((xmlReader.NodeType == XmlNodeType.Element) && (xmlReader.Name == "Object") ){
                if(xmlReader.HasAttributes){
                    for(int i = 0; i < xmlReader.AttributeCount; i++){
                        xmlReader.MoveToAttribute(i);
                        if(xmlReader.Value==condition1){
                            string attrName = xmlReader.Name;
                            string attrValue = xmlReader.Value;
                            string attr = attrName+":"+attrValue;
                            Array.Resize(ref Data, index +1);
                            Data[index]=attr;
                            index++;
                        }
                    }
                }
            }
        }  
        return Data;    
    }
    public string[] SelectTwoCondition(string[] script, string direction, string condition1, string condition2){
        string[] Data = new string[0];
        int index = 0;
        XmlReader xmlReader = XmlReader.Create(direction+".xml");

        while(xmlReader.Read()){
            if((xmlReader.NodeType == XmlNodeType.Element) && (xmlReader.Name == "Object") ){
                if(xmlReader.HasAttributes){
                    for(int i = 0; i < xmlReader.AttributeCount; i++){
                        xmlReader.MoveToAttribute(i);
                        if(xmlReader.Value==condition1){
                            for(int j = 0; j < xmlReader.AttributeCount; j++){
                                xmlReader.MoveToAttribute(j);
                                if(xmlReader.Value==condition2){
                                    string attrName = xmlReader.Name;
                                    string attrValue = xmlReader.Value;
                                    string attr = attrName+":"+attrValue;
                                    Array.Resize(ref Data, index +1);
                                    Data[index]=attr;
                                    index++;
                                }
                            }
                        }
                    }
                }
            }
        }  
        return Data;    
    }
    public string[] SelectThreeCondition(string[] script, string direction, string condition1, string condition2, string condition3){
        string[] Data = new string[0];
        int index = 0;
        XmlReader xmlReader = XmlReader.Create(direction+".xml");

        while(xmlReader.Read()){
            if((xmlReader.NodeType == XmlNodeType.Element) && (xmlReader.Name == "Object") ){
                if(xmlReader.HasAttributes){
                    for(int i = 0; i < xmlReader.AttributeCount; i++){
                        xmlReader.MoveToAttribute(i);
                        if(xmlReader.Value==condition1){
                            for(int j = 0; j < xmlReader.AttributeCount; j++){
                                xmlReader.MoveToAttribute(j);
                                if(xmlReader.Value==condition2){
                                    for(int k = 0; k < xmlReader.AttributeCount; k++){
                                        xmlReader.MoveToAttribute(k);
                                        if(xmlReader.Value==condition2){
                                            string attrName = xmlReader.Name;
                                            string attrValue = xmlReader.Value;
                                            string attr = attrName+":"+attrValue;
                                            Array.Resize(ref Data, index +1);
                                            Data[index]=attr;
                                            index++;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }  
        return Data;    
    }
}