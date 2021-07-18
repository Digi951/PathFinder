using System;
using System.Collections.Generic;
using System.Linq;
using PathFinder.Model;

namespace PathFinder
{
    public class AStar : Searching
    {
        bool[,] _closed;
        List<AStarNode> _open = new List<AStarNode>();
        List<AStarNode> _reconstructPath = new List<AStarNode>();

        public AStar(Finder finder, char[,] maze)
        :base(finder, maze)
        { 
            _closed = new bool[_maze.GetLength(0), _maze.GetLength(1)];
            _open.Add(new AStarNode
            {
                CurrentCoordinate = GetStartingPoint,
                HCosts = GetHeuristicCosts(GetStartingPoint),
                GCosts = 0
            });         
        }
        public override char[,] ToSolve()
        {
            FindExit();
            ReconstructPath();
            return _maze;
        }

        private void FindExit()
        {           
            var currentCoordinateList = _open.Where(x => x.FCosts == _open.Min(x => x.FCosts)).ToList();  
            var currentCoordinate = currentCoordinateList.Where(x => x.HCosts == currentCoordinateList.Min(x => x.HCosts)).First();
            (int Row, int Column) neighbourCoordinate = ((int, int)) GetStartingPoint;            
            var foundExit = false;

            while (neighbourCoordinate.Row <= _maze.GetLength(0) && neighbourCoordinate.Column <= _maze.GetLength(1) 
                && _open.Count > 0 && !foundExit)
            {
                currentCoordinate = _open.OrderBy(x => x.FCosts).ThenBy(x => x.HCosts).First();
                _reconstructPath.Add(currentCoordinate);
                
                _closed[currentCoordinate.CurrentCoordinate.Row, currentCoordinate.CurrentCoordinate.Column] = true;
                _open.Remove(_open.OrderBy(x => x.FCosts).ThenBy(x => x.HCosts).First());  

                if (_maze[currentCoordinate.CurrentCoordinate.Row, currentCoordinate.CurrentCoordinate.Column] != 'S')
                {
                    _maze[currentCoordinate.CurrentCoordinate.Row, currentCoordinate.CurrentCoordinate.Column] = '.';
                }

                //(-1, -1) (-1, 0) (-1, 1)
                //(0, -1)  (0, 0)  (0, 1)
                //(1, -1)  (1, 0)  (1, 1)
                var directionRow = new int[] {-1, -1, 0, 1, 1, 1, 0, -1};
                var directionColumn = new int[] {0, 1, 1, 1, 0, -1, -1, -1};

                for (int i = 0; i < 8; i++)
                {
                    neighbourCoordinate = (currentCoordinate.CurrentCoordinate.Row + directionRow[i], currentCoordinate.CurrentCoordinate.Column + directionColumn[i]);                     

                    if (neighbourCoordinate.Row < 0 || neighbourCoordinate.Column < 0) continue;
                    if (neighbourCoordinate.Row >= _maze.GetLength(0) || neighbourCoordinate.Column >= _maze.GetLength(1)) continue;

                    if(_maze[neighbourCoordinate.Row, neighbourCoordinate.Column] == 'E')
                    {                        
                        foundExit = true;                                    
                        break;
                    }

                    if(!_closed[neighbourCoordinate.Row, neighbourCoordinate .Column] && 
                        _maze[neighbourCoordinate.Row, neighbourCoordinate.Column] != '#' && !foundExit)
                    {
                        if(_open.Count == 0 || !_open.Exists(x => x.CurrentCoordinate.Equals(neighbourCoordinate)))
                        {
                            _open.Add(new AStarNode
                            {
                                CurrentCoordinate = neighbourCoordinate,
                                ParentCoordinate = currentCoordinate.CurrentCoordinate,
                                HCosts = GetHeuristicCosts(neighbourCoordinate),
                                GCosts = GetAbsolvedCost(neighbourCoordinate)
                            });
                        }
                    }
                }
            }
        }

        private int GetHeuristicCosts((int Row, int Column) currentCoordinate)
        {
            return GetCosts(currentCoordinate, GetEndPoint);
        }

        private int GetAbsolvedCost((int Row, int Column) currentCoordinate)
        {
            return GetCosts(currentCoordinate, GetStartingPoint);
        }

        private int GetCosts((int Row, int Column) currentCoordinate, (int Row, int Column) targetCoordinate)
        {
            //This costs will be calculated horizontal and vertival by 10 per square and vertically by 14
            //14 10 14
            //10 S  10
            //14 10 14

            var diffRow = Math.Abs(currentCoordinate.Row - targetCoordinate.Row); 
            var diffColumn = Math.Abs(currentCoordinate.Column - targetCoordinate.Column);
            var amountDiagonals = diffRow <= diffColumn ? diffRow : diffColumn;
            var amountStraight = Math.Abs(diffRow - diffColumn);

            var result = amountStraight * 10 + amountDiagonals * 14;
                
            return result;
        }

        private void ReconstructPath()
        {
            var listItem = _reconstructPath.Last();

            (int Row, int Column) currentCoordinate = listItem.CurrentCoordinate;
            (int Row, int Column) parentCoordinate = listItem.ParentCoordinate;
            _maze[listItem.CurrentCoordinate.Row, listItem.CurrentCoordinate.Column] = 'O';

            while (parentCoordinate != GetStartingPoint)
            {
                listItem = _reconstructPath.Find(x => x.CurrentCoordinate.Equals(parentCoordinate));
                currentCoordinate = listItem.CurrentCoordinate;
                parentCoordinate = listItem.ParentCoordinate;
                _maze[listItem.CurrentCoordinate.Row, listItem.CurrentCoordinate.Column] = 'O';
            }
        }
    }
}