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
        public static int currentParticles;


        public static void Init(int _poolSize)
        {
            poolSize = _poolSize;
            particles = new Particle[poolSize]; 

            for(int i = 0; i < poolSize; i++)
            {
                particles[i] = new Particle(); 
            }

        }

        public static void AddParticle(Vector2 position, float mass, float force, int id, Color color)
        {

            for(int i = 0; i < particles.Length; i++)
            {
                if (!particles[i].alive)
                {
                    //Console.WriteLine("adding particle " + i); 
                    particles[i].CreateParticle(position, mass, Game1.random.Next((int)force, (int)force * 2), 1, id, color);
                    return; 
                }
            }
        }

        public static void AddParticle(Vector2 position, Particle particle)
        {

            for (int i = 0; i < particles.Length; i++)
            {
                if (!particles[i].alive)
                {
                    
                    particles[i].size = particle.size;
                    particles[i].maxSize = particle.maxSize;
                    particles[i].minSize = particle.minSize;
                    particles[i].turnAngle = particle.turnAngle;
                    particles[i].rotation = particle.rotation;
                    particles[i].sizeRate = particle.sizeRate;
                    particles[i].minDampening = particle.minDampening;
                    particles[i].maxDampening = particle.maxDampening;
                    particles[i].color = particle.color;
                    particles[i].fadeRate = particle.fadeRate;
                    particles[i].minAngle = particle.minAngle;
                    particles[i].maxAngle = particle.maxAngle;

                    particles[i].CreateParticle(position, particle.mass, Game1.random.Next((int)particle.force, (int)particle.force * 5), 1, particle.id, particle.color);

                    return;
                }
            }
        }

        public static void Update(GameTime gameTime)
        {
            currentParticles = 0; 
            for(int i = 0; i < particles.Length; i++)
            {
                if (particles[i].alive)
                {
                    particles[i].Update(gameTime);
                    currentParticles++; 
                }
            }
                        
        } 

        public static void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < particles.Length; i++)
            {
                if (particles[i].alive)
                {
                    //spriteBatch.Draw(Utilities.TextureManager.particles[particles[i].id], particles[i].position, particles[i].color * particles[i].velocity.Length() );
                    spriteBatch.Draw(Utilities.TextureManager.particles[particles[i].id],
                        particles[i].position, null,
                        particles[i].color * particles[i].fade,
                        particles[i].rotation,
                        new Vector2(Utilities.TextureManager.particles[particles[i].id].Width /2,
                        Utilities.TextureManager.particles[particles[i].id].Height / 2),
                        particles[i].size,
                        SpriteEffects.None,
                        0f);
                }
            }
        }

    }
}
