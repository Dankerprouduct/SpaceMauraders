using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceMarauders.Entity.Items.Projectiles;
using  Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceMarauders.Utilities;

namespace SpaceMarauders.Systems
{
    public static class ProjectileManager
    {

        public static List<Projectile> projectiles = new List<Projectile>();

        /// <summary>
        /// adds a new projectile to the world
        /// </summary>
        /// <param name="projectile"></param>
        public static void AddProjectile(Projectile projectile)
        {
            projectiles.Add(projectile);
            Console.WriteLine("Added projectile: " + projectile.GetType());
        }

        /// <summary>
        /// updates all of the projectiles in the world that are set to active
        /// </summary>
        /// <param name="gameTime"></param>
        public static void Update(GameTime gameTime)
        {
            for (int i = 0; i < projectiles.Count; i++)
            {
                if (projectiles[i].active)
                {
                    projectiles[i].Update(gameTime);
                }
                else
                {
                    projectiles.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// draws all of the projectiles in the world that are set to active
        /// </summary>
        /// <param name="spriteBatch"></param>
        public static void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < projectiles.Count; i++)
            {
                if (projectiles[i].active)
                {
                    spriteBatch.Draw(TextureManager.sprites[0], projectiles[i].position, Color.White);
                }
                else
                {
                    projectiles.RemoveAt(i);
                }
            }
        }
    }
}
