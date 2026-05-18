using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project6TD
{
    public abstract class GameObject
    {
        public Vector2 Position { get; protected set; }
        public bool IsActive { get; protected set; } = true;

        public virtual Rectangle Bounds { get; }
        public virtual void Update(GameTime gameTime) { }
        public virtual void Draw(SpriteBatch spriteBatch) { }
    }
}
