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
        snake.Move();
    }

    public bool ExistsSnakeAt((int, int) valueTuple)
    {
        return snake.X == valueTuple.Item1 && snake.Y == valueTuple.Item2;
    }
}