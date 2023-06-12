using angular_project.Models;

namespace angular_project.Models;
public class HuffmanListNode<T>
{
    public T Data { get; set; }
    public HuffmanListNode<T>? Next { get; set; }

    public HuffmanListNode(T data)
    {
        Data = data;
        Next = null;
    }
}
public class HuffmanTreeList<T>
{
    private HuffmanListNode<T>? head;
    private HuffmanListNode<T>? tail;

    public void Add(T data)
    {
        HuffmanListNode<T> newNode = new HuffmanListNode<T>(data);

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
        HuffmanListNode<T>? current = head;
        while (current?.Next != tail)
        {
            current = current?.Next;
        }

        // Remove the tail node
        if (current != null)
        {
            current.Next = null;
            tail = current;
        }
    }
}


    public void Print()
    {
        HuffmanListNode<T>? current = head;
        while (current != null)
        {
            Console.WriteLine(current.Data);
            current = current.Next;
        }
    }
}
