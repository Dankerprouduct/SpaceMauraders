using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using NLua;
using SpaceMarauders.Entity.Items;
using SpaceMarauders.Entity.Items.Weapons;

namespace SpaceMarauders.Utilities
{
    public class Debug
    {
        KeyboardState keyboardState;
        KeyboardState oldKeyboardState;

        Lua debugLua; 

        public static bool debug;
        public static bool showBoundariesAndMesh;
        public static bool showTextualDebug; 
        public Utilities.LuaConsole luaConsole = new LuaConsole();
        public Debug()
        {
            LoadLuaFunctions(); 
        }

        /// <summary>
        /// loads lua functions that can be used in debug
        /// </summary>
        public void LoadLuaFunctions()
        {
            debugLua = new Lua();
            debugLua.RegisterFunction("SpawnEntity", this, GetType().GetMethod("SpawnEntity"));
            debugLua.RegisterFunction("SpawnItem", this, GetType().GetMethod("SpawnItem"));
        }

        public void SpawnEntity(string name, int amount = 1)
        {
            Console.WriteLine("Spawning Entity " + name); 
            switch (name)
            {
                case "NPC":
                    {
                        for (int i = 0; i < amount; i++)
                        {
                            Game1.world.AddEntity(new Entity.Factions.Federation.General(Game1.worldPosition));
                        }
                        break;
                    }
                
            }
        }

        public void SpawnItem(int id, int amount = 1)
        {
            Console.WriteLine("Spawning Item " + id);
            for (int i = 0; i < amount; i++)
            {
                Item item = ItemDictionary.itemDictinary[id];
                item.position = Game1.worldPosition;
                Game1.world.spaceStation.ItemsCellSpacePartition.AddEntity(ItemDictionary.itemDictinary[id]);
            }
        }

        public void Update()
        {
            keyboardState = Keyboard.GetState();


            if (keyboardState.IsKeyDown(Keys.F5) && oldKeyboardState.IsKeyUp(Keys.F5))
            {
                showTextualDebug = !showTextualDebug;
            }

            if (keyboardState.IsKeyDown(Keys.F6) && oldKeyboardState.IsKeyUp(Keys.F6))
            {
                debug = !debug;
                
            }
            
            if (keyboardState.IsKeyDown(Keys.F7) && oldKeyboardState.IsKeyUp(Keys.F7))
            {
                showBoundariesAndMesh = !showBoundariesAndMesh; 

            }

            if (keyboardState.IsKeyDown(Keys.OemTilde) && oldKeyboardState.IsKeyUp(Keys.OemTilde))
            {
                luaConsole.showDebug = !luaConsole.showDebug; 
            }

            if (luaConsole.showDebug)
            {
                luaConsole.Update(); 

                if(keyboardState.IsKeyDown(Keys.Enter) && oldKeyboardState.IsKeyUp(Keys.Enter))
                {
                    LoadLuaFunctions();
                    try
                    {
                        debugLua.DoString(luaConsole.text);
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                    
                    //luaConsole.text = ""; 
                }
            }

            

            oldKeyboardState = keyboardState; 
        }

        public void Draw()
        {
            if (luaConsole.showDebug)
            {
                GUI.GUI.DrawBox(new Rectangle(5, 20, Game1.width, 26), Color.Black * .5f);
                //  spriteBatch.DrawString(debugFont, luaDebug.text, new Vector2(10, ScreenHeight - 20), Color.White);
                GUI.GUI.DrawString(luaConsole.text, new Vector2(5, 22),1,1, Color.White); 

            }
        }
        
    }
}
