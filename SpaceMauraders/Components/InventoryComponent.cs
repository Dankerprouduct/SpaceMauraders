using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SpaceMauraders.Entity.Items;
using SpaceMauraders.Entity.Items.Weapons;


namespace SpaceMauraders.Components
{
    public class InventoryComponent: Component
    {
        public int width;
        public int height;
        private int xOffset = 100;
        private int yOffset = 40;
        private int boxWidth = 150;
        private int boxHeight = 50;
        private int spacing = 5;

        // kind of the "backpack" of the entity
        public Item[,] inventory; 

        // whats currently in the entity's hand
        public Item slot1 = new FusionRifle();
        public Item slot2 = new EmptyItem();
        public Item slot3 = new EmptyItem();
        public Item slot4 = new EmptyItem();
        public Item slot5 = new EmptyItem();

        // determines whether or not the inventory is drawn.
        public bool drawInventory;
        
        // inputs
        private MouseState currentMouseState;
        private MouseState previousMouseState;
        private Point mousePoint;
        private KeyboardState currentKeyboardState;
        private KeyboardState previousKeyboardState; 


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
            inventory = new Item[inventoryWidth, inventoryHeight];

            for(int y = 0; y < inventoryHeight; y++)
            {
                for(int x = 0; x < inventoryWidth; x++)
                {
                    // -1 empty Item
                    inventory[x, y] = new EmptyItem();
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
                    if(inventory[x,y].itemID == -1)
                    {
                        inventory[x, y] = ItemDictionary.itemDictinary[itemId];
                        Console.WriteLine("Added " + inventory[x,y].entityName);
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
                    if(inventory[x,y].itemID == itemId)
                    {
                        inventory[x, y] = new EmptyItem();
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
                Item tempItemId = slot1;
                if (tempItemId == null)
                {
                    tempItemId = new EmptyItem();
                }
                slot1 = inventory[x, y];
                inventory[x, y] = tempItemId;
            }
            if(z == 2)
            {
                Item tempItemId = slot1;
                if (tempItemId == null)
                {
                    tempItemId = new EmptyItem();
                }
                slot2 = inventory[x, y];
                inventory[x, y] = tempItemId;
            }
            if (z == 3)
            {
                Item tempItemId = slot1;
                if (tempItemId == null)
                {
                    tempItemId = new EmptyItem();
                }
                slot3 = inventory[x, y];
                inventory[x, y] = tempItemId;
            }
            if (z == 4)
            {
                Item tempItemId = slot1;
                if (tempItemId == null)
                {
                    tempItemId = new EmptyItem();
                }
                slot4 = inventory[x, y];
                inventory[x, y] = tempItemId;
            }
            if (z == 5)
            {
                Item tempItemId = slot1;
                if (tempItemId == null)
                {
                    tempItemId = new EmptyItem();
                }
                slot5 = inventory[x, y];
                inventory[x, y] = tempItemId;
            }
        }

        /// <summary>
        /// Adds primary slot item back to inventory 
        /// </summary>
        public void SwapPrimary()
        {
            AddItem(slot1.itemID);
            slot1 = new EmptyItem();
        }
        

        public void ToggleDraw()
        {
            drawInventory = !drawInventory; 
        }

        public override void Update(GameTime gameTime, Entity.Entity entity)
        {
            if (entity is Entity.Player)
            {
                currentKeyboardState = Keyboard.GetState();
                currentMouseState = Mouse.GetState();
                UpdateInventoryLogic();

                if (slot1.itemID != -1)
                {
                    slot1.Update(gameTime, entity);

                    if (currentMouseState.LeftButton == ButtonState.Pressed  && !drawInventory)
                    {
                        slot1.Use(entity);
                        slot1.inUse = true;
                    }
                    else
                    {
                        slot1.inUse = false;
                    }

                }
            }
            else
            {
                //slot1.Use(entity);
                //slot1.inUse = true;
            }

            previousMouseState = currentMouseState; 
            base.Update(gameTime, entity);
        }

        /// <summary>
        /// Deals with updating what gets clicked and swapped in the inventory
        /// </summary>
        private void UpdateInventoryLogic()
        {
            if (drawInventory)
            {
                mousePoint = new Point(Mouse.GetState().X, Mouse.GetState().Y);
                

                for (int x = 0; x < inventory.GetLength(0); x++)
                {
                    for (int y = 0; y < inventory.GetLength(1); y++)
                    {

                        if (inventory[x, y].itemID != -1)
                        {
                            Rectangle rectangle = new Rectangle(xOffset + (x * boxWidth) + (x * spacing),
                                yOffset + (y * boxHeight) + (y * spacing), boxWidth, boxHeight);

                            if (rectangle.Contains(mousePoint))
                            {
                                if (currentMouseState.LeftButton == ButtonState.Pressed &&
                                    previousMouseState.LeftButton == ButtonState.Released)
                                {
                                    Console.WriteLine("Clicked " + Entity.Items.ItemDictionary.itemDictinary[inventory[x, y].guiItemID].entityName);
                                    Swap(x, y, 1);
                                }
                            }
                        }
                    }
                }

                if (new Rectangle(
                    xOffset + xOffset + (inventory.GetLength(0) * (boxWidth / 2)) + (inventory.GetLength(0) * spacing) +
                    50,
                    yOffset, boxWidth, boxHeight).Contains(mousePoint))
                {
                    if (currentMouseState.LeftButton == ButtonState.Pressed &&
                        previousMouseState.LeftButton == ButtonState.Released)
                    {
                        SwapPrimary();
                    }
                }


            }
        }

        public void DrawWorld()
        {
            if (slot1.itemID != -1)
            {
                slot1.Draw();
            }
        }

        public void Draw()
        {
            
            if (drawInventory)
            {
                GUI.GUI.Draw2dArray(xOffset, yOffset, boxWidth, boxHeight, width, height, spacing, Color.Teal);

                GUI.GUI.DrawBox(
                    new Rectangle(xOffset + xOffset + (inventory.GetLength(0) * (boxWidth / 2)) + (inventory.GetLength(0) * spacing) + 50,
                        yOffset, boxWidth, boxHeight)
                    ,Color.Teal);
                if (slot1.itemID != -1)
                {
                    GUI.GUI.DrawTexture(Utilities.TextureManager.guiItemTextures[slot1.guiItemID],
                        new Vector2(
                            xOffset + xOffset + (inventory.GetLength(0) * (boxWidth / 2)) +
                            (inventory.GetLength(0) * spacing) + 50,
                            yOffset));                                        
                }

                for (int x = 0; x < inventory.GetLength(0); x++)
                {
                    for (int y = 0; y < inventory.GetLength(1); y++)
                    {

                        if (inventory[x, y].itemID != -1 && inventory[x, y] != null)
                        {
                            GUI.GUI.DrawTexture(
                                Utilities.TextureManager.guiItemTextures[inventory[x, y].guiItemID],
                                new Vector2(xOffset + (x * boxWidth) + (x * spacing),
                                yOffset + (y * boxHeight) + (y * spacing)));
                                                        
                        }

                        Rectangle rectangle = new Rectangle(xOffset + (x * boxWidth) + (x * spacing),
                                yOffset + (y * boxHeight) + (y * spacing), boxWidth, boxHeight);

                        if (rectangle.Contains(mousePoint))
                        {
                            GUI.GUI.DrawBox(rectangle, Color.Black * .2f); 
                        }

                    }
                }
            }
        }



    }
}
