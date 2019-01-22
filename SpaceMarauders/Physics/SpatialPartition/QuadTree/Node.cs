using Microsoft.Xna.Framework;

namespace SpaceMarauders.Physics.SpatialPartition.QuadTree
{
    public class Node<T>
    {
        public Point pos;
        public T data;

        public Node(Point _pos, T _data)
        {
            pos = _pos;
            data = _data; 
        }

        public Node()
        {
            
        }

    }
}
