using Microsoft.Xna.Framework;

namespace SpaceMarauders.Components
{
    public class HealthComponent: Component

    {
        public int health; 


        public HealthComponent(int health): base()
        {
            this.health = health;
            ComponentName = "HealthComponent"; 
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

        public override void Update(GameTime gameTime, SpaceMarauders.Entity.Entity entity)
        {
            base.Update(gameTime, entity);
        }
    }
}
