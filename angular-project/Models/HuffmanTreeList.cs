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
    private int size;

    public void Add(HuffmanNode data)
    {
        HuffmanListNode<HuffmanNode> newNode = new HuffmanListNode<HuffmanNode>(data);

        if (head == null)
        {
            // If the list is empty, set the new node as both the head and tail
            head = newNode;
            size++;
        }
        else
        {
            HuffmanListNode<HuffmanNode>? current=head;
            while(current.Next!=null){
                current=current.Next;
            }
            current.Next=newNode;
            newNode.Next=null;
            size++;
        }
    }

    public void RemoveLast()
{
    if (head == null)
        return;

    if(GetCount()==1){
        head=null;
        size--;
    }
    else
    {
        // Find the node before the tail
        HuffmanListNode<HuffmanNode>? current = head;
        while (current?.Next.Next != null)
        {
            current = current?.Next;
        }
        current.Next=null;
        size--;
    }
}


    public void Print()
    {
        HuffmanListNode<HuffmanNode>? current = head;
        while (current != null)
        {
            Console.WriteLine(current.Data.Frequency.ToString()+"   "+current.Data.Symbol.ToString());
            current = current.Next;
        }
    }

    public void Sort()
    {
        if (head == null || GetCount()==1)
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
        
        if(position==0){
            return head;
        }
        HuffmanListNode<HuffmanNode>? current = head.Next;
        int count = 1;
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
    public int getSize(){
        return this.size;
    }
    public void RemoveAt(int position)
    {
        if (position < 0 || position >= GetCount())
        {
            // Invalid position, do nothing
            return;
        }

        if (head == null)
        {
            // List is empty, do nothing
            return;
        }

        if (position == 0)
        {
            // Remove the head node
            head = head.Next;
            size--;
            return;
        }

        // Find the node before the position
        HuffmanListNode<HuffmanNode>? current = head;
        for (int i = 0; i < position - 1; i++)
        {
            current = current?.Next;
        }

        if (current == null || current.Next == null)
        {
            // Invalid position, do nothing
            return;
        }

        // Remove the node at the given position
        current.Next = current.Next?.Next;
        size--;
    }
    
}
