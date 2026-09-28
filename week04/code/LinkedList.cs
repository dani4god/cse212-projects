using System.Collections;

public class LinkedList : IEnumerable<int>
{
    private Node? _head;
    private Node? _tail;

    /// <summary>
    /// Insert a new node at the front of the linked list.
    /// </summary>
    public void InsertHead(int value)
    {
        Node newNode = new(value);

        // When the list is empty, the new node becomes both
        // the head and the tail.
        if (_head is null)
        {
            _head = newNode;
            _tail = newNode;
        }
        else
        {
            // Connect the new node to the previous head.
            newNode.Next = _head;
            _head.Prev = newNode;

            // Make the new node the head.
            _head = newNode;
        }
    }

    /// <summary>
    /// Insert a new node at the back of the linked list.
    /// </summary>
    public void InsertTail(int value)
    {
        Node newNode = new(value);

        // When the list is empty, the new node becomes both
        // the head and the tail.
        if (_tail is null)
        {
            _head = newNode;
            _tail = newNode;
        }
        else
        {
            // Connect the new node to the current tail.
            newNode.Prev = _tail;
            _tail.Next = newNode;

            // Make the new node the tail.
            _tail = newNode;
        }
    }

    /// <summary>
    /// Remove the first node from the linked list.
    /// </summary>
    public void RemoveHead()
    {
        // This condition covers both an empty list and a list
        // containing only one node.
        if (_head == _tail)
        {
            _head = null;
            _tail = null;
        }
        else if (_head is not null)
        {
            // Save the old head so its connections can be cleared.
            Node oldHead = _head;

            // Move the head to the second node.
            _head = oldHead.Next;
            _head!.Prev = null;

            // Disconnect the removed node.
            oldHead.Next = null;
        }
    }

    /// <summary>
    /// Remove the last node from the linked list.
    /// </summary>
    public void RemoveTail()
    {
        // This condition covers both an empty list and a list
        // containing only one node.
        if (_head == _tail)
        {
            _head = null;
            _tail = null;
        }
        else if (_tail is not null)
        {
            // Save the old tail so its connections can be cleared.
            Node oldTail = _tail;

            // Move the tail to the previous node.
            _tail = oldTail.Prev;
            _tail!.Next = null;

            // Disconnect the removed node.
            oldTail.Prev = null;
        }
    }

    /// <summary>
    /// Insert newValue after the first occurrence of value.
    /// </summary>
    public void InsertAfter(int value, int newValue)
    {
        Node? curr = _head;

        while (curr is not null)
        {
            if (curr.Data == value)
            {
                // If the matching node is the tail, use InsertTail.
                if (curr == _tail)
                {
                    InsertTail(newValue);
                }
                else
                {
                    Node newNode = new(newValue);

                    // Connect the new node to the matching node.
                    newNode.Prev = curr;

                    // Connect the new node to the following node.
                    newNode.Next = curr.Next;

                    // Connect the following node back to the new node.
                    curr.Next!.Prev = newNode;

                    // Connect the matching node to the new node.
                    curr.Next = newNode;
                }

                // Stop after inserting after the first match.
                return;
            }

            curr = curr.Next;
        }
    }

    /// <summary>
    /// Remove the first node containing value.
    /// </summary>
    public void Remove(int value)
    {
        // Begin searching at the head.
        Node? curr = _head;

        while (curr is not null)
        {
            if (curr.Data == value)
            {
                // Reuse RemoveHead when the matching node is the head.
                if (curr == _head)
                {
                    RemoveHead();
                }
                // Reuse RemoveTail when the matching node is the tail.
                else if (curr == _tail)
                {
                    RemoveTail();
                }
                else
                {
                    // Connect the previous node to the next node.
                    curr.Prev!.Next = curr.Next;

                    // Connect the next node to the previous node.
                    curr.Next!.Prev = curr.Prev;

                    // Disconnect the removed node.
                    curr.Prev = null;
                    curr.Next = null;
                }

                // Remove only the first matching node.
                return;
            }

            curr = curr.Next;
        }
    }

    /// <summary>
    /// Replace every occurrence of oldValue with newValue.
    /// </summary>
    public void Replace(int oldValue, int newValue)
    {
        Node? curr = _head;

        while (curr is not null)
        {
            if (curr.Data == oldValue)
            {
                curr.Data = newValue;
            }

            // Continue searching so that every match is replaced.
            curr = curr.Next;
        }
    }

    /// <summary>
    /// Support iteration through the linked list.
    /// </summary>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <summary>
    /// Iterate forward through the linked list.
    /// </summary>
    public IEnumerator<int> GetEnumerator()
    {
        // Forward iteration begins at the head.
        Node? curr = _head;

        while (curr is not null)
        {
            yield return curr.Data;
            curr = curr.Next;
        }
    }

    /// <summary>
    /// Iterate backward through the linked list.
    /// </summary>
    public IEnumerable Reverse()
    {
        // Reverse iteration begins at the tail.
        Node? curr = _tail;

        while (curr is not null)
        {
            yield return curr.Data;
            curr = curr.Prev;
        }
    }

    public override string ToString()
    {
        return "<LinkedList>{" + string.Join(", ", this) + "}";
    }

    // Just for testing.
    public Boolean HeadAndTailAreNull()
    {
        return _head is null && _tail is null;
    }

    // Just for testing.
    public Boolean HeadAndTailAreNotNull()
    {
        return _head is not null && _tail is not null;
    }
}

public static class IntArrayExtensionMethods
{
    public static string AsString(this IEnumerable array)
    {
        return "<IEnumerable>{" +
               string.Join(", ", array.Cast<int>()) +
               "}";
    }
}