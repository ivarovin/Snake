namespace Snake;

public class SnakeGame
{
    const int MapSize = 10;
    public (int x, int y) Fruit { get; set; }
    public List<(int x, int y)> Snake { get; private set; } = new() { (0, 0) };
    (int x, int y) Direction { get; set; } = (1, 0);
    public bool GameOver => IsEatingItselfAt(Snake.First()) || IsOutOfMap;
    bool IsOutOfMap => Snake.Any(body => body.x > MapSize || body.x < -MapSize || body.y > MapSize || body.y < -MapSize);

    public void Tick()
    {
        if (GameOver)
            throw new InvalidOperationException("Game Over");

        EatFruitInFront();
        MoveSnake();
    }

    void EatFruitInFront()
    {
        if (Fruit != NextPosition) return;

        Grow();
        CultivateFruit();
    }

    void CultivateFruit() => Fruit = (new Random().Next(-MapSize, MapSize), new Random().Next(-MapSize, MapSize));
    public void MoveSnake() => Snake = Snake.Select((part, i) => BodyPartInFrontOf(i)).ToList();
    (int x, int y) BodyPartInFrontOf(int bodyIndex) => bodyIndex == 0 ? NextPosition : Snake[bodyIndex - 1];
    (int x, int y) NextPosition => (Snake.First().x + Direction.x, Snake.First().y + Direction.y);
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

    public void Grow() => Snake.Add(Snake.Last());
    public bool IsEatingItselfAt((int x, int y) nextPosition)
        => Snake.Skip(1).Any(bodyPart => bodyPart == nextPosition);
}