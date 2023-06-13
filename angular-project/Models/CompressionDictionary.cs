namespace angular_project.Models;

using angular_project.Models;

public class CompressionNode<T>
{
    public CompressionNodeData Data { get; set; }
    public CompressionNode<T>? Next { get; set; }

    public CompressionNode(CompressionNodeData data)
    {
        Data = data;
        Next = null;
    }
}

public class CompressionDictionary<T>
{
    private CompressionNode<T>? head;
    private CompressionNode<T>? tail;
    private int count;

    public int getCount(){
        return this.count;
    }
    public void Add(char charValue, string valueStr)
{
    CompressionNodeData dataDoub = new CompressionNodeData(charValue, valueStr);
    CompressionNode<T> newNode = new CompressionNode<T>(dataDoub);

    if (head == null)
    {
        // If the list is empty, set the new node as both the head and tail
        head = newNode;
        tail = newNode;
        count=1;
    }
    else
    {
        // Add the new node to the end of the list
        if (tail != null)
        {
            tail.Next = newNode;
            tail = newNode;
            count++;
        }
    }

    
}


    public string GetInChar(char charValue)
{
    CompressionNode<T>? current = head; // Add the null-forgiving operator here

    while (current != null)
    {
        if (current.Data.getChar() == charValue)
            return current.Data.getString();

        current = current.Next;
    }

    return "NONE";
}

    public bool charInDictionary(char charSearchValue){
        CompressionNode<T>? current = head;
        while (current != null)
        {
            if (current.Data.getChar() == charSearchValue)
                return true;
            current = current.Next;
        }

        return false;
    }
    public bool StringInDictionary(string stringSearchVal){
        CompressionNode<T>? current = head;
        while (current != null)
        {
            if (current.Data.getString() == stringSearchVal)
                return true;
            current = current.Next;
        }

        return false;
    }

    public char? GetInString(string value)
    {
            CompressionNode<T>? current = head; // Add the null-forgiving operator here

        while (current != null)
        {
            if (current.Data.getString() == value)
            
                return current.Data.getChar();

            current = current.Next;
        }

        return null;
    }
    public char getCharFromPos(int pos)
    {
            CompressionNode<T>? current = head; // Add the null-forgiving operator here
            int counter=0;
        while (current != null)
        {   
            if (counter == pos)
                return current.Data.getChar();

            current = current.Next;
            counter++;
        }

        return 'd';
    }

    public string getStringFromPos(int pos)
    {
            CompressionNode<T>? current = head; // Add the null-forgiving operator here
            int counter=0;
        while (current != null)
        {   
            if (counter == pos)
                return current.Data.getString();

            current = current.Next;
            counter++;
        }

        return "NONE";
    }
    //sets the char value for a int
    public void setCharInString(string value, char newValue)
    {
            CompressionNode<T>? current = head; // Add the null-forgiving operator here

        while (current != null)
        {
            if (current.Data.getString() == value)
            
                current.Data.setChar(newValue);

            current = current.Next;
        }
    }
    //sets the int value for a char
    public void setStringInChar(string newValue, char charValue)
    {
            CompressionNode<T>? current = head; // Add the null-forgiving operator here

        while (current != null)
        {
            if (current.Data.getChar() == charValue)
            
                current.Data.setString(newValue);

            current = current.Next;
        }
    }

    public void removeByChar(char charValue)
    {
        CompressionNode<T>? current = head;
        CompressionNode<T>? previous = null;

        while (current != null)
        {
            if (current.Data.getChar() == charValue)
            {
                if (previous != null)
                {
                    // Remove the node from the middle of the list
                    previous.Next = current.Next;

                    if (current == tail)
                        tail = previous;
                }
                else
                {
                    // Remove the head node
                    head = current.Next;

                    if (head == null)
                        tail = null;
                }

                count--;
                break;
            }

            previous = current;
            current = current.Next;
        }
    }

    public void RemoveLast()
    {
        if (head == null)
            return;

        if (head == tail)
        {
            // Remove the only node in the list
            head = null;
            tail = null;
        }
        else
        {
            // Find the node before the tail
            CompressionNode<T>? current = head;
            while (current!.Next != tail)
            {
                current = current.Next;
            }

            // Remove the tail node
            current.Next = null;
            tail = current;
        }

        count--;
    }

    public void Print()
    {
        CompressionNode<T>? current = head;
        while (current != null)
        {
            Console.WriteLine(current.Data.getString());
            current = current.Next;
        }
    }
}
