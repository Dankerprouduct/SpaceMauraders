using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics; 

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



        public Vector2 GetEntityPosition()
        {
            return position; 
        }

        #region Partition Methods
        public void SetPartitionCell(int cellX, int cellY)
        {
            this.cellX = cellX;
            this.cellY = cellY; 
        }


        public int GetCenterPartition()
        {
            int center = cellIndex;
            return center; 
        }

        public int GetRightPartition()
        {
            int center = cellIndex;
            int right = center + 150;
            return right; 
        }

        public int GetLeftPartition()
        {
            int center = cellIndex;
            int left = center - 150;
            return left; 
        }

        public int GetBottomPartition()
        {
            int center = cellIndex;
            int left = center - 1;
            return left; 
        }

        public int GetTopPartition()
        {
            int center = cellIndex;
            int top = cellIndex + 1;
            return top; 
        }

        public int GetBottomRightPartition()
        {
            int center = cellIndex;
            int bottomRight = center + 1 + 150;
            return bottomRight; 
        }

        public int GetBottomLeftPartition()
        {
            int center = cellIndex;
            int bottomLeft = center + 1 - 150;
            return bottomLeft; 
        }

        public int GetTopRightPartition()
        {
            int center = cellIndex;
            int topRight = center + 150 - 1;
            return topRight; 
        }

        public int GetTopLeftPartition()
        {
            int center = cellIndex;
            int topLeft = center - 150 - 1;
            return topLeft; 
        }
        #endregion

        public virtual void Update(GameTime gameTime)
        {
            cellIndex = Game1.world.spaceStation.cellSpacePartition.PositionToIndex(this);

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
