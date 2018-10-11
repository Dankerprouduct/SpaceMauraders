using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics; 

namespace SpaceMauraders.Entity
{
    public class Tile: Entity
    {

        public int tileID;
        public Tile(Vector2 position, int tileID): base()
        {
            this.tileID = tileID;

            this.position = position; 
        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Utilities.TextureManager.tiles[tileID], position, Color.White); 
        }

    }
}
