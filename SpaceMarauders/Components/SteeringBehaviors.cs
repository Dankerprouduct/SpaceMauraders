using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace SpaceMarauders.Components
{
    public class SteeringBehaviors: Component
    {
        public SteeringBehaviors()
        {
            ComponentName = "SteeringBehaviors";
        }

        List<SpaceMarauders.Entity.Entity> entities = new List<SpaceMarauders.Entity.Entity>();
        Vector2 position; 

        public override void Update(GameTime gameTime, SpaceMarauders.Entity.Entity entity)
        {

        }

        public override bool FireEvent(Event _event)
        {
            if(_event.id == "Seek")
            {
                           
            }

            if(_event.id == "Arrive")
            {

            }

            if(_event.id == "Pursue")
            {

            }

            if(_event.id  == "Flee")
            {

            }

            if(_event.id == "Evade")
            {

            }

            return false;
        }
    }
}
