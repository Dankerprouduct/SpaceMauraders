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


        public Point goal = Game1.world.spaceStation.nodeMesh.FindNodeOnMesh().arrayPosition;
        public NPC(Vector2 position) : base()
        {
            this.position = position;

            
            components.Add(new Components.PhysicsComponent(this.id));
            components.Add(new Components.InventoryComponent(id, 10));

            //pathfinding 
            //goap
            Console.WriteLine("STARTING PATHFINDING GOAL: "+ goal); 
            position.X += 100;
            pathFinding = new World.Pathfinding();
            

        }

        public override void Update(GameTime gameTime)
        {
            
            if ( nodes != null && nodes.Count > 0)
            {
                Vector2 nodePosition = new Vector2(nodes[0].arrayPosition.X * 128, nodes[0].arrayPosition.Y * 128);
                Vector2 direction = nodePosition - position;

                if (direction.Length() != 0)
                {
                    direction.Normalize();
                }
                

                Components.Event velocityEvent = new Components.Event();
                velocityEvent.id = "AddVelocity";
                velocityEvent.parameters.Add("Velocity", direction * 2);
                FireEvent(velocityEvent);
                

                if (Vector2.Distance(position, new Vector2((nodes[0].arrayPosition.X * 128)  + 64 , (nodes[0].arrayPosition.Y * 128) + 64) ) < 128)
                {    
                    nodes.RemoveAt(0); 
                }
            }
            else
            {
                //Console.WriteLine("NPC NODE POSITION : " + new Point((int)position.X / 128, (int)position.Y / 128));
                World.Node startNode = new World.Node(new Point((int)position.X / 128, (int)position.Y / 128));
                startNode.arrayPosition = new Point((int)position.X / 128, (int)position.Y / 128);
                goal = new Point((int)Game1.player.position.X / 128, (int)Game1.player.position.Y / 128);
                World.Node goalNode = new World.Node(new Point((int)Game1.player.position.X/ 128, (int)Game1.player.position.Y / 128) );
                goalNode.arrayPosition = goal;
                nodes = pathFinding.FindPath(startNode, goalNode);


            }

            base.Update(gameTime);
        }
        

        public override void Draw(SpriteBatch spriteBatch)
        {
            

            

            if (Utilities.Debug.debug)
            {
                if (nodes.Count > 0)
                {
                    GUI.GUI.DrawLine(position, new Vector2(goal.X * 128, goal.Y * 128), 10, Color.Blue);
                    GUI.GUI.DrawLine(position, new Vector2(nodes[0].arrayPosition.X * 128, nodes[0].arrayPosition.Y * 128), 10, Color.Yellow);
                }
                pathFinding.DrawSets();
            }

            spriteBatch.Draw(Utilities.TextureManager.sprites[0], position, Color.White);
            base.Draw(spriteBatch);
        }


    }
}