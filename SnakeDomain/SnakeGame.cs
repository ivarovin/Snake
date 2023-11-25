using System.Collections.Generic;
using System.Linq;
using static Snake.Coordinate;

namespace Snake
{
    public class SnakeGame
    {
        const int MapSize = 10;
        public readonly Coordinate Fruit;
        public readonly IReadOnlyCollection<Coordinate> Snake;
        readonly Coordinate direction;
        readonly SnakeGame previous;
        Coordinate NextPosition => Snake.First() + direction;
        bool CanEat => Fruit.Equals(NextPosition);
        public bool GameOver => IsEatingItselfAt(Snake.First()) || Snake.Any(IsOutsideMap);

        SnakeGame(IEnumerable<Coordinate> snake, Coordinate fruit, Coordinate direction, SnakeGame previous)
        {
            Snake = snake.ToList();
            Fruit = fruit;
            this.direction = direction;
            this.previous = previous;
        }

        public SnakeGame Tick() => GameOver ? this : EatFruitInFront().MoveSnake();
        SnakeGame EatFruitInFront() => CanEat ? GrowSnake().Cultivate(new RandomGardener(MapSize)) : this;

        public SnakeGame Cultivate(Gardener gardener)
        {
            Coordinate result;

            do result = gardener.Cultivate();
            while (!CanCultivateAt(result));

            return new SnakeGame(Snake, result, direction, this);
        }

        public bool CanCultivateAt(Coordinate position) => !ExistsSnakeAt(position) && !IsOutsideMap(position);
        static bool IsOutsideMap(Coordinate position) => IsInsideMap(position, MapSize);
        public SnakeGame MoveSnake() =>
            new SnakeGame(Snake.Select((part, i) => BodyPartInFrontOf(i)), Fruit, direction, this);
        Coordinate BodyPartInFrontOf(int bodyIndex) => bodyIndex == 0 ? NextPosition : Snake.ElementAt(bodyIndex - 1);
        public SnakeGame TurnLeft() =>
            new SnakeGame(Snake, Fruit, RightDirectionOf((direction.X * -1, direction.Y * -1)), this);
        public SnakeGame TurnRight() => new SnakeGame(Snake, Fruit, RightDirectionOf(direction), this);
        public SnakeGame GrowSnake() => new SnakeGame(Snake.Append(Snake.Last()), Fruit, direction, this);
        public bool IsEatingItselfAt(Coordinate position) => Snake.Skip(1).Any(bodyPart => bodyPart.Equals(position));
        public bool ExistsSnakeAt(Coordinate position) => Snake.Any(body => body.Equals(position));
        public SnakeGame Undo() => previous ?? this;
        public bool CanUndo() => previous !=null;

        public static SnakeGame NewGame => new SnakeGame(new[] { Origin }, (0, 0), (1, 0), null);
        public static SnakeGame CreateWithFruitAt(Coordinate fruit) => new SnakeGame(new[] { Origin }, fruit, (1, 0), null);
    }
}