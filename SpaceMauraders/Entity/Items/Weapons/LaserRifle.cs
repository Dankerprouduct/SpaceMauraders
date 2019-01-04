using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace SpaceMauraders.Entity.Items.Weapons
{
    public class LaserRifle: ProjectileWeapon
    {
        public LaserRifle(): base()
        {
            itemID = 0;
            guiItemID = 0;
            worldItemTextureID = 1;
            entityName = "Laser Rifle"; 
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Use(Entity entity)
        {
            Console.WriteLine("ZAP");
            base.Use(entity);
        }

    }
}
