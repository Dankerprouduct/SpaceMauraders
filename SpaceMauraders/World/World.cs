using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMauraders.World
{
    public class World
    {
        

        struct Module
        {
            public int[,] tileMap; 

            public void InitializeRoom(int width, int height)
            {
                tileMap = new int[width, height]; 
                for(int x = 0; x < width; x++)
                {
                    for(int y = 0; y < height; y++)
                    {
                        tileMap[x, y] = 0; 
                    }
                }
            }

            public void SetRoomID(int id)
            {
                for (int x = 0; x < tileMap.GetLength(0); x++)
                {
                    for (int y = 0; y < tileMap.GetLength(1); y++)
                    {
                        tileMap[x, y] = id;
                    }
                }
            }


        }

        Module[,] modules; 

        public World(int width, int height)
        {
            InitializeTileMap(width, height); 

            // make space station
        }
        
       
        public void InitializeTileMap(int width, int height)
        {
            int w = width;
            int h = height;
            modules = new Module[w, h]; 

            for (int y = 0; y < modules.GetLength(1); y++)
            {
                for(int x = 0; x < modules.GetLength(0); x++)
                {

                    modules[x, y] = new Module();
                    modules[x, y].InitializeRoom(10, 10); 

                }
            }
        }
                
        public void GenerateSpaceStation()
        {
            InitializeTileMap(5, 10);

            int middle = modules.GetLength(0) / 2; 

            // creates a row in the middle .. hopefully
            for(int i = 0; i < modules.GetLength(1); i++)
            {
                modules[middle, i].SetRoomID(1); 
            }


        }

        // generates a space station that has been worn dowm 
        public void GenerateDerelict()
        {
            


        }

    }
}
