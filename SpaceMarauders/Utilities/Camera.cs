using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceMarauders.Utilities
{
    public class Camera
    {

        KeyboardState keyboardState;
        KeyboardState oldKeyboardState;
        MouseState mouseState;
        MouseState oldMouseState;

        public Matrix transform;

        Viewport viewPort;
        public Vector2 center;
        public Vector2 lerpedCenter;
        float scale = 1f;

        bool camControl = false;
        float speed = 60;
        public static Vector2 position;
        public Camera(Viewport vPort)
        {
            position = Game1.world.spaceStation.center; 
            viewPort = vPort;
        }

        public void Update(ref Game1 game)
        {
            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();

            if (mouseState.ScrollWheelValue > oldMouseState.ScrollWheelValue)
            {
                scale += .025f;
            }
            if (mouseState.ScrollWheelValue < oldMouseState.ScrollWheelValue)
            {
                scale -= .025f;
            }
            // Tree tree = new Tree(Vector2.Zero, 0, 0);

            if (scale > 4)
            {
                scale = 4;
            }
            if (scale <= .01f)
            {
                scale = .01f;
            }




            if (keyboardState.IsKeyDown(Keys.F1) && oldKeyboardState.IsKeyUp(Keys.F1))
            {
                camControl = !camControl;
            }

            if (camControl)
            {
                center = position;
                lerpedCenter = Vector2.Lerp(lerpedCenter, center, .5f);
                keyboardState = Keyboard.GetState();

                if (keyboardState.IsKeyDown(Keys.Up))
                {
                    position.Y -= speed;
                }
                if (keyboardState.IsKeyDown(Keys.Down))
                {
                    position.Y += speed;
                }
                if (keyboardState.IsKeyDown(Keys.Right))
                {
                    position.X += speed;
                }
                if (keyboardState.IsKeyDown(Keys.Left))
                {
                    position.X -= speed;
                }

            }
            else
            {
                //center = Game1.player.GetCenter();
                center = Game1.player.GetEntityPosition();
                position = center; 
                lerpedCenter = Vector2.Lerp(lerpedCenter, center, .1f);
            }


            transform = Matrix.CreateTranslation(new Vector3(-lerpedCenter.X, -lerpedCenter.Y, 0)) *
                Matrix.CreateScale(new Vector3(scale, scale, 1)) *
                Matrix.CreateTranslation(new Vector3(game.GraphicsDevice.Viewport.Width * 0.5f, game.GraphicsDevice.Viewport.Height * 0.5f, 0));

            oldMouseState = mouseState;
            oldKeyboardState = keyboardState;
        }

        public Vector2 WindowToCameraSpace(Vector2 windowPosition)
        {
            // Scale for camera bounds that vary from window
            // Also, must adjust for translation if camera isn't at 0, 0 in screen space (such as a mini-map)
            return (scale * windowPosition) + center;
        }

    }
}

