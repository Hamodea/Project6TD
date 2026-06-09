using CatmullRom;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;

namespace Project6TD.Enemies
{
    public class Enemy : GameObject
    {
        private CatmullRomPath path;
        private float t;
        private float speed;
        private Animation walkAnimation;

        public float MaxHealth { get; protected set; }
        public float Health { get; protected set; }
        public int Reward { get; protected set; }
        public float Scale { get; protected set; } = 1f;

        //slow
        private float baseSpeed;
        private float slowTimer = 0f;
        private float slowMultiplier = 1f;
        private float freezeTimer = 0f;


        public Enemy(CatmullRomPath path, Animation walkAnimation, float speed)
        {
            this.path = path;
            this.walkAnimation = walkAnimation;
            this.baseSpeed = speed;
            this.speed = speed;

            t = 0f;

            MaxHealth = 100f;
            Health = MaxHealth;
            Reward = 10;
        }

        public override void Update(GameTime gameTime)
        {
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (freezeTimer > 0)
            {
                freezeTimer -= delta;
                speed = 0f;
            }
            else if (slowTimer > 0)
            {
                slowTimer -= delta;
                speed = baseSpeed * slowMultiplier;
            }
            else
            {
                speed = baseSpeed;
            }

            t += speed * delta;

            if (t >= 1f)
            {
                IsActive = false;
                return;
            }

            Position = path.EvaluateAt(t);
            walkAnimation.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Color tint = freezeTimer > 0 ? Color.Lerp(Color.White, Color.Blue, 0.7f) : Color.White;
            walkAnimation.Draw(spriteBatch, Position, Scale, tint);
           
        }

        public void TakeDamage(float damage)
        {
            Health -= damage;
            

            if (Health <= 0)
            {
                Health = 0;
                IsActive = false;
                Debug.WriteLine("Enemy.TakeDamage: enemy died");
                
            }
        }

        public void ApplyFreeze(float duration)
        {
            freezeTimer = duration;
        }
    }
}
