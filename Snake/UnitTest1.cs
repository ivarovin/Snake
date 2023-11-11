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
        var sut = new SnakeGame();

        sut.Drag();

        sut.Snake.First().x.Should().Be(1);
    }

    [Test]
    public void SnakeTurnsLeft()
    {
        var sut = new SnakeGame();

        sut.TurnLeft();
        sut.Drag();

        sut.Snake.First().x.Should().Be(0);
        sut.Snake.First().y.Should().Be(1);
    }

    [Test]
    public void SnakeTurnsRight()
    {
        var sut = new SnakeGame();

        sut.TurnRight();
        sut.Drag();

        sut.Snake.First().x.Should().Be(0);
        sut.Snake.First().y.Should().Be(-1);
    }

    [Test]
    public void SnakeTurnsRightTwice()
    {
        var sut = new SnakeGame();

        sut.TurnRight();
        sut.TurnRight();
        sut.Drag();

        sut.Snake.First().x.Should().Be(-1);
        sut.Snake.First().y.Should().Be(0);
    }

    [Test]
    public void TurnLeftTwice()
    {
        var sut = new SnakeGame();

        sut.TurnLeft();
        sut.TurnLeft();
        sut.Drag();

        sut.Snake.First().x.Should().Be(-1);
        sut.Snake.First().y.Should().Be(0);
    }

    [Test]
    public void Grow()
    {
        var sut = new SnakeGame();

        sut.Grow();

        sut.Count().Should().Be(2);
    }

    [Test]
    public void Snake_BodyFollows_LastHeadPosition()
    {
        var sut = new SnakeGame();

        sut.Grow();
        sut.Drag();

        sut.ElementAt(1).Should().Be((0, 0));
    }

    [Test]
    public void Snake_Drags_ItsBody()
    {
        var sut = new SnakeGame();

        sut.Grow();
        sut.Drag();
        sut.Grow();
        sut.Drag();
        sut.Drag();

        sut.Snake.First().x.Should().Be(3);
        sut.ElementAt(1).Should().Be((2, 0));
        sut.ElementAt(2).Should().Be((1, 0));
    }

    [Test]
    public void SnakeGrows_WhenEats_Fruit()
    {
        var sut = new SnakeGame() { Fruit = (1, 0) };

        sut.Tick();

        sut.Count().Should().Be(2);
    }

    [Test]
    public void SnakeLength_IsOne_ByDefault()
    {
        new SnakeGame().Count().Should().Be(1);
    }

    [Test]
    public void DieBy_EatingItself()
    {
        var sut = new SnakeGame();

        sut.Grow();
        sut.Tick();
        sut.Grow();
        sut.TurnLeft();
        sut.Tick();
        sut.Grow();
        sut.TurnLeft();
        sut.Grow();
        sut.Tick();
        sut.TurnLeft();
        sut.Tick();

        sut.GameOver.Should().BeTrue();
    }

    [Test]
    public void Grow_BeforeMoving()
    {
        var sut = new SnakeGame() { Fruit = (1, 0) };

        sut.Tick();

        sut.Snake.First().Should().Be((1, 0));
        sut.ElementAt(1).Should().Be((0, 0));
    }
    
    [Test]
    public void Check_IfSnakeExists_AtPosition()
    {
        new SnakeGame().IsEatingItselfAt((0, 0)).Should().BeTrue();
        new SnakeGame().IsEatingItselfAt((1, 0)).Should().BeFalse();
    }

    [Test]
    public void Check_IfSnakeBodyExists_AtPosition()
    {
        var sut = new SnakeGame();
        
        sut.Grow();
        sut.Drag();
        
        using var _ = new AssertionScope();
        sut.IsEatingItselfAt((0, 0)).Should().BeTrue();
        sut.IsEatingItselfAt((1, 0)).Should().BeTrue();
    }

    [Test]
    public void GrowFruit_InNewPosition_WhenEaten()
    {
        var sut = new SnakeGame { Fruit = (1, 0) };

        sut.Tick();

        sut.Fruit.Should().NotBe((1, 0));
    }
}