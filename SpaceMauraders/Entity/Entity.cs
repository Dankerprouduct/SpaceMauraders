using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Threading;
using SpaceMauraders.Utilities;
using SpaceMauraders.World;

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
        public float rotation;
        public bool active = true;
        public Vector2 currentPathingTarget; 

        Thread componentThread;

        
        public Body.Body body;
        

        #region Pathing
        private List<World.Node> pathingNode;
        private World.Pathfinding pathFinding = new Pathfinding();
        private bool isPathing = false;
        private Point pathingGoal;
        Thread pathingThread;
        #endregion

        public string entityName;

        public List<Components.Component> components = new List<Components.Component>();
        public Rectangle collisionRectanlge;
        int physicsIndex; 
        public Entity()
        {
            id = nextAvailibleID;
            nextAvailibleID++;
            active = true; 

        }

        /// <summary>
        /// fires an event to the entity
        /// </summary>
        /// <param name="_event"></param>
        /// <returns>returns whether or not the event returned true or false</returns>
        public bool FireEvent(Components.Event _event)
        {
            if (_event.id == "Collider")
            {
                if (components.Count > 0)
                {
                    if (components[physicsIndex].FireEvent(_event))
                    {
                        return true;
                    }
                }
            }
            else
            {
                for (int i = 0; i < components.Count; i++)
                {
                    //Console.WriteLine(_event.id); 
                    if (components[i].FireEvent(_event))
                    {
                        //Console.WriteLine(_event.id);
                        return true;//components[i].FireEvent(_event);
                    }
                }
            }
            return false;
        }
        
        /// <summary>to the entity's list of components
        /// this will automatically be updated
        /// Adds a component 
        /// </summary>
        /// <param name="component"></param>
        public void AddComponent(Components.Component component)
        {
            components.Add(component);
            if(component is Components.PhysicsComponent)
            {
                physicsIndex = components.Count; 
            }
        }

        // TODO replace with LINQ 
        /// <summary>
        /// returns a component given a name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Components.Component GetComponent(string name)
        {
            return components.Find(c => c.componentName == name);
        }

        /// <summary>
        /// returns the vector2 position of entity
        /// </summary>
        /// <returns></returns>
        public Vector2 GetEntityPosition()
        {
            return position;
        }


        // DO NOT TOUCH UNLESS YOURE SURE YOU KNOW WHAT YOURE DOING
        #region Pathfinding
        /// <summary>
        /// Translates toward a vector 2 point 
        /// </summary>
        /// <param name="target">Point that you wish to travel to</param>
        public void MoveTo(Vector2 target)
        {
            //Console.WriteLine("moving");
            currentPathingTarget = target;
            Vector2 direction = target - position;
           
            if (direction.Length() != 0)
            {
                //Console.WriteLine("Direction: " + direction);
                direction.Normalize();
            }
            //Console.WriteLine();
            rotation = Utilities.MathHelper.RotationFromVector2(target, position);

            Components.Event velocityEvent = new Components.Event();
            velocityEvent.id = "AddVelocity";
            
            velocityEvent.parameters.Add("Velocity", direction * 2);
            this.FireEvent(velocityEvent);
        }

        /// <summary>
        /// What inherrited objects interface with
        /// if target is close enough it moves straight towards it 
        /// </summary>
        /// <param name="target"></param>
        public void FindPathTo(Vector2 target)
        {
            
            if (!isPathing)
            {
                if (!(Vector2.Distance(target, position) <= 128 * 10))
                {
                    pathingNode?.Clear();
                    Pathfind(target); 
                    isPathing = true;
                }
                else
                {                     
                    if ((Vector2.Distance(target, position) <= 128 * 3) &&
                        (Vector2.Distance(target, position) >= 128 * 1))
                    {
                        MoveTo(target);
                    }
                    
                    if (Vector2.Distance(target, position) <= 128 * 5)
                    {
                        rotation = Utilities.MathHelper.RotationFromVector2(target, position);
                    }
                    
                }
            }

            if (isPathing)
            {
                Pathfind(target);
                //Console.WriteLine("3");
                //StartPathfindingThread(target);
            }
        }
        
        /// <summary>
        /// Is a Courotine for the pathfinding algorithm,
        /// moves to the next node in the array then removes it once its too close
        /// 
        /// </summary>
        /// <param name="target"></param>
        void Pathfind(Vector2 target)
        {
            //Console.WriteLine(pathingNode + " " + "THIS IS STUPID");
            if (pathingNode != null && pathingNode.Count > 0)
            {
                Vector2 nodePosition = new Vector2((pathingNode[0].arrayPosition.X * 128) + 64, (pathingNode[0].arrayPosition.Y * 128) + 64);
                MoveTo(nodePosition);
                //Console.WriteLine(nodePosition);
                //Console.WriteLine("trying to move");

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

        /// <summary>
        ///  a new thread for A*
        /// </summary>
        /// <param name="target"></param>
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
        /// <summary>
        /// Sets the partitions indexes for the entity
        /// </summary>
        /// <param name="cellX">x partition index</param>
        /// <param name="cellY">y partition index</param>
        public void SetPartitionCell(int cellX, int cellY)
        {
            this.cellX = cellX;
            this.cellY = cellY;
            
        }

        /// <summary>
        /// Sets the index of the partition 
        /// </summary>
        /// <param name="index"></param>
        public void SetCellIndex(int index)
        {
            //Console.WriteLine("set cell index " + index); 
            cellIndex = index;
            oldCellIndex = cellIndex; 
        }

        /// <summary>
        /// Returns the current Partition
        /// </summary>
        /// <returns></returns>
        public int GetCurrentDynamicPartition()
        {
            return Game1.world.dynamicCellSpacePartition.PositionToIndex(this); 
        }

        /// <summary>
        /// Returns the center partition
        /// </summary>
        /// <returns>Center partition</returns>
        public int GetCenterPartition()
        {
            cellIndex = Game1.world.spaceStation.cellSpacePartition.PositionToIndex(this);
            int center = cellIndex;
            return center; 
        }

        /// <summary>
        /// Returns the partition right of the entity 
        /// </summary>
        /// <returns>partition right of the entity</returns>
        public int GetRightPartition()
        {
            cellIndex = Game1.world.spaceStation.cellSpacePartition.PositionToIndex(this);
            int center = cellIndex;
            int right = center + 150;
            return right; 
        }

        /// <summary>
        /// returns the partition left of the entity
        /// </summary>
        /// <returns>partition left of the entity</returns>
        public int GetLeftPartition()
        {
            cellIndex = Game1.world.spaceStation.cellSpacePartition.PositionToIndex(this);
            int center = cellIndex;
            int left = center - 150;
            return left; 
        }

        /// <summary>
        /// returns the partition towards the bottom of the entity
        /// </summary>
        /// <returns>partition south of entity</returns>
        public int GetBottomPartition()
        {
            cellIndex = Game1.world.spaceStation.cellSpacePartition.PositionToIndex(this);
            int center = cellIndex;
            int left = center - 1;
            return left; 
        }

        /// <summary>
        /// returns the partition towards the top of the entity
        /// </summary>
        /// <returns>partition towards the top of the entity</returns>
        public int GetTopPartition()
        {
            cellIndex = Game1.world.spaceStation.cellSpacePartition.PositionToIndex(this);
            int center = cellIndex;
            int top = cellIndex + 1;
            return top; 
        }

        /// <summary>
        /// returns the southeast partition
        /// </summary>
        /// <returns>souith east partition</returns>
        public int GetBottomRightPartition()
        {
            cellIndex = Game1.world.spaceStation.cellSpacePartition.PositionToIndex(this);
            int center = cellIndex;
            int bottomRight = center + 1 + 150;
            return bottomRight; 
        }

        /// <summary>
        /// returns the bottom left partition 
        /// </summary>
        /// <returns>bottom left partition</returns>
        public int GetBottomLeftPartition()
        {
            cellIndex = Game1.world.spaceStation.cellSpacePartition.PositionToIndex(this);
            int center = cellIndex;
            int bottomLeft = center + 1 - 150;
            return bottomLeft; 
        }

        /// <summary>
        /// returns the top right partition 
        /// </summary>
        /// <returns>top right partition</returns>
        public int GetTopRightPartition()
        {
            cellIndex = Game1.world.spaceStation.cellSpacePartition.PositionToIndex(this);
            int center = cellIndex;
            int topRight = center + 150 - 1;
            return topRight; 
        }

        /// <summary>
        /// returns the top left partition
        /// </summary>
        /// <returns>top left partition</returns>
        public int GetTopLeftPartition()
        {
            cellIndex = Game1.world.spaceStation.cellSpacePartition.PositionToIndex(this);
            int center = cellIndex;
            int topLeft = center - 150 - 1;
            return topLeft; 
        }
        #endregion

        /// <summary>
        /// Update function that is called by the Cell Space Partitions
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update(GameTime gameTime)
        {

            if (cellIndex != oldCellIndex)
            {
                Game1.world.dynamicCellSpacePartition.ChangeCell(this);
                cellIndex = GetCenterPartition();
                SetCellIndex(cellIndex);
            }
            
            UpdateComponents(gameTime); 
        }

        /// <summary>
        /// A helper update funtion. use as needed as long as the entity calling it is being updated
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="entity"></param>
        public virtual void Update(GameTime gameTime, Entity entity)
        {

        }

        /// <summary>
        /// returns a SaveTemplate of the entity 
        /// </summary>
        /// <returns>Save Template</returns>
        public Utilities.EntitySaveTemplate<Entity> Save()
        {
            return new EntitySaveTemplate<Entity>()
            {
                name = entityName,
                position = GetEntityPosition(),
                rotation = this.rotation,
                body = this.body,
                components = this.components,
                data = this
            };
        }

        /// <summary>
        /// updates all of the components in componetns
        /// </summary>
        /// <param name="gameTime"></param>
        public void UpdateComponents(GameTime gameTime)
        {
            for (int i = 0; i < components.Count(); i++)
            {

                components[i].Update(gameTime, this);
            }
        }

        /// <summary>
        /// gets the center of the collision rectangle 
        /// </summary>
        /// <returns></returns>
        public Vector2 GetCenter()
        {
            return collisionRectanlge.Center.ToVector2(); 
        }

        /// <summary>
        /// a debug utility used to draw the nodes currently being used for pathing
        /// </summary>
        public void DrawPathingNodes()
        {
            if (pathingNode != null)
            {
                if (pathingNode.Count > 0 && pathingNode != null)
                {
                    GUI.GUI.DrawLine(position, new Vector2(pathingGoal.X * 128, pathingGoal.Y * 128), 10, Color.Blue);
                    GUI.GUI.DrawLine(position, new Vector2(pathingNode[0].arrayPosition.X * 128, pathingNode[0].arrayPosition.Y * 128), 10, Color.Yellow);
                }
            }
            pathFinding.DrawSets();
        }
        
        /// <summary>
        /// Draws the entity
        /// base draws the body
        /// </summary>
        /// <param name="spriteBatch"></param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (body != null)
            {
                body.Draw(spriteBatch);
            }
        }

        /// <summary>
        /// sets active state of the entity to 0
        /// marks it to be deleted
        /// </summary>
        public virtual void Detroy()
        {
            Console.WriteLine("Destroying entity " + id);
            active = false; 
        }

        /// <summary>
        /// A helper funtion for draw
        /// </summary>
        public virtual void Draw()
        {

        }

    }
}
