using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project6TD.Enemies;
using Project6TD.Projectiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project6TD.Towers
{
    public class SlowTower : Tower
    {
        private Texture2D texture;
        private float damage = 8f;
        private float projectileSpeed = 400f;
        private readonly ParticleSystem particleSystem;

        public SlowTower(Vector2 position, Texture2D texture, ParticleSystem particleSystem) : base(position, range:170f, fireRate:1.0f)
        {
            this.texture = texture;
        }

        public override void TryAttack(Enemy enemy, ProjectileManager projectileManager, GameTime gameTime)
        {
            if (fireTimer < fireRate)
                return;

            RotateTowards(enemy.Position, gameTime);

            projectileManager.SpawnProjectile(
                new Projectile(
                    Position,
                    enemy,
                    projectileSpeed,          // projectile speed
                    (int)damage,
                    particleSystem,
                    true           // isSlowProjectile
                )
            );

            fireTimer = 0f;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                texture,
                Position,
                null,
                Color.Cyan, // färgindikator
                rotation,
                new Vector2(texture.Width / 2, texture.Height / 2),
                1f,
                SpriteEffects.None,
                0f
            );
        }

        
    }
}
