using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace SpaceMauraders.Components
{
    public class PhysicsComponent : Component
    {

        public Vector2 velocity = Vector2.Zero;
        public int speed = 2;
        Vector2 position;
        public int cellIndex;
        Entity.Entity entity; 

        public PhysicsComponent(int parentID) : base(parentID)
        {
            componentName = "PhysicsComponent"; 
        }

        public PhysicsComponent()
        {
            componentName = "PhysicsComponent";
        }

        public override void Update(GameTime gameTime, Entity.Entity entity)
        {
            CheckCollisionInMovementDirection(entity);
            position = entity.position;
            cellIndex = entity.cellIndex;
            
            
            this.entity = entity; 
        }

        void CheckCollisionInMovementDirection(Entity.Entity entity)
        {

            CheckCollisionInPartitionNumber(entity);

        }

        public override bool FireEvent(Event _event)
        {
            if(_event.id == "Flee")
            {
                velocity += (Vector2)_event.parameters["SteeringForce"]; 
            }

            if (_event.id == "AddVelocity")
            {
                foreach(KeyValuePair<string, object> parameters in _event.parameters)
                {
                    if(parameters.Key == "Velocity")
                    {
                        velocity += (Vector2)parameters.Value;
                        CheckSpeed(10); 
                    }
                }
            }

            if (_event.id == "move")
            {
                foreach (KeyValuePair<string, object> parameters in _event.parameters)
                {
                    if(parameters.Key == "Move Up")
                    {
                        
                        velocity.Y  -= (int)parameters.Value;
                        
                    }

                    if (parameters.Key == "Move Down")
                    {
                        velocity.Y += (int)parameters.Value;
                        
                    }

                    if (parameters.Key == "Move Left")
                    {
                        velocity.X -= (int)parameters.Value;
                        

                    }

                    if (parameters.Key == "Move Right")
                    {
                        velocity.X += (int)parameters.Value;
                        
                    }
                }
                
            }

            

            return false; 
        }
        float maxVelocity = 5; 
        #region 
        public Vector2 Seek(Vector2 target)
        {

            Vector2 desiredVelocity = target - position;
            float distance = desiredVelocity.Length();
            desiredVelocity.Normalize();
            desiredVelocity *= maxVelocity;
            Vector2 steering = desiredVelocity - velocity;
            return steering;
        }

        public Vector2 Flee(Vector2 target)
        {

            Vector2 desiredVelocity = position - target;
            float distance = desiredVelocity.Length();
            desiredVelocity.Normalize();
            desiredVelocity *= maxVelocity;
            Vector2 steering = desiredVelocity - velocity;
            return steering;
        }

        public Vector2 Arrive(Entity.Entity target, float slowingDistance)
        {
            Vector2 desiredVelocity = target.position - position;
            float distance = desiredVelocity.Length();

            if (distance < slowingDistance)
            {
                desiredVelocity.Normalize();
                desiredVelocity *= maxVelocity * (distance / slowingDistance);
            }
            else
            {
                desiredVelocity.Normalize();
                desiredVelocity *= maxVelocity;
            }
            Vector2 steering = desiredVelocity - velocity;
            return steering;
        }

        public Vector2 Pursue(Entity.Entity target)
        {
            float distance = Vector2.Distance(target.position, position);
            float ahead = distance / 10;
            Vector2 futurePosition = target.position + ((PhysicsComponent)target.GetComponent("PhysicsComponent")).velocity * ahead;
            return Seek(futurePosition);
        }

        public Vector2 Evade(Entity.Entity target)
        {
            float distance = Vector2.Distance(target.position, position);
            float ahead = distance / 10;
            Vector2 futurePosition = target.position + ((PhysicsComponent)target.GetComponent("PhysicsComponent")).velocity * ahead;
            return Flee(futurePosition);
        }

        public Vector2 Separation()
        {

            Vector2 steeringForce = Vector2.Zero;

            if (Game1.world.EntityWithinBounds(cellIndex))
            {
                if (Game1.world.dynamicCellSpacePartition.dynamicCells[cellIndex].members != null)
                {

                    for (int i = 0; i < Game1.world.dynamicCellSpacePartition.dynamicCells[cellIndex].members.Count; i++)
                    {
                        if (Game1.world.dynamicCellSpacePartition.dynamicCells[cellIndex].members[i] != entity)
                        {
                            Vector2 toAgent = position - Game1.world.dynamicCellSpacePartition.dynamicCells[cellIndex].members[i].GetCenter();
                            Vector2 origanal = toAgent;
                            toAgent.Normalize();
                            steeringForce += toAgent / origanal.Length() * 5;
                        }
                    }
                }
            }
            if(steeringForce.Length() >= 5)
            {
                steeringForce = Vector2.Zero; 
            }
            return steeringForce;
        }
        #endregion

        void CheckSpeed(float max)
        {
            if(velocity.Length() > max)
            {
                //velocity.Normalize();
                //velocity *= max;
            }
        }

        public void CheckCollisionInPartitionNumber(Entity.Entity entity)
        {
            /// ** FUTURE PLANS AFTER TESTING **
            /// main "univererse" partition
            /// secondary space station partition
            /// -----------------------------------
            /// update (currentUniverseCell)
            ///     update(membersInUniverseCell)  these can have their own partitions \    
            ///         update(performPhysicsChecksOnMembersInUniverseCell)


            // for now were just updating the one space station

            
            entity.oldPosition = entity.position;

            velocity += Separation(); 
            velocity *= .85f;


            float j = 1.2f; 
            entity.position.X += (int)velocity.X;
            if (Game1.world.FireGlobalEvent(FireCollisionEvent(entity), entity))
            {
               

                entity.position.X = entity.oldPosition.X;
                velocity.X = -velocity.X * j;
                //Console.WriteLine("hit"); 
            }

            entity.position.Y += (int)velocity.Y;
            if (Game1.world.FireGlobalEvent(FireCollisionEvent(entity), entity))
            {
                entity.position.Y = entity.oldPosition.Y;
                velocity.Y = -velocity.Y * j;
            }


        }

        public Event FireCollisionEvent(Entity.Entity entity)
        {
            entity.collisionRectanlge = new Rectangle((int)entity.position.X, (int)entity.position.Y,
                Utilities.TextureManager.sprites[0].Width,
                Utilities.TextureManager.sprites[0].Height);
            
            Event physicsEvent = new Event
            {
                id = "Collider"
            };
            
            physicsEvent.parameters.Add("rectangle", (Rectangle)entity.collisionRectanlge);
            physicsEvent.parameters.Add("entity", (Entity.Entity)entity);
            if (entity is Entity.NPC)
            {
                physicsEvent.parameters.Add("npc", 0);
            }
            return physicsEvent; 
        }
        


        
    }
}
