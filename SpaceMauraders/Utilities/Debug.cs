using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using NLua; 

namespace SpaceMauraders.Utilities
{
    public class Debug
    {
        KeyboardState keyboardState;
        KeyboardState oldKeyboardState;

        Lua debugLua; 

        public static bool debug;
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
        }

        public void SpawnEntity(string name, int ammount = 1)
        {
            Console.WriteLine("Spawning Entity " + name); 
            switch (name)
            {
                case "NPC":
                    {
                        for (int i = 0; i < ammount; i++)
                        {
                            Game1.world.AddEntity(new Entity.NPC(Game1.worldPosition));
                        }
                        break;
                    }
            }
        }

        public void Update()
        {
            keyboardState = Keyboard.GetState();

            if(keyboardState.IsKeyDown(Keys.F3) && oldKeyboardState.IsKeyUp(Keys.F3))
            {
                debug = !debug;
                
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
                    debugLua.DoString(luaConsole.text);
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
