using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SpaceMauraders.AI.GOAP;
using Action = SpaceMauraders.AI.GOAP.Action;

namespace SpaceMauraders.Entity.Factions.Federation
{
    public class General: NPC
    {
        
        public General(Vector2 position): base(position)
        {
            //goap
            //Console.WriteLine("STARTING PATHFINDING GOAL: "+ goal); 
            entityName = Utilities.NameGenerator.GenreateFirstLastName();
            Console.WriteLine("General with name " + name);
            position.X += 100;
            //pathFinding = new World.Pathfinding();
            CreateBody();
            

            // setup action planner
            planner = new ActionPlanner();
            var sleep = new Action("sleep"); 
            sleep.setPrecondition("fatigued", true);
            sleep.setPostcondition("fatigued", false);
            planner.addAction(sleep);
            

        }

        
        void CreateState()
        {

        }

        void CreateBody() // in my image
        {
            body = new Body.Body();
            body.AddBodyPart(new Body.Torso(0, Vector2.Zero)
            {
                lerpSpeed = .2f,
                turnAngle = 25,
                scale = 1
            });
            body.AddBodyPart(new Body.Head(1, new Vector2(-22, 0))
            {
                lerpSpeed = 1,
                turnAngle = 5,
                scale = 1f
            });
            body.AddBodyPart(new Body.Hand(2, new Vector2(30, -44))
            {
                scale = 1.3f,
                // try 25 for lols
                lerpSpeed = .1f,
                turnAngle = 10
            });
            body.AddBodyPart(new Body.Hand(2, new Vector2(30, 11))
            {
                scale = 1.3f,
                lerpSpeed = .1f,
                turnAngle = 10
            });
        }

        public override void Update(GameTime gameTime)
        {
            //Console.WriteLine("updating");


            base.Update(gameTime);
        }

        public override void Draw()
        {
            base.Draw();
        }

    }
}
