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

        Components.InputComponent inputComponent; 

        public Player():base()
        {
            inputComponent = new Components.InputComponent(); 
        }

        public override void Update(GameTime gameTime)
        {
            inputComponent.Update(this); 

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

    }
}
