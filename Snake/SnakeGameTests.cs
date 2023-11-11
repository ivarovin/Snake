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
        new Snake().IsEatingItselfAt((0, 0)).Should().BeTrue();
        new Snake().IsEatingItselfAt((1, 0)).Should().BeFalse();
    }

    [Test]
    public void Check_IfSnakeBodyExists_AtPosition()
    {
        var doc = new Snake();
        
        doc.Grow();
        doc.Drag();
        
        using var _ = new AssertionScope();
        new SnakeGame(doc).ExistsSnakeAt((0, 0)).Should().BeTrue();
        new SnakeGame(doc).ExistsSnakeAt((1, 0)).Should().BeTrue();
    }
}