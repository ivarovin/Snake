using FluentAssertions;
using FluentAssertions.Execution;

namespace Snake;

public class SnakeGameTests
{
    [Test]
    public void Check_IfSnakeExists_AtPosition()
    {
        new Snake().IsEatingItselfAt((0, 0)).Should().BeTrue();
        new Snake().IsEatingItselfAt((1, 0)).Should().BeFalse();
    }

    [Test]
    public void Check_IfSnakeBodyExists_AtPosition()
    {
        var sut = new Snake();
        
        sut.Grow();
        sut.Drag();
        
        using var _ = new AssertionScope();
        sut.IsEatingItselfAt((0, 0)).Should().BeTrue();
        sut.IsEatingItselfAt((1, 0)).Should().BeTrue();
    }
}