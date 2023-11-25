namespace Snake
{
    public class StubGardener : Gardener
    {
        readonly Queue<(int x, int y)> positions;
        public StubGardener(params (int x, int y)[] positions) => this.positions = new Queue<(int x, int y)>(positions);
        public (int x, int y) Cultivate() => positions.Dequeue();
    }
}