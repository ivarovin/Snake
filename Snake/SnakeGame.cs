namespace Snake;

public class SnakeGame
{
    public (int x, int y) Fruit { get; set; }
    readonly Snake snake;

    public SnakeGame(Snake snake)
    {
        this.snake = snake;
    }
    
    public void Tick()
    {
        snake.Drag();
    }

    public bool ExistsSnakeAt((int, int) cell)
    {
        if (snake.Any(bodyPart => bodyPart == cell))
            return true;

        return snake.Head.x == cell.Item1 && snake.Head.y == cell.Item2;
    }
}