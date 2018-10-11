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
        public Vector2 velocity;
        public int cellX;
        public int cellY;
        public int cellIndex; // holds current cell 

        public Entity()
        {
            id = nextAvailibleID;
            nextAvailibleID++; 

        }

        public Vector2 GetEntityPosition()
        {
            return position; 
        }

        public void SetPartitionCell(int cellX, int cellY)
        {
            this.cellX = cellX;
            this.cellY = cellY; 
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            
        }

    }
}
