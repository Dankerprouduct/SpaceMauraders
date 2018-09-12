using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace SpaceMauraders.Utilities
{
    public static class TextureManager
    {
        public static List<Texture2D> tiles = new List<Texture2D>(); 

        public static void LoadContent(ContentManager content)
        {
            // loading tiles 
            AddTiles("tempWall", content);
            AddTiles("tempFloor", content); 


        }

        public static void AddTiles(string name, ContentManager content)
        {
            tiles.Add(content.Load<Texture2D>("Tiles/" + name)); 
        }

    }
}
