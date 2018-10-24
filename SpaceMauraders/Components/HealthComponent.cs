using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SpaceMauraders.Entity;

namespace SpaceMauraders.Components
{
    public class HealthComponent: Component

    {
        public int health; 


        public HealthComponent(int health): base()
        {
            this.health = health;
            componentName = "HealthComponent"; 
        }

        public override bool FireEvent(Event _event)
        {
            if(_event.id == "TakeDamage")
            {
                
            }
            if(_event.id == "Heal")
            {

            }

            return base.FireEvent(_event);
        }

        public override void Update(GameTime gameTime, Entity.Entity entity)
        {
            base.Update(gameTime, entity);
        }
    }
}
