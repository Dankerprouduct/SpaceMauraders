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

        Vector2 velocity = Vector2.Zero;
        public int speed = 2; 

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
            

        }

        void CheckCollisionInMovementDirection(Entity.Entity entity)
        {

            CheckCollisionInPartitionNumber(entity);

        }

        public override bool FireEvent(Event _event)
        {

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
