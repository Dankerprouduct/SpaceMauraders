using System.Collections.Generic;
using Microsoft.Xna.Framework;

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

        public override void Update(GameTime gameTime, SpaceMarauders.Entity.Entity entity)
        {
            base.Update(gameTime, entity); 
        }

    }
}
