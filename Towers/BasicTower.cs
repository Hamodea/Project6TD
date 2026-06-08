using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project6TD.Enemies;

using Project6TD.Projectiles;
namespace Project6TD.Towers
{
    public class BasicTower : Tower
    {
        private Texture2D texture;
        private float damage = 18f;
        private float projectileSpeed = 400f;
        private readonly ParticleSystem particleSystem;

        public BasicTower(Vector2 position, Texture2D texture, ParticleSystem particleSystem)
            : base(position, range: 160f, fireRate: 0.6f)
        {
            this.texture = texture;
            this.particleSystem = particleSystem;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        //  
        public override void TryAttack(Enemy enemy, ProjectileManager projectileManager, GameTime gameTime)
        {
            RotateTowards(enemy.Position, gameTime);

            if (fireTimer < fireRate)
                return;

            float distance = Vector2.Distance(Position, enemy.Position);

            if (distance <= range)
            {
                projectileManager.SpawnProjectile(
                    new Projectile(
                        Position,
                        enemy,
                        projectileSpeed,
                        (int)damage,
                        particleSystem,
                        false
                    )
                );

                fireTimer = 0f;
            }
        }

        

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                texture,
                Position,
                null,
                Color.White,
                rotation,
                new Vector2(texture.Width / 2, texture.Height / 2),
                0.50f,
                SpriteEffects.None,
                0f
            );
        }
    }
}
