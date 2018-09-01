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
        public Vector2 velocity; 

        public Entity()
        {
            id = nextAvailibleID;
            nextAvailibleID++; 

        }

        public virtual void Update()
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }

    }
}
