using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics; 


namespace SpaceMauraders.World
{
    public class World
    {
        
        public Entity.SpaceStation spaceStation;


        public World(int width, int height)
        {
            spaceStation = new Entity.SpaceStation(301); 
        }
        
        public void Update(GameTime gameTime)
        {
            spaceStation.Update();
        }
       
        public void GenerateSpaceStation(int radius)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spaceStation.Draw(spriteBatch); 
        }

        public bool FireGlovalEvent(Components.Event _event)
        {
            spaceStation.FireEvent(_event); 
            return false; 
        }

    }
}
