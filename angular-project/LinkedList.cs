namespace angular_project;

using angular_project;

public class LinkedListNode<T>
{
    public DoubleObjects Data { get; set; }
    public LinkedListNode<T>? Next { get; set; }

    public LinkedListNode(DoubleObjects data)
    {
        Data = data;
        Next = null;
    }
}

public class LinkedList<T>
{
    private LinkedListNode<T>? head;
    private LinkedListNode<T>? tail;
    private int count;

    public int Count { get { return count; } }

    public void Add(string key, string data)
{
    DoubleObjects dataDoub = new DoubleObjects(key, data);
    LinkedListNode<T> newNode = new LinkedListNode<T>(dataDoub);

    if (head == null)
    {
        // If the list is empty, set the new node as both the head and tail
        head = newNode;
        tail = newNode;
    }
    else
    {
        // Add the new node to the end of the list
        if (tail != null)
        {
            tail.Next = newNode;
            tail = newNode;
        }
    }

    count++;
}


    public string GetDataByKey(string key)
{
    LinkedListNode<T>? current = head; // Add the null-forgiving operator here

    while (current != null)
    {
        if (current.Data.getKey() == key)
            return current.Data.getData();

        current = current.Next;
    }

    return "NONE";
}


    public string GetKeyByData(string data)
    {
            LinkedListNode<T>? current = head; // Add the null-forgiving operator here

        while (current != null)
        {
            if (current.Data.getData() == data)
            
                return current.Data.getKey();

            current = current.Next;
        }

        return "NONE";
    }

    public void RemoveByKey(string key)
    {
        LinkedListNode<T>? current = head;
        LinkedListNode<T>? previous = null;

        while (current != null)
        {
            if (current.Data.getKey() == key)
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
            LinkedListNode<T>? current = head;
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
        LinkedListNode<T>? current = head;
        while (current != null)
        {
            Console.WriteLine(current.Data.getData());
            current = current.Next;
        }
    }
}
