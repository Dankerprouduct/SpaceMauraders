using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SpaceMauraders.Entity;

namespace SpaceMauraders.Components
{
    public class InputComponent: Component
    {

        KeyboardState keyboardState;
        KeyboardState oldKeyboardState;

        MouseState mouseState;
        MouseState oldMouseState;

        public InputComponent()
        {
            componentName = "InputComponent";
        }

        public InputComponent(int parentID) : base(parentID)
        {
            componentName = "InputComponent";
        }

        public override void Update(GameTime gameTime, Entity.Entity entity)        
        {
            
            keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.W))
            {
                Event moveEvent = new Event();
                moveEvent.id = "AddVelocity";
                moveEvent.parameters.Add("Velocity", new Vector2(0, -2));
                entity.FireEvent(moveEvent);
            }

            if (keyboardState.IsKeyDown(Keys.S))
            {
                Event moveEvent = new Event();
                moveEvent.id = "AddVelocity";
                moveEvent.parameters.Add("Velocity", new Vector2(0, 2));
                entity.FireEvent(moveEvent);
            }

            if (keyboardState.IsKeyDown(Keys.A))
            {
                Event moveEvent = new Event();
                moveEvent.id = "AddVelocity";
                moveEvent.parameters.Add("Velocity", new Vector2(-2, 0));
                entity.FireEvent(moveEvent);
            }

            if (keyboardState.IsKeyDown(Keys.D))
            {
                Event moveEvent = new Event();
                moveEvent.id = "AddVelocity";
                moveEvent.parameters.Add("Velocity", new Vector2(2, 0));
                entity.FireEvent(moveEvent);
            }

            if(keyboardState.IsKeyDown(Keys.Tab) && oldKeyboardState.IsKeyUp(Keys.Tab))
            {
                Event openInventoryEvent = new Event();
                openInventoryEvent.id = "OpenInventory";
                entity.FireEvent(openInventoryEvent); 
            }


            oldKeyboardState = keyboardState; 
        }
        
    }
}
