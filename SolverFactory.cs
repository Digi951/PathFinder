namespace PathFinder
{
    public class SolverFactory     
    {
        protected Finder _finder;
        protected char[,] _maze;
        public SolverFactory
        (Finder finder, char[,] maze)
        {
            _finder = finder;
            _maze = maze;
        }

        public Searching GetSolver()
        {
            switch (_finder)
            {
                case Finder.BreadthFirstSearch: 
                    return new BreadthFirstSearch(_finder, _maze);
                    break;
                case Finder.DepthFirstSearch: 
                    return new DepthFirstSearch(_finder, _maze);
                    break;
                case Finder.Dijkstra:
                    return new Dijkstra(_finder, _maze);
                    break;
                case Finder.AStar:
                    return new AStar(_finder, _maze);    
                    break;
                default:
                    return null;            
            }
        }
    }
}