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
            if (raycast.points != null)
            {
                GUI.GUI.DrawLine(raycast.points[0].ToVector2(), raycast.points[raycast.points.Count - 1].ToVector2(), 3, Color.Red); 
            }
            spriteBatch.Draw(Utilities.TextureManager.sprites[0], position, Color.White);
            base.Draw(spriteBatch);
        }


    }
}