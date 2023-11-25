using System;

namespace Snake
{
    public readonly struct Coordinate
    {
        readonly (int x, int y) whereIs;
        public int X => whereIs.x;
        public int Y => whereIs.y;
        Coordinate((int x, int y) whereIs) => this.whereIs = whereIs;
        public static implicit operator (int x, int y)(Coordinate coordinate) => coordinate.whereIs;
        public static implicit operator Coordinate((int x, int y) whereIs) => new Coordinate(whereIs);
    
        public static Coordinate operator +(Coordinate a, Coordinate b) => (a.X + b.X, a.Y + b.Y);
    
        public static Coordinate RightDirectionOf(Coordinate direction)
            => direction.whereIs switch
            {
                (1, 0) => (0, -1),
                (0, -1) => (-1, 0),
                (-1, 0) => (0, 1),
                (0, 1) => (1, 0),
                _ => throw new Exception("Invalid direction")
            };
    
        public static bool IsInsideMap(Coordinate position, int mapSize)
            => position.X > mapSize || position.X < -mapSize || position.Y > mapSize || position.Y < -mapSize;
        public static Coordinate Origin => (0, 0);
    }
}