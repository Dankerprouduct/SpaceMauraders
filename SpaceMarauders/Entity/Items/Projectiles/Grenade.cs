using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SpaceMarauders.Components;

namespace SpaceMarauders.Entity.Items.Projectiles
{
    public class Grenade : Projectile
    {
        Event _event = new Event();
        public Grenade(Vector2 position, Vector2 direction, float force)
        {
            this.position = position;
            components.Add(new PhysicsComponent(id));
            _event.id = "AddVelocity";
            _event.parameters.Add("Velocity", (direction * force));

        }

        private bool fired = false;
        public override void Update(GameTime gameTime)
        {
            UpdateComponents(gameTime);

            if (!fired)
            {

                FireEvent(_event);
                fired = true;
            }

            base.Update(gameTime);
        }

        public override void OnHit()
        {
            throw new NotImplementedException();
        }

        public override void Use(Entity entity)
        {
            throw new NotImplementedException();
        }
    }
}
