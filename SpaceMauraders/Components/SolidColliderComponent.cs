using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SpaceMauraders.Entity;

namespace SpaceMauraders.Components
{
    public class SolidColliderComponent: Component
    {

        Rectangle rectangle; 
        public SolidColliderComponent(Entity.Entity entity, int parentID): base(parentID)
        {
            
            rectangle = entity.collisionRectanlge;
            Console.WriteLine(rectangle); 
        }

        public override bool FireEvent(Event _event)
        {
            if(_event.id == "Collider")
            {
                
                foreach (KeyValuePair<string, object> parameter in _event.parameters)
                {

                    if(parameter.Key == "rectangle")
                    {

                        if (((Rectangle)parameter.Value).Intersects(rectangle))
                        {
                            return true; 
                        }
                    }

                }
            }
            return false;
        }

        public override void Update(GameTime gameTime, Entity.Entity entity)
        {
            
            base.Update(gameTime, entity);
        }
    }
}
