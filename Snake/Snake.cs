using System.Collections;

namespace Snake;

public class Snake : IEnumerable<(int x, int y)>
{
    public (int x, int y) Fruit { get; set; }
    List<(int x, int y)> body = new() { (0, 0) };
    public (int x, int y) Head => body[0];
    (int x, int y) Direction { get; set; } = (1, 0);
    public bool IsDead { get; private set; }

    public void Move()
    {
        if (IsEatingItselfAt(NextPosition))
            IsDead = true;
        if (Fruit == NextPosition)
            Grow();

        Drag();
    }

    public void Drag() => body = body.Select((part, i) => InFrontOf(i)).ToList();

    (int x, int y) InFrontOf(int bodyIndex)
        => bodyIndex == 0 ? NextPosition : body[bodyIndex - 1];

    (int x, int y) NextPosition => (Head.x + Direction.x, Head.y + Direction.y);
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

    public void Grow() => body.Add((Head.x, Head.y));

    public IEnumerator<(int x, int y)> GetEnumerator() => body.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public bool IsEatingItselfAt((int x, int y) nextPosition) => body.Any(bodyPart => bodyPart == nextPosition);
}