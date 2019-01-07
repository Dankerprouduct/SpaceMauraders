using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceMauraders.Systems;
using SpaceMauraders.Utilities;
using System.Threading;
using MathHelper = Microsoft.Xna.Framework.MathHelper;
using  Microsoft.Xna.Framework.Input; 

namespace SpaceMauraders.Entity.Items.Weapons
{
    public class LaserRifle : ProjectileWeapon
    {
        ParticleEmitter emitter = new ParticleEmitter(2);
        private Thread rayThread;
        private float charge;
        private float maxCharge = 5;
        private int distance = 20;

        public LaserRifle() : base()
        {
            itemID = 0;
            guiItemID = 0;
            worldItemTextureID = 1;
            entityName = "Laser Rifle";
            firePoint = new Vector2(40, -5);
            raycast = new Raycast();
            raycast.rayEvent.parameters.Add("Destroy", true);
            emitter.AddParticle(new Systems.Particle(position, 1, 0, 1f, 0, 0, Color.White)
            {
                fadeRate = .95f,
                maxDampening = 99,
                minDampening = 99,
                minSpeed = .01f,
                minAngle = 0,
                maxAngle = 360,
                size = 1f,
                minSize = .01f,
                sizeRate = .95f,
                maxSize = 5
            });
            emitter.AddParticle(new Systems.Particle(position, 1, 0, 1f, 0, 0, Color.DarkGray)
            {
                fadeRate = .85f,
                maxDampening = 99,
                minDampening = 99,
                minSpeed = .01f,
                minAngle = 0,
                maxAngle = 360,
                size = 1f,
                minSize = .01f,
                sizeRate = .95f,
                maxSize = 5,
                fade = .2f
            });


        }

        public override void Update(GameTime gameTime, Entity entity)
        {
            currentKeyboardState = Keyboard.GetState();
            currentMouseState = Mouse.GetState();


            if (inUse)
            {
                if (charge > 0)
                {
                    emitter.Update(gameTime);
                    emitter.active = true;
                }
            }
            else
            {
                emitter.active = false;
            }


            if (currentMouseState.LeftButton == ButtonState.Pressed)
            {
                charge--;
                if (charge < 0)
                {
                    charge = 0;
                }
            }
            else
            {
                charge++;
                if (charge > maxCharge)
                {
                    charge = maxCharge;
                }
            }
            //Console.WriteLine(charge);

            previousKeyboardState = currentKeyboardState;
            previousMouseState = currentMouseState;
            base.Update(gameTime, entity);
        }

        public override void Use(Entity entity)
        {

            if (charge > 0)
            {
                rayThread = new Thread(RayThread);
                if (!rayThread.IsAlive)
                {
                    rayThread.Start();
                }
                rayThread.Join();
            }

            //GUI.GUI.DrawLine(raycast.points[0].ToVector2());
            base.Use(this);
        }

        private void RayThread()
        {
            //raycast.MakeRay(this, 128 * 20, rotation, 30);
            bool hit = (raycast.MakeRay(this, Utilities.MathHelper.Vec2ToEntitySpace(firePoint, position, rotation),
                128 * distance,
                rotation, 26));
            //emitter.active = true;

            emitter.position = raycast.points[raycast.points.Count - 1].ToVector2();
            if (hit)
            {
                emitter.position = raycast.hit.ToVector2();
                //Console.WriteLine(emitter.position);
                //emitter.active = true; 

            }

        }


        public override void Draw()
        {
            if (raycast.points != null && inUse && charge > 0)
            {
                for (int i = 0; i < raycast.points.Count; i++)
                {

                    if (i < 1)
                    {
                        ParticleSystem.AddParticle(raycast.points[i].ToVector2(),
                            new Systems.Particle(position, 1, 0, 1f, 0, 0, Color.Orange)
                            {
                                fadeRate = .95f,
                                maxDampening = 99,
                                minDampening = 95,
                                minSpeed = .01f,
                                minAngle = MathHelper.ToDegrees(rotation) - 3,
                                maxAngle = MathHelper.ToDegrees(rotation) + 3,
                                size = .2f,
                                minSize = .01f,
                                sizeRate = .99f,
                                maxSize = 5,
                                mass = .3f,

                            });
                    }
                    ParticleSystem.AddParticle(raycast.points[i].ToVector2(),
                            new Systems.Particle(position, 1, 0, 1f, 0, 0, Color.Yellow)
                            {
                                fadeRate = .95f,
                                maxDampening = 99,
                                minDampening = 99,
                                minSpeed = .01f,
                                minAngle = MathHelper.ToDegrees(rotation) - 90,
                                maxAngle = MathHelper.ToDegrees(rotation) + 90,
                                size = .05f,
                                minSize = .01f,
                                sizeRate = .95f,
                                maxSize = 5
                            });
                    ParticleSystem.AddParticle(raycast.points[i].ToVector2(),
                        new Systems.Particle(position, 1, 0, 1f, 0, 0, Color.LightBlue)
                        {
                            fadeRate = .95f,
                            maxDampening = 99,
                            minDampening = 99,
                            minSpeed = .01f,
                            minAngle = MathHelper.ToDegrees(rotation) - 90,
                            maxAngle = MathHelper.ToDegrees(rotation) + 90,
                            size = .09f,
                            minSize = .01f,
                            sizeRate = .95f,
                            maxSize = 5,
                            mass = 1.2f
                        });
                    ParticleSystem.AddParticle(raycast.points[i].ToVector2(),
                        new Systems.Particle(position, 1, 0, 1f, 0, 0, Color.Orange)
                        {
                            fadeRate = .95f,
                            maxDampening = 95,
                            minDampening = 90,
                            minSpeed = .01f,
                            minAngle = MathHelper.ToDegrees(rotation) - 3,
                            maxAngle = MathHelper.ToDegrees(rotation) + 3,
                            size = .2f,
                            minSize = .01f,
                            sizeRate = .99f,
                            maxSize = 5,
                            mass = 1.5f,

                        });
                }

            }
            //base.Draw(spriteBatch);
        }
    }
}
