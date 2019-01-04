using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using  Microsoft.Xna.Framework; 

namespace SpaceMauraders.Entity.Items.Weapons
{
    public class ProjectileWeapon: Item
    {

        public int maxAmmo;
        public int currentAmmo; 

        public int clipSize;
        public int currentClip;

        // the point where the ray or projectile fires from
        public Vector2 firePoint; 

        public ProjectileWeapon()
        {
            
        }
        
        public override void Use(Entity entity)
        {
            //Console.WriteLine("fired Weapon");
            
        }

    }
}
