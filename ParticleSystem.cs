using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Project6TD
{
    public class ParticleSystem
    {
        private List<Particle> particles = new();
        private Random random = new();

        public void SpawnExplosion(Vector2 position)
        {
            // More particles, larger and longer-lived so they're visible in-game
            for (int i = 0; i < 20; i++)
            {
                float angle = (float)(random.NextDouble() * MathHelper.TwoPi);
                float speed = 30 + random.Next(70); // slower-to-medium speeds

                Vector2 velocity = new Vector2(
                    MathF.Cos(angle),
                    MathF.Sin(angle)
                ) * speed;

                float lifeTime = 0.6f + (float)random.NextDouble() * 0.6f; // 0.6 - 1.2s
                float size = 1.5f + (float)random.NextDouble() * 2.5f;     // 1.5 - 4.0 scale

                particles.Add(
                    new Particle(
                        position,
                        velocity,
                        lifeTime: lifeTime,
                        size: size
                    )
                );
            }
        }

        public void Update(GameTime gameTime)
        {
            for (int i = particles.Count - 1; i >= 0; i--)
            {
                particles[i].Update(gameTime);

                if (!particles[i].IsAlive)
                    particles.RemoveAt(i);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var p in particles)
                p.Draw(spriteBatch);
        }
    }
}
