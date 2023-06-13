using angular_project.Models;

namespace angular_project.Models;
public class HuffmanListNode<HuffmanNode>
{
    public HuffmanNode Data { get; set; }
    public HuffmanListNode<HuffmanNode>? Next { get; set; }

    public HuffmanListNode(HuffmanNode data)
    {
        Data = data;
        Next = null;
    }
}
public class HuffmanTreeList<T>
{
    private HuffmanListNode<HuffmanNode>? head;
    private HuffmanListNode<HuffmanNode>? tail;
    private int size;

    public void Add(HuffmanNode data)
    {
        HuffmanListNode<HuffmanNode> newNode = new HuffmanListNode<HuffmanNode>(data);

        if (head == null)
        {
            // If the list is empty, set the new node as both the head and tail
            head = newNode;
            tail = newNode;
            size=1;
        }
        else
        {
            // Add the new node to the end of the list
            if (tail != null)
            {
                tail.Next = newNode;
                tail = newNode;
                size++;
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
        size--;
    }
    else
    {
        // Find the node before the tail
        HuffmanListNode<HuffmanNode>? current = head;
        while (current?.Next != tail)
        {
            current = current?.Next;
        }

        // Remove the tail node
        if (current != null)
        {
            current.Next = null;
            tail = current;
            size--;
        }
    }
}


    public void Print()
    {
        HuffmanListNode<HuffmanNode>? current = head;
        while (current != null)
        {
            Console.WriteLine(current.Data);
            current = current.Next;
        }
    }

    public void Sort()
    {
        if (head == null || head == tail)
            return;

        bool swapped;
        do
        {
            HuffmanListNode<HuffmanNode>? current = head;
            HuffmanListNode<HuffmanNode>? previous = null;
            swapped = false;

            while (current != null && current.Next != null)
            {
                HuffmanNode currentNodeData = current.Data;
                HuffmanNode nextNodeData = current.Next.Data;

                if (currentNodeData.Frequency > nextNodeData.Frequency)
                {
                    if (previous != null)
                    {
                        previous.Next = current.Next;
                    }
                    else
                    {
                        head = current.Next;
                    }

                    HuffmanListNode<HuffmanNode>? temp = current.Next.Next;
                    current.Next.Next = current;
                    current.Next = temp;

                    swapped = true;
                }

                previous = current;
                current = current.Next;
            }
        }
        while (swapped);
    }

    public HuffmanListNode<HuffmanNode>? GetNodeAtPosition(int position)
    {
        HuffmanListNode<HuffmanNode>? current = head;
        int count = 0;

        while (current != null)
        {
            if (count == position)
                return current;

            current = current.Next;
            count++;
        }

        return null;
    }


    public int GetCount()
    {
        HuffmanListNode<HuffmanNode>? current = head;
        int count = 0;

        while (current != null)
        {
            count++;
            current = current.Next;
        }

        return count;
    }
}
