using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMauraders.Entity.Items.Weapons
{
    public class ProjectileWeapon: Item
    {

        public int maxAmmo;
        public int currentAmmo; 

        public int clipSize;
        public int currentClip; 

        public ProjectileWeapon()
        {
            
        }
        
        public override void Use()
        {
            Console.WriteLine("fired Weapon");
            
        }

    }
}
