using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1
{
    abstract class Sprite
    {
        public Vector2 Position;
        public Texture2D Texture;

        public virtual int Width => Texture.Width;
        public virtual int Height => Texture.Height;

        public Sprite(Texture2D Texture)
        {
            this.Texture = Texture;
        }

        public virtual void Spawn(float x = 0, float y = 0)
        {
            Position.X = x; Position.Y = y;
        }

        public void Move(float x = 0, float y = 0)
        {
            Position.X += x; Position.Y += y;
        }

        public virtual void Draw(SpriteBatch sb)
        {
            sb.Draw(Texture, Position, Color.White);
        }
    }
}
