using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace PathFinder
{
    public class BreadthFirstSearch : Searching
    {                
        bool[,] _visited;       
        Queue _bfsQueue = new Queue();
        int[,] _distance;
        
        public BreadthFirstSearch(Finder finder, char[,] maze)
        : base(finder, maze)
        {
            _visited = new bool[_maze.GetLength(0), _maze.GetLength(1)];
            _visited[GetStartingPoint.Row, GetStartingPoint.Column] = true;

            _distance = new int[_maze.GetLength(0), _maze.GetLength(1)];
            _distance[GetStartingPoint.Row, GetStartingPoint.Column] = 0;
            _bfsQueue.Enqueue(GetStartingPoint);            
         }

        public override char[,] ToSolve()
        {        
            FindExit();   
            ShowShortestPath();

            return _maze;
        }

        private void FindExit()
        {
            (int Row, int Column) currentCoordinate = ((int, int)) _bfsQueue.Peek();              
            (int Row, int Column) neighbourCoordinate = ((int, int)) GetStartingPoint;            
            var foundExit = false;
                 

            while (neighbourCoordinate.Row <= _maze.GetLength(0) && neighbourCoordinate.Column <= _maze.GetLength(1) && _bfsQueue.Count > 0 && !foundExit)
            {
                currentCoordinate = ((int, int)) _bfsQueue.Dequeue();                

                //North, East, South, West directions
                var directionRow = new int[] {-1, 1, 0, 0};
                var directionColumn = new int[] {0, 0, 1, -1};

                for (int i = 0; i < 4; i++)
                {
                    neighbourCoordinate = (currentCoordinate.Row + directionRow[i], currentCoordinate.Column + directionColumn[i]);                     

                    if (neighbourCoordinate.Row < 0 || neighbourCoordinate.Column < 0) continue;
                    if (neighbourCoordinate.Row >= _maze.GetLength(0) || neighbourCoordinate.Column >= _maze.GetLength(1)) continue;

                    if(_maze[neighbourCoordinate.Row, neighbourCoordinate.Column] == 'E')
                    {                        
                        foundExit = true;
                        _distance[neighbourCoordinate.Row, neighbourCoordinate.Column] += _distance[currentCoordinate.Row, currentCoordinate.Column] + 1;                
                        break;
                    }

                    if(!_visited[neighbourCoordinate.Row, neighbourCoordinate .Column] && 
                        _maze[neighbourCoordinate.Row, neighbourCoordinate.Column] != '#' && !foundExit)
                    {
                        _visited[neighbourCoordinate.Row, neighbourCoordinate.Column] = true;  

                        _distance[neighbourCoordinate.Row, neighbourCoordinate.Column] += _distance[currentCoordinate.Row, currentCoordinate.Column] + 1;                

                        if(_maze[neighbourCoordinate.Row, neighbourCoordinate.Column] != 'E')
                        {
                            _maze[neighbourCoordinate.Row, neighbourCoordinate.Column] = '.';                                                        
                        }                      
                        
                        _bfsQueue.Enqueue((neighbourCoordinate));                                                                     
                    }      
                }                    
            }
        }

        private void ShowShortestPath()
        {
            (int Row, int Column) currentCoordinate = ((int, int)) GetEndPoint;

            //North, East, South, West directions
            var directionRow = new int[] {-1, 1, 0, 0};
            var directionColumn = new int[] {0, 0, 1, -1};

            (int Row, int Column) neighbourCoordinate = ((int, int)) GetEndPoint; 

            while (currentCoordinate != GetStartingPoint)
            {
                for (int i = 0; i < 4; i++)
                {
                    neighbourCoordinate = (currentCoordinate.Row + directionRow[i], currentCoordinate.Column + directionColumn[i]);                     

                    if (neighbourCoordinate.Row < 0 || neighbourCoordinate.Column < 0) continue;
                    if (neighbourCoordinate.Row >= _maze.GetLength(0) || neighbourCoordinate.Column >= _maze.GetLength(1)) continue;

                    if (_distance[neighbourCoordinate.Row, neighbourCoordinate.Column] < _distance[currentCoordinate.Row, currentCoordinate.Column] 
                    && _maze[neighbourCoordinate.Row, neighbourCoordinate.Column] != '#' && _visited[neighbourCoordinate.Row, neighbourCoordinate.Column])
                    {
                        if(_maze[neighbourCoordinate.Row, neighbourCoordinate.Column] != 'S')
                        {
                            _maze[neighbourCoordinate.Row, neighbourCoordinate.Column] = 'O';
                        }
                        
                        currentCoordinate = neighbourCoordinate;
                    }
                }
            }
        }
    }
}