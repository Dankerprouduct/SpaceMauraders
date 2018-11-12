using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework; 


namespace SpaceMauraders.Systems
{
    public class Particle
    {
        public Vector2 position;
        public Vector2 velocity;
        public float mass;
        public float rotation; 

        public bool alive;
        int timeCounter; 
        int timeLimit;

        public Particle()
        {
            alive = false; 
        }

        public Particle(Vector2 position, float mass, float rotation, float force, int lifeSpan)
        {
            this.alive = true; 
            this.position = position;
            this.mass = mass;
            AddForce(rotation, force);

            timeCounter = 0;
            timeLimit = lifeSpan; 
        }

        public void CreateParticle(Vector2 position, float mass, float rotation, float force, int lifeSpan)
        {
            this.alive = true;
            this.position = position;
            this.mass = mass;
            AddForce(rotation, force);
            timeCounter = 0;
            timeLimit = lifeSpan;
        }


        public void Update(GameTime gameTime)
        {

            Timer(gameTime); 

        }
            
        void Timer(GameTime gameTime)
        {
            timeCounter += (int)gameTime.ElapsedGameTime.TotalSeconds; 
            if(timeCounter >= timeLimit)
            {
                alive = false; 
                timeCounter = 0; 
            }
        }

        public void AddForce(float rotation, float force)
        {
            velocity.X += (float)Math.Cos(MathHelper.ToRadians(rotation)) * force;
            velocity.Y += (float)Math.Sin(MathHelper.ToRadians(rotation)) * force; 
        }


    }
}
