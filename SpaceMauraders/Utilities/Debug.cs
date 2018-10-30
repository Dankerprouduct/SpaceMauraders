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

            if (keyboardState.IsKeyDown(Keys.OemTilde) && oldKeyboardState.IsKeyUp(Keys.OemTilde))
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
                GUI.GUI.DrawBox(new Rectangle(5, 20, Game1.width, 26), Color.Black * .5f);
                //  spriteBatch.DrawString(debugFont, luaDebug.text, new Vector2(10, ScreenHeight - 20), Color.White);
                GUI.GUI.DrawString(luaConsole.text, new Vector2(5, 22),1,1, Color.White); 

            }
        }
        
    }
}
