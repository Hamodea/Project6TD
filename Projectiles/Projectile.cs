using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project6TD.Enemies;
using System;




namespace Project6TD.Projectiles
{
    public class Projectile
    {
        public Vector2 Position;
        private Texture2D texture;
        private Enemy target;
        private float speed;
        private int damage;
        public bool IsActive = true;
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

        public void Update(GameTime gameTime)
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
                    target.ApplySlow(0.5f, 2f);
                }

                particleSystem?.SpawnExplosion(Position);

                IsActive = false;
                return;
            }

            direction.Normalize();
            rotation = MathF.Atan2(direction.Y, direction.X);
            Position += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void Draw(SpriteBatch spriteBatch)
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
