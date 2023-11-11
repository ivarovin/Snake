using System.Collections;

namespace Snake;

public class SnakeGame : IEnumerable<(int x, int y)>
{
    public (int x, int y) Fruit { get; set; }
    public List<(int x, int y)> Snake { get; private set; } = new() { (0, 0) };
    (int x, int y) Direction { get; set; } = (1, 0);
    public bool GameOver => IsEatingItselfAt(Snake.First()) || IsOutOfMap;
    bool IsOutOfMap => Snake.Any(body => body.x > 10 || body.x < -10 || body.y > 10 || body.y < -10);

    public void Tick()
    {
        if (GameOver)
            throw new InvalidOperationException("Game Over");

        MoveSnake();
    }

    void MoveSnake()
    {
        EatFruitInFront();
        Drag();
    }

    void EatFruitInFront()
    {
        if (Fruit != NextPosition) return;

        Grow();
        CultivateNewFruit();
    }

    void CultivateNewFruit() => Fruit = (new Random().Next(-10, 10), new Random().Next(-10, 10));
    public void Drag() => Snake = Snake.Select((part, i) => InFrontOf(i)).ToList();
    (int x, int y) InFrontOf(int bodyIndex) => bodyIndex == 0 ? NextPosition : Snake[bodyIndex - 1];
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

    public void Grow() => Snake.Add(Snake.First());
    public bool IsEatingItselfAt((int x, int y) nextPosition)
        => Snake.Except(new []{Snake.First()}).Any(bodyPart => bodyPart == nextPosition);
    public IEnumerator<(int x, int y)> GetEnumerator() => Snake.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}