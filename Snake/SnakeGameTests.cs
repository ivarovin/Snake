using FluentAssertions;

namespace Snake;

public class SnakeGameTests
{
    [Test]
    public void Field_ContainFruit_ByDefault()
    {
        new SnakeGame(new Snake()).Fruit.Should().NotBeNull();
    }

    [Test]
    public void Move_Snake()
    {
        var doc = new Snake();
        var sut = new SnakeGame(doc);

        sut.Tick();

        doc.X.Should().Be(1);
    }
}