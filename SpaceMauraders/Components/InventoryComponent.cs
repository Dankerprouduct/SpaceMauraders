using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SpaceMauraders.Entity;
using SpaceMauraders.GUI;
using Microsoft.Xna.Framework.Graphics; 

namespace SpaceMauraders.Components
{
    public class InventoryComponent: Component
    {
        public int width;
        public int height;

        // kind of the "backpack" of the entity
        public int[,] inventory; 

        // whats currently in the entity's hand
        public int primarySlot;
        public int secondarySlot;

        // determines whether or not the inventory is drawn.
        public bool drawInventory; 

        /// <summary>
        /// Initializes the inventory. inventory size is determined by width * height
        /// </summary>
        /// <param name="inventoryWidth"></param>
        /// <param name="inventoryHeight"></param>
        public InventoryComponent(int inventoryWidth, int inventoryHeight): base()
        {
            componentName = "InventoryComponent"; 

            width = inventoryWidth;
            height = inventoryHeight; 
            InitializeInventory(inventoryWidth, inventoryHeight);

            /*
            inventory[1, 1] = 0;
            inventory[0, 3] = 0;
            inventory[1, 8] = 0;
            inventory[0, 5] = 0;
            */


        }

        void InitializeInventory(int inventoryWidth, int inventoryHeight)
        {
            inventory = new int[inventoryWidth, inventoryHeight];

            for(int y = 0; y < inventoryHeight; y++)
            {
                for(int x = 0; x < inventoryWidth; x++)
                {
                    // -1 empty Item
                    inventory[x, y] = -1; 
                }
            }
        }

        /// <summary>
        /// Takes event from "the world" or from the entity
        /// </summary>
        /// <param name="_event"></param>
        /// <returns></returns>
        public override bool FireEvent(Event _event)
        {
            if(_event.id == "AddItem")
            {
                AddItem((int)_event.parameters["itemId"]);
                return true;
            }

            if(_event.id == "RemoveItem")
            {
                RemoveItem((int)_event.parameters["itemId"]);
                return true; 
            }

            if(_event.id == "OpenInventory")
            {
                ToggleDraw(); 
            }

            return false; 
        }


        /// <summary>
        /// finds empty slot and places item in. Item Stacking not currently implemented
        /// </summary>
        /// <param name="itemId"></param>
        private void AddItem(int itemId)
        {
            for(int y =  0; y < height; y++)
            {
                for(int x = 0; x < width; x++)
                {
                    if(inventory[x,y] == -1)
                    {
                        inventory[x, y] = itemId;
                        return; 
                    }
                }
            }
        }

        /// <summary>
        /// finds item with the same id and then removes it by setting it to id -1 (Empty)
        /// </summary>
        /// <param name="itemId"></param>
        private void RemoveItem(int itemId)
        {
            for(int y = 0; y < height; y++)
            {
                for(int x = 0; x < width; x++)
                {
                    if(inventory[x,y] == itemId)
                    {
                        inventory[x, y] = -1;
                        return;
                    }
                }
            }
        }
        
        /// <summary>
        /// swaps inventory with one of the slots
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z">1 = primary slot, 2 = secondary slot</param>
        public void Swap(int x, int y, int z)
        {
            if (z == 1)
            {
                int tempItemID = primarySlot;
                primarySlot = inventory[x, y];
                inventory[x, y] = tempItemID;
            }
            if(z == 2)
            {
                int tempItemID = secondarySlot;
                secondarySlot = inventory[x, y];
                inventory[x, y] = tempItemID;
            }
        }

        public void ToggleDraw()
        {
            drawInventory = !drawInventory; 
        }

        public override void Update(GameTime gameTime, Entity.Entity entity)
        {
            base.Update(gameTime, entity);
        }


        public void Draw()
        {
            int _x = 100;
            int _y = 40;
            int boxWidth = 130;
            int boxHeight = 50;
            int _rows = width;
            int _collums = height;
            int spacing = 5; 
            if (drawInventory)
            {
                GUI.GUI.Draw2dArray(_x, _y, boxWidth, boxHeight, width, height, spacing, Color.Teal);
                for (int x = 0; x < inventory.GetLength(0); x++)
                {
                    for (int y = 0; y < inventory.GetLength(1); y++)
                    {

                        if (inventory[x, y] != -1)
                        {
                            GUI.GUI.DrawTexture(
                                Utilities.TextureManager.guiItemTextures[inventory[x, y]],
                                new Vector2(_x + (x * boxWidth) + (x * spacing),
                                _y + (y * boxHeight) + (y * spacing)));
                        }
                    }
                }
            }
        }



    }
}
