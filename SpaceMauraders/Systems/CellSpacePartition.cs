using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceMauraders.Entity;
using SpaceMauraders.Utilities;


namespace SpaceMauraders.Systems
{
    public class CellSpacePartition
    {
        public struct DynamicCell
        {
            public List<Entity.Entity> members; 
            
            public void AddEntity(Entity.Entity entity)
            {
                if (members != null)
                {

                    if (entity is Player == false)
                    {
                        members.Add(entity);
                    }
                    //Console.WriteLine("added " + entity);

                }
                else
                {
                    members = new List<Entity.Entity>
                    {
                        entity
                    };
                    //Console.WriteLine("added " + entity);

                }
            }

            public void RemoveEntity(Entity.Entity entity)
            {
                if (members != null)
                {
                    for (int i = 0; i < members.Count; i++)
                    {
                        //Console.WriteLine("trying to remove entity...");
                        if (members[i].id == entity.id)
                        {
                            members.RemoveAt(i);

                             //Console.WriteLine("removed " + entity);
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
                        if (members[i].active)
                        {
                            members[i].Update(gameTime);
                        }
                        else
                        {
                            RemoveEntity(members[i]); 
                        }
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
                        GUI.GUI.DrawString(i.ToString(), members[i].position, 1, 1f, Color.Green);  
                    }
                }
            }

            public List<Entity.Entity> GetEntities()
            {
                return members; 
            }

            public List<EntitySaveTemplate<Entity.Entity>> SaveEntities()
            {
                List<EntitySaveTemplate<Entity.Entity>> temps = new List<EntitySaveTemplate<Entity.Entity>>();

                for (int i = 0; i < members.Count; i++)
                {
                    temps.Add(members[i].Save());
                }

                return temps; 
            }

            public bool FireEvent(Components.Event _event)
            {

                if (members != null && members.Count > 0)
                {
                    {
                        for (int i = 0; i < members.Count; i++)
                        {

                            if (members[i].FireEvent(_event))
                            {

                                return true;
                            }
                        }
                    }
                }

                return false; 
            }

        }

        public struct StaticCell
        {
            public int x;
            public int y; 
            public Entity.Entity[,] members; 

            public void AddEntity(Entity.Entity entity)
            {
                if (members != null)
                {
                    //members.Add(entity);
                    int x = ((int)entity.position.X % (16 * 128)) / 128;
                    int y = ((int)entity.position.Y % (16 * 128)) / 128;

                    members[x, y] = entity; 
                }
                else
                {
                    members = new Entity.Entity[16, 16];   
                    for(int y = 0; y < members.GetLength(1); y++)
                    {
                        for(int x = 0; x < members.GetLength(0); x++)
                        {
                            members[x, y] = new Entity.Entity(); 
                        }
                    }
                    x = entity.cellX;
                    y = entity.cellY;
                    AddEntity(entity);
                    
                }
            }


            public List<Entity.Entity> GetEntities()
            {
                List<Entity.Entity> tempEmEntities = new List<Entity.Entity>();

                for (int x = 0; x < members.GetLength(0); x++)
                {
                    for (int y = 0; y < members.GetLength(1); y++)
                    {
                        
                        tempEmEntities.Add(members[x,y]);
                    }
                }

                return tempEmEntities;
            }


            public Entity.Entity GetEntity(Point position)
            {
                int x = ((int)position.X % (16 * 128)) / 128;
                int y = ((int)position.Y % (16 * 128)) / 128;

                return members[x, y]; 
            }

            public Point GetEntityIndex(Point position)
            {
                int x = ((int)position.X % (16 * 128)) / 128;
                int y = ((int)position.Y % (16 * 128)) / 128;

                return new Point(x, y); 
            }

            public void Update(GameTime gameTime)
            {
                if (members != null)
                {
                    for (int y = 0; y < members.GetLength(1); y++)
                    {
                        for (int x = 0; x < members.GetLength(0); x++)
                        {
                            if (members[x, y] != null)
                            {
                                members[x, y].Update(gameTime);
                            }
                        }
                    }
                }
            }

            public void Draw(SpriteBatch spriteBatch)
            {
                if (members != null)
                {
                    for (int y = 0; y < members.GetLength(1); y++)
                    {
                        for (int x = 0; x < members.GetLength(0); x++)
                        {
                            if (members[x, y] != null)
                            {
                                members[x, y].Draw(spriteBatch);
                            }
                        }
                    }
                }
            }

