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

        public static Entity.Player player = new Entity.Player(new Vector2(16594, 37319));

        public static Entity.NPC npc1;

        Entity.NPC[] npcs = new Entity.NPC[5]; 
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
            Utilities.TextureManager.LoadContent(Content);


            world = new World.World(10, 10);

            debug = new Utilities.Debug();
            npc1 = new Entity.NPC(new Vector2(130 * 128, 269 * 128));
            
            npcs[0] = new Entity.NPC(new Vector2(134 * 128, 269 * 128));
            npcs[0].goal = Game1.world.spaceStation.nodeMesh.FindNodeOnMesh().arrayPosition;  

            npcs[1] = new Entity.NPC(new Vector2(136 * 128, 269 * 128));
            npcs[0].goal = Game1.world.spaceStation.nodeMesh.FindNodeOnMesh().arrayPosition;

            npcs[2] = new Entity.NPC(new Vector2(139 * 128, 269 * 128));
            npcs[0].goal = Game1.world.spaceStation.nodeMesh.FindNodeOnMesh().arrayPosition;

            npcs[3] = new Entity.NPC(new Vector2(140 * 128, 269 * 128));
            npcs[0].goal = Game1.world.spaceStation.nodeMesh.FindNodeOnMesh().arrayPosition;

            npcs[4] = new Entity.NPC(new Vector2(142 * 128, 269 * 128));
            npcs[0].goal = Game1.world.spaceStation.nodeMesh.FindNodeOnMesh().arrayPosition;
            
            GUI.GUI.Init(); 
            camera = new Utilities.Camera(GraphicsDevice.Viewport);

            Console.WriteLine("Number of Entities: " + Entity.Entity.nextAvailibleID);

            Entity.EntityDictionary.Init(); 
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
            
            npc1.Update(gameTime);
            
            for (int i = 0; i < npcs.Length; i++)
            {
                npcs[i].Update(gameTime);
            }
            player.Update(gameTime);
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
            GUI.GUI.Draw(spriteBatch); 
            world.Draw(spriteBatch);

            player.Draw(spriteBatch);
            npc1.Draw(spriteBatch);
            
            for (int i = 0; i < npcs.Length; i++)
            {
                npcs[i].Draw(spriteBatch);
            }
            
            spriteBatch.End();
            

            spriteBatch.Begin(SpriteSortMode.Deferred,null, SamplerState.PointClamp);
            GUI.GUI.Draw(spriteBatch);
            GUI.GUI.DrawString("DEVELOPMENT BUILD", new Vector2(GUI.GUI.screenBounds.X + 20, GUI.GUI.screenBounds.Height - 20),1, 1, Color.Gray);
            if (Utilities.Debug.debug)
            {

                GUI.GUI.DrawString("Mouse Position: " + Game1.worldPosition.ToString(), new Vector2(10, 10),1,1, Color.White);
                GUI.GUI.DrawString("Cell Mouse Position: " + (Game1.worldPosition / 128).ToPoint(), new Vector2(10, 30), 1, 1, Color.White);
                GUI.GUI.DrawString("Cell Posiiton: " + player.GetCenterPartition().ToString(), new Vector2(10, 50), 1, 1, Color.White);
            }

            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
