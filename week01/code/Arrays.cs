public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number'
    /// followed by multiples of 'number'.
    ///
    /// For example, MultiplesOf(7, 5) will result in:
    /// {7, 14, 21, 28, 35}.
    ///
    /// Assume that length is a positive integer greater than 0.
    /// </summary>
    /// <returns>
    /// An array of doubles containing multiples of the supplied number.
    /// </returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // TODO Problem 1 Start

        // Step 1: Create a new double array with the requested length.
        // Step 2: Use a loop to visit every position in the array.
        // Step 3: Calculate each multiple by multiplying the starting number
        //         by the position number plus 1.
        // Step 4: Store each calculated multiple in the corresponding position.
        // Step 5: Return the completed array.

        double[] multiples = new double[length];

        for (int index = 0; index < length; index++)
        {
            multiples[index] = number * (index + 1);
        }

        return multiples;
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.
    ///
    /// For example, if the data is
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9}
    /// and the amount is 3, the list becomes
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.
    ///
    /// The value of amount will be in the range of 1 to data.Count,
    /// inclusive.
    ///
    /// Because a list is dynamic, this function modifies the existing
    /// data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // TODO Problem 2 Start

        // Step 1: Calculate where the section that must move begins.
        // Step 2: Copy the values from that position to the end of the list.
        // Step 3: Remove the copied values from their original positions.
        // Step 4: Insert the copied values at the beginning of the list.
        //         This modifies the original list instead of returning a new one.

        int startingIndex = data.Count - amount;

        List<int> valuesToMove = data.GetRange(startingIndex, amount);

        data.RemoveRange(startingIndex, amount);

        data.InsertRange(0, valuesToMove);
    }
}