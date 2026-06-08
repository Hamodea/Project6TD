using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project6TD.Enemies;
using Project6TD.Projectiles;
namespace Project6TD.Towers
{
    public class StrongTower : Tower
    {
        Texture2D texture;
        private float projectileSpeed = 350f;
        private readonly ParticleSystem particleSystem;
        private float damage = 45f;
    

        public StrongTower(Vector2 Position, Texture2D texture, ParticleSystem particleSystem)
            : base(Position, range: 190f, fireRate: 1.2f)
        {
            this.texture = texture;
            this.particleSystem = particleSystem;
        }



        // Example: override TryAttack with stronger projectile
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

        //public override void Attack(EnemyType enemy)
        //{
        //    // Optional: immediate damage instead of projectiles
        //}

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (texture != null)
            {
                spriteBatch.Draw(
                    texture,
                    Position,
                    null,
                    Color.White,
                    rotation,
                    new Vector2(texture.Width / 2, texture.Height / 2),
                    1f,
                    SpriteEffects.None,
                    0f
                );
            }
        }
    }
}
