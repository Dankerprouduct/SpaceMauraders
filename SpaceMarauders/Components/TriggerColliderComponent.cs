using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SpaceMarauders.Entity;

namespace SpaceMarauders.Components
{
    public class TriggerColliderComponent: Component
    {

        Rectangle rectangle;
        bool destroy = false; 
        public TriggerColliderComponent(): base()
        {
            ComponentName = "TriggerColliderComponent";
        }

        public TriggerColliderComponent(Rectangle rectangle) : base()
        {
            this.rectangle = rectangle; 
            ComponentName = "TriggerColliderComponent";
        }

        public override bool FireEvent(Event _event)
        {
            //Console.WriteLine(_event.id); 
            if (_event.id == "RayHit")
            {
                //Console.WriteLine("Rectangle " + rectangle + " " + (Point)_event.parameters["Ray"]);
                //Console.WriteLine("ray " + rectangle.Contains((Point)_event.parameters["Ray"]));
                bool hit = rectangle.Contains((Point)_event.parameters["Ray"]);

                if (hit)
                {
                    foreach (KeyValuePair<string, object> parameters in _event.parameters)
                    {

                        if (parameters.Key == "Destroy")
                        {
                            //Console.WriteLine("fsa");
                            destroy = true;
                        }
                    }
                }

                if (hit)
                {
                    //destroy = true;
                }

                return hit; 
            }


            return false; 
        }

        public override void Update(GameTime gameTime, Entity.Entity entity)
        {
            rectangle = entity.collisionRectanlge;
            // = entity; 

            if (destroy)
            {
                entity.Detroy(); 
            }
            base.Update(gameTime, entity);
        }

    }
}
