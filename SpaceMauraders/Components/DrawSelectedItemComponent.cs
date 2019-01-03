using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace SpaceMauraders.Components
{
    public class DrawSelectedItemComponent: Component
    {

        public DrawSelectedItemComponent() : base()
        {

        }

        public override void Update(GameTime gameTime, Entity.Entity entity)
        {
            //((InventoryComponent)entity.GetComponent("InventoryComponent")).primarySlot

            base.Update(gameTime, entity);
        }

        public override bool FireEvent(Event _event)
        {
            return base.FireEvent(_event);
        }
    }
}
