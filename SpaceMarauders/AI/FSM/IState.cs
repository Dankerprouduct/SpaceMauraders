using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMarauders.AI.FSM
{
    public interface IState
    {
        IState EnterState(Entity.NPC entity);

        IState ExitState(Entity.NPC entity);

        IState Update(Entity.NPC entity);
        


    }
}
