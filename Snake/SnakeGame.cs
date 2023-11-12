using static Snake.Coordinate;

namespace Snake;

public class SnakeGame
{
    const int MapSize = 10;
    public Coordinate Fruit { get; set; }
    public List<Coordinate> Snake { get; }
    Coordinate Direction { get; }
    public bool GameOver => IsEatingItselfAt(Snake.First()) || Snake.Any(IsOutsideMap);

    SnakeGame(IEnumerable<Coordinate> snake, Coordinate fruit, Coordinate direction)
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
        => !Fruit.Equals(NextPosition)
            ? this
            : new SnakeGame(GrowSnake(), CultivateFruit(new RandomGardener(MapSize)), Direction);

    public Coordinate CultivateFruit(Gardener gardener)
    {
        Coordinate result;

        do result = gardener.Cultivate();
        while (!CanCultivateAt(result));

        return result;
    }

    public bool CanCultivateAt(Coordinate position) => !ExistsSnakeAt(position) && !IsOutsideMap(position);
    static bool IsOutsideMap(Coordinate position) => IsInsideMap(position, MapSize);
    public SnakeGame MoveSnake() => new(Snake.Select((part, i) => BodyPartInFrontOf(i)).ToList(), Fruit, Direction);
    Coordinate BodyPartInFrontOf(int bodyIndex) => bodyIndex == 0 ? NextPosition : Snake[bodyIndex - 1];
    Coordinate NextPosition => Snake.First() + Direction;
    public SnakeGame TurnLeft() => new(Snake, Fruit, RightDirectionOf((Direction.X * -1, Direction.Y * -1)));
    public SnakeGame TurnRight() => new(Snake, Fruit, RightDirectionOf(Direction));
    public IEnumerable<Coordinate> GrowSnake() => Snake.Append(Snake.Last());
    public bool IsEatingItselfAt(Coordinate position) => Snake.Skip(1).Any(bodyPart => bodyPart.Equals(position));
    bool ExistsSnakeAt(Coordinate position) => Snake.Any(body => body.Equals(position));

    public static SnakeGame NewGame => new(new[] { Origin }, (0, 0), (1, 0));
    public static SnakeGame CreateWithFruitAt(Coordinate fruit) => new(new[] { Origin }, fruit, (1, 0));
}