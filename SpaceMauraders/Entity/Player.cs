using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework; 

namespace SpaceMauraders.Entity
{
    public class Player: Entity
    {
        

        public Player():base()
        {
            components.Add(new Components.InputComponent(this.id));
            components.Add(new Components.PhysicsComponent(this.id)); 

        }


        public override void Update(GameTime gameTime)
        {
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

    }
}
