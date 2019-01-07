using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMauraders.AI.GOAP
{
    public class Action
    {

        public string name; 

        public int cost = 1;

        public HashSet<KeyValuePair<string, bool>> preConditions = new HashSet<KeyValuePair<string, bool>>();
        public HashSet<KeyValuePair<string, bool>> effects = new HashSet<KeyValuePair<string, bool>>();

        public Action() { }

        public Action(string name)
        {
            this.name = name; 
        }

        public Action(string name, int cost): this(name)
        {
            this.cost = cost; 
        }

        public void AddPrecondition(string conditionName, bool value)
        {
            preConditions.Add(new KeyValuePair<string, bool>(conditionName, value));
        }

        public void AddEffect(string conditionName, bool value)
        {
            preConditions.Add(new KeyValuePair<string, bool>(conditionName, value));
        }


    }
}
