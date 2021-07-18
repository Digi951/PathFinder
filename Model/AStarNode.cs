using System.Collections;

namespace PathFinder.Model
{
    public class AStarNode : IEnumerable
    {
        public (int Row, int Column) CurrentCoordinate {get; set;}
        public (int Row, int Column) ParentCoordinate {get; set;}
        public int HCosts { get; set; }
        public int GCosts { get; set; } 
        public int FCosts => HCosts + GCosts;

        public IEnumerator GetEnumerator()
        {
            throw new System.NotImplementedException();
        }
    }
}