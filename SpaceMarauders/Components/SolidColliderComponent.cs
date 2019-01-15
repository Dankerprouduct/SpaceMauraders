using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SpaceMarauders.Entity;

namespace SpaceMarauders.Components
{
    /// <summary>
    /// Gives entity a solid collider that makes it impassible 
    /// </summary>
    public class SolidColliderComponent: Component
    {

        Rectangle rectangle;

        /// <summary>
        /// Gives entity a solid collider that makes it impassible 
        /// </summary>
        public SolidColliderComponent()
        {
            ComponentName = "SolidColliderComponent";
        }

        /// <summary>
        /// Gives entity a solid collider that makes it impassible 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="parentID"></param>
        public SolidColliderComponent(Entity.Entity entity, int parentID): base(parentID)
        {
            ComponentName = "SolidColliderComponent"; 
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
                        return ((Rectangle)parameter.Value).Intersects(rectangle); 


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
