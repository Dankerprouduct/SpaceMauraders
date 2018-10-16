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
        

        public Player(Vector2 position):base()
        {
            this.position = position; 

            components.Add(new Components.InputComponent(this.id));
            components.Add(new Components.PhysicsComponent(this.id)); 

        }


        public override void Update(GameTime gameTime)
        {
                        
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(Utilities.TextureManager.sprites[0], position, Color.White); 
            base.Draw(spriteBatch);
        }

    }
}
