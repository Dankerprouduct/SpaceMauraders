using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using SpaceMauraders.Components;
using SpaceMauraders.Entity.Factions.Federation;
using SpaceMauraders.Utilities;

namespace SpaceMauraders
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

        public static int width = 1920;
        public static int height = 0; 

        public static Vector2 worldPosition;

        Vector2 mousePosition;
        GameData<EntitySaveTemplate<Entity.Entity>> gameData = new GameData<EntitySaveTemplate<Entity.Entity>>();
        GameData<Entity.Entity> entityData = new GameData<Entity.Entity>();

        public static Entity.Player player;
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
            
            IsMouseVisible = true;
            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Utilities.TextureManager.LoadContent(Content);
            Systems.ParticleSystem.Init(20000);
            Entity.Items.ItemDictionary.LoadItemDatabase();
            GUI.GUI.Init();
            Reset();

        }

        public void Reset()
        {
            

            world = new World.World(10, 10);

            debug = new Utilities.Debug();

            player = new Entity.Player(new Vector2(18586 - 1024, 38309));

            for (int i = 0; i < 0; i++)
            {

                world.AddEntity(new Entity.NPC(new Vector2(world.spaceStation.nodeMesh.FindNodeOnMesh().arrayPosition.X * 128 + 64,
                    world.spaceStation.nodeMesh.FindNodeOnMesh().arrayPosition.Y * 128 + 64)));

                //world.AddEntity(new Entity.NPC(new Vector2(18586, 38309)));
            }

            //world.AddEntity(new Entity.NPC(new Vector2(18586, 38309)));
            
            camera = new Utilities.Camera(GraphicsDevice.Viewport);

            Console.WriteLine("Number of Entities: " + Entity.Entity.nextAvailibleID);
        }

        protected override void UnloadContent()
        {

        }

        public void SaveGame()
        {
            //gameData.SaveData(world.dynamicCellSpacePartition.SaveDynamicEntities(), "save1");
            entityData.SaveData(world.dynamicCellSpacePartition.GetDynamicEntities(), "save2");
        }

        public void LoadGame()
        {

            //EntitySaveTemplate<Entity.Entity>[] tempE = gameData.LoadData("save1");
            Entity.Entity[] ents = entityData.LoadData("save2");

            //foreach (var template in tempE)
            //{
            //    Entity.Entity ent = new Entity.Entity()
            //    {
                
            //        position = template.position,
            //        entityName = template.name,
            //        rotation = template.rotation,
            //        body = template.body,
                  
            //    };
            //    ent = (typeof(template)) ent;
            //    for (int i = 0; i < template.components.Count; i++)
            //    {
            //        if (template.components[i].componentName == "PhysicsComponent")
            //        {
            //            ent.AddComponent(new PhysicsComponent());
            //        }
            //        else
            //        {
            //            ent.AddComponent(template.components[i]);
            //        }
            //    }
            //    world.AddEntity(ent);
            //}

            for (int i = 0; i < ents.Length; i++)
            {
                world.AddEntity(ents[i]);
            }
        }



        protected override void Update(GameTime gameTime)
        {
            GUI.GUI.Draw(spriteBatch);

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.F12))
                Reset();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.F11))
                SaveGame();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.F10))
                LoadGame();

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
            GraphicsDevice.Clear(new Color(10,10,10));
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, camera.transform);
            GUI.GUI.Draw(spriteBatch); 
            world.Draw(spriteBatch);

            player.Draw(spriteBatch);
            Systems.ParticleSystem.Draw(spriteBatch);

            // Draw2dArray(x, y, width, height, rows, collums)
            spriteBatch.End();
            

            spriteBatch.Begin(SpriteSortMode.Deferred,null, SamplerState.PointClamp);
            GUI.GUI.Draw(spriteBatch);
            GUI.GUI.DrawString("DEVELOPMENT BUILD", new Vector2(GUI.GUI.screenBounds.X + 20, GUI.GUI.screenBounds.Height - 20),1, 1, Color.Gray);
            if (Utilities.Debug.showTextualDebug)
            {

                GUI.GUI.DrawString("Mouse Position: " + worldPosition.ToString(), new Vector2(10, 10),1,1, Color.White);
                GUI.GUI.DrawString("Cell Mouse Position: " + (worldPosition / 128).ToPoint(), new Vector2(10, 30), 1, 1, Color.White);
                GUI.GUI.DrawString("Cell Position: " + player.GetCenterPartition().ToString(), new Vector2(10, 50), 1, 1, Color.White);
                try
                {
                    GUI.GUI.DrawString("Tile Position: " + world.spaceStation.cellSpacePartition.staticCells[world.spaceStation.cellSpacePartition.PositionToIndex(worldPosition)].
                        GetEntityIndex(new Point((int)worldPosition.X, (int)worldPosition.Y)).ToString(), new Vector2(10, 70), 1, 1, Color.White);
                }
                catch (Exception ex)
                {
                    
                }
                GUI.GUI.DrawString("Active Particles: " + Systems.ParticleSystem.currentParticles, new Vector2(10, 90), 1, 1, Color.White);
            }

            // draw inventory gui
            player.DrawInventory(); 

            debug.Draw(); 

            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
