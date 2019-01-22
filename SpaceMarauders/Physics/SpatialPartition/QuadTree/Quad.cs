using System;
using Microsoft.Xna.Framework;

namespace SpaceMarauders.Physics.SpatialPartition.QuadTree
{
    public class Quad<T>
    {
        public Point topLeft;
        public Point bottomRight;

        private Node<T> node; 

        // Children of this tree
        private Quad<T> topLeftTree;
        private Quad<T> topRightTree;
        private Quad<T> botLeftTree;
        private Quad<T> botRightTree;

        public Quad()
        {
            topLeft = new Point(0, 0);
            
            bottomRight = new Point(0, 0);

            node = null;
            topLeftTree = null;
            topRightTree = null;
            botLeftTree = null;
            botRightTree = null; 
            
        }

        public Quad(Point topL, Point botR)
        {
            node = null;
            topLeft = topL;
            bottomRight = botR;

            topLeftTree = null;
            topRightTree = null;
            botLeftTree = null;
            botRightTree = null;            
        }

        public void Insert(Node<T> _node)
        {
            if (node == null)
            {
                return; 
            }

            if (!InQuad(node.pos))
            {
                return;
            }

            if (Math.Abs(topLeft.X - bottomRight.X) <= 1 &&
                Math.Abs(topLeft.Y - bottomRight.Y) <= 1)
            {
                if (node == null)
                    node = _node;
                return;
            }

            if ((topLeft.X + bottomRight.X) / 2 >= _node.pos.X)
            {
                if ((topLeft.Y + bottomRight.Y) / 2 >= _node.pos.Y)
                {

                    if (topLeftTree == null)
                    {
                        topLeftTree = new Quad<T>(
                            new Point(topLeft.X, topLeft.Y) ,
                            new Point((topLeft.X + bottomRight.X) / 2,
                                (topLeft.Y + bottomRight.Y) / 2));
                        
                    }
                    topLeftTree.Insert(_node);
                }
                else
                {
                    if (botLeftTree == null)
                    {
                        botLeftTree = new Quad<T>(
                            new Point(topLeft.X, (topLeft.Y + bottomRight.Y) / 2),
                            new Point((topLeft.X + bottomRight.X) / 2, bottomRight.Y));
                    }
                    botLeftTree.Insert(_node);
                }
            }
            else
            {
                if ((topLeft.Y + bottomRight.Y) / 2 >= _node.pos.Y)
                {
                    if (topRightTree == null)
                    {
                        topRightTree = new Quad<T>(
                            new Point((topLeft.X + bottomRight.X) / 2, topLeft.Y),
                            new Point(bottomRight.X, (topLeft.Y + bottomRight.Y) / 2));
                    }

                    topRightTree.Insert(_node); 
                }
                else
                {
                    if (botRightTree == null)
                    {
                        botRightTree = new Quad<T>(
                            new Point((topLeft.X + bottomRight.X) / 2, (topLeft.Y + bottomRight.Y) / 2),
                            new Point(bottomRight.X, bottomRight.Y));
                    }

                    botRightTree.Insert(_node); 
                }
            }

        }

        private bool InQuad(Point p)
        {
            return (p.X >= topLeft.X &&
                    p.X <= bottomRight.X &&
                    p.Y >= topLeft.Y &&
                    p.Y <= bottomRight.Y);
        }

        public Node<T> Search(Point p)
        {
            if (!InQuad(p))
            {
                return null; 
            }

            if (node != null)
            {
                return node; 
            }

            if ((topLeft.X + bottomRight.X) / 2 >= p.X)
            {
                // Indicates topLeftTree 
                if ((topLeft.Y + bottomRight.Y) / 2 >= p.Y)
                {
                    if (topLeftTree == null)
                        return null;
                    return topLeftTree.Search(p);
                }

                // Indicates botLeftTree 
                else
                {
                    if (botLeftTree == null)
                        return null;
                    return botLeftTree.Search(p);
                }
            }
            else
            {
                // Indicates topRightTree 
                if ((topLeft.Y + bottomRight.Y) / 2 >= p.Y)
                {
                    if (topRightTree == null)
                        return null;
                    return topRightTree.Search(p);
                }

                // Indicates botRightTree 
                else
                {
                    if (botRightTree == null)
                        return null;
                    return botRightTree.Search(p);
                }
            }
        }
    }
}
