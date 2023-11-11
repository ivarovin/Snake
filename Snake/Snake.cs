using System.Collections;

namespace Snake;

public class Snake : IEnumerable<(int x, int y)>
{
    readonly List<(int x, int y)> body = new() { (0, 0) };
    public (int x, int y) Head => body[0];
    (int x, int y) Direction { get; set; } = (1, 0);
    public int X => Head.x;
    public int Y => Head.y;
    public int Length => body.Count;
    public bool IsDead { get; set; }

    public void Move(SnakeGame where)
    {
        if (where.ExistsSnakeAt(NextPosition))
            IsDead = true;

        Move();
        if (where.Fruit == (X, Y))
            Grow();
    }

    public void Move()
    {
        DragBody();
        MoveForward();
    }

    void MoveForward() => body[0] = NextPosition;
    (int x, int y) NextPosition => (X + Direction.x, Y + Direction.y);

    void DragBody()
    {
        for (var i = body.Count - 1; i > 0; i--)
            body[i] = body[i - 1];
    }

    public void TurnLeft() => Direction = RightDirectionOf((Direction.x * -1, Direction.y * -1));
    public void TurnRight() => Direction = RightDirectionOf(Direction);

    static (int x, int y) RightDirectionOf((int x, int y) direction)
        => direction switch
        {
            (1, 0) => (0, -1),
            (0, -1) => (-1, 0),
            (-1, 0) => (0, 1),
            (0, 1) => (1, 0),
            _ => throw new Exception("Invalid direction")
        };

    public void Grow() => body.Add((X, Y));

    public IEnumerator<(int x, int y)> GetEnumerator() => body.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}