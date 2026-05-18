using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project6TD
{
    public class Particle
    {
        public Vector2 Position;
        private Vector2 velocity;
        private float lifeTime;
        private float age;
        private float size;

        public bool IsAlive => age < lifeTime;

        public Particle(Vector2 position, Vector2 velocity, float lifeTime, float size)
        {
            Position = position;
            this.velocity = velocity;
            this.lifeTime = lifeTime;
            this.size = size;
            age = 0f;
        }

        public void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            age += dt;
            Position += velocity * dt;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            float alpha = 1f - (age / lifeTime);

            spriteBatch.Draw(
                AssetsManager.pixel,
                Position,
                null,
                Color.Red * alpha,
                0f,
                new Vector2(0.5f, 0.5f),
                size,
                SpriteEffects.None,
                0f
            );
        }
    }
}
