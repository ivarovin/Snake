using FluentAssertions;
using FluentAssertions.Execution;
using static Snake.SnakeGame;

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
        NewGame.Tick().Snake.First().X.Should().Be(1);
    }

    [Test]
    public void SnakeTurnsLeft()
    {
        NewGame.TurnLeft().MoveSnake().Snake.First().X.Should().Be(0);
        NewGame.TurnLeft().MoveSnake().Snake.First().Y.Should().Be(1);
    }

    [Test]
    public void SnakeTurnsRight()
    {
        NewGame.TurnRight().MoveSnake().Snake.First().X.Should().Be(0);
        NewGame.TurnRight().MoveSnake().Snake.First().Y.Should().Be(-1);
    }

    [Test]
    public void SnakeTurnsRightTwice()
    {
        NewGame.TurnRight().TurnRight().MoveSnake().Snake.First().X.Should().Be(-1);
        NewGame.TurnRight().TurnRight().MoveSnake().Snake.First().Y.Should().Be(0);
    }

    [Test]
    public void TurnLeftTwice()
    {
        NewGame.TurnLeft().TurnLeft().MoveSnake().Snake.First().X.Should().Be(-1);
        NewGame.TurnLeft().TurnLeft().MoveSnake().Snake.First().Y.Should().Be(0);
    }

    [Test]
    public void Grow()
    {
        NewGame.GrowSnake().Snake.Count.Should().Be(2);
    }

    [Test]
    public void Snake_BodyFollows_LastHeadPosition()
    {
        CreateWithFruitAt((1, 0)).Tick().Snake.ElementAt(1).Should().Be((Coordinate)(0, 0));
    }

    [Test]
    public void Snake_Drags_ItsBody()
    {
        var sut = NewGame;

        for (var i = 0; i < 3; i++)
        {
            sut.Fruit = (i + 1, 0);
            sut = sut.Tick();
        }

        sut.Snake.First().X.Should().Be(3);
        sut.Snake.ElementAt(1).Should().Be((Coordinate)(2, 0));
        sut.Snake.ElementAt(2).Should().Be((Coordinate)(1, 0));
    }

    [Test]
    public void SnakeGrows_WhenEats_Fruit()
    {
        CreateWithFruitAt((1, 0)).Tick().Snake.Count.Should().Be(2);
    }

    [Test]
    public void SnakeLength_IsOne_ByDefault()
    {
        NewGame.Snake.Count.Should().Be(1);
    }

    [Test]
    public void DieBy_EatingItself()
    {
        var sut = NewGame;

        for (var i = 0; i < 5; i++)
        {
            sut.Fruit = (i + 1, 0);
            sut = sut.Tick();
        }

        for (var i = 0; i < 3; i++)
            sut = sut.TurnLeft().Tick();

        sut.GameOver.Should().BeTrue();
    }

    [Test]
    public void GrowSnake_FromTail()
    {
        var sut = NewGame;

        for (var i = 0; i < 5; i++)
        {
            sut.Fruit = (i + 1, 0);
            sut = sut.Tick();
        }

        sut.Snake.First().Should().Be((Coordinate)(5, 0));
        sut.Snake.Last().Should().Be((Coordinate)(0, 0));
    }

    [Test]
    public void Grow_BeforeMoving()
    {
        CreateWithFruitAt((1, 0)).Tick().Snake.First().Should().Be((Coordinate)(1, 0));
        CreateWithFruitAt((1, 0)).Tick().Snake.ElementAt(1).Should().Be((Coordinate)(0, 0));
    }

    [Test]
    public void Check_IfSnakeExists_AtPosition()
    {
        NewGame.IsEatingItselfAt((0, 0)).Should().BeFalse();
        NewGame.IsEatingItselfAt((1, 0)).Should().BeFalse();
    }

    [Test]
    public void Snake_CannotEat_ItsOwnHead()
    {
        using var _ = new AssertionScope();
        CreateWithFruitAt((1, 0)).Tick().IsEatingItselfAt((0, 0)).Should().BeTrue();
        CreateWithFruitAt((1, 0)).Tick().IsEatingItselfAt((1, 0)).Should().BeFalse();
    }

    [Test]
    public void GrowFruit_InNewPosition_WhenEaten()
    {
        CreateWithFruitAt((1, 0)).Tick().Fruit.Should().NotBe((1, 0));
    }

    [Test]
    public void Die_WhenReach_Edge()
    {
        var sut = NewGame;

        for (var i = 0; i < 11; i++)
            sut = sut.Tick();

        sut.GameOver.Should().BeTrue();
    }

    [Test]
    public void Fruit_CannotBeCultivated_AtSnakePosition()
    {
        CreateWithFruitAt((1, 0)).Tick().CanCultivateAt((0, 0)).Should().BeFalse();
        CreateWithFruitAt((1, 0)).Tick().CanCultivateAt((1, 0)).Should().BeFalse();
        CreateWithFruitAt((1, 0)).Tick().CanCultivateAt((1, 1)).Should().BeTrue();
    }

    [Test]
    public void Fruit_CannotBeCultivated_OutsideMap()
    {
        NewGame.CanCultivateAt((11, 0)).Should().BeFalse();
        NewGame.CanCultivateAt((-11, 0)).Should().BeFalse();
        NewGame.CanCultivateAt((0, 11)).Should().BeFalse();
        NewGame.CanCultivateAt((0, -11)).Should().BeFalse();
        NewGame.CanCultivateAt((5, -5)).Should().BeTrue();
    }

    [Test]
    public void Fruit_IsNotCultivated_UntilPosition_IsValid()
    {
        NewGame
            .Cultivate(new StubGardener((0, 0), (1, 0)))
            .Fruit.Should().Be((Coordinate)(1, 0));
    }
}