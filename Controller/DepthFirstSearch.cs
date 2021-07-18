using System;
using System.Collections;

namespace PathFinder
{
    public class DepthFirstSearch : Searching
    {
        bool[,] _visited;       
        Stack _dfsStack = new Stack();
        Queue _reconstructPath = new Queue();
        
        public DepthFirstSearch(Finder finder, char[,] maze)
        : base(finder, maze)
        { 
            _visited = new bool[_maze.GetLength(0), _maze.GetLength(1)];
            _visited[GetStartingPoint.Row, GetStartingPoint.Column] = true;
            _dfsStack.Push(GetStartingPoint);  
        }
        
        public override char[,] ToSolve()
        {        
            Backtracking(GetStartingPoint);
            ShowShortestPath();

            while (_reconstructPath.Count > 0)
            {
                Console.WriteLine(_reconstructPath.Peek());
                _reconstructPath.Dequeue();
            }

            return _maze;
        }

        private bool Backtracking((int Row, int Column) coordinate)
        {
            var row = coordinate.Row;
            var column = coordinate.Column;

            if(row >= _maze.GetLength(0) || column >= _maze.GetLength(1) || _dfsStack.Count == 0 || _maze[row, column] == 'E')
            {
                return true;
            }
            
            _dfsStack.Pop();           

            //North, East, South, West directions
                var directionRow = new int[] {-1, 0, 1, 0};
                var directionColumn = new int[] {0, 1, 0, -1};

                for (int i = 0; i < 4; i++)
                {        
                    (int Row, int Column) newCoordinate = (coordinate.Row + directionRow[i], coordinate.Column + directionColumn[i]); 

                    if (newCoordinate.Row < 0 || newCoordinate.Column < 0) continue;
                    if (newCoordinate.Row >= _maze.GetLength(0) || newCoordinate.Column >= _maze.GetLength(1)) continue;                    

                    if(!_visited[newCoordinate.Row, newCoordinate.Column] && _maze[newCoordinate.Row, newCoordinate.Column] != '#')
                    {
                        _visited[newCoordinate.Row, newCoordinate.Column] = true;                  

                        if(_maze[newCoordinate.Row, newCoordinate.Column] != 'E')
                        {
                            _maze[newCoordinate.Row, newCoordinate.Column] = '.'; 
                            Output.PrintMaze(_maze);                             
                        }                                            
                        
                        _dfsStack.Push((newCoordinate.Row, newCoordinate.Column));
                        
                        Console.WriteLine($"New Coordinate: {newCoordinate}");                        

                        if (Backtracking(newCoordinate))
                        {
                            _reconstructPath.Enqueue(newCoordinate);
                            return true;
                        }                        
                    }                          
                }    

            return false;        
        }

        private void ShowShortestPath()
        {
            while (_reconstructPath.Count > 0)
            {
                (int Row, int Column) currentCoordinate = ((int,int))_reconstructPath.Peek();

                if(_maze[currentCoordinate.Row, currentCoordinate.Column] != 'E')
                {
                    _maze[currentCoordinate.Row, currentCoordinate.Column] = 'O';                            
                }  
                
                _reconstructPath.Dequeue();            
            }            
        }
    }
}