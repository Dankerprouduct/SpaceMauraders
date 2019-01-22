using System;
using Microsoft.Xna.Framework;
using SpaceMarauders;

namespace SpaceMarauders.Components
{
    public class PointTowardsMouseComponent: Component
    {

        public PointTowardsMouseComponent(): base()
        {
            ComponentName = "PointTowardsMouseComponent";


        }

        public override bool FireEvent(Event _event)
        {
            return false;
            //return base.FireEvent(_event);
        }

        public override void Update(GameTime gameTime, SpaceMarauders.Entity.Entity entity)
        {
            Vector2 direction = Game1.worldPosition - entity.position;
            direction.Normalize();

            entity.rotation = (float)Math.Atan2(direction.Y, direction.X);

            //base.Update(gameTime, entity);
        }
    }
}
