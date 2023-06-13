namespace angular_project.Models;

public class CompressionNodeData{
    private char charValue;
    private string strValue;
    public CompressionNodeData(char charValue, string strValue){
        this.charValue=charValue;
        this.strValue=strValue;
    }
    public char getChar(){
        return this.charValue;
    }
    public string getString(){
        return this.strValue;
    }

    public void setString(string strValue){
        this.strValue=strValue;
    }
    public void setChar(char value){
        this.charValue=value;
    }
}