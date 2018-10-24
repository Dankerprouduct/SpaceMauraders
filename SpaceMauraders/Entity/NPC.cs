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
            
            components.Add(new Components.PhysicsComponent(id));
            components.Add(new Components.InventoryComponent(id, 10));
            //pathfinding 
            //goap
            Console.WriteLine("STARTING PATHFINDING GOAL: "+ goal); 
            position.X += 100;
            pathFinding = new World.Pathfinding();
            

        }
        bool updateNext = false; 
        int counter = 0;
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


                //position += direction * 6f;
                bool stupid = true; 
                if (stupid)
                {
                    Components.Event velocityEvent = new Components.Event();
                    velocityEvent.id = "AddVelocity";
                    velocityEvent.parameters.Add("Velocity", direction * 6f);
                    FireEvent(velocityEvent);
                }
                if (Vector2.Distance(position, new Vector2(nodes[0].arrayPosition.X * 128 , nodes[0].arrayPosition.Y * 128) ) < 128)
                {    
                    nodes.RemoveAt(0); 
                }
            }
            else
            {
                Console.WriteLine("NPC NODE POSITION : " + new Point((int)position.X / 128, (int)position.Y / 128));
                World.Node startNode = new World.Node(new Point((int)position.X / 128, (int)position.Y / 128));
                startNode.arrayPosition = new Point((int)position.X / 128, (int)position.Y / 128);
                World.Node goalNode = new World.Node(goal);
                goalNode.arrayPosition = goal;
                nodes = pathFinding.FindPath(startNode, goalNode);


            }

            base.Update(gameTime);
        }
        

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Utilities.TextureManager.sprites[0], position, Color.White);

            if (nodes.Count > 0)
            {
                GUI.GUI.DrawLine(position, new Vector2(goal.X * 128, goal.Y * 128), 10, Color.Blue);
                GUI.GUI.DrawLine(position, new Vector2(nodes[0].arrayPosition.X * 128, nodes[0].arrayPosition.Y * 128), 10, Color.Yellow);
            }

            pathFinding.DrawSets();
            base.Draw(spriteBatch);
        }


    }
}