using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework; 

namespace SpaceMauraders.Entity.Factions.Federation
{
    public class General: NPC
    {
        public General(Vector2 position): base(position)
        {
            //goap
            //Console.WriteLine("STARTING PATHFINDING GOAL: "+ goal); 
            name = Utilities.NameGenerator.GenreateFirstLastName();
            Console.WriteLine("General with name " + name);
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
        }

        public override void Update(GameTime gameTime)
        {



            base.Update(gameTime);
        }

        public override void Draw()
        {
            base.Draw();
        }

    }
}
