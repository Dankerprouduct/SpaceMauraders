using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SpaceMauraders.Entity;

namespace SpaceMauraders.Components
{
    public class InventoryComponent: Component
    {

        public Entity.Entity[] inventory; 
        public InventoryComponent()
        {
        }

        public InventoryComponent(int parentID, int inventoryAmmount) : base(parentID)
        {
            InitializeInventory(inventoryAmmount); 
        }

        void InitializeInventory(int inventoryAmmount)
        {
            componentName = "InventoryComponent"; 
            inventory = new Entity.Entity[inventoryAmmount];

            for(int i = 0; i < inventoryAmmount; i++)
            {
                inventory[i] = new Entity.Entity(); 
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
            return base.FireEvent(_event);
        }

        public void AddItem(Entity.Entity entity)
        {
            for(int i = 0; i < inventory.Length; i++)
            {

            }
        }

        public void RemoveItem(Entity.Entity entity)
        {

        }

        public override void Update(GameTime gameTime, Entity.Entity entity)
        {

            base.Update(gameTime, entity);
        }
    }
}
