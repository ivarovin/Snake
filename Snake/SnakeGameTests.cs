using FluentAssertions;

namespace Snake;

public class SnakeGameTests
{
    [Test]
    public void Field_ContainFruit_ByDefault()
    {
        new SnakeGame().Fruit.Should().NotBeNull();
    }

    [Test]
    public void Move_Snake()
    {
        var sut = new SnakeGame();

        sut.Tick();

        sut.Snake.X.Should().Be(1);
    }
}