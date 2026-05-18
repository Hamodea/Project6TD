using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project6TD.Enemies;
using Project6TD.Projectiles;
using System;

namespace Project6TD.Towers
{
    public abstract class Tower : GameObject
    {
        protected float range;
        protected float fireRate;
        protected float fireTimer;
        protected float rotation = 0f;
        protected float rotationSpeed = 4f;


        public Tower(Vector2 position, float range, float fireRate)
        {
            Position = position;
            this.range = range;
            this.fireRate = fireRate;
            fireTimer = 0f;
        }

        public override void Update(GameTime gameTime)
        {
            fireTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
        }


        // New: allow polymorphic attack attempts from manager
        public virtual void TryAttack(Enemy enemy, ProjectileManager projectileManager, GameTime gameTime)
        {
            // Default: no-op. Subclasses override to implement firing logic.
        }

        protected void RotateTowards(Vector2 targetPosition, GameTime gameTime)
        {
            Vector2 direction = targetPosition - Position;

            if (direction.LengthSquared() < 0.001f)
                return;

            float targetAngle = MathF.Atan2(direction.Y, direction.X);

            rotation = MathHelper.WrapAngle(
                MathHelper.Lerp(
                    rotation,
                    targetAngle,
                    rotationSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds
                )
            );
        }

    }
}
