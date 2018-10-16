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
        public int speed = 5; 

        public PhysicsComponent(int parentID) : base(parentID)
        {
            
        }

        public override void Update(GameTime gameTime, Entity.Entity entity)
        {
            entity.position += velocity;
            velocity *= .85f;
            
        }

        void CheckCollisionInMovementDirection(Entity.Entity entity)
        {

            // right
            if (entity.velocity.X >= 0)
            {
                CheckCollisionInPartitionNumber(entity, entity.GetRightPartition());
                CheckCollisionInPartitionNumber(entity, entity.GetTopRightPartition());
                CheckCollisionInPartitionNumber(entity, entity.GetBottomRightPartition());
            }

            //left
            if (entity.velocity.X < 0)
            {
                CheckCollisionInPartitionNumber(entity, entity.GetLeftPartition());
                CheckCollisionInPartitionNumber(entity, entity.GetBottomLeftPartition());
                CheckCollisionInPartitionNumber(entity, entity.GetTopLeftPartition());
            }

            // down
            if (entity.velocity.Y >= 0)
            {
                CheckCollisionInPartitionNumber(entity, entity.GetBottomPartition());
                CheckCollisionInPartitionNumber(entity, entity.GetBottomLeftPartition());
                CheckCollisionInPartitionNumber(entity, entity.GetBottomRightPartition());
            }

            // up
            if (entity.velocity.Y < 0)
            {
                CheckCollisionInPartitionNumber(entity, entity.GetTopPartition());
                CheckCollisionInPartitionNumber(entity, entity.GetTopLeftPartition());
                CheckCollisionInPartitionNumber(entity, entity.GetTopRightPartition());
            }

        }

        public override bool FireEvent(Event _event)
        {
            if (_event.id == "move")
            {

                foreach(KeyValuePair<string, object> parameters in _event.parameters)
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
        }

        public bool CheckCollisionInPartitionNumber(Entity.Entity entity, int partitionNumber)
        {
            /// ** FUTURE PLANS AFTER TESTING **
            /// main "univererse" partition
            /// secondary space station partition
            /// -----------------------------------
            /// update (currentUniverseCell)
            ///     update(membersInUniverseCell)  these can have their own partitions \    
            ///         update(performPhysicsChecksOnMembersInUniverseCell)


            // for now were just updating the one space station
            entity.collisionRectanlge = new Rectangle((int)entity.position.X, (int)entity.position.Y,
                Utilities.TextureManager.sprites[0].Width,
                Utilities.TextureManager.sprites[0].Height); 

            Event physicsEvent = new Event();
            physicsEvent.id = "Collider";
            physicsEvent.parameters.Add("rectangle", entity.collisionRectanlge);

            return Game1.world.FireGlovalEvent(physicsEvent); 
                        
        }
        


        
    }
}
