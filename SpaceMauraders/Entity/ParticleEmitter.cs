using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SpaceMauraders.Systems;

namespace SpaceMauraders.Entity
{
    public class ParticleEmitter : Entity
    {
        public bool active; 
        public int intensity;
        private List<Particle> particles = new List<Particle>();

        public ParticleEmitter(int intensity): base()
        {
            this.intensity = intensity;
            //this.active = true; 
        }

        public ParticleEmitter(Vector2 position, int intensity): base()
        {
            this.intensity = intensity;
            //this.active = true; 
        }

        public void Toggle()
        {
            active = !active; 
        }

        public override void Update(GameTime gameTime)
        {
            if (active)
            {
                for (int i = 0; i < intensity; i++)
                {
                    
                    int index = Game1.random.Next(0, particles.Count);

                    ParticleSystem.AddParticle(position, particles[index]);
                }
            }
        }

        public void AddParticle(Particle particle)
        {
            particles.Add(particle); 
        }
    }
}
