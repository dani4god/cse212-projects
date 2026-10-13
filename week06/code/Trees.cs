public static class Trees
{
    /// <summary>
    /// Create a balanced binary search tree from a sorted array.
    /// </summary>
    public static BinarySearchTree CreateTreeFromSortedList(
        int[] sortedNumbers)
    {
        var bst = new BinarySearchTree();

        InsertMiddle(
            sortedNumbers,
            0,
            sortedNumbers.Length - 1,
            bst
        );

        return bst;
    }

    /// <summary>
    /// Recursively insert the middle element of each range.
    /// </summary>
    private static void InsertMiddle(
        int[] sortedNumbers,
        int first,
        int last,
        BinarySearchTree bst)
    {
        // The range is empty, so recursion is complete.
        if (first > last)
        {
            return;
        }

        // Integer division selects the lower middle for an even-sized range.
        int middle = first + (last - first) / 2;

        bst.Insert(sortedNumbers[middle]);

        // Insert the middle values from the left and right halves.
        InsertMiddle(sortedNumbers, first, middle - 1, bst);
        InsertMiddle(sortedNumbers, middle + 1, last, bst);
    }
}