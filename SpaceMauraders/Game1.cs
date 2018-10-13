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
        public static Utilities.Debug debug; 

        public static World.World world;

        public static int width = 1080;
        public static int height = (width / 16) * 9;

        public static Vector2 worldPosition;

        Vector2 mousePosition; 

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
            
            IsMouseVisible = true; 
            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            world = new World.World(10, 10);

            debug = new Utilities.Debug();
            Utilities.TextureManager.LoadContent(Content);

            GUI.GUI.Init(); 
            camera = new Utilities.Camera(GraphicsDevice.Viewport);
        }
        
        protected override void UnloadContent()
        {

        }
        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            mousePosition = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            worldPosition = Vector2.Transform(mousePosition, Matrix.Invert(camera.transform)) ;


            world.Update(gameTime); 

            debug.Update(); 
            Game1 game = this;
            camera.Update(ref game); 
            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(10,10,10));
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, camera.transform);
            
            world.Draw(spriteBatch);
            spriteBatch.End();
            

            spriteBatch.Begin(SpriteSortMode.Deferred,null, SamplerState.PointClamp);
            GUI.GUI.Draw(spriteBatch);
            GUI.GUI.DrawString("DEVELOPMENT BUILD", new Vector2(GUI.GUI.screenBounds.X + 20, GUI.GUI.screenBounds.Height - 20),1, 1, Color.Gray);


            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
