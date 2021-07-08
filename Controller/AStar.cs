namespace PathFinder
{
    public class AStar : Searching
    {
        public AStar(Finder finder, char[,] maze)
        :base(finder, maze)
        { }
        public override char[,] ToSolve()
        {
            throw new System.NotImplementedException();
        }
    }
}