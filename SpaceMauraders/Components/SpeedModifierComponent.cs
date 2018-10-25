using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework; 

namespace SpaceMauraders.Components
{
    public class SpeedModifierComponent : Component
    {
        float speedModifier; 
        public SpeedModifierComponent(float modifier) : base()
        {
            componentName = "SpeedModifierComponent";
            speedModifier = modifier;
        }

        public override bool FireEvent(Event _event)
        {

            if (_event.id == "AddVelocity")
            {
                
                if(_event.parameters["Velocity"] != null)
                {
                    _event.parameters["Velocity"] = ((Vector2)_event.parameters["Velocity"]* speedModifier);

                }
            }

            

            return base.FireEvent(_event);
        }

    }
}
