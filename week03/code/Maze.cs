/// <summary>
/// Defines a maze using a dictionary. The dictionary is provided by the
/// user when the Maze object is created. The dictionary contains:
///
/// (x, y) : [left, right, up, down]
///
/// The Boolean values indicate whether movement in each direction
/// is allowed.
/// </summary>
public class Maze
{
    private readonly Dictionary<ValueTuple<int, int>, bool[]> _mazeMap;

    private int _currX = 1;
    private int _currY = 1;

    public Maze(Dictionary<ValueTuple<int, int>, bool[]> mazeMap)
    {
        _mazeMap = mazeMap;
    }

    /// <summary>
    /// Move one position to the left when the path is open.
    /// </summary>
    public void MoveLeft()
    {
        // Index 0 represents movement to the left.
        bool canMoveLeft = _mazeMap[(_currX, _currY)][0];

        if (!canMoveLeft)
        {
            throw new InvalidOperationException("Can't go that way!");
        }

        _currX--;
    }

    /// <summary>
    /// Move one position to the right when the path is open.
    /// </summary>
    public void MoveRight()
    {
        // Index 1 represents movement to the right.
        bool canMoveRight = _mazeMap[(_currX, _currY)][1];

        if (!canMoveRight)
        {
            throw new InvalidOperationException("Can't go that way!");
        }

        _currX++;
    }

    /// <summary>
    /// Move one position upward when the path is open.
    /// </summary>
    public void MoveUp()
    {
        // Index 2 represents movement upward.
        bool canMoveUp = _mazeMap[(_currX, _currY)][2];

        if (!canMoveUp)
        {
            throw new InvalidOperationException("Can't go that way!");
        }

        _currY--;
    }

    /// <summary>
    /// Move one position downward when the path is open.
    /// </summary>
    public void MoveDown()
    {
        // Index 3 represents movement downward.
        bool canMoveDown = _mazeMap[(_currX, _currY)][3];

        if (!canMoveDown)
        {
            throw new InvalidOperationException("Can't go that way!");
        }

        _currY++;
    }

    public string GetStatus()
    {
        return $"Current location (x={_currX}, y={_currY})";
    }
}