using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMauraders.Entity.Items.Weapons
{
    public class LaserRifle: ProjectileWeapon
    {
        public LaserRifle(): base()
        {
            itemID = 1;
            guiItemID = 1;
            worldItemTextureID = 1;
            entityName = "Laser Rifle"; 
        }

        public override void Use()
        {

            base.Use();
        }

    }
}
