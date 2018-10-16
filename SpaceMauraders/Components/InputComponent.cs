using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input; 

namespace SpaceMauraders.Components
{
    public class InputComponent: Component
    {

        KeyboardState keyboardState;
        KeyboardState oldKeyboardState;

        MouseState mouseState;
        MouseState oldMouseState; 

        public InputComponent(int parentID) : base(parentID)
        {

        }

        public void Update(Entity.Entity entity)
        {
            keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.W))
            {

            }
            if (keyboardState.IsKeyDown(Keys.S))
            {

            }
            if (keyboardState.IsKeyDown(Keys.A))
            {

            }
            if (keyboardState.IsKeyDown(Keys.D))
            {

            }


            oldKeyboardState = keyboardState; 
        }
        
    }
}
