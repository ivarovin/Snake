using FluentAssertions;
using FluentAssertions.Execution;

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

        doc.Head.x.Should().Be(1);
    }

    [Test]
    public void Check_IfSnakeExists_AtPosition()
    {
        new SnakeGame(new Snake()).ExistsSnakeAt((0, 0)).Should().BeTrue();
        new SnakeGame(new Snake()).ExistsSnakeAt((1, 0)).Should().BeFalse();
    }

    [Test]
    public void Check_IfSnakeBodyExists_AtPosition()
    {
        var doc = new Snake();
        
        doc.Grow();
        doc.Move();
        
        using var _ = new AssertionScope();
        new SnakeGame(doc).ExistsSnakeAt((0, 0)).Should().BeTrue();
        new SnakeGame(doc).ExistsSnakeAt((1, 0)).Should().BeTrue();
    }
}