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

        List<World.Node> nodes;
        World.Pathfinding pathFinding;
        Utilities.Raycast raycast; 

        
        public Point goal = Game1.world.spaceStation.nodeMesh.FindNodeOnMesh().arrayPosition;
        public NPC(Vector2 position) : base()
        {
            this.position = position;
            components.Add(new Components.SpeedModifierComponent(.95f));

            components.Add(new Components.PhysicsComponent(this.id));
            
            components.Add(new Components.InventoryComponent(id, 10));

             
            //goap
            //Console.WriteLine("STARTING PATHFINDING GOAL: "+ goal); 
            position.X += 100;
            pathFinding = new World.Pathfinding();
            

        }

        bool isPathing = false; 
        public override void Update(GameTime gameTime)
        {


            raycast = new Utilities.Raycast(position, Game1.player.GetCenter());

            // THIS IS MAGICAL CODE. FOR THE LOVE OF GOD DO NOT TOUCH
            // I PROMISE IT WILL SUMMON AN ELDER GOD IF RAN INCORRECTLY
            if (!isPathing)
            {
                if (!raycast.MakeRay(this, 10 * 128, 30)) 
                {
                    Pathfind();
                    isPathing = true;
                }
                else
                {
                    Vector2 direction = Game1.player.GetCenter() - position;

                    if (direction.Length() != 0)
                    {
                        direction.Normalize();
                    }


                    Components.Event velocityEvent = new Components.Event();
                    velocityEvent.id = "AddVelocity";
                    velocityEvent.parameters.Add("Velocity", direction * 2);
                    FireEvent(velocityEvent);
                }
            }

            if(isPathing)
            {
                Pathfind(); 
            }


            base.Update(gameTime);
        }

        public void Pathfind()
        {
            if (nodes != null && nodes.Count > 0)
            {
                Vector2 nodePosition = new Vector2((nodes[0].arrayPosition.X * 128) + 64, (nodes[0].arrayPosition.Y * 128) + 64);
                Vector2 direction = nodePosition - position;

                if (direction.Length() != 0)
                {
                    direction.Normalize();
                }


                Components.Event velocityEvent = new Components.Event();
                velocityEvent.id = "AddVelocity";
                velocityEvent.parameters.Add("Velocity", direction * 2);
                FireEvent(velocityEvent);


                if (Vector2.Distance(position, new Vector2((nodes[0].arrayPosition.X * 128) + 64, (nodes[0].arrayPosition.Y * 128) + 64)) < 64)
                {
                    nodes.RemoveAt(0);

                }
            }
            else
            {
                isPathing = false;
                //Console.WriteLine("NPC NODE POSITION : " + new Point((int)position.X / 128, (int)position.Y / 128));
                World.Node startNode = new World.Node(new Point((int)position.X / 128, (int)position.Y / 128));
                startNode.arrayPosition = new Point((int)position.X / 128, (int)position.Y / 128);
                goal = new Point((int)Game1.player.position.X / 128, (int)Game1.player.position.Y / 128);
                World.Node goalNode = new World.Node(new Point((int)Game1.player.position.X / 128, (int)Game1.player.position.Y / 128));
                goalNode.arrayPosition = goal;
                nodes = pathFinding.FindPath(startNode, goalNode);


            }
        }
            

        public override void Draw(SpriteBatch spriteBatch)
        {
            

            

            if (Utilities.Debug.debug)
            {
                if (nodes.Count > 0 && nodes != null)
                {
                    GUI.GUI.DrawLine(position, new Vector2(goal.X * 128, goal.Y * 128), 10, Color.Blue);
                    GUI.GUI.DrawLine(position, new Vector2(nodes[0].arrayPosition.X * 128, nodes[0].arrayPosition.Y * 128), 10, Color.Yellow);
                }
                pathFinding.DrawSets();
            }
            if (raycast.points != null)
            {
                GUI.GUI.DrawLine(raycast.points[0].ToVector2(), raycast.points[raycast.points.Count - 1].ToVector2(), 3, Color.Red); 
            }
                spriteBatch.Draw(Utilities.TextureManager.sprites[0], position, Color.White);
            base.Draw(spriteBatch);
        }


    }
}