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
}

public class Snake
{
    public int X { get; private set; }
    
    public void Move()
    {
        X++;
    }
}