            public bool FireEvent(Components.Event _event)
            {
                if (members != null)
                {
                    for (int y = 0; y < members.GetLength(1); y++)
                    {
                        for (int x = 0; x < members.GetLength(0); x++)
                        {
                            if (members[x, y] != null)
                            {
                                if (members[x, y].FireEvent(_event))
                                {
                                    return true;
                                }
                            }
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


        public DynamicCell[] dynamicCells;
        public StaticCell[] staticCells; 

        public CellSpacePartition(int cellX, int cellY, int partitionSize)
        {
            cellX /= partitionSize;
            cellY /= partitionSize;
            this.partitionSize = partitionSize;

            int cellIndex = cellX * cellY;
            cellLength = cellIndex;
            dynamicCells = new DynamicCell[cellIndex];
            staticCells = new StaticCell[cellIndex];

            for (int i = 0; i < cellIndex; i++)
            {
                dynamicCells[i] = new DynamicCell();
                staticCells[i] = new StaticCell();
                
            }

            numCellsX = cellX;
            numCellsY = cellY;

        }

        /// <summary>
        /// retrieves dynamic entities in an array
        /// </summary>
        /// <returns></returns>
        public Entity.Entity[] GetDynamicEntities()
        {
            List<Entity.Entity> temp = new List<Entity.Entity>();
            for (int i = 0; i < dynamicCells.Length; i++)
            {
                if (dynamicCells[i].members != null)
                {
                    temp.AddRange(dynamicCells[i].GetEntities());
                }
            }

            return temp.ToArray(); 
            ///lis
        }

        /// <summary>
        /// returns an array of current dynamic entities
        /// </summary>
        /// <returns></returns>
        public EntitySaveTemplate<Entity.Entity>[] SaveDynamicEntities()
        {
            List<EntitySaveTemplate<Entity.Entity>> temp = new List<EntitySaveTemplate<Entity.Entity>>();
            for (int i = 0; i < dynamicCells.Length; i++)
            {
                if (dynamicCells[i].members != null)
                {
                    temp.AddRange(dynamicCells[i].SaveEntities());
                }
            }

            return temp.ToArray();
        }

        // adds entity to appropriate static cell
        public void AddStaticEntity(Entity.Entity entity)
        {
            entity.SetPartitionCell(PositionToCell(entity).X, PositionToCell(entity).Y);
            entity.SetCellIndex(PositionToIndex(entity));
            staticCells[entity.cellIndex].AddEntity(entity);

        }

        // adds entity to appropriate cell
        public void AddEntity(Entity.Entity entity)
        { 

            entity.SetPartitionCell(PositionToCell(entity).X, PositionToCell(entity).Y);
            entity.SetCellIndex(PositionToIndex(entity));
            dynamicCells[PositionToIndex(entity)].AddEntity(entity);
            Console.WriteLine("Added dynamic entity of type " + entity.GetType()); 
        }

        public void LoadEntity(Entity.Entity entity)
        {
            dynamicCells[PositionToIndex(entity)].AddEntity(entity);
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
            //Console.WriteLine("changing cell"); 

            if (EntityWithinBounds(entity.cellIndex))
            {
                if (dynamicCells[entity.cellIndex].members != null)
                {
                    dynamicCells[entity.oldCellIndex].RemoveEntity(entity);

                    int i = PositionToIndex(entity);
                    if (i >= 0 && i < cellLength)
                    {
                        dynamicCells[PositionToIndex(entity)].AddEntity(entity);

                    }
                }
                else
                {
                    dynamicCells[entity.cellIndex].members = new List<Entity.Entity>();
                    ChangeCell(entity);
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

        public Point PositionToCell(Vector2 position)
        {
            int cellX = (int)(position.X / (partitionSize * ((partitionSize * partitionSize) * 8)) / partitionSize);
            int cellY = (int)(position.Y / (partitionSize * ((partitionSize * partitionSize) * 8)) / partitionSize);
            return new Point(cellX, cellY);
        }

        public bool EntityWithinBounds(int checkedCell)
        {
            if (checkedCell >= 0 && checkedCell < cellLength)
            {
                return true;
            }
            return false;
        }

        // updates current cell
        public void Update(GameTime gameTime)
        {

        }

    }
}
