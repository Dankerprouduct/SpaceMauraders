using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics; 

namespace SpaceMauraders
{
    public class SpaceStation: Entity.Entity
    {
        int diameter;
        int[,] tileMap;
        Systems.CellSpacePartition cellSpacePartition;
        public Vector2 center; 


        public SpaceStation(int radius): base()
        {            
            diameter = radius * 2;
            tileMap = new int[diameter, diameter];
            center = new Vector2((diameter / 2) * 128, (diameter / 2) * 128);

            InitializeMap();
            BuildRings(125, 175);
            BuildRings(250, 300);
            

            position = new Vector2(0, 0);
            LoadCellSpacePartition();
            
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
                BuildRing(i, 2);

            }

            BuildRing(r1, 3);
            BuildRing(r2, 3);
        }

        void BuildRing(float r, int id)
        {
            Point middle = new Point(tileMap.GetLength(0) / 2, tileMap.GetLength(1) / 2);

            for (float d = 0; d < 360; d += .0005f)
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

        void LoadCellSpacePartition()
        {
            cellSpacePartition = new Systems.CellSpacePartition(diameter, diameter, 4);

            for (int y = 0; y < tileMap.GetLength(1); y++)
            {
                for(int x = 0; x < tileMap.GetLength(0); x++)
                {
                    if (tileMap[x, y] != -1)
                    {
                        //Console.WriteLine(tileMap[x,y]);
                        cellSpacePartition.AddEntity(new Entity.Tile(new Vector2(position.X + (x * 128), position.Y + (y * 128)), tileMap[x, y]));
                                              
                    }
                }
            }
            tileMap = null;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < cellSpacePartition.cells.Length; i++)
            {
                cellSpacePartition.cells[i].Draw(spriteBatch);

                
            }


            if (Game1.debug.debug)
            {
                for (int i = 0; i < cellSpacePartition.cells.Length; i++)
                {

                    if (cellSpacePartition.cells[i].members != null)
                    {
                        GUI.GUI.DrawBox(new Rectangle(
                            (int)cellSpacePartition.cells[i].members[0].GetEntityPosition().X,
                            (int)cellSpacePartition.cells[i].members[0].GetEntityPosition().Y,
                            (int)cellSpacePartition.cells[i].members[cellSpacePartition.cells[i].members.Count() - 1].GetEntityPosition().X + 128,
                            (int)cellSpacePartition.cells[i].members[cellSpacePartition.cells[i].members.Count() - 1].GetEntityPosition().Y + 128), 80, Color.Red);

                        spriteBatch.DrawString(Utilities.TextureManager.fonts[0], i.ToString(), cellSpacePartition.cells[i].members[0].GetEntityPosition(), Color.White);
                    }

                }
                for (int i = 0; i < cellSpacePartition.cells.Length; i++)
                {

                    if (cellSpacePartition.cells[i].members != null)
                    {
                        spriteBatch.DrawString(Utilities.TextureManager.fonts[0],
                            i.ToString(),
                            cellSpacePartition.cells[i].members[0].GetEntityPosition(), Color.White);
                    }

                }
            }


        }
        
    }
}
