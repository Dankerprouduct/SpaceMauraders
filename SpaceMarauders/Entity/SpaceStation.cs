using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Threading;
using SpaceMarauders.Systems;
using Newtonsoft.Json;
using SpaceMarauders.Utilities;
using MathHelper = Microsoft.Xna.Framework.MathHelper;

namespace SpaceMarauders.Entity
{
    public class SpaceStation: Entity
    {
        public int Diameter;
        int[,] tileMap;
        public CellSpacePartition CellSpacePartition;
        public CellSpacePartition LocalSpacePartition; 
        public Vector2 center;
        
        private int floorTile = 6;
        public int loadedCell;
        List<Room> rooms = new List<Room>();

        public World.NodeMesh nodeMesh; 

        public SpaceStation(int radius): base()
        {
            InitializeMap(radius);



            Thread stationThread = new Thread(BuildStation);
            stationThread.Start();
            stationThread.Join();


            position = new Vector2(0, 0);

            nodeMesh = new World.NodeMesh();
            InitializeCellSpacePartition();
            InitializeILocalCellPartition();


        }

        public SpaceStation(int radius, string name)
        {
            InitializeMap(radius);
            entityName = name;

            GameData<Room> roomData = new GameData<Room>();
            roomData.folderPath = @"Saves\Rooms\";

            Room tempRoom = roomData.LoadObjectData("Room1");
            rooms.Add(tempRoom);
            BuildRoomsOnRing(radius / 4, MathHelper.ToRadians(20));
            BuildRoomsOnRing(radius / 2, MathHelper.ToRadians(10));

            position = new Vector2();
            nodeMesh = new World.NodeMesh();
            InitializeCellSpacePartition();
            InitializeILocalCellPartition();
        }

        /// <summary>
        /// creates the tilemap with diameter (radius * 2)
        /// tilemap assigned -1 values
        /// </summary>
        /// <param name="radius"></param>
        private void InitializeMap(int radius)
        {
            Diameter = radius * 2;
            tileMap = new int[Diameter, Diameter];
            center = new Vector2((Diameter / 2) * 128, (Diameter / 2) * 128);

            InitializeMap();
        }


        /// <summary>
        /// Builds a  predefined room on the SpaceStation tileMap
        /// </summary>
        /// <param name="spawnX"></param>
        /// <param name="spawnY"></param>
        /// <param name="roomId"></param>
        public void CreateRoom(int spawnX, int spawnY, int roomId)
        {
            Room tempRoom = rooms[roomId];

            for (int x = spawnX; x < spawnX + tempRoom.Width; x++)
            {
                for (int y = spawnY; y < spawnY + tempRoom.Height; y++)
                {
                    tileMap[x, y] = tempRoom.tileMap[
                        (spawnX + tempRoom.Width) - x - 1,
                        (spawnY + tempRoom.Height) - y - 1];
                }
            }
        }


        /// <summary>
        /// Builds rooms around radius every angle increment
        /// Takes angleIncrement in Radians
        /// </summary>
        /// <param name="radius"></param>
        /// <param name="angleIncrement">In Radians</param>
        private void BuildRoomsOnRing(float radius, float angleIncrement)
        {
            Point middle = new Point(tileMap.GetLength(0) / 2, tileMap.GetLength(1) / 2);
            

            for (float d = 0; d < 2 * Math.PI; d += angleIncrement)
            {
                int tileX;//= (int)(r + middle.X);
                int tileY;// (int)(r + middle.Y);

                float mRadius = radius;
                tileX = (int)(Math.Cos(d) * mRadius) + middle.X;
                tileY = (int)(Math.Sin(d) * mRadius) + middle.Y;
                
                CreateRoom(tileX, tileY, 0);

            }


        }

