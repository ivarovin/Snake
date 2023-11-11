using FluentAssertions;

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

        sut.Move();

        sut.X.Should().Be(1);
    }

    [Test]
    public void SnakeTurnsLeft()
    {
        var sut = new Snake();

        sut.TurnLeft();
        sut.Move();

        sut.X.Should().Be(0);
        sut.Y.Should().Be(1);
    }

    [Test]
    public void SnakeTurnsRight()
    {
        var sut = new Snake();

        sut.TurnRight();
        sut.Move();

        sut.X.Should().Be(0);
        sut.Y.Should().Be(-1);
    }

    [Test]
    public void SnakeTurnsRightTwice()
    {
        var sut = new Snake();

        sut.TurnRight();
        sut.TurnRight();
        sut.Move();

        sut.X.Should().Be(-1);
        sut.Y.Should().Be(0);
    }

    [Test]
    public void TurnLeftTwice()
    {
        var sut = new Snake();

        sut.TurnLeft();
        sut.TurnLeft();
        sut.Move();

        sut.X.Should().Be(-1);
        sut.Y.Should().Be(0);
    }

    [Test]
    public void Grow()
    {
        var sut = new Snake();

        sut.Grow();

        sut.Length.Should().Be(1);
    }

    [Test]
    public void Snake_BodyFollows_LastHeadPosition()
    {
        var sut = new Snake();

        sut.Grow();
        sut.Move();

        sut.ElementAt(0).Should().Be((0, 0));
    }

    [Test]
    public void Snake_Drags_ItsBody()
    {
        var sut = new Snake();

        sut.Grow();
        sut.Move();
        sut.Grow();
        sut.Move();
        sut.Move();

        sut.X.Should().Be(3);
        sut.ElementAt(0).Should().Be((2, 0));
        sut.ElementAt(1).Should().Be((1, 0));
    }

    [Test]
    public void SnakeGrows_WhenEats_Fruit()
    {
        var sut = new Snake();

        sut.Move(new SnakeGame(sut) { Fruit = (1, 0) });

        sut.Length.Should().Be(1);
    }
    
    [Test]
    public void SnakeLength_IsZero_ByDefault()
    {
        new Snake().Length.Should().Be(0);
    }

    [Test]
    public void DieBy_EatingItself()
    {
        var sut = new Snake();
        var doc = new SnakeGame(sut);

        sut.Grow();
        sut.Move(doc);
        sut.TurnLeft();
        sut.Move(doc);
        sut.TurnLeft();
        sut.Move(doc);
        sut.TurnLeft();
        sut.Move(doc);
        sut.Move(doc);

        sut.IsDead.Should().BeTrue();
    }
}