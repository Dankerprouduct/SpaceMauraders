using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework; 

namespace SpaceMauraders.Systems
{
    public class ParticleEmitter
    {
        public bool active; 
        public int intensity;
        public Vector2 position; 
        private List<Particle> particles = new List<Particle>();

        public ParticleEmitter(int intensity)
        {
            this.intensity = intensity;
            //this.active = true; 
        }

        public void Toggle()
        {
            active = !active; 
        }

        public void Update()
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
