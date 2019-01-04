using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceMauraders.Entity
{
    public class NPC: Entity
    {
                
        public Body.Body body;

        Systems.ParticleEmitter emittter = new Systems.ParticleEmitter(1);

        public NPC(Vector2 position) : base()
        {
            this.position = position;
            components.Add(new Components.SpeedModifierComponent(.5f));

            components.Add(new Components.PhysicsComponent(this.id));
            components.Add(new Components.InventoryComponent(2, 10));
            components.Add(new Components.DrawSelectedItemComponent());
            //components.Add(new Components.InventoryComponent(id, 5,5));


            //goap
            //Console.WriteLine("STARTING PATHFINDING GOAL: "+ goal); 
            position.X += 100;
            pathFinding = new World.Pathfinding();

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



            Random random = new Random();
            Systems.Particle particle = new Systems.Particle(position, 1, 0, 1, 0, 0, Color.White);
            particle.turnAngle = 0;
            particle.maxSize = 1;
            particle.size = .3f;
            particle.minSize = .001f;
            particle.sizeRate = .99f;
            particle.maxDampening = 98;
            particle.minDampening = 99;
            particle.fadeRate = .98f;
            particle.minAngle = 0;
            particle.maxAngle = 360;
            particle.mass = 2.5f; 

            Systems.Particle particle2 = new Systems.Particle(position, 1, 0, 1, 0, 0, Color.GhostWhite);
            particle2.turnAngle = 0;
            particle2.maxSize = 1;
            particle2.size = .3f;
            particle2.minSize = .001f;
            particle2.sizeRate = .99f;
            particle2.maxDampening = 98;
            particle2.minDampening = 99;
            particle2.fadeRate = .98f;
            particle2.minAngle = 0;
            particle2.maxAngle = 360;
            particle2.mass = 2.5f; 

            emittter.AddParticle(particle);
            emittter.AddParticle(particle2);
            //emittter.Toggle();
        }


        public override void Update(GameTime gameTime)
        {

            // THIS IS MAGICAL CODE. FOR THE LOVE OF GOD DO NOT TOUCH
            // I PROMISE IT WILL SUMMON AN ELDER GOD IF RAN INCORRECTLY
            FindPathTo(Game1.player.GetCenter());

            if (cellIndex != oldCellIndex)
            {
                Game1.world.dynamicCellSpacePartition.ChangeCell(this);
                cellIndex = GetCenterPartition();
                SetCellIndex(cellIndex);
                //Console.WriteLine("Switched to partition " + GetCenterPartition());
            }
            /*
            Vector2 direction = currentPathingTarget - position;
            direction.Normalize();
            rotation = (float)Math.Atan2(direction.Y, direction.X);
            */
            body.Update(position, rotation);
            emittter.Update();
            emittter.position = position;
            //Console.WriteLine(cellIndex); 
            base.Update(gameTime);
        }        

        public override void Draw(SpriteBatch spriteBatch)
        {
            
                     

            if (Utilities.Debug.debug)
            {
                if (pathingNode != null)
                {
                    if (pathingNode.Count > 0 && pathingNode != null)
                    {
                        GUI.GUI.DrawLine(position, new Vector2(pathingGoal.X * 128, pathingGoal.Y * 128), 10, Color.Blue);
                        GUI.GUI.DrawLine(position, new Vector2(pathingNode[0].arrayPosition.X * 128, pathingNode[0].arrayPosition.Y * 128), 10, Color.Yellow);
                    }
                }
                pathFinding.DrawSets();
            }
            if (raycast != null)
            {
                if (raycast.points != null)
                {
                    //GUI.GUI.DrawLine(raycast.points[0].ToVector2(), raycast.points[raycast.points.Count - 1].ToVector2(), 3, Color.Red); 
                }
            }

            body.Draw(spriteBatch);
            ((Components.DrawSelectedItemComponent)GetComponent("DrawSelectedItemComponent")).Draw(spriteBatch);
            //spriteBatch.Draw(Utilities.TextureManager.sprites[0], position, Color.White);
            base.Draw(spriteBatch);
        }


    }
}