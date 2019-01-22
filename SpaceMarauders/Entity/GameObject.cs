using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using  Microsoft.Xna.Framework.Graphics;

namespace SpaceMarauders.Entity
{
    public abstract class GameObject
    {
        
        public abstract void Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch spriteBatch);



    }
}
