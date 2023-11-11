namespace Snake;

public class SnakeGame
{
    public (int x, int y) Fruit { get; set; }
    public readonly Snake Snake = new();

    public void Tick()
    {
        Snake.Move();
    }
}