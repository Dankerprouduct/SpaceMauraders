using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SpaceMauraders.Entity;

namespace SpaceMauraders.Components
{
    public class SteeringBehaviors: Component
    {
        public SteeringBehaviors()
        {
            ComponentName = "SteeringBehaviors";
        }

        List<Entity.Entity> entities = new List<Entity.Entity>();
        Vector2 position; 

        public override void Update(GameTime gameTime, Entity.Entity entity)
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