        private void BuildStation()
        {

            BuildRings(125, 175);


            BuildRings(250, 300);



            BuildBridge(175, 250, 45, 45.2f, floorTile);
            BuildBridge(175, 250, 45.2f, 45.2f, 3);
            BuildBridge(175, 250, 45, 45, 3);


            BuildBridge(175, 250, 45 - 180, 45.2f - 180, floorTile);
            BuildBridge(175, 250, 45.2f - 180f, 45.2f - 180, 3);
            BuildBridge(175, 250, 45 - 180, 45 - 180, 3);

            BuildBridge(175, 250, 45 - 360, 45.2f - 360, floorTile);
            BuildBridge(175, 250, 45.2f - 360, 45.2f - 360, 3);
            BuildBridge(175, 250, 45 - 360, 45 - 360, 3);


            GameData<Room> roomData = new GameData<Room>();
            roomData.folderPath = @"Saves\Rooms\";            
            Room tempRoom = roomData.LoadObjectData("FloorDecal3");

            rooms.Add(tempRoom);
            BuildRoomsOnRing(150, MathHelper.ToRadians(10f));

            BuildRooms(125, 175, 1.85f);
            BuildRooms(250, 300, 1.95f);

        }

        private void InitializeMap()
        {
            for (int y = 0; y < tileMap.GetLength(1); y++)
            {
                for (int x = 0; x < tileMap.GetLength(0); x++)
                {
                    tileMap[x, y] = -1;
                }
            }
        }
                
        private void BuildRings(int r1, int r2)
        {

            for (int i =r1 + 1; i <= r2 - 1; i++)
            {
                BuildRing(i, floorTile);

            }

            BuildRing(r1, 3);
            BuildRing(r2, 3);
            
        }

        private void BuildBridge(int r1, int r2,float a1, float a2, int id)
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

        private void BuildRing(float r, int id)
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

        private void BuildRooms(int r1, int r2, float roomModifier)
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

                    tileMap[tileX, tileY] = floorTile;
                }

                BuildWall(r1, r2, roomSize, startAngle);
                BuildWall(r1, r2, roomSize, endAngle);

