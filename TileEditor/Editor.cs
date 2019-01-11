using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using  MonoGame.Forms.Controls;
using SpaceMauraders.Utilities;
using Microsoft.Xna.Framework.Input;
using MonoGame.Forms.Components;
using  SpaceMauraders.GUI;
using Color = Microsoft.Xna.Framework.Color;
using Matrix = Microsoft.Xna.Framework.Matrix;
using Point = System.Drawing.Point;

namespace TileEditor
{
    public class Editor: MonoGameControl
    {
        private List<Bitmap> tileBitmaps = new List<Bitmap>();
        public int currentSelectedItem = 0;
        public MouseState currentMouseState;
        public MouseState previousMouseState;
        private KeyboardState curKeyboardState;
        private KeyboardState prevKeyboardState;
        private Vector2 mousePosiiton;

        public bool mapCreated = false;
        private int[,] tileMap;
        private List<Texture2D> tileTextures;
        Camera2D camera = new Camera2D();
        private float scale = 1; 

        protected override void Initialize()
        {

            base.Initialize();
            LoadBitmaps();
            
            
        }

        public void LoadBitmaps()
        {
            tileBitmaps = new List<Bitmap>();
            SpaceMauraders.Utilities.TextureManager.LoadContent(Editor.Content);
            tileTextures = TextureManager.tiles;


            for (int i = 0; i < tileTextures.Count; i++)
            {
                MemoryStream memoryStream = new MemoryStream();
                tileTextures[i].SaveAsPng(memoryStream, tileTextures[i].Width, tileTextures[i].Height);

                tileBitmaps.Add( ResizeImage(new Bitmap(memoryStream),
                    tileTextures[i].Width * 1,
                    tileTextures[i].Height * 1));
                Console.WriteLine("added bitmap " + i);
            }

            
        }

        /// <summary>
        /// Resize the image to the specified width and height.
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <returns>The resized image.</returns>
        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new System.Drawing.Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        public List<Bitmap> GetBitmaps()
        {
            return tileBitmaps;
        }

        public void CreateMap(int width, int height)
        {
            tileMap = new int[width, height];
            for (int y = 0; y < tileMap.GetLength(1); y++)
            {
                for (int x = 0; x < tileMap.GetLength(0); x++)
                {
                    tileMap[x, y] = -1; 
                }
            }
            mapCreated = true; 
        }
        
        protected override void Update(GameTime gameTime)
        {

            currentMouseState = Mouse.GetState();
            curKeyboardState = Keyboard.GetState();

            mousePosiiton = Vector2.Transform(currentMouseState.Position.ToVector2(),
                Matrix.Invert(camera.Transform));

            if (mapCreated)
            {
                UpdateMapLogic();
                Camera();
            }
            

            previousMouseState = currentMouseState;
            prevKeyboardState = curKeyboardState;

        }

        void Camera()
        {
            if (currentMouseState.RightButton == ButtonState.Pressed)
            {
                if (curKeyboardState.IsKeyUp(Keys.Space))
                {
                    if (Mouse.GetState().Position.Y < 50)
                    {
                        camera.Move(new Vector2(0, -5));
                    }

                    if (Mouse.GetState().Position.Y > GraphicsDevice.Viewport.Height - 50)
                    {
                        camera.Move(new Vector2(0, 5));
                    }

                    if (Mouse.GetState().Position.X < 50)
                    {
                        camera.Move(new Vector2(-5, 0));
                    }

                    if (Mouse.GetState().Position.X > GraphicsDevice.Viewport.Width - 50)
                    {
                        camera.Move(new Vector2(5, 0));
                    }
                }
            }


            if (currentMouseState.ScrollWheelValue > previousMouseState.ScrollWheelValue)
            {
                scale += .05f;
            }
            if (currentMouseState.ScrollWheelValue < previousMouseState.ScrollWheelValue)
            {
                scale -= .05f;
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

            camera.GetZoom = scale;
        }

        private bool startedDrag = false; 
        private Point downPoint;
        private Point upPoint; 
        void UpdateMapLogic()
        {
            
            if (currentMouseState.LeftButton == ButtonState.Pressed)
            {
                int x = (int)mousePosiiton.X / 128;
                int y = (int)mousePosiiton.Y / 128;

                // picker tool
                if (curKeyboardState.IsKeyDown(Keys.LeftShift))
                {
                    currentSelectedItem = tileMap[x, y]; 
                }
                else if(curKeyboardState.IsKeyDown(Keys.A))
                {
                    if (!startedDrag)
                    {
                        downPoint = new Point(x,y);
                        startedDrag = true; 
                        Console.WriteLine("down point " + downPoint);
                    }

                    if (prevKeyboardState.IsKeyUp(Keys.A))
                    {
                        if (startedDrag)
                        {
                            upPoint = new Point(x, y);

                            for (int dY = downPoint.Y; dY < upPoint.Y; dY++)
                            {
                                for (int dX = downPoint.X; dX < upPoint.X; dX++)
                                {
                                    tileMap[dX, dY] = currentSelectedItem;
                                }
                            }
                            Console.WriteLine("up point "+ upPoint);
                            startedDrag = false;
                        }

                        
                    }
                    
                    
                }
                else
                {
                    try
                    {
                        tileMap[x, y] = currentSelectedItem;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("OUT OF BOUNDS");
                    }
                }
                

                
            }

            if (currentMouseState.RightButton == ButtonState.Pressed)
            {
                if (curKeyboardState.IsKeyDown(Keys.Space))
                {
                    int x = (int) mousePosiiton.X / 128;
                    int y = (int) mousePosiiton.Y / 128;

                    try
                    {

                        tileMap[x, y] = -1;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("OUT OF BOUNDS");
                    }
                }
            }
        }

        protected override void Draw()
        {
            base.Draw();
            Editor.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null,
                null, null, camera.GetTransformation(GraphicsDevice));
            GUI.Draw(Editor.spriteBatch);


            ///GUI.Draw2dArray(10, 10, 128, 128, tileMap.GetLength(0), tileMap.GetLength(1), 0, Color.AliceBlue);

            if (tileMap != null)
            {
                for (int y = -1; y < tileMap.GetLength(1); y++)
                {
                    for (int x = -1; x < tileMap.GetLength(0); x++)
                    {

                        GUI.DrawBox(
                            new Microsoft.Xna.Framework.Rectangle((x * 128) + 128, (y * 128) + 128, 128, 128), 3,
                            Color.DarkGray);

                    }
                }
                for (int y = 0; y < tileMap.GetLength(1); y++)
                {
                    for (int x = 0; x < tileMap.GetLength(0); x++)
                    {
                        if (tileMap[x, y] != -1)
                        {
                            GUI.spriteBatch.Draw(tileTextures[tileMap[x, y]], new Vector2(x * 128, y * 128),
                                Color.White);
                        }
                    }
                }

                if (currentSelectedItem != -1)
                {
                    Editor.spriteBatch.Draw(tileTextures[currentSelectedItem],
                        new Vector2(((int)(mousePosiiton.X / 128)) * 128, ((int)(mousePosiiton.Y / 128)) * 128),
                        Color.Red * .5f);
                }
            }
            
            Editor.spriteBatch.End();
            
        }
    }
}
