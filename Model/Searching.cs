using System;

namespace PathFinder
{
    public abstract class Searching
    {
        protected Finder _finder;
        protected char[,] _maze;
        private readonly (int Row, int Column) _startingPoint;
        private readonly (int Row, int Column) _endPoint;

        public Searching(Finder finder, char[,] maze)
        {
            _finder = finder;
            _maze = maze;

            _startingPoint = GetPoint('S');
            _endPoint = GetPoint('E');
        }

        public (int Row, int Column) GetStartingPoint => _startingPoint;
        public (int Row, int Column) GetEndPoint => _endPoint;
        
        public abstract char[,] ToSolve();        

        private (int Row, int Column) GetPoint(char point)
        {
            for (int i = 0; i < _maze.GetLength(0); i++)
            {
                for (int j = 0; j < _maze.GetLength(1); j++)
                {
                    if(_maze[i, j] == point)
                    {                        
                        return (i, j);
                    }
                }
            }

            return (-1, -1);
        }
    }
}