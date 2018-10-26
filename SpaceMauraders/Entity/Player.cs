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
            this.collisionRectanlge = new Rectangle((int)position.X, (int)position.Y, Utilities.TextureManager.sprites[0].Width, Utilities.TextureManager.sprites[0].Height);

            components.Add(new Components.InputComponent(this.id));
            components.Add(new Components.SpeedModifierComponent(5)); 
            components.Add(new Components.PhysicsComponent(this.id));
            
            components.Add(new Components.TriggerColliderComponent());

        }


        public override void Update(GameTime gameTime)
        {
                        
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //Utilities.Raycast ray = new Utilities.Raycast();
            //ray.MakeRay(4, 10, 3000, this);
            

            spriteBatch.Draw(Utilities.TextureManager.sprites[0], position, Color.White);
            //ray.DrawRay();
            base.Draw(spriteBatch);
        }

    }
}
