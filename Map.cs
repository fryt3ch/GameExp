using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace Game1
{
    class Map
    {
        private int Width;
        private int Height;
        private int TileWidth;
        private int TileHeight;

        private float TileBorder = 2;

        public Map(int Width, int Height, int TileWidth, int TileHeight, GraphicsDevice Device)
        {
            this.Width = Width;
            this.Height = Height;
            this.TileWidth = TileWidth;
            this.TileHeight = TileHeight;
        }

        public void Draw(SpriteBatch sb)
        {
            Vector2 TilePosition = Vector2.Zero;

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    sb.FillRectangle(TilePosition, new Size2(TileWidth, TileHeight), Color.White);
                    sb.FillRectangle(TilePosition + new Vector2(1, 1), new Size2(TileWidth - TileBorder, TileHeight - TileBorder), Color.Black);

                    TilePosition.Y += TileHeight;
                }

                TilePosition.Y = 0;
                TilePosition.X += TileWidth;
            }
        }
    }
}
