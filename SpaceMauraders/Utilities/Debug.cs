using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework; 

namespace SpaceMauraders.Utilities
{
    public class Debug
    {
        KeyboardState keyboardState;
        KeyboardState oldKeyboardState;


        public static bool debug;
        public Utilities.LuaConsole luaConsole = new LuaConsole();
        public Debug()
        {

        }
        
        public void Update()
        {
            keyboardState = Keyboard.GetState();

            if(keyboardState.IsKeyDown(Keys.F3) && oldKeyboardState.IsKeyUp(Keys.F3))
            {
                debug = !debug;
                
            }

            if (keyboardState.IsKeyDown(Keys.OemTilde) && oldKeyboardState.IsKeyDown(Keys.OemTilde))
            {
                luaConsole.showDebug = !luaConsole.showDebug; 
            }

            if (luaConsole.showDebug)
            {
                luaConsole.Update(); 
            }

            

            oldKeyboardState = keyboardState; 
        }

        public void Draw()
        {
            if (luaConsole.showDebug)
            {
                GUI.GUI.DrawBox(new Rectangle(0, 24, Game1.width, 24), Color.Black * .5f);
                //  spriteBatch.DrawString(debugFont, luaDebug.text, new Vector2(10, ScreenHeight - 20), Color.White);
                GUI.GUI.DrawString(luaConsole.text, new Vector2(0, 20),1,1, Color.White); 

            }
        }
        
    }
}
