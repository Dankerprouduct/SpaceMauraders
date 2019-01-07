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
        public static List<Texture2D> torsos = new List<Texture2D>();
        public static List<Texture2D> bodyParts = new List<Texture2D>();
        public static List<Texture2D> particles = new List<Texture2D>();
        public static List<Texture2D> worldItems = new List<Texture2D>();
        public static List<Texture2D> guiItemTextures = new List<Texture2D>();




        public static void LoadContent(ContentManager content)
        {
            // loading tiles 
            AddTile("tempWall", content);             //0
            AddTile("tempFloor", content);            //1
            AddTile("Floor1", content);               //2
            AddTile("Wall1", content);                //3


            // loading fonts
            AddFont("debug", content);                //0
            AddFont("Arial12", content);              //1
            

            // loading gui elements
            AddGUI("Pixel", content);                //0
            AddGUI("Circle1", content);              //1

            // loading sprites
            AddSprite("TempSprite", content);        //0
            AddSprite("FederationSoilder", content); //1

            // loading particles
            AddParticle("CircleParticle", content); // 0

            //loading body parts 
            AddBodyPart("FederationSoilderBody", content); //0
            AddBodyPart("FederationSoilderHead", content); //1
            AddBodyPart("FederationSoilderHand", content); //2
            AddBodyPart("RebelSoilderBody", content);      //3

            // loading inventory items
            AddGuiItem("LaserRifle", content);
            AddGuiItem("FusionRifle", content);

        }

        #region Loading Methods

        public static void AddTile(string name, ContentManager content)
        {
            Console.WriteLine("Loading " + name); 
            tiles.Add(content.Load<Texture2D>("Tiles/" + name)); 
        }

        public static void AddFont(string name, ContentManager content)
        {
            Console.WriteLine("Loading " + name);
            fonts.Add(content.Load<SpriteFont>("Fonts/" + name));
        }

        public static void AddGUI(string name, ContentManager content)
        {
            Console.WriteLine("Loading " + name);
            gui.Add(content.Load<Texture2D>("GUI/" + name));
        }

        public static void AddSprite(string name, ContentManager content)
        {
            Console.WriteLine("Loading " + name);
            sprites.Add(content.Load<Texture2D>("Sprites/" + name));
        }

        public static void AddBodyPart(string name, ContentManager content)
        {
            Console.WriteLine("Loading " + name);
            bodyParts.Add(content.Load<Texture2D>("Sprites/BodyParts/" + name));
        }
        
        public static void AddParticle(string name, ContentManager content)
        {
            Console.WriteLine("Loading " + name);
            particles.Add(content.Load<Texture2D>("Particles/" + name));
        }

        public static void AddWorldItemTexture(string name, ContentManager content)
        {
            Console.WriteLine("Loading " + name);
            worldItems.Add(content.Load<Texture2D>("Sprites/Items/" + name));
        }

        public static void AddGuiItem(string name, ContentManager content)
        {
            Console.WriteLine("Loading " + name);
            guiItemTextures.Add(content.Load<Texture2D>("GUI/Items/" + name));

        }

        #endregion
    }
}
