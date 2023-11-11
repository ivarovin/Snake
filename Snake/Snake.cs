namespace Snake;

public class Snake
{
    public int X { get; private set; }
    public int Y { get; private set; }
    (int x, int y) Direction { get; set; } = (1, 0);

    public void Move()
    {
        X += Direction.x;
        Y += Direction.y;
    }

    public void TurnLeft()
    {
        Direction = Direction switch
        {
            (1, 0) => (0, 1),
            (0, 1) => (-1, 0),
            (-1, 0) => (0, -1),
            (0, -1) => (1, 0),
            _ => throw new Exception("Invalid direction")
        };
    }

    public void TurnRight()
    {
        Direction = Direction switch
        {
            (1, 0) => (0, -1),
            (0, -1) => (-1, 0),
            (-1, 0) => (0, 1),
            (0, 1) => (1, 0),
            _ => throw new Exception("Invalid direction")
        };
    }
}