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
            
        }

        public PhysicsComponent()
        {
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
            if (_event.id == "Entity")
            {
                 //CheckCollisionInMovementDirection((Entity.Entity)_event.parameters["entity"]);
            }

            if (_event.id == "move")
            {
                foreach (KeyValuePair<string, object> parameters in _event.parameters)
                {
                    if(parameters.Key == "Move Up")
                    {
                        
                        velocity.Y  -= (int)parameters.Value;
                        CheckSpeed(velocity);
                    }

                    if (parameters.Key == "Move Down")
                    {
                        velocity.Y += (int)parameters.Value;
                        CheckSpeed(velocity);
                    }

                    if (parameters.Key == "Move Left")
                    {
                        velocity.X -= (int)parameters.Value;
                        CheckSpeed(velocity);

                    }

                    if (parameters.Key == "Move Right")
                    {
                        velocity.X += (int)parameters.Value;
                        CheckSpeed(velocity);
                    }
                }
                
            }

            

            return true; 
        }

        void CheckSpeed(Vector2 velocity)
        {
            /*
            if(velocity.X > speed)
            {
                velocity.X = speed;
            }
            if( velocity.X < -speed)
            {
                velocity.X = -speed; 
            }

            if (velocity.Y > speed)
            {
                velocity.Y = speed;
            }
            if (velocity.Y < -speed)
            {
                velocity.Y = -speed;
            }
            */
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
            if (Game1.world.FireGlobalEvent(FireCollisionEvent(entity)))
            {
                entity.position.X = entity.oldPosition.X;
                velocity.X = -velocity.X * j; 
            }

            entity.position.Y += (int)velocity.Y;
            if (Game1.world.FireGlobalEvent(FireCollisionEvent(entity)))
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

            return physicsEvent; 
        }
        


        
    }
}
