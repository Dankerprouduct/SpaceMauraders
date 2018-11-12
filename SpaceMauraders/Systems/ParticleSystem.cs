using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics; 

namespace SpaceMauraders.Systems
{
    public static class ParticleSystem
    {
        public static Particle[] particles; 
        static int poolSize;

        public static void Init(int _poolSize)
        {
            poolSize = _poolSize;
            particles = new Particle[poolSize]; 

            for(int i = 0; i < poolSize; i++)
            {
                particles[i] = new Particle(); 
            }

        }

        public static void AddParticle(Vector2 position, float mass, float rotation, float force)
        {
            for(int i = 0; i < particles.Length; i++)
            {
                if (!particles[i].alive)
                {
                    particles[i].CreateParticle(position, mass, rotation, force, 1); 
                }
            }
        }

        public static void Update(GameTime gameTime)
        {

        } 

        public static void Draw(SpriteBatch spriteBatch)
        {
            for(int i = 0; i < particles.Length; i++)
            {
                
            }
        }

    }
}
