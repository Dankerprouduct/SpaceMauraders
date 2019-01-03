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
        public float turnAngle; 
        public float force;
        public Color color; 

        public bool alive;
        int timeCounter; 
        int timeLimit;
        public int id;

        public float size = 1;
        public float maxSize = 1;
        public float minSize = 1;
        public float sizeRate = 1;
        public float maxDampening = 99; 
        public float minDampening = 95;
        public float fade = 1;
        public float fadeRate = .995f;
        public float minAngle = 0;
        public float maxAngle = 0;
        public float minSpeed = .0001f; 
        float initialRotation; 
        float damping; 

        public Particle()
        {
            alive = false; 
        }

        public Particle(Vector2 position, float mass, float rotation, float force, int lifeSpan, int id, Color color)
        {
            this.alive = true; 
            this.position = position;
            this.mass = mass;
            this.rotation = rotation;
            this.force = force; 
            AddForce(rotation, force);

            timeCounter = 0;
            timeLimit = lifeSpan;
            this.id = id;
        
            damping = Game1.random.Next(95, 99) / 100;
            this.color = color; 
        }

        public void CreateParticle(Vector2 position, float mass, float force, int lifeSpan, int id, Color color)
        {
            
            this.alive = true;
            this.position = position;
            this.mass = mass;

            this.rotation = MathHelper.ToRadians(Game1.random.Next((int)minAngle, (int)maxAngle));
            //Console.WriteLine(minAngle + " " + maxAngle +" " + rotation);
            AddForce(MathHelper.ToDegrees( rotation), force);
            initialRotation = rotation;
            timeCounter = 0;
            timeLimit = lifeSpan;
            this.id = id;
            
            damping = Game1.random.Next(95, 99);// / 100;
            damping /= 100;
            fade = 1; 
            this.color = color; 
        }
        
        void Initialize()
        {

        }

        public void Update(GameTime gameTime)
        {

            Timer(gameTime);

            size *= sizeRate;
            //Console.WriteLine(sizeRate);
            fade *= fadeRate;
            //color *= fade; 
            rotation += 0;//MathHelper.ToRadians(turnAngle);
            
            velocity *= damping;
            position += velocity; 
            //AddForce(rotation, velocity); 

        }
            
        void Timer(GameTime gameTime)
        {
            timeCounter += (int)gameTime.ElapsedGameTime.TotalSeconds;

            //Console.WriteLine(velocity.Length());
            if (Math.Abs( velocity.Length()) <= minSpeed|| size > maxSize || size < minSize || fade <= 0)
            {
                alive = false;
                //Console.WriteLine("I am dead"); 
            }
        }

        public void AddForce(float rotation, float force)
        {
            velocity.X += ((float)Math.Cos(MathHelper.ToRadians(rotation)) * force) / mass;
            velocity.Y += ((float)Math.Sin(MathHelper.ToRadians(rotation)) * force) / mass;
            //position += velocity;
        }

        public void AddForce(float rotation, Vector2 velocity)
        {
            velocity.X += ((float)Math.Cos(MathHelper.ToRadians(initialRotation)) * velocity.LengthSquared()) / mass;
            velocity.Y += ((float)Math.Sin(MathHelper.ToRadians(initialRotation)) * velocity.LengthSquared()) / mass;
            position += velocity; 
        }


    }
}
