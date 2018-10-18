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

        
        public InventoryComponent(int parentID, int space): base(parentID)
        {

        }

        public override bool FireEvent(Event _event)
        {

            return base.FireEvent(_event);
        }

        public override void Update(GameTime gameTime, Entity.Entity entity)
        {

            base.Update(gameTime, entity);
        }
    }
}
