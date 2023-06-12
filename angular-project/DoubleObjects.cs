namespace angular_project;

public class DoubleObjects{
    private string key;
    private string data;
    public DoubleObjects(string key, string data){
        this.key=key;
        this.data=data;
    }
    public string getKey(){
        return this.key;
    }
    public string getData(){
        return this.data;
    }
}