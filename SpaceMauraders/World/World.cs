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
        
        public SpaceStation spaceStation;


        public World(int width, int height)
        {
            //InitializeTileMap(width, height);

            // GenerateSpaceStation(); 
            // make space station
            spaceStation = new SpaceStation(301); 
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

    }
}
