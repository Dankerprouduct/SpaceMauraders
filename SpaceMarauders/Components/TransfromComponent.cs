using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SpaceMarauders.Entity;

namespace SpaceMarauders.Components
{
    public class TransfromComponent: Component
    {
        Vector2 position; 
        
        public TransfromComponent(): base()
        {
            ComponentName = "TransformComponent"; 
        }

        public override bool FireEvent(Event _event)
        {
            if(_event.id == "Translate")
            {
                foreach(KeyValuePair<string, object> parameters in _event.parameters)
                {
                    this.position += (Vector2)parameters.Value; 
                }
            }
            return base.FireEvent(_event);
        }

        public override void Update(GameTime gameTime, Entity.Entity entity)
        {
            base.Update(gameTime, entity); 
        }

    }
}
