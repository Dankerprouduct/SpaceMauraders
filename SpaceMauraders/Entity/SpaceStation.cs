using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics; 

namespace SpaceMauraders.Entity
{
    public class SpaceStation: Entity
    {
        int diameter;
        int[,] tileMap;
        public Systems.CellSpacePartition cellSpacePartition;
        public Vector2 center;

        public int loadedCell;

        public World.NodeMesh nodeMesh; 

        public SpaceStation(int radius): base()
        {            
            diameter = radius * 2;
            tileMap = new int[diameter, diameter];
            center = new Vector2((diameter / 2) * 128, (diameter / 2) * 128);

            InitializeMap();
            BuildRings(125, 175);
            
            BuildRings(250, 300);

            BuildBridge(175, 250, 45, 45.2f, 2); 
            BuildBridge(175, 250, 45.2f, 45.2f, 3);
            BuildBridge(175, 250, 45, 45, 3);


            BuildBridge(175, 250, 45 - 180, 45.2f - 180, 2);
            BuildBridge(175, 250, 45.2f - 180f, 45.2f - 180, 3);
            BuildBridge(175, 250, 45 - 180, 45 - 180, 3);

            BuildBridge(175, 250, 45 - 360, 45.2f - 360, 2);
            BuildBridge(175, 250, 45.2f - 360, 45.2f - 360, 3);
            BuildBridge(175, 250, 45 - 360, 45 - 360, 3);

            position = new Vector2(0, 0);

            nodeMesh = new World.NodeMesh();
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

        void BuildBridge(int r1, int r2,float a1, float a2, int id)
        {
            
            Point middle = new Point(tileMap.GetLength(0) / 2, tileMap.GetLength(1) / 2);
            int tileX; //= (int)(r1 + middle.X);
            int tileY; //= (int)(r1 + middle.Y);

            for (float angle = a1; angle <= a2; angle += .001f)
            {
                for (float d = r1; d <= r2; d += .01f)
                {
                    tileX = (int)(Math.Cos(angle) * d) + middle.X;
                    tileY = (int)(Math.Sin(angle) * d) + middle.Y;
                    tileMap[tileX, tileY] = id;

                }
            }
        }

        void BuildRing(float r, int id)
        {
            Point middle = new Point(tileMap.GetLength(0) / 2, tileMap.GetLength(1) / 2);

            for (float d = 0; d < 2 * Math.PI; d += .0005f)
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

                        Tile tempTile = new Tile();

                        switch (tileMap[x, y])
                        {
                            case 0:
                                {
                                    Console.WriteLine("Using Old Tile, please check ");
                                    break;
                                }
                            case 1:
                                {
                                    Console.WriteLine("Using Old Tile, please check ");
                                    break; 
                                }
                            case 2:
                                {
                                    tempTile = new Tile(new Vector2(position.X + (x * 128), position.Y + (y * 128)), tileMap[x, y], Tile.TileType.NonSolid);
                                    break; 
                                }
                            case 3:
                                {
                                    tempTile = new Tile(new Vector2(position.X + (x * 128), position.Y + (y * 128)), tileMap[x, y], Tile.TileType.Solid);
                                    break; 
                                }
                        }
                        if (Utilities.Debug.debug)
                        {
                            Console.WriteLine(tempTile.id + " added tile type " + tempTile.tileType.ToString() + " with " + tempTile.components.Count() + " components");
                        }
                            cellSpacePartition.AddEntity(tempTile);
                                              
                    }
                }
            }

            nodeMesh.MakeMap(2, 3, tileMap); 

