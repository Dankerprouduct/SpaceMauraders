using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics; 



namespace SpaceMauraders.Systems
{
    public class CellSpacePartition
    {
        public struct Cell
        {
            public List<Entity.Entity> members; 
            
            public void AddEntity(Entity.Entity entity)
            {
                if (members != null)
                {
                    members.Add(entity);
                }
                else
                {
                    members = new List<Entity.Entity>
                    {
                        entity
                    };
                }
            }

            public void RemoveEntity(Entity.Entity entity)
            {
                if (members != null)
                {
                    for (int i = 0; i < members.Count; i++)
                    {
                        if (members[i].id == entity.id)
                        {
                            members.RemoveAt(i);
                        }
                    }
                }
            }

            public void Update(GameTime gameTime)
            {
                if (members != null)
                {
                    for (int i = 0; i < members.Count; i++)
                    {
                        members[i].Update(gameTime);
                    }
                }
            }

            public void Draw(SpriteBatch spriteBatch)
            {
                if (members != null)
                {
                    for (int i = 0; i < members.Count; i++)
                    {
                        members[i].Draw(spriteBatch); 
                    }
                }
            }

            public bool FireEvent(Components.Event _event)
            {

                if (members != null)
                {
                    for (int i = 0; i < members.Count; i++)
                    {
                        if (members[i].FireEvent(_event))
                        {
                            return true; 
                        }
                    }
                }
                return false; 
            }

        }


        public int partitionSize;
        public int cellLength;
        public int numCellsX;
        public int numCellsY;


        public Cell[] cells;

        public CellSpacePartition(int cellX, int cellY, int partitionSize)
        {
            cellX /= partitionSize;
            cellY /= partitionSize;
            this.partitionSize = partitionSize;

            int cellIndex = cellX * cellY;
            cellLength = cellIndex;
            cells = new Cell[cellIndex];

            for (int i = 0; i < cells.Length; i++)
            {
                cells[i] = new Cell();
            }

            numCellsX = cellX;
            numCellsY = cellY;

        }
        
        // adds entity to appropriate cell
        public void AddEntity(Entity.Entity entity)
        { 
            entity.SetPartitionCell(PositionToCell(entity).X, PositionToCell(entity).Y);
            cells[PositionToIndex(entity)].AddEntity(entity);
        }    
        

        // changes vector to to a 1D array index
        public int PositionToIndex(Entity.Entity entity)
        {
            return PositionToCell(entity).X * numCellsX + PositionToCell(entity).Y; 
        }

        public int PositionToIndex(Vector2 posiiton)
        {
            return PositionToCell(posiiton).X * numCellsX + PositionToCell(posiiton).Y;
        }

        // Changes cell property from one cell to another 
        public void ChangeCell(Entity.Entity entity)
        {
            if (cells[entity.cellIndex].members != null)
            {
                cells[entity.cellIndex].members.Remove(entity);
                int i = PositionToIndex(entity);
                if (i >= 0 && i < cellLength)
                {
                    cells[PositionToIndex(entity)].AddEntity(entity);
                }
            }
            
        }             

        // changes vector to to a cell point
        Point PositionToCell(Entity.Entity entity)
        {
            int cellX = (int)(entity.GetEntityPosition().X / (partitionSize * ((partitionSize * partitionSize) * 8)) / partitionSize);
            int cellY = (int)(entity.GetEntityPosition().Y / (partitionSize * ((partitionSize * partitionSize) * 8)) / partitionSize);
            return new Point(cellX, cellY); 
        }

        Point PositionToCell(Vector2 position)
        {
            int cellX = (int)(position.X / (partitionSize * ((partitionSize * partitionSize) * 8)) / partitionSize);
            int cellY = (int)(position.Y / (partitionSize * ((partitionSize * partitionSize) * 8)) / partitionSize);
            return new Point(cellX, cellY);
        }
        

        // updates current cell
        public void Update(GameTime gameTime)
        {

        }

    }
}
