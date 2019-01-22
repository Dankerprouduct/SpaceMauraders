using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using SpaceMarauders.AI.GOAP;
using SpaceMarauders.Components;
using Action = SpaceMarauders.AI.GOAP.Action;


namespace SpaceMarauders.Entity
{
    public class NPC: Entity
    {

        //public Body.Body body;
        public ActionPlanner planner;
        public Stack<AI.GOAP.Action> actionPlan;
        ParticleEmitter emittter = new ParticleEmitter(1);
        
        public string name; 

        public NPC(Vector2 position) : base()
        {
            this.position = position;
            components.Add(new SpeedModifierComponent(0.5f));
            components.Add(new TriggerColliderComponent());
            components.Add(new PhysicsComponent());
            components.Add(new InventoryComponent(2, 10));
            components.Add(new DrawSelectedItemComponent());
            //components.Add(new Components.InventoryComponent(id, 5,5));
            
        }


        public override void Update(GameTime gameTime)
        {

            // THIS IS MAGICAL CODE. FOR THE LOVE OF GOD DO NOT TOUCH
            // I PROMISE IT WILL SUMMON AN ELDER GOD IF RAN INCORRECTLY
            FindPathTo(Game1.player.GetCenter());
            //Console.WriteLine("update");
            body.Update(position, rotation);
            //emittter.Update(gameTime);
            ///emittter.position = position;
            base.Update(gameTime);
        }

        public virtual WorldState GetWorldState()
        {
            var worldState = planner.createWorldState();

            return worldState; 
        }

        public virtual WorldState GetGoalState()
        {
            var goalState = planner.createWorldState();

            return goalState;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            
                     

            if (Utilities.Debug.debug)
            {
                DrawPathingNodes();
            }
            base.Draw(spriteBatch);

            ((DrawSelectedItemComponent)GetComponent("DrawSelectedItemComponent")).Draw(spriteBatch);
            ((InventoryComponent)GetComponent("InventoryComponent")).DrawWorld();
            //spriteBatch.Draw(Utilities.TextureManager.sprites[0], position, Color.White);
            
        }


    }
}