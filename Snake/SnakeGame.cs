namespace Snake;

public class SnakeGame
{
    const int MapSize = 10;
    public (int x, int y) Fruit { get; set; }
    public List<(int x, int y)> Snake { get; } = new() { (0, 0) };
    (int x, int y) Direction { get; } = (1, 0);
    public bool GameOver => IsEatingItselfAt(Snake.First()) || Snake.Any(IsOutsideMap);

    SnakeGame(IEnumerable<(int x, int y)> snake, (int x, int y) fruit, (int x, int y) direction)
    {
        Snake = snake.ToList();
        Fruit = fruit;
        Direction = direction;
    }

    public SnakeGame Tick()
    {
        if (GameOver)
            throw new InvalidOperationException("Game Over");

        return EatFruitInFront().MoveSnake();
    }

    SnakeGame EatFruitInFront()
        => Fruit != NextPosition ? this : new SnakeGame(GrowSnake(), CultivateFruit(), Direction);

    (int x, int y) CultivateFruit()
    {
        (int, int) result;

        do
        {
            result = (new Random().Next(-MapSize, MapSize), new Random().Next(-MapSize, MapSize));
        } while (CanCultivateAt(result));

        return result;
    }

    public bool CanCultivateAt((int x, int y) position) => !ExistsSnakeAt(position) && !IsOutsideMap(position);

    static bool IsOutsideMap((int x, int y) position)
        => position.x > MapSize || position.x < -MapSize || position.y > MapSize || position.y < -MapSize;

    public SnakeGame MoveSnake() => new(Snake.Select((part, i) => BodyPartInFrontOf(i)).ToList(), Fruit, Direction);
    (int x, int y) BodyPartInFrontOf(int bodyIndex) => bodyIndex == 0 ? NextPosition : Snake[bodyIndex - 1];
    (int x, int y) NextPosition => (Snake.First().x + Direction.x, Snake.First().y + Direction.y);
    public SnakeGame TurnLeft() => new(Snake, Fruit, RightDirectionOf((Direction.x * -1, Direction.y * -1)));
    public SnakeGame TurnRight() => new(Snake, Fruit, RightDirectionOf(Direction));
    static (int x, int y) RightDirectionOf((int x, int y) direction)
        => direction switch
        {
            (1, 0) => (0, -1),
            (0, -1) => (-1, 0),
            (-1, 0) => (0, 1),
            (0, 1) => (1, 0),
            _ => throw new Exception("Invalid direction")
        };

    public IEnumerable<(int x, int y)> GrowSnake() => Snake.Append(Snake.Last());
    public bool IsEatingItselfAt((int x, int y) position) => Snake.Skip(1).Any(bodyPart => bodyPart == position);
    bool ExistsSnakeAt((int x, int y) nextPosition) => Snake.Any(bodyPart => bodyPart == nextPosition);
    public static SnakeGame NewGame => new(new[] { (0, 0) }, (0, 0), (1, 0));
    public static SnakeGame CreateWithFruitAt((int x, int y) fruit) => new(new[] { (0, 0) }, fruit, (1, 0));
}