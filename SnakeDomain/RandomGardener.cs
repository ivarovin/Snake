using System;

namespace Snake
{
    public class RandomGardener : Gardener
    {
        readonly int mapSize;
        public RandomGardener(int mapSize) => this.mapSize = mapSize;

        public (int x, int y) Cultivate() 
            => (new Random().Next(-mapSize, mapSize), new Random().Next(-mapSize, mapSize));
    }
}