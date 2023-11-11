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

    public bool ExistsSnakeAt((int, int) cell) => snake.IsEatingItselfAt(cell);
}