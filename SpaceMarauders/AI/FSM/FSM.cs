using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceMarauders.Entity;

namespace SpaceMarauders.AI.FSM
{
    public class FSM
    {
        private IState state; 
        public FSM(NPC npc, IState initialState)
        {
            state = initialState.EnterState(npc);
        }

        public void Update(Entity.NPC npc)
        {

            if (state.Update(npc) != null)
            {
                state = state.Update(npc);
                state.EnterState(npc); 
            }
        }
    }
}
