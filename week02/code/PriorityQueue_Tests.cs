using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Add three values with different priorities. The item added
    // last has the highest priority. Dequeue all three items.
    // Expected Result: "High" is returned first, "Medium" is returned second,
    // and "Low" is returned last.
    // Defect(s) Found: The initial test failed because the loop did not examine
    // the last item. The selected item was also returned without being removed.
    // Final Result: Passed after every item was examined and the selected
    // item was removed.
    public void TestPriorityQueue_HighestPriority()
    {
        var priorityQueue = new PriorityQueue();

        priorityQueue.Enqueue("Low", 1);
        priorityQueue.Enqueue("Medium", 3);
        priorityQueue.Enqueue("High", 5);

        Assert.AreEqual("High", priorityQueue.Dequeue());
        Assert.AreEqual("Medium", priorityQueue.Dequeue());
        Assert.AreEqual("Low", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Add three values that all have the same priority.
    // Expected Result: The values are returned in FIFO order:
    // "First", followed by "Second", followed by "Third".
    // Defect(s) Found: The initial test failed because >= caused a later
    // item to be selected when two items had equal priorities.
    // Final Result: Passed after changing the comparison from >= to >.
    public void TestPriorityQueue_EqualPrioritiesUseFifo()
    {
        var priorityQueue = new PriorityQueue();

        priorityQueue.Enqueue("First", 5);
        priorityQueue.Enqueue("Second", 5);
        priorityQueue.Enqueue("Third", 5);

        Assert.AreEqual("First", priorityQueue.Dequeue());
        Assert.AreEqual("Second", priorityQueue.Dequeue());
        Assert.AreEqual("Third", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Add values with negative, zero, and positive priorities.
    // Expected Result: "Positive" is returned first, "Zero" is returned
    // second, and "Negative" is returned last.
    // Defect(s) Found: The selected item was initially not removed, causing
    // the same value to be returned again.
    // Final Result: Passed after RemoveAt was added to Dequeue.
    public void TestPriorityQueue_MixedPriorities()
    {
        var priorityQueue = new PriorityQueue();

        priorityQueue.Enqueue("Negative", -5);
        priorityQueue.Enqueue("Zero", 0);
        priorityQueue.Enqueue("Positive", 4);

        Assert.AreEqual("Positive", priorityQueue.Dequeue());
        Assert.AreEqual("Zero", priorityQueue.Dequeue());
        Assert.AreEqual("Negative", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Attempt to dequeue from an empty priority queue.
    // Expected Result: An InvalidOperationException is thrown with
    // the exact message "The queue is empty."
    // Defect(s) Found: No defect was found in the empty-queue handling.
    // Final Result: Passed.
    public void TestPriorityQueue_EmptyQueue()
    {
        var priorityQueue = new PriorityQueue();

        var exception = Assert.ThrowsException<InvalidOperationException>(
            () => priorityQueue.Dequeue()
        );

        Assert.AreEqual("The queue is empty.", exception.Message);
    }

    [TestMethod]
    // Scenario: Add three items and inspect the queue before removing anything.
    // Expected Result: Every item appears in the same order in which it
    // was enqueued, showing that Enqueue adds items to the back.
    // Defect(s) Found: No defect was found in PriorityQueue.Enqueue.
    // Final Result: Passed.
    public void TestPriorityQueue_EnqueueAddsToBack()
    {
        var priorityQueue = new PriorityQueue();

        priorityQueue.Enqueue("First", 1);
        priorityQueue.Enqueue("Second", 2);
        priorityQueue.Enqueue("Third", 3);

        Assert.AreEqual(
            "[First (Pri:1), Second (Pri:2), Third (Pri:3)]",
            priorityQueue.ToString()
        );
    }
}