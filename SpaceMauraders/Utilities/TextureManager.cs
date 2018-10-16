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
        public static List<SpriteFont> fonts = new List<SpriteFont>();
        public static List<Texture2D> gui = new List<Texture2D>();
        public static List<Texture2D> sprites = new List<Texture2D>();

        public static void LoadContent(ContentManager content)
        {
            // loading tiles 
            AddTile("tempWall", content);      //0
            AddTile("tempFloor", content);     //1
            AddTile("Floor1", content);        //2
            AddTile("Wall1", content);         //3


            // loading fonts
            AddFont("debug", content);         //0
            AddFont("Arial12", content);       //1

            // loading gui elements
            AddGUI("Pixel", content);          //0

            // loading sprites
            AddSprite("TempSprite", content);  //0
            

        }

        public static void AddTile(string name, ContentManager content)
        {
            tiles.Add(content.Load<Texture2D>("Tiles/" + name)); 
        }

        public static void AddFont(string name, ContentManager content)
        {
            fonts.Add(content.Load<SpriteFont>("Fonts/" + name));
        }

        public static void AddGUI(string name, ContentManager content)
        {
            gui.Add(content.Load<Texture2D>("GUI/" + name));
        }

        public static void AddSprite(string name, ContentManager content)
        {
            sprites.Add(content.Load<Texture2D>("Sprites/" + name));
        }
    }
}
