using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMauraders.Systems.GOAP
{
    public class Action<T> : Action
    {
        protected T _context;

        public Action(T context, string name): base(name)
        {
            _context = context;
            this.name = name;
        }

        public Action(T contenxt, string name, int cost) : this(contenxt, name)
        {
            this.cost = cost;
        }

        public virtual void Execute()
        {

        }
    }
}
