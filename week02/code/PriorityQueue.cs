public class PriorityQueue
{
    private List<PriorityItem> _queue = new();

    /// <summary>
    /// Add a new value to the queue with an associated priority.
    /// The item is always added to the back of the queue,
    /// regardless of its priority.
    /// </summary>
    /// <param name="value">The value</param>
    /// <param name="priority">The priority</param>
    public void Enqueue(string value, int priority)
    {
        var newNode = new PriorityItem(value, priority);
        _queue.Add(newNode);
    }

    /// <summary>
    /// Remove and return the value with the highest priority.
    /// If multiple items have the highest priority, the item closest
    /// to the front of the queue is removed first.
    /// </summary>
    public string Dequeue()
    {
        if (_queue.Count == 0)
        {
            throw new InvalidOperationException("The queue is empty.");
        }

        // Initially assume the first item has the highest priority.
        int highPriorityIndex = 0;

        // Examine every other item, including the final item.
        for (int index = 1; index < _queue.Count; index++)
        {
            // Use > and not >= so the first item wins when priorities are equal.
            if (_queue[index].Priority >
                _queue[highPriorityIndex].Priority)
            {
                highPriorityIndex = index;
            }
        }

        // Save the selected value before removing its item.
        string value = _queue[highPriorityIndex].Value;

        // Remove the selected item from the queue.
        _queue.RemoveAt(highPriorityIndex);

        return value;
    }

    // DO NOT MODIFY THE CODE IN THIS METHOD
    // The graders rely on this method to check if you fixed all the bugs,
    // so changes to it will cause you to lose points.
    public override string ToString()
    {
        return $"[{string.Join(", ", _queue)}]";
    }
}

internal class PriorityItem
{
    internal string Value { get; set; }
    internal int Priority { get; set; }

    internal PriorityItem(string value, int priority)
    {
        Value = value;
        Priority = priority;
    }

    // DO NOT MODIFY THE CODE IN THIS METHOD
    // The graders rely on this method to check if you fixed all the bugs,
    // so changes to it will cause you to lose points.
    public override string ToString()
    {
        return $"{Value} (Pri:{Priority})";
    }
}