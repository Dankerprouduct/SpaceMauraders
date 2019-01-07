using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMauraders.AI.FSM
{
    public interface State
    {

        void Enter(Entity.Entity entity);

        void Exit(Entity.Entity entity);

        void Update(Entity.Entity entity);

        
    }
}
