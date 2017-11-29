using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyclicalList<T> : List<T>
{
    public new T this[int index]
    {
        get
        {
            //perform the index wrapping
            while (index < 0)
                index = Count + index;
            if (index >= Count)
                index %= Count;

            return base[index];
        }
        set
        {
            //perform the index wrapping
            while (index < 0)
                index = Count + index;
            if (index >= Count)
                index %= Count;

            base[index] = value;
        }
    }

    public CyclicalList() { }

    public CyclicalList(IEnumerable<T> collection)
        : base(collection)
    {
    }

    public new void RemoveAt(int index)
    {
        Remove(this[index]);
    }
}

public class IndexableCyclicalLinkedList<T> : LinkedList<T>
{
    /// <summary>
    /// Gets the LinkedListNode at a particular index.
    /// </summary>
    /// <param name="index">The index of the node to retrieve.</param>
    /// <returns>The LinkedListNode found at the index given.</returns>
    public LinkedListNode<T> this[int index]
    {
        get
        {
            //perform the index wrapping
            while (index < 0)
                index = Count + index;
            if (index >= Count)
                index %= Count;

            //find the proper node
            LinkedListNode<T> node = First;
            for (int i = 0; i < index; i++)
                node = node.Next;

            return node;
        }
    }

    /// <summary>
    /// Removes the node at a given index.
    /// </summary>
    /// <param name="index">The index of the node to remove.</param>
    public void RemoveAt(int index)
    {
        Remove(this[index]);
    }

    /// <summary>
    /// Finds the index of a given item.
    /// </summary>
    /// <param name="item">The item to find.</param>
    /// <returns>The index of the item if found; -1 if the item is not found.</returns>
    public int IndexOf(T item)
    {
        for (int i = 0; i < Count; i++)
            if (this[i].Value.Equals(item))
                return i;

        return -1;
    }
}