using System.Collections.Generic;
using Microsoft.Xna.Framework;

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
        public SolidColliderComponent(SpaceMarauders.Entity.Entity entity, int parentID): base(parentID)
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

        public override void Update(GameTime gameTime, SpaceMarauders.Entity.Entity entity)
        {
            
            base.Update(gameTime, entity);
        }
    }
}
