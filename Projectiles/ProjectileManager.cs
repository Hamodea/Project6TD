using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Diagnostics;

namespace Project6TD.Projectiles
{
    public class ProjectileManager
    {
        private List<Projectile> projectiles = new();

        public void SpawnProjectile(Projectile projectile)
        {
            projectiles.Add(projectile);
            //AssetsManager.enemyDamage.Play(0.8f, 0f, 0f);
            AssetsManager.towerShoot.Play();
            
        }

        public void Update(GameTime gameTime)
        {
            for (int i = projectiles.Count - 1; i >= 0; i--)
            {
                projectiles[i].Update(gameTime);

                if (!projectiles[i].IsActive)
                    projectiles.RemoveAt(i);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var p in projectiles)
                p.Draw(spriteBatch);
        }
    }
}
