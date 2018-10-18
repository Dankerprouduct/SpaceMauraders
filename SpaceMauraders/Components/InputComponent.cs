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
        }

        public InputComponent(int parentID) : base(parentID)
        {

        }

        public override void Update(GameTime gameTime, Entity.Entity entity)        
        {
            
            keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.W))
            {
                Event enityEvent = new Event();
                enityEvent.id = "Entity";
                enityEvent.parameters.Add("entity", entity);
                entity.FireEvent(enityEvent);

                Event moveEvent = new Event();
                moveEvent.id = "move";
                moveEvent.parameters.Add("Move Up", 2);
                entity.FireEvent(moveEvent);
            }

            if (keyboardState.IsKeyDown(Keys.S))
            {
                Event enityEvent = new Event();
                enityEvent.id = "Entity";
                enityEvent.parameters.Add("entity", entity);
                entity.FireEvent(enityEvent);

                Event moveEvent = new Event();
                moveEvent.id = "move";
                moveEvent.parameters.Add("Move Down", 2);
                entity.FireEvent(moveEvent);
            }

            if (keyboardState.IsKeyDown(Keys.A))
            {
                Event enityEvent = new Event();
                enityEvent.id = "Entity";
                enityEvent.parameters.Add("entity", entity);
                entity.FireEvent(enityEvent);

                Event moveEvent = new Event();
                moveEvent.id = "move";
                moveEvent.parameters.Add("Move Left", 2);
                entity.FireEvent(moveEvent);
            }

            if (keyboardState.IsKeyDown(Keys.D))
            {
                Event enityEvent = new Event();
                enityEvent.id = "Entity";
                enityEvent.parameters.Add("entity", entity);
                entity.FireEvent(enityEvent);

                Event moveEvent = new Event();
                moveEvent.id = "move";
                moveEvent.parameters.Add("Move Right", 2);
                entity.FireEvent(moveEvent);
            }


            oldKeyboardState = keyboardState; 
        }
        
    }
}
