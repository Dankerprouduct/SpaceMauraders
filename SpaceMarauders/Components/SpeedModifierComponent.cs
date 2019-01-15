using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework; 

namespace SpaceMarauders.Components
{
    public class SpeedModifierComponent : Component
    {
        public float speedModifier; 
        public SpeedModifierComponent(float modifier) : base()
        {
            ComponentName = "SpeedModifierComponent";
            speedModifier = modifier;
        }
        
        public override bool FireEvent(Event _event)
        {

            if (_event.id == "AddVelocity")
            {
                
                if(_event.parameters["Velocity"] != null)
                {
                    //Console.WriteLine(componentName + " "+ _event.parameters["Velocity"]);
                    _event.parameters["Velocity"] = ((Vector2)_event.parameters["Velocity"]* speedModifier);

                }
            }

            

            return base.FireEvent(_event);
        }

    }
}
