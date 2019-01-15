using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceMarauders.Entity;

namespace SpaceMarauders.AI.FSM
{
    public class Idle : IState
    {
        public IState EnterState(NPC entity)
        {
            // find plan
            entity.actionPlan = entity.planner.plan(entity.GetWorldState(), entity.GetGoalState());

            if (entity.actionPlan != null && entity.actionPlan.Count > 0)
            {
                return ExitState(entity);
            }
            else
            {
                return null;
            }
            
        }

        public IState ExitState(NPC entity)
        {
            // found something to do move to 
            return new GoTo();
            return null;
        }

        public IState Update(NPC entity)
        {
            return null; 
        }
    }
}
