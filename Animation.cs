using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project6TD
{
    public class Animation
    {
        Texture2D texture;
        Rectangle[] frames;
        int currentFrame;
        float timer;
        float frameTime;
        float Scale = 1f;
        

        public Animation(Texture2D texture, Rectangle[] frames, float frameTime)
        {
            this.texture = texture;
            this.frames = frames;
            this.frameTime = frameTime;
        }

        
        public int CurrentFrame => currentFrame;
        public Rectangle[] Frames => frames;

        public void Update(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            while (timer >= frameTime)
            {
                timer -= frameTime;
                currentFrame = (currentFrame + 1) % frames.Length;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, float scale, Color color)
        {
            var frame = frames[currentFrame];

            Vector2 origin = new Vector2(frame.Width / 2f, frame.Height);

            spriteBatch.Draw(
                texture,
                position,
                frame,
                color,
                0f,
                origin,
                scale,
                SpriteEffects.None,
                0f
            );
        }
        public Animation Clone()
        {
            return new Animation(texture, frames, frameTime);
        }
    }
}
