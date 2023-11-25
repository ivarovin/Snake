using SnakeDomain;
using static SnakeGame;

var game = CreateWithFruitAt((5, 0));

while (!game.GameOver)
{
    Render(game);

    game = Console.ReadLine() switch
    {
        "d" => game.TurnRight(),
        "a" => game.TurnLeft(),
        _ => game
    };

    game = game.Tick();
    await Task.Delay(500);
}

while (game.CanUndo())
{
    Render(game);

    game = game.Undo();
    await Task.Delay(100);
}

return;

void Render(SnakeGame snakeGame)
{
    Console.Clear();

    for (var y = 10; y > -10; y--)
    {
        for (var x = 10; x > -10; x--)
        {
            if (snakeGame.Fruit.Equals((Coordinate)(x, y)))
            {
                Console.Write("*");
                continue;
            }

            Console.Write(snakeGame.ExistsSnakeAt((x, y)) ? "o" : " ");
        }

        Console.WriteLine();
    }
}