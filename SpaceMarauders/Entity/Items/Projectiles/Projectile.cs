using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework; 

namespace SpaceMarauders.Entity.Items.Projectiles
{
    public abstract class Projectile : Item
    {        
        public Vector2 direction;
        
        /// <summary>
        /// Once the projectile has either hit the end of the raypoint or the speed has slowed down to a standstill
        /// </summary>
        public abstract void OnHit(); 
		        
    }
}
