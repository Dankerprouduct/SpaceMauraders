using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace SpaceMauraders
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static Random random = new Random();

        Utilities.Camera camera; 
        World.World world;

        static int width = 1920;
        static int height = (width / 16) * 9; 

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = width;
            graphics.PreferredBackBufferHeight = height;
        }
        
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            camera = new Utilities.Camera(GraphicsDevice.Viewport);
            IsMouseVisible = true; 
            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            world = new World.World(10, 10);
            Utilities.TextureManager.LoadContent(Content); 
        }
        
        protected override void UnloadContent()
        {

        }
        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            Game1 game = this;
            camera.Update(ref game); 
            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, camera.transform);

            spriteBatch.Draw(Utilities.TextureManager.tiles[0], new Vector2(100, 100), Color.White); 
            world.Draw(spriteBatch); 

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
