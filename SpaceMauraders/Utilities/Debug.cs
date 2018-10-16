using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input; 


namespace SpaceMauraders.Utilities
{
    public class Debug
    {
        KeyboardState keyboardState;
        KeyboardState oldKeyboardState;


        public static bool debug;
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

            

            oldKeyboardState = keyboardState; 
        }

        
    }
}