            tileMap = null;
        }

        public bool FireEvent(Components.Event _event, Entity entity)
        {
            

            if (EntityWithinBounds(entity.GetCenterPartition()))
            {
                
                return cellSpacePartition.cells[entity.GetCenterPartition()].FireEvent(_event);
            }

            if (EntityWithinBounds(entity.GetTopLeftPartition()))
            {
                return cellSpacePartition.cells[entity.GetTopLeftPartition()].FireEvent(_event);
            }

            if (EntityWithinBounds(entity.GetTopPartition()))
            {
                return cellSpacePartition.cells[entity.GetTopPartition()].FireEvent(_event);
            }

            if (EntityWithinBounds(entity.GetTopRightPartition()))
            {
                return cellSpacePartition.cells[entity.GetTopRightPartition()].FireEvent(_event);
            }

            if (EntityWithinBounds(entity.GetRightPartition()))
            {
                return cellSpacePartition.cells[entity.GetRightPartition()].FireEvent(_event);
            }

            if (EntityWithinBounds(entity.GetLeftPartition()))
            {
                return cellSpacePartition.cells[entity.GetLeftPartition()].FireEvent(_event);
            }

            if (EntityWithinBounds(entity.GetBottomLeftPartition()))
            {
                return cellSpacePartition.cells[entity.GetBottomLeftPartition()].FireEvent(_event);
            }

            if (EntityWithinBounds(entity.GetBottomPartition()))
            {
                return cellSpacePartition.cells[entity.GetBottomPartition()].FireEvent(_event);
            }

            if (EntityWithinBounds(entity.GetBottomRightPartition()))
            {
                return cellSpacePartition.cells[entity.GetBottomRightPartition()].FireEvent(_event);
            }

            return false;
        }

        public void Update()
        {
            
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            DrawLoadedCells(spriteBatch); 

            if (Utilities.Debug.debug)
            {
                nodeMesh.DrawNodes(); 

                for (int i = 0; i < cellSpacePartition.cells.Length; i++)
                {

                    if (cellSpacePartition.cells[i].members != null)
                    {
                        GUI.GUI.DrawBox(new Rectangle(
                            (int)(cellSpacePartition.cells[i].members[0].cellX * 2048),
                            (int)(cellSpacePartition.cells[i].members[0].cellY * 2048),
                            (int)(cellSpacePartition.cells[i].members[0].cellX * 2048) + 2048,
                            (int)(cellSpacePartition.cells[i].members[0].cellY * 2048) + 2048), 80, Color.Red *.1f);
                        
                    }

                }
                for (int i = 0; i < cellSpacePartition.cells.Length; i++)
                {

                    if (cellSpacePartition.cells[i].members != null)
                    {
                        spriteBatch.DrawString(Utilities.TextureManager.fonts[0],
                            i.ToString(),
                            new Vector2((int)(cellSpacePartition.cells[i].members[0].cellX * 2048),
                            (int)(cellSpacePartition.cells[i].members[0].cellY * 2048)), Color.White);
                    }

                }
            }


        }

        public void DrawLoadedCells(SpriteBatch spriteBatch)
        {
            if (EntityWithinBounds(Game1.player.GetCenterPartition()))
            {
                cellSpacePartition.cells[Game1.player.GetCenterPartition()].Draw(spriteBatch);
            }

            if (EntityWithinBounds(Game1.player.GetTopLeftPartition()))
            {
                cellSpacePartition.cells[Game1.player.GetTopLeftPartition()].Draw(spriteBatch);
            }

            if (EntityWithinBounds(Game1.player.GetTopPartition()))
            {
                cellSpacePartition.cells[Game1.player.GetTopPartition()].Draw(spriteBatch);
            }

            if (EntityWithinBounds(Game1.player.GetTopRightPartition()))
            {
                cellSpacePartition.cells[Game1.player.GetTopRightPartition()].Draw(spriteBatch);
            }

            if (EntityWithinBounds(Game1.player.GetRightPartition()))
            {
                cellSpacePartition.cells[Game1.player.GetRightPartition()].Draw(spriteBatch);
            }

            if (EntityWithinBounds(Game1.player.GetLeftPartition()))
            {
                cellSpacePartition.cells[Game1.player.GetLeftPartition()].Draw(spriteBatch);
            }

            if (EntityWithinBounds(Game1.player.GetBottomLeftPartition()))
            {
                cellSpacePartition.cells[Game1.player.GetBottomLeftPartition()].Draw(spriteBatch);
            }

            if (EntityWithinBounds(Game1.player.GetBottomPartition()))
            {
                cellSpacePartition.cells[Game1.player.GetBottomPartition()].Draw(spriteBatch);
            }

            if (EntityWithinBounds(Game1.player.GetBottomRightPartition()))
            {
                cellSpacePartition.cells[Game1.player.GetBottomRightPartition()].Draw(spriteBatch);
            }
        }

        public bool EntityWithinBounds(int checkedCell)
        {
            if(checkedCell >= 0 && checkedCell < cellSpacePartition.cellLength)
            {
                return true; 
            }
            return false; 
        }
    }
}
