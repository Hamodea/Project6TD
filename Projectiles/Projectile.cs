using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project6TD.Enemies;
using System;




namespace Project6TD.Projectiles
{
    public class Projectile: GameObject
    {
        private Texture2D texture;
        private Enemy target;
        private float speed;
        private int damage;
        private float rotation;
        private float scale = 0.2f;
        private ParticleSystem particleSystem;
        private bool appliesSlow;


        public Projectile(Vector2 startPos, Enemy target, float speed, int damage, ParticleSystem particleSystem, bool appliesSlow)
        {
            Position = startPos;
            this.target = target;
            this.speed = speed;
            this.damage = damage;

            texture = AssetsManager.projectileTex;
            this.particleSystem = particleSystem;
            this.appliesSlow = appliesSlow;
        }

        public override void Update(GameTime gameTime)
        {
            if (target == null || !target.IsActive)
            {
                IsActive = false;
                return;
            }

            Vector2 direction = target.Position - Position;
            float distance = direction.Length();

            if (distance < 10f)
            {
                target.TakeDamage(damage);

                if (appliesSlow)
                {
                    target.ApplyFreeze(1.5f);
                }

                particleSystem?.SpawnExplosion(Position);

                IsActive = false;
                return;
            }

            direction.Normalize();
            rotation = MathF.Atan2(direction.Y, direction.X);
            Position += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                texture,
                Position,
                null,
                Color.Yellow,
                rotation,
                new Vector2(texture.Width / 2f, texture.Height / 2f), // pivot i mitten
                scale,
                SpriteEffects.None,
                0f
            );
        }

    }
}
