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

        public enum TileType
        {
            Solid,
            NonSolid
        }
        public int tileID;
        public TileType tileType;
        
        public Tile()
        {

        }

        public Tile(Vector2 position, int tileID, TileType tileType) : base()
        {
            this.tileID = tileID;

            this.position = position;

            this.collisionRectanlge =
                new Rectangle((int)position.X,
                (int)position.Y,
                Utilities.TextureManager.tiles[tileID].Width,
                Utilities.TextureManager.tiles[tileID].Height);

            this.tileType = tileType;

            if (tileType == TileType.Solid)
            {
                components.Add(new Components.SolidColliderComponent(this, id));
                components.Add(new Components.TriggerColliderComponent(collisionRectanlge));
            }
        }

        

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime); 
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Utilities.TextureManager.tiles[tileID], position, Color.White); 
        }

    }
}
