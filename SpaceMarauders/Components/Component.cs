using Microsoft.Xna.Framework;

namespace SpaceMarauders.Components
{
    public class Component
    {
        int parentID;

        public string ComponentName;
        public bool Active = true; 

        public Component()
        {

        }

        public Component(int parentID)
        {
            this.parentID = parentID; 
        }

        public virtual void Update(GameTime gameTime, SpaceMarauders.Entity.Entity entity)
        {

        }

        public virtual bool FireEvent(Event _event)
        {

            return false; 
        }
    }
}
