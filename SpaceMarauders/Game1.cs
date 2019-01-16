using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using SpaceMarauders.Utilities;
using SpaceMarauders.Components;
using SpaceMarauders.Entity.Factions.Federation;
using SpaceMarauders.Shaders;

namespace SpaceMarauders
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static Game1 game;
        public static Random random = new Random(92899);

        public static Utilities.Camera camera;
        public static Utilities.Debug debug; 

        public static World.World world;

        public static int width = 1080;
        public static int height = 0; 

        public static Vector2 worldPosition;
        Vector2 mousePosition;
        
        public static Entity.Player player;
        RenderTarget2D renderTarget1, renderTarget2;
        private RenderTarget2D regularRenderTarget; 
        Bloom bloom;
        private PresentationParameters pp;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";


            graphics.PreferredBackBufferWidth = width;
                
            height = (width / 16) * 9;
            
            graphics.PreferredBackBufferHeight = height;
            graphics.GraphicsProfile = GraphicsProfile.HiDef; 
            graphics.IsFullScreen = false; 
            
            
        }
        
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            pp = GraphicsDevice.PresentationParameters;
            

            IsMouseVisible = true;
            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            bloom = new Bloom(GraphicsDevice, spriteBatch);
            bloom.Settings = BloomSettings.PresetSettings[3];
            renderTarget1 = new RenderTarget2D(GraphicsDevice, width, height, false, pp.BackBufferFormat, pp.DepthStencilFormat, pp.MultiSampleCount, RenderTargetUsage.DiscardContents);
            renderTarget2 = new RenderTarget2D(GraphicsDevice, width, height, false, pp.BackBufferFormat, pp.DepthStencilFormat, pp.MultiSampleCount, RenderTargetUsage.DiscardContents);


            regularRenderTarget = new RenderTarget2D(GraphicsDevice, width, height, false, pp.BackBufferFormat, pp.DepthStencilFormat, pp.MultiSampleCount, RenderTargetUsage.DiscardContents);

        Utilities.TextureManager.LoadContent(Content);
            Systems.ParticleSystem.Init(20000);
            Entity.Items.ItemDictionary.LoadItemDatabase();
            bloom.LoadContent(Content, pp);
            GUI.GUI.Init();
            Reset();

        }

        public void Reset()
        {
            
            world = new World.World(10, 10);
            debug = new Utilities.Debug();
            player = new Entity.Player(new Vector2(18586 - 1024, 38309));            
            camera = new Utilities.Camera(GraphicsDevice.Viewport);


            Console.WriteLine("Number of Entities: " + Entity.Entity.nextAvailibleID);
        }

        protected override void UnloadContent()
        {
            bloom.UnloadContent();
            renderTarget1.Dispose(); renderTarget2.Dispose();
        }
        
        protected override void Update(GameTime gameTime)
        {
            //bloom.Update();
            GUI.GUI.Draw(spriteBatch);

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.F12))
                Reset();

            // Save Game
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.F11))
                GameManager.SaveGame();

            // Loads Game
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.F10))
                GameManager.LoadGame();
            GameManager.Update();

            mousePosition = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            worldPosition = Vector2.Transform(mousePosition, Matrix.Invert(camera.transform)) ;

            if (!debug.luaConsole.showDebug)
            {

                player.Update(gameTime);
                world.Update(gameTime);
                               
            }
            Systems.ParticleSystem.Update(gameTime);


            debug.Update(); 
            game = this;
            camera.Update(ref game); 
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.SetRenderTarget(renderTarget1); // bloom shader
            GraphicsDevice.Clear(Color.TransparentBlack);
            // particle effects
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, camera.transform);
            Systems.ParticleSystem.Draw(spriteBatch);
            spriteBatch.End();
            bloom.Draw(renderTarget1, renderTarget2);           

            GraphicsDevice.SetRenderTarget(null);



            // Background
            GraphicsDevice.Clear(Color.Transparent);
            spriteBatch.Begin();
            world.DrawBackground(spriteBatch);
            spriteBatch.End();

            // player space
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, camera.transform);
            GUI.GUI.Draw(spriteBatch);
            world.Draw(spriteBatch);
            player.Draw(spriteBatch);

            spriteBatch.End();


            // Draw bloomed layer over top: 
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            spriteBatch.Draw(renderTarget2, new Rectangle(0, 0, width, height), Color.White);
            spriteBatch.End();

            // Dev stuff
            spriteBatch.Begin(SpriteSortMode.Deferred,null, SamplerState.PointClamp);
            GUI.GUI.Draw(spriteBatch);
            GUI.GUI.DrawString("DEVELOPMENT BUILD", new Vector2(GUI.GUI.screenBounds.X + 20, GUI.GUI.screenBounds.Height - 20),1, 1, Color.Gray);
            if (Utilities.Debug.showTextualDebug)
            {

                GUI.GUI.DrawString("Mouse Position: " + worldPosition.ToString(), new Vector2(10, 10),1,1, Color.White);
                GUI.GUI.DrawString("Cell Mouse Position: " + (worldPosition / 128).ToPoint(), new Vector2(10, 30), 1, 1, Color.White);
                GUI.GUI.DrawString("Cell Position: " +
                                   world.spaceStation.CellSpacePartition.PositionToCell(player.GetEntityPosition()) +
                                   " - " + player.GetCenterPartition().ToString(),
                    new Vector2(10, 50), 1, 1, Color.White);
                try
                {
                    GUI.GUI.DrawString("Tile Position: " + world.spaceStation.CellSpacePartition.staticCells[world.spaceStation.CellSpacePartition.PositionToIndex(worldPosition)].
                        GetEntityIndex(new Point((int)worldPosition.X, (int)worldPosition.Y)).ToString(), new Vector2(10, 70), 1, 1, Color.White);
                }
                catch (Exception ex)
                {
                    
                }
                GUI.GUI.DrawString("Active Particles: " + Systems.ParticleSystem.currentParticles, new Vector2(10, 90), 1, 1, Color.White);
                GUI.GUI.DrawString(
                    "World Cell Position: " + world.dynamicCellSpacePartition.PositionToCell(player.GetEntityPosition()),
                    new Vector2(10, 110), 1, 1, Color.White);
            }
            
            // draw inventory gui & regular gui
            player.DrawInventory(); 

            debug.Draw(); 

            spriteBatch.End();


            base.Draw(gameTime);
        }

        private void DrawRTToTarget(RenderTarget2D map, RenderTarget2D target, SpriteBatch _spriteBatch)
        {

            GraphicsDevice.SetRenderTarget(target);
            _spriteBatch.Begin(0, BlendState.Opaque, SamplerState.PointClamp);
            _spriteBatch.Draw(map, new Rectangle(0, 0, map.Width, map.Height), Color.White);
            _spriteBatch.End();
        }
    }
}
