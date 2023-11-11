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
}

public class Snake
{
    public int X { get; private set; }
    public int Y { get; private set; }
    (int x, int y) Direction { get; set; } = (1, 0);

    public void Move()
    {
        X += Direction.x;
        Y += Direction.y;
    }

    public void TurnLeft()
    {
        Direction = (0, 1);
    }

    public void TurnRight()
    {
        Direction = Direction switch
        {
            (1, 0) => (0, -1),
            (0, -1) => (-1, 0),
            (-1, 0) => (0, 1),
            (0, 1) => (1, 0),
            _ => throw new Exception("Invalid direction")
        };
    }
}