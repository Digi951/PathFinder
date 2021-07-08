namespace PathFinder
{
    public class Dijkstra : Searching
    {
        public Dijkstra(Finder finder, char[,] maze)
        :base (finder, maze)
        { }
        
        public override char[,] ToSolve()
        {
            throw new System.NotImplementedException();
        }
    }
}