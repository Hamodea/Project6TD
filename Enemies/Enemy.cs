using CatmullRom;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

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


        public Enemy(CatmullRomPath path, Animation walkAnimation, float speed)
        {
            this.path = path;
            this.walkAnimation = walkAnimation;
            this.speed = speed;

            t = 0f;

            MaxHealth = 100f;
            Health = MaxHealth;
            Reward = 10;
        }

        public override void Update(GameTime gameTime)
        {
            t += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

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
            walkAnimation.Draw(spriteBatch, Position, Scale);
        }

        public void TakeDamage(float damage)
        {
            Health -= damage;
            System.Diagnostics.Debug.WriteLine($"Enemy.TakeDamage: -{damage} -> Health={Health}");
            Console.WriteLine($"Enemy.TakeDamage: -{damage} -> Health={Health}");

            if (Health <= 0)
            {
                Health = 0;
                IsActive = false;
                System.Diagnostics.Debug.WriteLine("Enemy.TakeDamage: enemy died");
                Console.WriteLine("Enemy.TakeDamage: enemy died");
            }
        }
    }
}
