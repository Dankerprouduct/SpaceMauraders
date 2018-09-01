using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;


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

            GenerateSpaceStation(); 
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

            int middle = (int)Math.Ceiling((double)(modules.GetLength(0) / 2));

            int max = modules.GetLength(0) - middle;
            Console.WriteLine(max); 

            
            for (int i = 0; i < modules.GetLength(1); i++)
            {
                // creates a row in the middle .. hopefully
                modules[middle, i].SetRoomID(1);

                // place random ammount of rooms adjacently 
                int placeAmmount = Game1.random.Next(0, max);
                Console.WriteLine("place ammount: " + placeAmmount);
                if (Game1.random.Next(0, 2) % 2 == 0)
                {
                    for (int x = 0; x < placeAmmount + 1; x++)
                    {
                        modules[middle + x, i].SetRoomID(1);
                    }
                }
                else
                {
                    for (int x = 0; x < placeAmmount + 1; x++)
                    {
                        modules[middle - x, i].SetRoomID(1);
                    }
                }
            }


            for (int y = 0; y < modules.GetLength(1); y++)
            {
                
                for( int x = 0; x < modules.GetLength(0 ); x++)
                {
                    Console.Write(modules[x, y].tileMap[0, 0] + " "); 
                }
                Console.WriteLine(); 
            }

        }

        // generates a space station that has been worn dowm 
        public void GenerateDerelict()
        {
            


        }

    }
}
