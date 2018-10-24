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

        public SolidColliderComponent()
        {
            componentName = "SolidColliderComponent";
        }

        public SolidColliderComponent(Entity.Entity entity, int parentID): base(parentID)
        {
            componentName = "SolidColliderComponent"; 
            rectangle = entity.collisionRectanlge;
        }

        public override bool FireEvent(Event _event)
        {
            //Console.WriteLine(_event.id); 
            
            if (_event.id == "Collider")
            {
                
                foreach (KeyValuePair<string, object> parameter in _event.parameters)
                {

                    if(parameter.Key == "rectangle")
                    {
                        Console.WriteLine("cakked"); 
                        if (((Rectangle)parameter.Value).Intersects(rectangle))
                        {
                            Console.WriteLine("hit "); 
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
