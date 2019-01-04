using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SpaceMauraders.Entity
{
    public class Player: Entity
    {

        Systems.ParticleEmitter emittter = new Systems.ParticleEmitter(2);
        public Body.Body body;
        
        public Player(Vector2 position):base()
        {
            this.position = position;
            this.collisionRectanlge = new Rectangle((int)position.X, (int)position.Y, Utilities.TextureManager.sprites[0].Width, Utilities.TextureManager.sprites[0].Height);
            
            components.Add(new Components.InputComponent(this.id));
            components.Add(new Components.SpeedModifierComponent(1)); 
             //components.Add(new Components.SpeedModifierComponent(.5f));
            components.Add(new Components.PhysicsComponent(this.id));
            
            components.Add(new Components.TriggerColliderComponent());

            components.Add(new Components.InventoryComponent(2,10));
            components.Add(new Components.PointTowardsMouseComponent());
            components.Add(new Components.DrawSelectedItemComponent());
            

            Random random = new Random();
            emittter.AddParticle(new Systems.Particle(position, 1, random.Next(0, 360), .5f, 0, 0, Color.DarkRed)
            {
                fadeRate = .95f,
                maxDampening =99,
                minDampening =  99,
                minSpeed = .000001f,
                minAngle = 0,
                maxAngle = 360,
                size = .2f,
                minSize = .000000001f,
                sizeRate = .95f,
                maxSize = 5
            });
            emittter.AddParticle(new Systems.Particle(position, 1, random.Next(0, 360), 1, 0, 0, Color.Red)
            {
                fadeRate = .95f,
                maxDampening = 99,
                minDampening = 99,
                minSpeed = .000001f,
                minAngle = 0,
                maxAngle = 360,
                size = .1f,
                minSize = .000000001f,
                sizeRate = .99f,
                maxSize =5,
                mass = 2
                
            });
            //emittter.AddParticle(new Systems.Particle(position, 1, random.Next(0, 360), 5, 10, 0, Color.Red));
            //emittter.AddParticle(new Systems.Particle(position, 1, random.Next(0, 360), 5, 10, 0, Color.Orange));
            //emittter.AddParticle(new Systems.Particle(position, 1, random.Next(0, 360), 5, 10, 0, Color.Black));
            //emittter.Toggle(); 


            body = new Body.Body();
            
            body.AddBodyPart(new Body.Torso(0, Vector2.Zero)
            {
                lerpSpeed = .2f,
                turnAngle = 25
            });
            
            body.AddBodyPart(new Body.Head(1, new Vector2(-9, 0))
            {
                lerpSpeed = .2f,
                turnAngle = 5,
                scale = .5f
            });
            body.AddBodyPart(new Body.Hand(2, new Vector2(15, -22))
            {
                scale = .7f,
                // try 25 for lols
                lerpSpeed = .1f,
                turnAngle = 10
            });
            body.AddBodyPart(new Body.Hand(2, new Vector2(15, 22))
            {
                scale = .7f,
                lerpSpeed = .1f,
                turnAngle = 10
            });


            Components.Event addItem = new Components.Event();
            addItem.id = "AddItem";
            addItem.parameters.Add("itemId", 0);

            for (int i =0; i < 2; i++)
            {
                FireEvent(addItem); 
            }
            addItem = new Components.Event();
            addItem.id = "AddItem";
            addItem.parameters.Add("itemId", 1);
            FireEvent(addItem);

            Components.Event removeItem = new Components.Event();
            removeItem.id = "RemoveItem";
            removeItem.parameters.Add("itemId", 0);
            //FireEvent(removeItem);
            FireEvent(removeItem);

        }


        public override void Update(GameTime gameTime)
        {

            emittter.Update(); 
            emittter.position = position;
            
            body.Update(position, rotation); 

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            body.Draw(spriteBatch);
            ((Components.DrawSelectedItemComponent)GetComponent("DrawSelectedItemComponent")).Draw(spriteBatch);
            base.Draw(spriteBatch);
        }

        public void DrawInventory()
        {
            ((Components.InventoryComponent)GetComponent("InventoryComponent")).Draw();
        }

    }
}
