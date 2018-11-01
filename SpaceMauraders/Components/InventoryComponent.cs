using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SpaceMauraders.Entity;
using SpaceMauraders.GUI;

namespace SpaceMauraders.Components
{
    public class InventoryComponent: Component
    {

        bool showInventory; 
        public Entity.Entity[,] inventory;
        public int width;
        public int height; 

        public InventoryComponent()
        {
        }

        public InventoryComponent(int parentID, int width, int height) : base(parentID)
        {
            this.width = width;
            this.height = height; 

            InitializeInventory(width, height); 
        }

        void InitializeInventory(int width, int height)
        {
            componentName = "InventoryComponent"; 
            inventory = new Entity.Entity[width, height];
            
            for(int x = 0; x < width; x++)
            {
                for(int y = 0; y < height; y++)
                {
                    inventory[x, y] = new Entity.Entity(); 
                }
            }

        }

        public override bool FireEvent(Event _event)
        {
            switch (_event.id)
            {
                case "AddItem":
                    {
                        foreach(KeyValuePair<string, object> parameter in _event.parameters)
                        {
                            AddItem((Entity.Entity)parameter.Value); 
                        }
                        break; 
                    }
                case "RemoveItem":
                    {
                        foreach (KeyValuePair<string, object> parameter in _event.parameters)
                        {
                            RemoveItem((Entity.Entity)parameter.Value);
                        }
                        break;
                    }
                
            }

            if(_event.id == "OpenInventory")
            {
                showInventory = !showInventory;
            }

            return base.FireEvent(_event);
        }

        /// <summary>
        /// Adds Item to inventory
        /// </summary>
        /// <param name="entity"></param>
        public void AddItem(Entity.Entity entity)
        {
            for(int i = 0; i < inventory.Length; i++)
            {
                
            }
        }

        /// <summary>
        /// Opens player inventory if "OpenInventory" event is sent
        /// </summary>
        

        public void RemoveItem(Entity.Entity entity)
        {

        }

        public override void Update(GameTime gameTime, Entity.Entity entity)
        {

            base.Update(gameTime, entity);
        }

        public void DrawInventory()
        {
            if (showInventory)
            {
                GUI.GUI.Draw2dArray(100, 100, 32, 32, 5, width,height, Color.Red * .5f); 
            }
        }
    }
}