                d = endAngle + MathHelper.ToRadians(3); 
            }

            //tileMap[tileX, tileY] = 3;
        }

        private void BuildWall(float r1, float r2, float roomSize, float angle)
        {
            Point middle = new Point(tileMap.GetLength(0) / 2, tileMap.GetLength(1) / 2);

            for (float w = r2; w >= ((r1 + r2) / roomSize); w -= .01f)
            {
                int tileX = (int)(Math.Cos(angle) * w) + middle.X;
                int tileY = (int)(Math.Sin(angle) * w) + middle.Y;
                tileMap[tileX, tileY] = 3;
            }
        }

        private int[,] GetProperTextures()
        {

            int[,] newTileMap = new int[tileMap.GetLength(0), tileMap.GetLength(1)];
            
            for (int x = 0; x < tileMap.GetLength(0); x++)
            {
                for (int y = 0; y < tileMap.GetLength(1); y++)
                {
                    newTileMap[x, y] = -1;

                    // checks 2 tiles north - west
                    if (IsArrayInBounds(newTileMap, x , y - 1) && tileMap[x , y - 1] == 3)
                    {
                        if (IsArrayInBounds(newTileMap, x - 1, y) && tileMap[x - 1, y] == 3)
                        {
                            newTileMap[x, y] = 11;

                        }
                    }

                    // checks 2 tiles north - east
                    if (IsArrayInBounds(newTileMap, x, y - 1) && tileMap[x, y - 1] == 3)
                    {
                        if (IsArrayInBounds(newTileMap, x +  1, y) && tileMap[x + 1, y] == 3)
                        {
                            newTileMap[x, y] = 10;

                        }
                    }

                    // checks 2 tiles south - east
                    if (IsArrayInBounds(newTileMap, x, y + 1) && tileMap[x, y + 1] == 3)
                    {
                        if (IsArrayInBounds(newTileMap, x + 1, y) && tileMap[x + 1, y] == 3)
                        {
                            newTileMap[x, y] = 9;

                        }
                    }

                    // checks 2 tiles south - west
                    if (IsArrayInBounds(newTileMap, x, y + 1) && tileMap[x, y + 1] == 3)
                    {
                        if (IsArrayInBounds(newTileMap, x - 1, y) && tileMap[x - 1, y] == 3)
                        {
                            newTileMap[x, y] = 8;

                        }
                    }


                    ////////

                    // checks 2 tiles north - south
                    if (IsArrayInBounds(newTileMap, x + 1, y) && tileMap[x + 1, y] == 3)
                    {
                        if (IsArrayInBounds(newTileMap, x - 1, y) && tileMap[x - 1, y] == 3)
                        {
                            newTileMap[x, y] = 5;

                        }
                    }


                    // checks 2 tiles west - east
                    if (IsArrayInBounds(newTileMap, x, y - 1) && tileMap[x, y - 1] == 3)
                    {
                        if (IsArrayInBounds(newTileMap, x, y + 1) && tileMap[x, y + 1] == 3)
                        {
                            newTileMap[x, y] = 6;

                        }
                    }

                    // checks 3 tiles west
                    if (IsArrayInBounds(newTileMap, x - 1, y) && tileMap[x - 1, y] != 3)
                    {
                        if (IsArrayInBounds(newTileMap, x, y - 1) && tileMap[x, y - 1] != 3)
                        {
                            if (IsArrayInBounds(newTileMap, x, y + 1) && tileMap[x, y + 1] != 3)
                            {
                                newTileMap[x, y] = 4;

                            }
                        }
                    }

                    // checks 3 tiles south
                    if (IsArrayInBounds(newTileMap, x - 1, y) && tileMap[x - 1, y] != 3)
                    {
                        if (IsArrayInBounds(newTileMap, x + 1, y) && tileMap[x + 1, y] != 3)
                        {
                            if (IsArrayInBounds(newTileMap, x, y + 1) && tileMap[x, y + 1] != 3)
                            {
                                newTileMap[x, y] = 3; // tile id 3

                            }
                        }
                    }

                    // checks 3 tiles east  
                    if (IsArrayInBounds(newTileMap, x + 1, y) && tileMap[x + 1, y] != 3)
                    {
                        if (IsArrayInBounds(newTileMap, x, y - 1) && tileMap[x, y - 1] != 3)
                        {
                            if (IsArrayInBounds(newTileMap, x, y + 1) && tileMap[x, y + 1] != 3)
                            {
                                newTileMap[x, y] = 2; // tile id 2

                            }
                        }
                    }

                    // checks 3 tiles north
                    if (IsArrayInBounds(newTileMap, x - 1, y) && tileMap[x - 1, y] != 3)
                    {
                        if (IsArrayInBounds(newTileMap, x + 1, y) && tileMap[x + 1, y] != 3)
                        {
                            if (IsArrayInBounds(newTileMap, x, y - 1) && tileMap[x, y - 1] != 3)
                            {
                                newTileMap[x, y] = 1; // tile id 1 

                            }
                        }
                    }

                    // check all 4 surrounding tiles
                    if (IsArrayInBounds(newTileMap, x - 1, y) && tileMap[x - 1, y] == 3) // 3 is wall
                    {
                        if (IsArrayInBounds(newTileMap, x + 1, y) && tileMap[x + 1, y] == 3)
                        {
                            if (IsArrayInBounds(newTileMap, x, y - 1) && tileMap[x, y - 1] == 3)
                            {
                                if (IsArrayInBounds(newTileMap, x, y + 1) && tileMap[x, y + 1] == 3)
                                {

                                    newTileMap[x, y] = 7; // tileid 7
                                    

                                }
                            }
                        }
                    }

                    





                }
            }

            return newTileMap; 
        }

        private bool IsArrayInBounds(int[,] map, int x, int y)
        {
            if (x >= 0 && x < map.GetLength(0))
            {
                if (y >= 0 && y < map.GetLength(1))
                {
                    return true;
                }
            }

            return false;
        }

        private void InitializeCellSpacePartition()
        {
            CellSpacePartition = new Systems.CellSpacePartition(Diameter, Diameter, 4);
            int[,] newTileMap = GetProperTextures();
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
                            case 3:
                                {
                                    switch (newTileMap[x,y])
                                    {
                                        case 1:
                                        {
                                            tempTile = new Tile(new Vector2(position.X + (x * 128), position.Y + (y * 128)), 10, Tile.TileType.Solid);
                                            break;
                                        }
                                        case 2:
                                        {
                                            tempTile = new Tile(new Vector2(position.X + (x * 128), position.Y + (y * 128)), 11, Tile.TileType.Solid);
                                            break;
                                        }
                                        case 3:
                                        {
                                            tempTile = new Tile(new Vector2(position.X + (x * 128), position.Y + (y * 128)), 12, Tile.TileType.Solid);
                                            break;
                                        }
                                        case 4:
                                        {
                                            tempTile = new Tile(new Vector2(position.X + (x * 128), position.Y + (y * 128)), 13, Tile.TileType.Solid);
                                            break;
                                        }
                                        case 5:
                                        {
                                            tempTile = new Tile(new Vector2(position.X + (x * 128), position.Y + (y * 128)), 14, Tile.TileType.Solid);
                                            break;
                                        }
                                        case 6:
                                        {
                                            tempTile = new Tile(new Vector2(position.X + (x * 128), position.Y + (y * 128)), 15, Tile.TileType.Solid);
                                            break;
                                        }
                                        case 7:
                                        {
                                            tempTile = new Tile(new Vector2(position.X + (x * 128), position.Y + (y * 128)), 16, Tile.TileType.Solid);
                                            break;
                                        }
                                        case 8:
                                        {
                                            tempTile = new Tile(new Vector2(position.X + (x * 128), position.Y + (y * 128)), 17, Tile.TileType.Solid);
                                            break;
                                        }
                                        case 9:
                                        {
                                            tempTile = new Tile(new Vector2(position.X + (x * 128), position.Y + (y * 128)), 18, Tile.TileType.Solid);
                                            break;
                                        }
                                        case 10:
                                        {
                                            tempTile = new Tile(new Vector2(position.X + (x * 128), position.Y + (y * 128)), 19, Tile.TileType.Solid);
                                            break;
                                        }
                                        case 11:
                                        {
                                            tempTile = new Tile(new Vector2(position.X + (x * 128), position.Y + (y * 128)), 20, Tile.TileType.Solid);
                                            break;
                                        }
                                        default:
                                        {
                                            tempTile = new Tile(new Vector2(position.X + (x * 128), position.Y + (y * 128)), tileMap[x,y], Tile.TileType.Solid);
                                            break;
                                        }
                                    }
                                    
                                    break; 
                                }
                            default:
                                   {
                                    tempTile = new Tile(new Vector2(position.X + (x * 128), position.Y + (y * 128)), tileMap[x, y], Tile.TileType.NonSolid);
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
                                CellSpacePartition.AddStaticEntity(tempTile);
                                    break;
                            }
                            case Tile.TileType.Solid:
                            {
                                CellSpacePartition.AddEntity(tempTile);
                                break;
                            }
                        }
                        
                                              
                    }
                }
            }

            nodeMesh.MakeMap(3, tileMap); 

            tileMap = null;
        }

        private void InitializeILocalCellPartition()
        {
            LocalSpacePartition = new CellSpacePartition(Diameter, Diameter, 2);
        }

        public bool FireEvent(Components.Event _event, Entity entity)
        {
            
            if (Game1.player.FireEvent(_event))
            {
                return true;
            }

            if (_event.id != "RayHit")
            {

                if (EntityWithinBounds(entity.GetCenterPartition(), CellSpacePartition))
                {
                    if (CellSpacePartition.staticCells[entity.GetCenterPartition()].FireEvent(_event))
                    {
                        return true;
                    }
                }

                if (EntityWithinBounds(entity.GetTopLeftPartition(), CellSpacePartition))
                {
                    if (CellSpacePartition.staticCells[entity.GetTopLeftPartition()].FireEvent(_event))
                    {
                        return true;
                    }
                }

                if (EntityWithinBounds(entity.GetTopPartition(), CellSpacePartition))
                {
                    if (CellSpacePartition.staticCells[entity.GetTopPartition()].FireEvent(_event))
                    {
                        return true;
                    }
                }

                if (EntityWithinBounds(entity.GetTopRightPartition(), CellSpacePartition))
                {
                    if (CellSpacePartition.staticCells[entity.GetTopRightPartition()].FireEvent(_event))
                    {
                        return true;
                    }
                }

                if (EntityWithinBounds(entity.GetRightPartition(), CellSpacePartition))
                {
                    if (CellSpacePartition.staticCells[entity.GetRightPartition()].FireEvent(_event))
                    {
                        return true;
                    }
                }

                if (EntityWithinBounds(entity.GetLeftPartition(), CellSpacePartition))
                {
                    if (CellSpacePartition.staticCells[entity.GetLeftPartition()].FireEvent(_event))
                    {
                        return true;
                    }
                }

                if (EntityWithinBounds(entity.GetBottomLeftPartition(), CellSpacePartition))
                {
                    if (CellSpacePartition.staticCells[entity.GetBottomLeftPartition()].FireEvent(_event))
                    {
                        return true;
                    }
                }

                if (EntityWithinBounds(entity.GetBottomPartition(), CellSpacePartition))
                {
                    if (CellSpacePartition.staticCells[entity.GetBottomPartition()].FireEvent(_event))
                    {
                        return true;
                    }
                }

                if (EntityWithinBounds(entity.GetBottomRightPartition(), CellSpacePartition))
                {
                    if (CellSpacePartition.staticCells[entity.GetBottomRightPartition()].FireEvent(_event))
                    {
                        return true;
                    }
                }


                // DYNAMIC CELLS 
                if (EntityWithinBounds(entity.GetCenterPartition(), CellSpacePartition))
                {
                    if (CellSpacePartition.dynamicCells[entity.GetCenterPartition()].FireEvent(_event))
                    {
                        return true;
                    }
                }

                if (EntityWithinBounds(entity.GetTopLeftPartition(), CellSpacePartition))
                {
                    if (CellSpacePartition.dynamicCells[entity.GetTopLeftPartition()].FireEvent(_event))
                    {
                        return true;
                    }
                }

                if (EntityWithinBounds(entity.GetTopPartition(), CellSpacePartition))
                {
                    if (CellSpacePartition.dynamicCells[entity.GetTopPartition()].FireEvent(_event))
                    {
                        return true;
                    }
                }

                if (EntityWithinBounds(entity.GetTopRightPartition(), CellSpacePartition))
                {
                    if (CellSpacePartition.dynamicCells[entity.GetTopRightPartition()].FireEvent(_event))
                    {
                        return true;
                    }
                }

                if (EntityWithinBounds(entity.GetRightPartition(), CellSpacePartition))
                {
                    if (CellSpacePartition.dynamicCells[entity.GetRightPartition()].FireEvent(_event))
                    {
                        return true;
                    }
                }

                if (EntityWithinBounds(entity.GetLeftPartition(), CellSpacePartition))
                {
                    if (CellSpacePartition.dynamicCells[entity.GetLeftPartition()].FireEvent(_event))
                    {
                        return true;
                    }
                }

                if (EntityWithinBounds(entity.GetBottomLeftPartition(), CellSpacePartition))
                {
                    if (CellSpacePartition.dynamicCells[entity.GetBottomLeftPartition()].FireEvent(_event))
                    {
                        return true;
                    }
                }

                if (EntityWithinBounds(entity.GetBottomPartition(), CellSpacePartition))
                {
                    if (CellSpacePartition.dynamicCells[entity.GetBottomPartition()].FireEvent(_event))
                    {
                        return true;
                    }
                }

                if (EntityWithinBounds(entity.GetBottomRightPartition(), CellSpacePartition))
                {
                    if (CellSpacePartition.dynamicCells[entity.GetBottomRightPartition()].FireEvent(_event))
                    {
                        return true;
                    }
                }

            }

            if (_event.id == "RayHit")
            {
                int partitionIndex = CellSpacePartition.PositionToIndex(((Point) _event.parameters["Ray"]).ToVector2());
                if (EntityWithinBounds(partitionIndex, CellSpacePartition))
                {
                    if (CellSpacePartition.dynamicCells[partitionIndex].FireEvent(_event))
                    {
                        return true;
                    }
                }

                if (EntityWithinBounds(partitionIndex, CellSpacePartition))
                {

                    if (Game1.world.dynamicCellSpacePartition.dynamicCells[partitionIndex].FireEvent(_event))
                    {
                        return true;
                    }
                }
            }
            
            return false;
        }

        public bool FireLocalEvent(Components.Event _event, Entity entity)
        {

            if (EntityWithinBounds(entity.GetCenterPartition(), LocalSpacePartition))
            {
                if (LocalSpacePartition.dynamicCells[entity.GetCenterPartition()].FireEvent(_event))
                {
                    return true;
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

                for (int i = 0; i < CellSpacePartition.staticCells.Length; i++)
                {

                    if (CellSpacePartition.staticCells[i].members != null)
                    {
                        GUI.GUI.DrawBox(new Rectangle(
                            (int)(CellSpacePartition.staticCells[i].x * 2048),
                            (int)(CellSpacePartition.staticCells[i].y * 2048),
                            (int)(CellSpacePartition.staticCells[i].x * 2048) + 2048,
                            (int)(CellSpacePartition.staticCells[i].y * 2048) + 2048), 80, Color.Teal * 1f);
                        
                    }

                }

                for (int i = 0; i < CellSpacePartition.staticCells.Length; i++)
                {

                    if (CellSpacePartition.staticCells[i].members != null)
                    {
                        spriteBatch.DrawString(Utilities.TextureManager.fonts[0],
                            i.ToString(),
                            new Vector2((int)(CellSpacePartition.staticCells[i].members[0,0].cellX * 2048),
                            (int)(CellSpacePartition.staticCells[i].members[0,0].cellY * 2048)), Color.White);
                    }

                }
            }


        }

        public void DrawLoadedCells(SpriteBatch spriteBatch)
        {
            #region Static Cells
            if (EntityWithinBounds(Game1.player.GetCenterPartition(), CellSpacePartition))
            {
                CellSpacePartition.staticCells[Game1.player.GetCenterPartition()].Draw(spriteBatch);
            }

            if (EntityWithinBounds(Game1.player.GetTopLeftPartition(), CellSpacePartition))
            {
                CellSpacePartition.staticCells[Game1.player.GetTopLeftPartition()].Draw(spriteBatch);
            }

            if (EntityWithinBounds(Game1.player.GetTopPartition(), CellSpacePartition))
            {
                CellSpacePartition.staticCells[Game1.player.GetTopPartition()].Draw(spriteBatch);
            }

            if (EntityWithinBounds(Game1.player.GetTopRightPartition(), CellSpacePartition))
            {
                CellSpacePartition.staticCells[Game1.player.GetTopRightPartition()].Draw(spriteBatch);
            }

            if (EntityWithinBounds(Game1.player.GetRightPartition(), CellSpacePartition))
            {
                CellSpacePartition.staticCells[Game1.player.GetRightPartition()].Draw(spriteBatch);
            }

            if (EntityWithinBounds(Game1.player.GetLeftPartition(), CellSpacePartition))
            {
                CellSpacePartition.staticCells[Game1.player.GetLeftPartition()].Draw(spriteBatch);
            }

            if (EntityWithinBounds(Game1.player.GetBottomLeftPartition(), CellSpacePartition))
            {
                CellSpacePartition.staticCells[Game1.player.GetBottomLeftPartition()].Draw(spriteBatch);
            }

            if (EntityWithinBounds(Game1.player.GetBottomPartition(), CellSpacePartition))
            {
                CellSpacePartition.staticCells[Game1.player.GetBottomPartition()].Draw(spriteBatch);
            }

            if (EntityWithinBounds(Game1.player.GetBottomRightPartition(), CellSpacePartition))
            {
                CellSpacePartition.staticCells[Game1.player.GetBottomRightPartition()].Draw(spriteBatch);
            }
            #endregion

            #region Dynamic Cells
            if (EntityWithinBounds(Game1.player.GetCenterPartition(), CellSpacePartition))
            {
                CellSpacePartition.dynamicCells[Game1.player.GetCenterPartition()].Draw(spriteBatch);
            }

            if (EntityWithinBounds(Game1.player.GetTopLeftPartition(), CellSpacePartition))
            {
                CellSpacePartition.dynamicCells[Game1.player.GetTopLeftPartition()].Draw(spriteBatch);
            }

            if (EntityWithinBounds(Game1.player.GetTopPartition(), CellSpacePartition))
            {
                CellSpacePartition.dynamicCells[Game1.player.GetTopPartition()].Draw(spriteBatch);
            }

            if (EntityWithinBounds(Game1.player.GetTopRightPartition(), CellSpacePartition))
            {
                CellSpacePartition.dynamicCells[Game1.player.GetTopRightPartition()].Draw(spriteBatch);
            }

            if (EntityWithinBounds(Game1.player.GetRightPartition(), CellSpacePartition))
            {
                CellSpacePartition.dynamicCells[Game1.player.GetRightPartition()].Draw(spriteBatch);
            }

            if (EntityWithinBounds(Game1.player.GetLeftPartition(), CellSpacePartition))
            {
                CellSpacePartition.dynamicCells[Game1.player.GetLeftPartition()].Draw(spriteBatch);
            }

            if (EntityWithinBounds(Game1.player.GetBottomLeftPartition(), CellSpacePartition))
            {
                CellSpacePartition.dynamicCells[Game1.player.GetBottomLeftPartition()].Draw(spriteBatch);
            }

            if (EntityWithinBounds(Game1.player.GetBottomPartition(), CellSpacePartition))
            {
                CellSpacePartition.dynamicCells[Game1.player.GetBottomPartition()].Draw(spriteBatch);
            }

            if (EntityWithinBounds(Game1.player.GetBottomRightPartition(), CellSpacePartition))
            {
                CellSpacePartition.dynamicCells[Game1.player.GetBottomRightPartition()].Draw(spriteBatch);
            }
            #endregion

            #region Local Cells

            if (EntityWithinBounds(Game1.player.GetCenterPartition(), LocalSpacePartition))
            {
                LocalSpacePartition.dynamicCells[Game1.player.GetCenterPartition()].Draw(spriteBatch);
            }

            if (EntityWithinBounds(Game1.player.GetTopLeftPartition(), LocalSpacePartition))
            {
                LocalSpacePartition.dynamicCells[Game1.player.GetTopLeftPartition()].Draw(spriteBatch);
            }

            if (EntityWithinBounds(Game1.player.GetTopPartition(), LocalSpacePartition))
            {
                LocalSpacePartition.dynamicCells[Game1.player.GetTopPartition()].Draw(spriteBatch);
            }

            if (EntityWithinBounds(Game1.player.GetTopRightPartition(), LocalSpacePartition))
            {
                LocalSpacePartition.dynamicCells[Game1.player.GetTopRightPartition()].Draw(spriteBatch);
            }

            if (EntityWithinBounds(Game1.player.GetRightPartition(), LocalSpacePartition))
            {
                LocalSpacePartition.dynamicCells[Game1.player.GetRightPartition()].Draw(spriteBatch);
            }

            if (EntityWithinBounds(Game1.player.GetLeftPartition(), LocalSpacePartition))
            {
                LocalSpacePartition.dynamicCells[Game1.player.GetLeftPartition()].Draw(spriteBatch);
            }

            if (EntityWithinBounds(Game1.player.GetBottomLeftPartition(), LocalSpacePartition))
            {
                LocalSpacePartition.dynamicCells[Game1.player.GetBottomLeftPartition()].Draw(spriteBatch);
            }

            if (EntityWithinBounds(Game1.player.GetBottomPartition(), LocalSpacePartition))
            {
                LocalSpacePartition.dynamicCells[Game1.player.GetBottomPartition()].Draw(spriteBatch);
            }

            if (EntityWithinBounds(Game1.player.GetBottomRightPartition(), LocalSpacePartition))
            {
                LocalSpacePartition.dynamicCells[Game1.player.GetBottomRightPartition()].Draw(spriteBatch);
            }

            #endregion
        }

        /// <summary>
        /// Checks if entity is within bounds
        /// </summary>
        /// <param name="checkedCell">index of cell to check</param>
        /// <param name="partition"> partition to check </param>
        /// <returns></returns>
        public bool EntityWithinBounds(int checkedCell, CellSpacePartition partition)
        {
            if(checkedCell >= 0 && checkedCell < partition.cellLength)
            {
                return true; 
            }
            return false; 
        }
    }

    public class Room
    {
        public int[,] tileMap;

        public int Width
        {
            get
            {
                if (tileMap != null) return tileMap.GetLength(0);
                else
                {
                    return 0;
                }
            }
        }

        public int Height
        {
            get
            {
                if (tileMap != null) return tileMap.GetLength(1);
                else
                {
                    return 0;
                }
            }
        }
    }
}
