using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SpaceMarauders.Entity;

namespace SpaceMarauders.AI.FSM
{
    public class GoTo : IState
    {
        public IState EnterState(NPC entity)
        {
            var action = entity.actionPlan.Peek().name;

            switch (action)
            {
                case "sleep":
                {
                    // navigate to
                    //entity.FindPathTo(new Vector2());
                    break;
                }
            }

            throw new NotImplementedException();

        }

        public IState ExitState(NPC entity)
        {
            throw new NotImplementedException();
        }

        public IState Update(NPC entity)
        {
            
            return null;
        }
    }
}
