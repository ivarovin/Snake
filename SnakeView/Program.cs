using Snake;
using static Snake.SnakeGame;

var game = CreateWithFruitAt((5, 0));

while (!game.GameOver)
{
    Render(game);

    game = game.Tick();
    await Task.Delay(500);
}

while (game.CanUndo())
{
    Render(game);

    game = game.Undo();
    await Task.Delay(500);
}

return;

void Render(SnakeGame snakeGame)
{
    Console.Clear();

    for (var y = 0; y < 10; y++)
    {
        for (var x = 0; x < 10; x++)
        {
            if (snakeGame.Fruit.Equals((Coordinate)(x, y)))
            {
                Console.Write("*");
                continue;
            }

            Console.Write(snakeGame.ExistsSnakeAt((x, y)) ? "o" : "x");
        }

        Console.WriteLine();
    }
}