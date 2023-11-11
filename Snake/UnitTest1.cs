using FluentAssertions;
using FluentAssertions.Execution;

namespace Snake;

// 1.Head moves forward
// 2.Head can turn left or right or up or down
// 3.Head can't turn back
// 4.Fruit appear on the field
// 5.Snake can eat fruit
// 7.Snake can't go through walls (dies)
// 8.Snake can't go through itself (dies)
// 9.Snake grows when eats fruit
// 10.Snake body moves with head

public class Tests
{
    [Test]
    public void SnakeMovesForward()
    {
        var sut = new Snake();

        sut.Drag();

        sut.Head.x.Should().Be(1);
    }

    [Test]
    public void SnakeTurnsLeft()
    {
        var sut = new Snake();

        sut.TurnLeft();
        sut.Drag();

        sut.Head.x.Should().Be(0);
        sut.Head.y.Should().Be(1);
    }

    [Test]
    public void SnakeTurnsRight()
    {
        var sut = new Snake();

        sut.TurnRight();
        sut.Drag();

        sut.Head.x.Should().Be(0);
        sut.Head.y.Should().Be(-1);
    }

    [Test]
    public void SnakeTurnsRightTwice()
    {
        var sut = new Snake();

        sut.TurnRight();
        sut.TurnRight();
        sut.Drag();

        sut.Head.x.Should().Be(-1);
        sut.Head.y.Should().Be(0);
    }

    [Test]
    public void TurnLeftTwice()
    {
        var sut = new Snake();

        sut.TurnLeft();
        sut.TurnLeft();
        sut.Drag();

        sut.Head.x.Should().Be(-1);
        sut.Head.y.Should().Be(0);
    }

    [Test]
    public void Grow()
    {
        var sut = new Snake();

        sut.Grow();

        sut.Count().Should().Be(2);
    }

    [Test]
    public void Snake_BodyFollows_LastHeadPosition()
    {
        var sut = new Snake();

        sut.Grow();
        sut.Drag();

        sut.ElementAt(1).Should().Be((0, 0));
    }

    [Test]
    public void Snake_Drags_ItsBody()
    {
        var sut = new Snake();

        sut.Grow();
        sut.Drag();
        sut.Grow();
        sut.Drag();
        sut.Drag();

        sut.Head.x.Should().Be(3);
        sut.ElementAt(1).Should().Be((2, 0));
        sut.ElementAt(2).Should().Be((1, 0));
    }

    [Test]
    public void SnakeGrows_WhenEats_Fruit()
    {
        var sut = new Snake() { Fruit = (1, 0) };

        sut.Move();

        sut.Count().Should().Be(2);
    }

    [Test]
    public void SnakeLength_IsOne_ByDefault()
    {
        new Snake().Count().Should().Be(1);
    }

    [Test]
    public void DieBy_EatingItself()
    {
        var sut = new Snake();

        sut.Grow();
        sut.Move();
        sut.Grow();
        sut.TurnLeft();
        sut.Move();
        sut.Grow();
        sut.TurnLeft();
        sut.Grow();
        sut.Move();
        sut.TurnLeft();
        sut.Move();
        sut.TurnLeft();
        sut.Move();

        sut.GameOver.Should().BeTrue();
    }

    [Test]
    public void Grow_BeforeMoving()
    {
        var sut = new Snake() { Fruit = (1, 0) };

        sut.Move();

        sut.Head.Should().Be((1, 0));
        sut.ElementAt(1).Should().Be((0, 0));
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
        var sut = new Snake();
        
        sut.Grow();
        sut.Drag();
        
        using var _ = new AssertionScope();
        sut.IsEatingItselfAt((0, 0)).Should().BeTrue();
        sut.IsEatingItselfAt((1, 0)).Should().BeTrue();
    }

    [Test]
    public void GrowFruit_InNewPosition_WhenEaten()
    {
        var sut = new Snake { Fruit = (1, 0) };

        sut.Move();

        sut.Fruit.Should().NotBe((1, 0));
    }
}