namespace angular_project;

public class DictionaryNodeData{
    private char charValue;
    private int intValue;
    public DictionaryNodeData(char charValue, int intValue){
        this.charValue=charValue;
        this.intValue=intValue;
    }
    public char getChar(){
        return this.charValue;
    }
    public int getInt(){
        return this.intValue;
    }

    public void setInt(int value){
        this.intValue=value;
    }
    public void setChar(char value){
        this.charValue=value;
    }
}
