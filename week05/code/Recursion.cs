using System.Collections;

public static class Recursion
{
    /// <summary>
    /// Problem 1:
    /// Recursively calculate 1² + 2² + 3² + ... + n².
    /// </summary>
    public static int SumSquaresRecursive(int n)
    {
        // Base case: zero and negative numbers contribute nothing.
        if (n <= 0)
        {
            return 0;
        }

        // Recursive case:
        // Add n² to the solution for the smaller problem, n - 1.
        return (n * n) + SumSquaresRecursive(n - 1);
    }

    /// <summary>
    /// Problem 2:
    /// Generate permutations of the requested size.
    /// </summary>
    public static void PermutationsChoose(
        List<string> results,
        string letters,
        int size,
        string word = "")
    {
        // Base case: the required number of letters has been selected.
        if (word.Length == size)
        {
            results.Add(word);
            return;
        }

        // Try every remaining letter in the next position.
        for (int index = 0; index < letters.Length; index++)
        {
            char selectedLetter = letters[index];

            // Remove the selected letter so it cannot be reused
            // within the current permutation.
            string remainingLetters =
                letters[..index] + letters[(index + 1)..];

            PermutationsChoose(
                results,
                remainingLetters,
                size,
                word + selectedLetter
            );
        }
    }

    /// <summary>
    /// Problem 3:
    /// Count how many ways a person can climb a staircase by taking
    /// one, two, or three steps at a time.
    /// </summary>
    public static decimal CountWaysToClimb(
        int s,
        Dictionary<int, decimal>? remember = null)
    {
        // Base cases.
        if (s == 0)
        {
            return 0;
        }

        if (s == 1)
        {
            return 1;
        }

        if (s == 2)
        {
            return 2;
        }

        if (s == 3)
        {
            return 4;
        }

        // Create the memoization dictionary during the first call.
        remember ??= new Dictionary<int, decimal>();

        // If this problem was already solved, return the saved answer.
        if (remember.TryGetValue(s, out decimal savedAnswer))
        {
            return savedAnswer;
        }

        // Solve the three smaller problems using the same dictionary.
        decimal ways =
            CountWaysToClimb(s - 1, remember) +
            CountWaysToClimb(s - 2, remember) +
            CountWaysToClimb(s - 3, remember);

        // Save the answer so it does not need to be calculated again.
        remember[s] = ways;

        return ways;
    }

    /// <summary>
    /// Problem 4:
    /// Replace every wildcard with both possible binary values.
    /// </summary>
    public static void WildcardBinary(
        string pattern,
        List<string> results)
    {
        // Find the first wildcard.
        int wildcardIndex = pattern.IndexOf('*');

        // Base case: there are no wildcards left.
        if (wildcardIndex == -1)
        {
            results.Add(pattern);
            return;
        }

        // Create a pattern where the wildcard is replaced by zero.
        string zeroPattern =
            pattern[..wildcardIndex] +
            "0" +
            pattern[(wildcardIndex + 1)..];

        // Create a pattern where the wildcard is replaced by one.
        string onePattern =
            pattern[..wildcardIndex] +
            "1" +
            pattern[(wildcardIndex + 1)..];

        // Recursively process the remaining wildcards.
        WildcardBinary(zeroPattern, results);
        WildcardBinary(onePattern, results);
    }

    /// <summary>
    /// Problem 5:
    /// Recursively find every path from (0,0) to the end of the maze.
    /// </summary>
    public static void SolveMaze(
        List<string> results,
        Maze maze,
        int x = 0,
        int y = 0,
        List<ValueTuple<int, int>>? currPath = null)
    {
        // Create the path during the first call.
        currPath ??= new List<ValueTuple<int, int>>();

        // Add the current position to the path.
        currPath.Add((x, y));

        // Base case: the current position is the end of the maze.
        if (maze.IsEnd(x, y))
        {
            results.Add(currPath.AsString());

            // Remove the current position before returning so the
            // list can be reused while exploring another path.
            currPath.RemoveAt(currPath.Count - 1);
            return;
        }

        // Try moving left.
        if (maze.IsValidMove(currPath, x - 1, y))
        {
            SolveMaze(results, maze, x - 1, y, currPath);
        }

        // Try moving right.
        if (maze.IsValidMove(currPath, x + 1, y))
        {
            SolveMaze(results, maze, x + 1, y, currPath);
        }

        // Try moving up.
        if (maze.IsValidMove(currPath, x, y - 1))
        {
            SolveMaze(results, maze, x, y - 1, currPath);
        }

        // Try moving down.
        if (maze.IsValidMove(currPath, x, y + 1))
        {
            SolveMaze(results, maze, x, y + 1, currPath);
        }

        // Backtrack by removing the current position after all
        // possible paths from it have been explored.
        currPath.RemoveAt(currPath.Count - 1);
    }
}