using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMauraders.Systems.FSM
{
    public interface State
    {
        void EnterState(Entity.Entity entity);

        void ExitState(Entity.Entity entity);

        void Update(Entity.Entity entity);
        


    }
}
