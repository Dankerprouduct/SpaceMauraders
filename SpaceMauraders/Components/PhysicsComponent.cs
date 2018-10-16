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
        
        
        public PhysicsComponent(int parentID) : base(parentID)
        {
            
        }

        public override void Update(GameTime gameTime, Entity.Entity entity)
        {
            // right
            if(entity.velocity.X >= 0)
            {
                CheckCollisionInPartitionNumber(entity, entity.GetRightPartition());
                CheckCollisionInPartitionNumber(entity, entity.GetTopRightPartition());
                CheckCollisionInPartitionNumber(entity, entity.GetBottomRightPartition()); 
            }
            
            //left
            if(entity.velocity.X < 0)
            {
                CheckCollisionInPartitionNumber(entity, entity.GetLeftPartition());
                CheckCollisionInPartitionNumber(entity, entity.GetBottomLeftPartition());
                CheckCollisionInPartitionNumber(entity, entity.GetTopLeftPartition()) ;
            }
            
            // down
            if(entity.velocity.Y >=  0)
            {
                CheckCollisionInPartitionNumber(entity, entity.GetBottomPartition());
                CheckCollisionInPartitionNumber(entity, entity.GetBottomLeftPartition());
                CheckCollisionInPartitionNumber(entity, entity.GetBottomRightPartition()); 
            }
            
            // up
            if(entity.velocity.Y < 0)
            {
                CheckCollisionInPartitionNumber(entity, entity.GetTopPartition());
                CheckCollisionInPartitionNumber(entity, entity.GetTopLeftPartition());
                CheckCollisionInPartitionNumber(entity, entity.GetTopRightPartition()); 
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

            if (Game1.world.spaceStation.cellSpacePartition.cells[partitionNumber].members != null)
            {
                for (int i = 0; i < Game1.world.spaceStation.cellSpacePartition.cells[partitionNumber].members.Count; i++)
                {
                    Entity.Tile checkedTile = (Entity.Tile)Game1.world.spaceStation.cellSpacePartition.cells[partitionNumber].members[i]; 
                    if ( checkedTile.tileType == Entity.Tile.TileType.Solid )
                    {
                        // perform physics checks 

                        return entity.collisionRectanlge.Intersects(checkedTile.collisionRectanlge); 
                        
                    }
                                        
                }
            }


            return false; 
        }
        


        
    }
}
