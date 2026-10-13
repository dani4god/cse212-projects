public class Node
{
    public int Data { get; set; }
    public Node? Left { get; set; }
    public Node? Right { get; set; }

    public Node(int data)
    {
        Data = data;
    }

    /// <summary>
    /// Insert a value into the correct location in the tree.
    /// Duplicate values are ignored.
    /// </summary>
    public void Insert(int value)
    {
        if (value < Data)
        {
            if (Left is null)
            {
                Left = new Node(value);
            }
            else
            {
                Left.Insert(value);
            }
        }
        else if (value > Data)
        {
            if (Right is null)
            {
                Right = new Node(value);
            }
            else
            {
                Right.Insert(value);
            }
        }

        // If value equals Data, do nothing because the tree
        // should contain only unique values.
    }

    /// <summary>
    /// Search recursively for a value in the tree.
    /// </summary>
    public bool Contains(int value)
    {
        if (value == Data)
        {
            return true;
        }

        if (value < Data)
        {
            return Left is not null && Left.Contains(value);
        }

        return Right is not null && Right.Contains(value);
    }

    /// <summary>
    /// Calculate the height of this node and its subtrees.
    /// </summary>
    public int GetHeight()
    {
        int leftHeight = Left?.GetHeight() ?? 0;
        int rightHeight = Right?.GetHeight() ?? 0;

        return 1 + Math.Max(leftHeight, rightHeight);
    }
}