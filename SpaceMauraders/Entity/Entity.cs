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
        

        public void AddComponent(Components.Component component)
        {
            components.Add(component);
            if(component is Components.PhysicsComponent)
            {
                physicsIndex = components.Count; 
            }
        }

        // TODO replace with LINQ 
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
            //Console.WriteLine(this.GetComponent("PhysicsComponent") );
            //Console.WriteLine("velocity parameter : "+ velocityEvent.parameters["Velocity"]);
            this.FireEvent(velocityEvent);
            //GetComponent("PhysicsComponent").FireEvent(velocityEvent); 
        }

        public void FindPathTo(Vector2 target)
        {
            
            //rotation = Utilities.MathHelper.RotationFromVector2(position, Game1.player.position);
            //Console.WriteLine("pathing " + isPathing);
            if (!isPathing)
            {
                //(Vector2.Distance(target, position) <= 128 * 5)
                // !raycast.MakeRay(this, 10 * 128, 20)
                //Console.WriteLine(Vector2.Distance(target, position));
                
                if (!(Vector2.Distance(target, position) <= 128 * 10))
                {
                    
                    if (pathingNode != null)
                    {
                        pathingNode.Clear();
                    }
                    Pathfind(target); 
                    //tartPathfindingThread(target);
                    isPathing = true;
                    //Console.WriteLine("1");
                }
                else
                {// dedicate thread =. path 
                    
                    if ((Vector2.Distance(target, position) <= 128 * 3) &&
                        (Vector2.Distance(target, position) >= 128 * 1))
                    {
                        MoveTo(target);
                        //Console.WriteLine("2");
                    }

                    // turn toward target if theyre close enough
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

        void StartPathThread(Vector2 target)
        {
            Console.WriteLine("I AM BEING CALLED");
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

            if (cellIndex != oldCellIndex)
            {
                Game1.world.dynamicCellSpacePartition.ChangeCell(this);
                cellIndex = GetCenterPartition();
                SetCellIndex(cellIndex);
            }
            
            UpdateComponents(gameTime); 
        }

        public virtual void Update(GameTime gameTime, Entity entity)
        {

        }

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
        

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (body != null)
            {
                body.Draw(spriteBatch);
            }
        }

        public virtual void Detroy()
        {
            Console.WriteLine("Destroying entity " + id);
            active = false; 
        }

        public virtual void Draw()
        {

        }

    }
}
