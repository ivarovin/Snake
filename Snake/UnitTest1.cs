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

        sut.MoveSnake();

        sut.Snake.First().x.Should().Be(1);
    }

    [Test]
    public void SnakeTurnsLeft()
    {
        var sut = new SnakeGame();

        sut.TurnLeft();
        sut.MoveSnake();

        sut.Snake.First().x.Should().Be(0);
        sut.Snake.First().y.Should().Be(1);
    }

    [Test]
    public void SnakeTurnsRight()
    {
        var sut = new SnakeGame();

        sut.TurnRight();
        sut.MoveSnake();

        sut.Snake.First().x.Should().Be(0);
        sut.Snake.First().y.Should().Be(-1);
    }

    [Test]
    public void SnakeTurnsRightTwice()
    {
        var sut = new SnakeGame();

        sut.TurnRight();
        sut.TurnRight();
        sut.MoveSnake();

        sut.Snake.First().x.Should().Be(-1);
        sut.Snake.First().y.Should().Be(0);
    }

    [Test]
    public void TurnLeftTwice()
    {
        var sut = new SnakeGame();

        sut.TurnLeft();
        sut.TurnLeft();
        sut.MoveSnake();

        sut.Snake.First().x.Should().Be(-1);
        sut.Snake.First().y.Should().Be(0);
    }

    [Test]
    public void Grow()
    {
        var sut = new SnakeGame();

        sut.Grow();

        sut.Snake.Count.Should().Be(2);
    }

    [Test]
    public void Snake_BodyFollows_LastHeadPosition()
    {
        var sut = new SnakeGame();

        sut.Grow();
        sut.MoveSnake();

        sut.Snake.ElementAt(1).Should().Be((0, 0));
    }

    [Test]
    public void Snake_Drags_ItsBody()
    {
        var sut = new SnakeGame();

        sut.Grow();
        sut.MoveSnake();
        sut.Grow();
        sut.MoveSnake();
        sut.MoveSnake();

        sut.Snake.First().x.Should().Be(3);
        sut.Snake.ElementAt(1).Should().Be((2, 0));
        sut.Snake.ElementAt(2).Should().Be((1, 0));
    }

    [Test]
    public void SnakeGrows_WhenEats_Fruit()
    {
        var sut = new SnakeGame() { Fruit = (1, 0) };

        sut.Tick();

        sut.Snake.Count.Should().Be(2);
    }

    [Test]
    public void SnakeLength_IsOne_ByDefault()
    {
        new SnakeGame().Snake.Count.Should().Be(1);
    }

    [Test]
    public void DieBy_EatingItself()
    {
        var sut = new SnakeGame();

        for (var i = 0; i < 5; i++)
        {
            sut.Fruit = (i + 1, 0);
            sut.Tick();
        }

        for (var i = 0; i < 3; i++)
        {
            sut.TurnLeft();
            sut.Tick();
        }

        sut.GameOver.Should().BeTrue();
    }

    [Test]
    public void GrowSnake_FromTail()
    {
        var sut = new SnakeGame();

        for (var i = 0; i < 5; i++)
        {
            sut.Fruit = (i + 1, 0);
            sut.Tick();
        }

        sut.Snake.First().Should().Be((5, 0));
        sut.Snake.Last().Should().Be((0, 0));
    }

    [Test]
    public void Grow_BeforeMoving()
    {
        var sut = new SnakeGame() { Fruit = (1, 0) };

        sut.Tick();

        sut.Snake.First().Should().Be((1, 0));
        sut.Snake.ElementAt(1).Should().Be((0, 0));
    }

    [Test]
    public void Check_IfSnakeExists_AtPosition()
    {
        new SnakeGame().IsEatingItselfAt((0, 0)).Should().BeFalse();
        new SnakeGame().IsEatingItselfAt((1, 0)).Should().BeFalse();
    }

    [Test]
    public void Snake_CannotEat_ItsOwnHead()
    {
        var sut = new SnakeGame();

        sut.Grow();
        sut.MoveSnake();

        using var _ = new AssertionScope();
        sut.IsEatingItselfAt((0, 0)).Should().BeTrue();
        sut.IsEatingItselfAt((1, 0)).Should().BeFalse();
    }

    [Test]
    public void GrowFruit_InNewPosition_WhenEaten()
    {
        var sut = new SnakeGame { Fruit = (1, 0) };

        sut.Tick();

        sut.Fruit.Should().NotBe((1, 0));
    }

    [Test]
    public void Die_WhenReach_Edge()
    {
        var sut = new SnakeGame();

        for (var i = 0; i < 11; i++)
            sut.Tick();

        sut.GameOver.Should().BeTrue();
    }

    [Test]
    public void Fruit_CannotBeCultivated_AtSnakePosition()
    {
        var sut = new SnakeGame(){Fruit = (1, 0)};
        
        sut.Tick();
        
        sut.CanCultivateAt((0, 0)).Should().BeFalse();
        sut.CanCultivateAt((1, 0)).Should().BeFalse();
    }
}