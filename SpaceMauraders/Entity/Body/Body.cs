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
        public Rectangle bounds; 
        public List<BodyPart> bodyParts = new List<BodyPart>();
        List<int> headIndexes = new List<int>(); 
        List<int> handsIndexes = new List<int>();
        List<int> torsoIndexes = new List<int>();

        public Body()
        {

        }

        public void AddBodyPart(BodyPart bodyPart)
        {
            
            bodyParts.Add(bodyPart);

            if (bodyPart is Hand)
            {
                handsIndexes.Add(bodyParts.Count); 
            }
            else if(bodyPart is Head)
            {
                headIndexes.Add(bodyParts.Count); 
            }
            else if(bodyPart is Torso)
            {
                torsoIndexes.Add(bodyParts.Count);
                bounds = bodyPart.bounds; 
            }

        }

        public int[] GetHeadIndexes()
        {
            return headIndexes.ToArray<int>();
        }

        public int[] GetHandIndexes()
        {
            return handsIndexes.ToArray<int>();
        }

        public int[] GetTorsoIndexes()
        {
            return torsoIndexes.ToArray<int>();
        }

        public void Update(Vector2 positon, float rotation)
        {
            for(int i = 0; i < bodyParts.Count; i++)
            {
                // all body parts have the center and rotation. 
                // its just their job to align themselves properly 
                bodyParts[i].Update(positon, rotation);
                if (bodyParts[i] is Torso)
                {
                    bounds = bodyParts[i].bounds; 
                }
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
