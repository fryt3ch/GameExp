using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game1
{
    public class Game1 : Game
    {
        public static GameWindow Win;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Map Map;
        Player Player;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Win = Window;

            IsFixedTimeStep = true;
            IsMouseVisible = true;

            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            graphics.ApplyChanges();

            Map = new Map(50, 50, 32, 32, graphics.GraphicsDevice);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Player = new Player(Content.Load<Texture2D>("player"), graphics.GraphicsDevice);

            Player.Spawn(128, 32);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape)) Exit();

            Player.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.ForestGreen);

            spriteBatch.Begin(transformMatrix: Player.Camera.GetViewMatrix());

            Map.Draw(spriteBatch);
            Player.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
