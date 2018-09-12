using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics; 

namespace SpaceMauraders
{
    public class SpaceStation
    {
        int diameter;
        int[,] tileMap;
        public SpaceStation(int radius)
        {
            diameter = radius * 2;
            tileMap = new int[diameter, diameter];

            InitializeMap();
            BuildRings(125, 175);

            BuildRings(250, 300);
        }

        void InitializeMap()
        {
            for (int y = 0; y < tileMap.GetLength(1); y++)
            {
                for (int x = 0; x < tileMap.GetLength(0); x++)
                {
                    tileMap[x, y] = -1;
                }
            }
        }

        
        void BuildRings(int r1, int r2)
        {

            for (int i =r1 + 1; i <= r2 - 1; i++)
            {
                BuildRing(i, 1);

            }

            BuildRing(r1, 0);
            BuildRing(r2, 0);
        }

        void BuildRing(float r, int id)
        {
            Point middle = new Point(tileMap.GetLength(0) / 2, tileMap.GetLength(1) / 2);

            for (float d = 0; d < 360; d += .001f)
            {
                int tileX = (int)(r + middle.X);
                int tileY = (int)(r + middle.Y);
                //float mRadius = (float)Math.Pow(tileX - middle.X, 2) + (float)Math.Pow(tileY - middle.Y, 2);
                float mRadius = r; 
                tileX = (int)(Math.Cos(d) * mRadius) + middle.X;
                tileY = (int)(Math.Sin(d) * mRadius) + middle.Y;
                
                tileMap[tileX, tileY] = id;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(Utilities.TextureManager.tiles[0], new Vector2(0, 0), Color.Red);
            spriteBatch.Draw(Utilities.TextureManager.tiles[0], new Vector2(tileMap.GetLength(0) * 32, tileMap.GetLength(1) * 32), Color.Red);

            for (int y = 0; y < tileMap.GetLength(1); y++)
            {
                for(int x = 0; x < tileMap.GetLength(0); x++)
                {
                    if(tileMap[x,y] != -1)
                    {
                        spriteBatch.Draw(Utilities.TextureManager.tiles[tileMap[x, y]], new Vector2(x * 32, y * 32), Color.White);
                    }
                }
            }
        }
        


    }
}
