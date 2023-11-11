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
}