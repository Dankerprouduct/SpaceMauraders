using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceMauraders.Entity.Body
{
    public class BodyPart
    {
        int textureID;
        public float scale = .5f; 
        public float rotation;
        public Vector2 positon;
        public Vector2 center;
        public Vector2 offset;

        public float turnAngle = 5;
        public float lerpSpeed = .2f;

        public BodyPart(int textureID, Vector2 _offset)
        {
            offset = _offset;
            this.textureID = textureID; 
            center = new Vector2(
                            Utilities.TextureManager.bodyParts[textureID].Width / 2,
                            Utilities.TextureManager.bodyParts[textureID].Height / 2)
                            ;
        }         
        
        public virtual void Update(Vector2 center, float rotation)
        {
            //Console.WriteLine(rotation); 
            positon = center;
            this.rotation = rotation;
        }

        public Vector2 Vec2ToEntitySpace(Vector2 pos)
        {

            Matrix rotationMatrix = Matrix.CreateRotationZ(rotation);

            Vector2 _pos = pos;

            // REMEMBER THAT THE POSITON = CENTER
            return _pos =  (positon) + Vector2.Transform(_pos, rotationMatrix);


        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(Utilities.TextureManager.bodyPart[textureID], positon, null, Color.White, rotation, center, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Utilities.TextureManager.bodyParts[textureID],
                positon, null,
                Color.White,
                rotation,
                center,
                scale,
                SpriteEffects.None,
                0f);
        }

    }
}
