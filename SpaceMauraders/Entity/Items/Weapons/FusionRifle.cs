using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMauraders.Entity.Items.Weapons
{
    public class FusionRifle : ProjectileWeapon
    {
        public FusionRifle() : base()
        {
            itemID = 2;
            guiItemID = 2;
            worldItemTextureID = 2;
            entityName = "Fusion Rifle";
        }

        public override void Use()
        {

            base.Use();
        }

    }
}
