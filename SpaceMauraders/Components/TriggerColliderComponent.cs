using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SpaceMauraders.Entity;

namespace SpaceMauraders.Components
{
    public class TriggerColliderComponent: Component
    {

        Rectangle rectangle;
        public TriggerColliderComponent(): base()
        {
            componentName = "TriggerColliderComponent";
        }

        public override bool FireEvent(Event _event)
        {
            //Console.WriteLine(_event.id); 
            if (_event.id == "RayHit")
            {
                //Console.WriteLine("Rectangle " + rectangle + " " + (Point)_event.parameters["Ray"]);
                //Console.WriteLine("ray " + rectangle.Contains((Point)_event.parameters["Ray"]));
                return rectangle.Contains((Point)_event.parameters["Ray"]); 
            }

            return false; 
        }

        public override void Update(GameTime gameTime, Entity.Entity entity)
        {
            rectangle = new Rectangle(entity.position.ToPoint(),new Point(64,64)); 
            base.Update(gameTime, entity);
        }

    }
}
