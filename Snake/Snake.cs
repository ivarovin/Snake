using System.Collections;

namespace Snake;

public class Snake : IEnumerable<(int x, int y)>
{
    readonly List<(int x, int y)> body = new();
    public int X { get; private set; }
    public int Y { get; private set; }
    (int x, int y) Direction { get; set; } = (1, 0);
    public int Length => body.Count;
    public bool IsDead { get; set; }

    public void Move()
    {
        DragBody();
        MoveForward();
    }

    void MoveForward()
    {
        X += Direction.x;
        Y += Direction.y;
    }

    void DragBody()
    {
        if (body.Count <= 0) return;
        
        for (var i = body.Count; i < 1; i--) 
            body[i] = body[i - 1];

        body[0] = (X, Y);
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

    public void Grow()
    {
        body.Add((X, Y));
    }

    public IEnumerator<(int x, int y)> GetEnumerator() => body.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public void Move(SnakeGame where)
    {
        Move();
        if (where.Fruit == (X, Y))
            Grow();
    }
}