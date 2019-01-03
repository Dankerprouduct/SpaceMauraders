using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SpaceMauraders.Entity;
using Microsoft.Xna.Framework;

namespace SpaceMauraders.Components
{
    public class PointTowardsMouseComponent: Component
    {

        public PointTowardsMouseComponent(): base()
        {
            componentName = "PointTowardsMouseComponent";


        }

        public override bool FireEvent(Event _event)
        {
            return false;
            //return base.FireEvent(_event);
        }

        public override void Update(GameTime gameTime, Entity.Entity entity)
        {
            Vector2 direction = Game1.worldPosition - entity.position;
            direction.Normalize();

            entity.rotation = (float)Math.Atan2(direction.Y, direction.X);

            //base.Update(gameTime, entity);
        }
    }
}
