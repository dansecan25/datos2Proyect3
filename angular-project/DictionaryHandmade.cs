namespace angular_project;

using angular_project;

public class DictionaryNode<T>
{
    public DictionaryNodeData Data { get; set; }
    public DictionaryNode<T>? Next { get; set; }

    public DictionaryNode(DictionaryNodeData data)
    {
        Data = data;
        Next = null;
    }
}

public class DictionaryHandmade<T>
{
    private DictionaryNode<T>? head;
    private DictionaryNode<T>? tail;
    private int count;

    public int getCount(){
        return this.count;
    }
    public void Add(char charValue, int valueInt)
{
    DictionaryNodeData dataDoub = new DictionaryNodeData(charValue, valueInt);
    DictionaryNode<T> newNode = new DictionaryNode<T>(dataDoub);

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


    public int GetInChar(char charValue)
{
    DictionaryNode<T>? current = head; // Add the null-forgiving operator here

    while (current != null)
    {
        if (current.Data.getChar() == charValue)
            return current.Data.getInt();

        current = current.Next;
    }

    return -19;
}

    public bool charInDictionary(char charSearchValue){
        DictionaryNode<T>? current = head;
        while (current != null)
        {
            if (current.Data.getInt() == charSearchValue)
                return true;
            current = current.Next;
        }

        return false;
    }

    public char? GetInInt(int value)
    {
            DictionaryNode<T>? current = head; // Add the null-forgiving operator here

        while (current != null)
        {
            if (current.Data.getInt() == value)
            
                return current.Data.getChar();

            current = current.Next;
        }

        return null;
    }
    //sets the char value for a int
    public void setCharInInt(int value, char newValue)
    {
            DictionaryNode<T>? current = head; // Add the null-forgiving operator here

        while (current != null)
        {
            if (current.Data.getInt() == value)
            
                current.Data.setChar(newValue);

            current = current.Next;
        }
    }
    //sets the int value for a char
    public void setIntInChar(int newValue, char charValue)
    {
            DictionaryNode<T>? current = head; // Add the null-forgiving operator here

        while (current != null)
        {
            if (current.Data.getChar() == charValue)
            
                current.Data.setInt(newValue);

            current = current.Next;
        }
    }

    public void removeByChar(char charValue)
    {
        DictionaryNode<T>? current = head;
        DictionaryNode<T>? previous = null;

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
            DictionaryNode<T>? current = head;
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
        DictionaryNode<T>? current = head;
        while (current != null)
        {
            Console.WriteLine(current.Data.getInt());
            current = current.Next;
        }
    }
}
