using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SpaceMarauders.Entity.Items.Projectiles;
using SpaceMarauders.Systems;
using SpaceMarauders.Components;
using MathHelper = SpaceMarauders.Utilities.MathHelper;

namespace SpaceMarauders.Entity.Items.Weapons
{
    public class GrenadeLauncher: ProjectileWeapon
    {

        public GrenadeLauncher() : base()
        {
            itemID = 2;
            guiItemID = 1;
            worldItemTextureID = 2;
            entityName = "Grenade Launcher"; 

            firePoint = new Vector2(80, 1);
            components.Add(new PointTowardsMouseComponent());


        }

        public override void Update(GameTime gameTime, Entity entity)
        {
            currentMouseState = Mouse.GetState();
            UpdateComponents(gameTime);





            previousKeyboardState = currentKeyboardState;
            base.Update(gameTime, entity);
        }

        public override void Use(Entity entity)
        {
            
            ProjectileManager.AddProjectile(new Grenade(MathHelper.Vec2ToEntitySpace(firePoint, position, rotation), MathHelper.RotationToVector2(rotation), 100f ));
            base.Use(this);
        }
    }
}
