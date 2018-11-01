using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Threading;

namespace SpaceMauraders.Entity
{
    public class Entity
    {

        public static int nextAvailibleID = 0;

        public int id;
        public Vector2 position;
        public Vector2 oldPosition;
        public int cellX;
        public int cellY;
        public int cellIndex; // holds current cell
        public int oldCellIndex;

        Thread componentThread; 

        #region Pathing
        public List<World.Node> pathingNode;
        public World.Pathfinding pathFinding;
        public bool isPathing = false;
        public Point pathingGoal;
        public Utilities.Raycast raycast;
        Thread pathingThread;
        #endregion

        public string entityName;

        public List<Components.Component> components = new List<Components.Component>();
        public Rectangle collisionRectanlge;

        public Entity()
        {
            id = nextAvailibleID;
            nextAvailibleID++;


        }

        public bool FireEvent(Components.Event _event)
        {
            for (int i = 0; i < components.Count; i++)
            {

                //Console.WriteLine(_event.id); 
                if (components[i].FireEvent(_event))
                {
                    //Console.WriteLine(_event.id);
                    return components[i].FireEvent(_event);
                }
            }

            return false;
        }


        public void AddComponent(Components.Component component)
        {
            components.Add(component);
        }

        public Components.Component GetComponent(string name)
        {
            for(int i = 0; i < components.Count; i++)
            {
                if(components[i].componentName == name)
                {
                    return components[i]; 
                }
            }

            return new Components.Component(); 
        }

        public Vector2 GetEntityPosition()
        {
            return position;
        }


        // DO NOT TOUCH UNLESS YOURE SURE YOU KNOW WHAT YOURE DOING
        #region Pathfinding
        public void MoveTo(Vector2 target)
        {
            Vector2 direction = target - position;
            if (direction.Length() != 0)
            {
                direction.Normalize();
            }

            Components.Event velocityEvent = new Components.Event();
            velocityEvent.id = "AddVelocity";
            velocityEvent.parameters.Add("Velocity", direction * 2);
            FireEvent(velocityEvent);

        }

        public void FindPathTo(Vector2 target)
        {
            raycast = new Utilities.Raycast(position, target);

            if (!isPathing)
            {
                //(Vector2.Distance(target, position) <= 128 * 5)
                // !raycast.MakeRay(this, 10 * 128, 20)
                if (!(Vector2.Distance(target, position) <= 128 * 10))
                {
                    if (pathingNode != null)
                    {
                        pathingNode.Clear();
                    }
                    Pathfind(target); 
                    //tartPathfindingThread(target);
                    isPathing = true;
                }
                else
                {// dedicate thread =. path 
                    if ((Vector2.Distance(target, position) <= 128 * 3) &&
                        (Vector2.Distance(target, position) >= 128 * 1))
                    {
                        MoveTo(target);
                    }
                }
            }

            if (isPathing)
            {
                Pathfind(target);
                //StartPathfindingThread(target);
            }
        }
        
        void StartPathfindingThread(Vector2 target)
        {
            
            //pathingThread = new Thread(() => Pathfind(target));
            //if (!pathingThread.IsAlive)
            //{
            //    pathingThread.Start();
            //}
            //pathingThread.Join();
            
        }

        void Pathfind(Vector2 target)
        {
            if (pathingNode != null && pathingNode.Count > 0)
            {
                Vector2 nodePosition = new Vector2((pathingNode[0].arrayPosition.X * 128) + 64, (pathingNode[0].arrayPosition.Y * 128) + 64);
                MoveTo(nodePosition);


                if (Vector2.Distance(position, new Vector2((pathingNode[0].arrayPosition.X * 128) + 64, (pathingNode[0].arrayPosition.Y * 128) + 64)) < 64)
                {
                    pathingNode.RemoveAt(0);

                }
            }
            else
            {
                
                pathingThread = new Thread(() => StartPathThread(target));
                if (!pathingThread.IsAlive)
                {
                    pathingThread.Start();
                }
                pathingThread.Join();
                //StartPathThread(target); 
                
                //StartPathfindingThread(target); 
            }
            
        }

        void StartPathThread(Vector2 target)
        {
            isPathing = false;
            // Start Node
            World.Node startNode = new World.Node(new Point((int)position.X / 128, (int)position.Y / 128));
            startNode.arrayPosition = new Point((int)position.X / 128, (int)position.Y / 128);


            // Goal Node
            pathingGoal = new Point((int)target.X / 128, (int)target.Y / 128);
            World.Node goalNode = new World.Node(new Point((int)pathingGoal.X, (int)pathingGoal.Y));
            goalNode.arrayPosition = pathingGoal;


            pathingNode = pathFinding.FindPath(startNode, goalNode);

        }
        #endregion

        #region Partition Methods
        public void SetPartitionCell(int cellX, int cellY)
        {
            this.cellX = cellX;
            this.cellY = cellY;
            
        }

        public void SetCellIndex(int index)
        {
            //Console.WriteLine("set cell index " + index); 
            cellIndex = index;
            oldCellIndex = cellIndex; 
        }

        public int GetCurrentDynamicPartition()
        {
            return Game1.world.dynamicCellSpacePartition.PositionToIndex(this); 
        }

        public int GetCenterPartition()
        {
            cellIndex = Game1.world.spaceStation.cellSpacePartition.PositionToIndex(this);
            int center = cellIndex;
            return center; 
        }

        public int GetRightPartition()
        {
            cellIndex = Game1.world.spaceStation.cellSpacePartition.PositionToIndex(this);
            int center = cellIndex;
            int right = center + 150;
            return right; 
        }

        public int GetLeftPartition()
        {
            cellIndex = Game1.world.spaceStation.cellSpacePartition.PositionToIndex(this);
            int center = cellIndex;
            int left = center - 150;
            return left; 
        }

        public int GetBottomPartition()
        {
            cellIndex = Game1.world.spaceStation.cellSpacePartition.PositionToIndex(this);
            int center = cellIndex;
            int left = center - 1;
            return left; 
        }

        public int GetTopPartition()
        {
            cellIndex = Game1.world.spaceStation.cellSpacePartition.PositionToIndex(this);
            int center = cellIndex;
            int top = cellIndex + 1;
            return top; 
        }

        public int GetBottomRightPartition()
        {
            cellIndex = Game1.world.spaceStation.cellSpacePartition.PositionToIndex(this);
            int center = cellIndex;
            int bottomRight = center + 1 + 150;
            return bottomRight; 
        }

        public int GetBottomLeftPartition()
        {
            cellIndex = Game1.world.spaceStation.cellSpacePartition.PositionToIndex(this);
            int center = cellIndex;
            int bottomLeft = center + 1 - 150;
            return bottomLeft; 
        }

        public int GetTopRightPartition()
        {
            cellIndex = Game1.world.spaceStation.cellSpacePartition.PositionToIndex(this);
            int center = cellIndex;
            int topRight = center + 150 - 1;
            return topRight; 
        }

        public int GetTopLeftPartition()
        {
            cellIndex = Game1.world.spaceStation.cellSpacePartition.PositionToIndex(this);
            int center = cellIndex;
            int topLeft = center - 150 - 1;
            return topLeft; 
        }
        #endregion

        public virtual void Update(GameTime gameTime)
        {
            
            UpdateComponents(gameTime); 
        }

        public void UpdateComponents(GameTime gameTime)
        {
            for (int i = 0; i < components.Count(); i++)
            {

                components[i].Update(gameTime, this);
            }
        }

        public Vector2 GetCenter()
        {
            return collisionRectanlge.Center.ToVector2(); 
        }
        
        

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            
        }

    }
}
