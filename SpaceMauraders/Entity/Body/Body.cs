using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics; 

namespace SpaceMauraders.Entity.Body
{
    public class Body
    {
        List<BodyPart> bodyParts = new List<BodyPart>();

        public Body()
        {

        }

        public void AddBodyPart(BodyPart bodyPart)
        {
            bodyParts.Add(bodyPart); 
        }

        public void Update(Vector2 positon, float rotation)
        {
            for(int i = 0; i < bodyParts.Count; i++)
            {
                // all body parts have the center and rotation. 
                // its just their job to align themselves properly 
                bodyParts[i].Update(positon, rotation);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < bodyParts.Count; i++)
            {
                bodyParts[i].Draw(spriteBatch);
            }
        }
    }
}
