using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Threading;

namespace SpaceMauraders.Entity
{
    public class SpaceStation: Entity
    {
        public int diameter;
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

            
            Thread stationThread = new Thread(BuildStation);
            stationThread.Start();
            stationThread.Join(); 
            

            position = new Vector2(0, 0);

            nodeMesh = new World.NodeMesh();
            LoadCellSpacePartition();
           
            
        }

        void BuildStation()
        {

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

            BuildRooms(125, 175, 1.85f);
            BuildRooms(250, 300, 1.95f);

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

            int startRoom, endRoom; 

            for (float d = 0; d < 2 * Math.PI; d += .0005f)
            {
                int tileX;//= (int)(r + middle.X);
                int tileY;// (int)(r + middle.Y);

                float mRadius = r; 
                tileX = (int)(Math.Cos(d) * mRadius) + middle.X;
                tileY = (int)(Math.Sin(d) * mRadius) + middle.Y;
                
                tileMap[tileX, tileY] = id;
                
            }


        }

        void BuildRooms(int r1, int r2, float roomModifier)
        {
            int tileX = 0; 
            int tileY = 0;

            float startAngle;
            float endAngle;
            float roomSize = roomModifier;
            //float mRadius;

            Point middle = new Point(tileMap.GetLength(0) / 2, tileMap.GetLength(1) / 2);

            for (float d = 0; d < 2 * Math.PI; d += .0001f)
            {
                startAngle = d;
                endAngle = startAngle + MathHelper.ToRadians(Game1.random.Next(3, 7));
                
                
                // Close Room                             
                for (float a = startAngle; a < endAngle; a += .0001f)
                {

                    tileX = (int)(Math.Cos(a) * ((r1 + r2) / roomSize)) + middle.X;
                    tileY = (int)(Math.Sin(a) * ((r1 + r2) / roomSize)) + middle.Y;

                    tileMap[tileX, tileY] = 3;

                    tileX = (int)(Math.Cos(a) * r2) + middle.X;
                    tileY = (int)(Math.Sin(a) * r2) + middle.Y;

                    tileMap[tileX, tileY] = 3;
                }

                float roomOpening = ((startAngle + endAngle) / 2);
                for (float r = roomOpening; r <= roomOpening + MathHelper.ToRadians(.5f); r += .001f)
                {
                    tileX = (int)(Math.Cos(r) * ((r1 + r2) / roomSize)) + middle.X;
                    tileY = (int)(Math.Sin(r) * ((r1 + r2) / roomSize)) + middle.Y;

                    tileMap[tileX, tileY] = 2;
                }

                BuildWall(r1, r2, roomSize, startAngle);
                BuildWall(r1, r2, roomSize, endAngle);

                d = endAngle + MathHelper.ToRadians(3); 
            }

            //tileMap[tileX, tileY] = 3;
        }

