
namespace Server;
public class ClassifyScript{
    public int NumberOfAttributes { get; private set; }

    ModifyXml docXml = new ModifyXml();
    public void Operator(string script){
        string[] operations = script.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
        int size = operations.Length;
        int index = 0; 
        for(int i=0; i<size;i++){
            if(operations[i]=="CREATE"){
                string[] attributes = new string[0];
                for(int j=4;j<size;j++){
                    if(j%2==0){
                    Array.Resize(ref attributes,index +1);
                    attributes[index] = operations[j];
                    index++;
                    }
                }
                docXml.CreateXml(operations[3], attributes);
            }
            if(operations[i]=="INSERT"){
                string[] attributesForChange = new string[0];
                string[] elements = new string[0];
                int contAttributes=0;
                

                for(int a=4;a<size;a++){
                    if(operations[a]!=")"){
                        Array.Resize(ref attributesForChange, index + 1);
                        attributesForChange[index] = operations[a];
                        contAttributes++;
                        index ++;
                    }
                }
                index = 0;
                int initial=7+contAttributes;

                for(int a=initial;a<size;a++){
                    if(operations[a]!=")" && operations[a]!="," && operations[a]!="),"){
                        Array.Resize(ref elements, index + 1);
                        elements[index] = elements[a];
                        index ++;
                    }
                }
                docXml.Insert(operations[2], attributesForChange, elements, contAttributes);
            }
            if(operations[i]=="SELECT"){
                docXml.SelectThreeCondition(operations, operations[0],operations[0],operations[0],operations[0]);
            }
            if(operations[i]=="UPDATE"){
                docXml.Update(operations[1],operations[1],operations[1],operations[1]);
            }
            if(operations[i]=="DELETE"){
                if(size==2){
                    docXml.DeleteAll(operations[1]);
                }


                docXml.DeleteSome(operations[1]);
            }
            else{
            
            }
        }
    }
}