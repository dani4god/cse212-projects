/// <summary>
/// This queue is circular. When people are added via AddPerson, they are
/// added to the back of the queue according to FIFO rules.
///
/// When GetNextPerson is called, the next person is removed from the front.
/// If that person still has turns remaining, the person is added back to
/// the end of the queue.
///
/// A turns value of zero or less means that the person has an infinite
/// number of turns.
/// </summary>
public class TakingTurnsQueue
{
    private readonly PersonQueue _people = new();

    public int Length => _people.Length;

    /// <summary>
    /// Add a new person to the queue with a name and number of turns.
    /// </summary>
    /// <param name="name">Name of the person</param>
    /// <param name="turns">Number of turns remaining</param>
    public void AddPerson(string name, int turns)
    {
        var person = new Person(name, turns);
        _people.Enqueue(person);
    }

    /// <summary>
    /// Get and return the next person in the queue.
    ///
    /// The person goes to the back of the queue again if they have more
    /// turns. A turns value of zero or less represents infinite turns.
    ///
    /// An InvalidOperationException is thrown when the queue is empty.
    /// </summary>
    public Person GetNextPerson()
    {
        if (_people.IsEmpty())
        {
            throw new InvalidOperationException("No one in the queue.");
        }

        // Remove the person at the front of the queue.
        Person person = _people.Dequeue();

        if (person.Turns > 0)
        {
            // A person with limited turns has now used one turn.
            person.Turns--;

            // Return the person to the queue if they have another turn.
            if (person.Turns > 0)
            {
                _people.Enqueue(person);
            }
        }
        else
        {
            // Zero or a negative number means infinite turns.
            // Add the person back without changing the Turns value.
            _people.Enqueue(person);
        }

        return person;
    }

    public override string ToString()
    {
        return _people.ToString();
    }
}