        void BuildWall(float r1, float r2, float roomSize, float angle)
        {
            Point middle = new Point(tileMap.GetLength(0) / 2, tileMap.GetLength(1) / 2);

            for (float w = r2; w >= ((r1 + r2) / roomSize); w -= .01f)
            {
                int tileX = (int)(Math.Cos(angle) * w) + middle.X;
                int tileY = (int)(Math.Sin(angle) * w) + middle.Y;
                tileMap[tileX, tileY] = 3;
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

                        switch (tempTile.tileType)
                        {
                            case Tile.TileType.NonSolid:
                            {
                                cellSpacePartition.AddStaticEntity(tempTile);
                                    break;
                            }
                            case Tile.TileType.Solid:
                            {
                                cellSpacePartition.AddEntity(tempTile);
                                break;
                            }
                        }
                        
                                              
                    }
                }
            }

            nodeMesh.MakeMap(2, 3, tileMap); 

            tileMap = null;
        }

        public bool FireEvent(Components.Event _event, Entity entity)
        {
            
            if (Game1.player.FireEvent(_event))
            {
                //Console.WriteLine(Game1.player.FireEvent(_event)); 
                return true;
                //Console.WriteLine("git this ");
            }

            if (_event.id != "RayHit")
            {

                if (EntityWithinBounds(entity.GetCenterPartition()))
                {
                    if (cellSpacePartition.staticCells[entity.GetCenterPartition()].FireEvent(_event))
                    {
                        return true;
                    }
                }

                if (EntityWithinBounds(entity.GetTopLeftPartition()))
                {
                    if (cellSpacePartition.staticCells[entity.GetTopLeftPartition()].FireEvent(_event))
                    {
                        return true;
                    }
                }

                if (EntityWithinBounds(entity.GetTopPartition()))
                {
                    if (cellSpacePartition.staticCells[entity.GetTopPartition()].FireEvent(_event))
                    {
                        return true;
                    }
                }

                if (EntityWithinBounds(entity.GetTopRightPartition()))
                {
                    if (cellSpacePartition.staticCells[entity.GetTopRightPartition()].FireEvent(_event))
                    {
                        return true;
                    }
                }

                if (EntityWithinBounds(entity.GetRightPartition()))
                {
                    if (cellSpacePartition.staticCells[entity.GetRightPartition()].FireEvent(_event))
                    {
                        return true;
                    }
                }

                if (EntityWithinBounds(entity.GetLeftPartition()))
                {
                    if (cellSpacePartition.staticCells[entity.GetLeftPartition()].FireEvent(_event))
                    {
                        return true;
                    }
                }

                if (EntityWithinBounds(entity.GetBottomLeftPartition()))
                {
                    if (cellSpacePartition.staticCells[entity.GetBottomLeftPartition()].FireEvent(_event))
                    {
                        return true;
                    }
                }

                if (EntityWithinBounds(entity.GetBottomPartition()))
                {
                    if (cellSpacePartition.staticCells[entity.GetBottomPartition()].FireEvent(_event))
                    {
                        return true;
                    }
                }

                if (EntityWithinBounds(entity.GetBottomRightPartition()))
                {
                    if (cellSpacePartition.staticCells[entity.GetBottomRightPartition()].FireEvent(_event))
                    {
                        return true;
                    }
                }


                // DYNAMIC CELLS 
                if (EntityWithinBounds(entity.GetCenterPartition()))
                {
                    if (cellSpacePartition.dynamicCells[entity.GetCenterPartition()].FireEvent(_event))
                    {
                        return true;
                    }
                }

                if (EntityWithinBounds(entity.GetTopLeftPartition()))
                {
                    if (cellSpacePartition.dynamicCells[entity.GetTopLeftPartition()].FireEvent(_event))
                    {
                        return true;
                    }
                }

                if (EntityWithinBounds(entity.GetTopPartition()))
                {
                    if (cellSpacePartition.dynamicCells[entity.GetTopPartition()].FireEvent(_event))
                    {
                        return true;
                    }
                }

                if (EntityWithinBounds(entity.GetTopRightPartition()))
                {
                    if (cellSpacePartition.dynamicCells[entity.GetTopRightPartition()].FireEvent(_event))
                    {
                        return true;
                    }
                }

                if (EntityWithinBounds(entity.GetRightPartition()))
                {
                    if (cellSpacePartition.dynamicCells[entity.GetRightPartition()].FireEvent(_event))
                    {
                        return true;
                    }
                }

                if (EntityWithinBounds(entity.GetLeftPartition()))
                {
                    if (cellSpacePartition.dynamicCells[entity.GetLeftPartition()].FireEvent(_event))
                    {
                        return true;
                    }
                }

                if (EntityWithinBounds(entity.GetBottomLeftPartition()))
                {
                    if (cellSpacePartition.dynamicCells[entity.GetBottomLeftPartition()].FireEvent(_event))
                    {
                        return true;
                    }
                }

                if (EntityWithinBounds(entity.GetBottomPartition()))
                {
                    if (cellSpacePartition.dynamicCells[entity.GetBottomPartition()].FireEvent(_event))
                    {
                        return true;
                    }
                }

                if (EntityWithinBounds(entity.GetBottomRightPartition()))
                {
                    if (cellSpacePartition.dynamicCells[entity.GetBottomRightPartition()].FireEvent(_event))
                    {
                        return true;
                    }
                }

            }

            if (_event.id == "RayHit")
            {
                int partitionIndex = cellSpacePartition.PositionToIndex(((Point) _event.parameters["Ray"]).ToVector2());
                if (EntityWithinBounds(partitionIndex))
                {
                    if (cellSpacePartition.dynamicCells[partitionIndex].FireEvent(_event))
                    {
                        return true;
                    }
                }

                if (EntityWithinBounds(partitionIndex))
                {

                    if (Game1.world.dynamicCellSpacePartition.dynamicCells[partitionIndex].FireEvent(_event))
                    {
                        return true;
                    }
                }
            }
            
            return false;
        }

        public void Update()
        {
            
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            DrawLoadedCells(spriteBatch);

            if (Utilities.Debug.showBoundariesAndMesh)
            {
                nodeMesh.DrawNodes();
            }
            if (Utilities.Debug.debug)
            {
                

                for (int i = 0; i < cellSpacePartition.staticCells.Length; i++)
                {

                    if (cellSpacePartition.staticCells[i].members != null)
                    {
                        GUI.GUI.DrawBox(new Rectangle(
                            (int)(cellSpacePartition.staticCells[i].x * 2048),
                            (int)(cellSpacePartition.staticCells[i].y * 2048),
                            (int)(cellSpacePartition.staticCells[i].x * 2048) + 2048,
                            (int)(cellSpacePartition.staticCells[i].y * 2048) + 2048), 80, Color.Red *.1f);
                        
                    }

                }

                for (int i = 0; i < cellSpacePartition.staticCells.Length; i++)
                {

                    if (cellSpacePartition.staticCells[i].members != null)
                    {
                        spriteBatch.DrawString(Utilities.TextureManager.fonts[0],
                            i.ToString(),
                            new Vector2((int)(cellSpacePartition.staticCells[i].members[0,0].cellX * 2048),
                            (int)(cellSpacePartition.staticCells[i].members[0,0].cellY * 2048)), Color.White);
                    }

                }
            }


        }

        public void DrawLoadedCells(SpriteBatch spriteBatch)
        {
            if (EntityWithinBounds(Game1.player.GetCenterPartition()))
            {
                cellSpacePartition.staticCells[Game1.player.GetCenterPartition()].Draw(spriteBatch);
            }

            if (EntityWithinBounds(Game1.player.GetTopLeftPartition()))
            {
                cellSpacePartition.staticCells[Game1.player.GetTopLeftPartition()].Draw(spriteBatch);
            }

            if (EntityWithinBounds(Game1.player.GetTopPartition()))
            {
                cellSpacePartition.staticCells[Game1.player.GetTopPartition()].Draw(spriteBatch);
            }

            if (EntityWithinBounds(Game1.player.GetTopRightPartition()))
            {
                cellSpacePartition.staticCells[Game1.player.GetTopRightPartition()].Draw(spriteBatch);
            }

            if (EntityWithinBounds(Game1.player.GetRightPartition()))
            {
                cellSpacePartition.staticCells[Game1.player.GetRightPartition()].Draw(spriteBatch);
            }

            if (EntityWithinBounds(Game1.player.GetLeftPartition()))
            {
                cellSpacePartition.staticCells[Game1.player.GetLeftPartition()].Draw(spriteBatch);
            }

            if (EntityWithinBounds(Game1.player.GetBottomLeftPartition()))
            {
                cellSpacePartition.staticCells[Game1.player.GetBottomLeftPartition()].Draw(spriteBatch);
            }

            if (EntityWithinBounds(Game1.player.GetBottomPartition()))
            {
                cellSpacePartition.staticCells[Game1.player.GetBottomPartition()].Draw(spriteBatch);
            }

            if (EntityWithinBounds(Game1.player.GetBottomRightPartition()))
            {
                cellSpacePartition.staticCells[Game1.player.GetBottomRightPartition()].Draw(spriteBatch);
            }

            // DYNAMIC CELLS 
            if (EntityWithinBounds(Game1.player.GetCenterPartition()))
            {
                cellSpacePartition.dynamicCells[Game1.player.GetCenterPartition()].Draw(spriteBatch);
            }

            if (EntityWithinBounds(Game1.player.GetTopLeftPartition()))
            {
                cellSpacePartition.dynamicCells[Game1.player.GetTopLeftPartition()].Draw(spriteBatch);
            }

            if (EntityWithinBounds(Game1.player.GetTopPartition()))
            {
                cellSpacePartition.dynamicCells[Game1.player.GetTopPartition()].Draw(spriteBatch);
            }

            if (EntityWithinBounds(Game1.player.GetTopRightPartition()))
            {
                cellSpacePartition.dynamicCells[Game1.player.GetTopRightPartition()].Draw(spriteBatch);
            }

            if (EntityWithinBounds(Game1.player.GetRightPartition()))
            {
                cellSpacePartition.dynamicCells[Game1.player.GetRightPartition()].Draw(spriteBatch);
            }

            if (EntityWithinBounds(Game1.player.GetLeftPartition()))
            {
                cellSpacePartition.dynamicCells[Game1.player.GetLeftPartition()].Draw(spriteBatch);
            }

            if (EntityWithinBounds(Game1.player.GetBottomLeftPartition()))
            {
                cellSpacePartition.dynamicCells[Game1.player.GetBottomLeftPartition()].Draw(spriteBatch);
            }

            if (EntityWithinBounds(Game1.player.GetBottomPartition()))
            {
                cellSpacePartition.dynamicCells[Game1.player.GetBottomPartition()].Draw(spriteBatch);
            }

            if (EntityWithinBounds(Game1.player.GetBottomRightPartition()))
            {
                cellSpacePartition.dynamicCells[Game1.player.GetBottomRightPartition()].Draw(spriteBatch);
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
