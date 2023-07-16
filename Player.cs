using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System;
using System.Collections.Generic;

namespace Game1
{
    class Player : AnimatedSprite
    {
        GraphicsDevice Device;

        public OrthographicCamera Camera;

        Rectangle[] Rectangles =
        {
            new Rectangle(59, 76, 26, 52), new Rectangle(59, 204, 26, 52), new Rectangle(56, 12, 32, 50), new Rectangle(56, 136, 32, 54)
        };

        enum Pole
        {
            EAST = 0,
            WEST = 1,
            NORTH = 2,
            SOUTH = 3
        }
        int currentPole = (int)Pole.EAST;

        public override int Width
        {
            get
            {
                if (CurrentAnim != null) return CurrentAnim.Rect[(int)Math.Floor(Frame)].Width;
                else return Rectangles[currentPole].Width;
            }
        }
        public override int Height
        {
            get
            {
                if (CurrentAnim != null) return CurrentAnim.Rect[(int)Math.Floor(Frame)].Height;
                else return Rectangles[currentPole].Height;
            }
        }

        public float Velocity = 0.01f; // default - 0.01f
        float Speed;

        float DeltaZoom = 0.02f;

        public Player(Texture2D Texture, GraphicsDevice Device) : base(Texture)
        {
            this.Device = Device;
            Camera = new OrthographicCamera(Device)
            {
                MaximumZoom = 1.5f,
                MinimumZoom = 0.8f
            };

            InitializeAnimations();
        }

        public override void Spawn(float x = 0, float y = 0)
        {
            base.Spawn(x, y);
        }

        public override void Update(GameTime gt)
        {
            var KM = Keyboard.GetState();

            if (KM.IsKeyDown(Keys.LeftShift)) Speed = (Velocity / 2) * 10 * gt.ElapsedGameTime.Milliseconds;
            else if (KM.IsKeyDown(Keys.Space)) Speed = (Velocity * 1.5f) * 10 * gt.ElapsedGameTime.Milliseconds; else Speed = Velocity * 10 * gt.ElapsedGameTime.Milliseconds;

            foreach (Animation a in Animations) a.FPS = Speed / 10 / gt.ElapsedGameTime.Milliseconds;

            if (KM.IsKeyDown(Keys.W))
            {
                currentPole = (int)Pole.NORTH;

                Play("GO_NORTH");

                if (KM.IsKeyDown(Keys.A))
                {
                    //if (Position.Y >= playerSpeed && Position.X >= playerSpeed)
                    Move(x: -Speed / 1.414f, y: -Speed / 1.414f);
                }
                else
                if (KM.IsKeyDown(Keys.D))
                {
                    //if (Position.Y >= playerSpeed && Position.X <= Game1.Win.ClientBounds.Width - Width - playerSpeed)
                    Move(x: Speed / 1.414f, y: -Speed / 1.414f);
                }
                else
                {
                    //if (Position.Y >= playerSpeed)
                    Move(y: -Speed);
                }
            }
            else
            if (KM.IsKeyDown(Keys.S))
            {
                currentPole = (int)Pole.SOUTH;

                Play("GO_SOUTH");

                if (KM.IsKeyDown(Keys.A))
                {
                    //if (Position.Y <= Game1.Win.ClientBounds.Height - Height - playerSpeed && Position.X >= playerSpeed)
                    Move(x: -Speed / 1.414f, y: Speed / 1.414f);
                }
                else
                if (KM.IsKeyDown(Keys.D))
                {
                    //if (Position.Y <= Game1.Win.ClientBounds.Height - Height - playerSpeed && Position.X <= Game1.Win.ClientBounds.Width - Width - playerSpeed)
                    Move(x: Speed / 1.414f, y: Speed / 1.414f);
                }
                else
                {
                    //if (Position.Y <= Game1.Win.ClientBounds.Height - Height - playerSpeed)
                    Move(y: Speed);
                }
            }
            else
            if (KM.IsKeyDown(Keys.A))
            {
                if (KM.IsKeyDown(Keys.W))
                {
                    currentPole = (int)Pole.NORTH;
                    Play("GO_NORTH");

                    //if (Position.Y >= playerSpeed && Position.X >= playerSpeed)
                    Move(x: -Speed / 1.414f, y: -Speed / 1.414f);
                }
                else
                if (KM.IsKeyDown(Keys.S))
                {
                    currentPole = (int)Pole.SOUTH;
                    Play("GO_SOUTH");

                    //if (Position.Y <= Game1.Win.ClientBounds.Height - Height - playerSpeed && Position.X >= playerSpeed)
                    Move(x: -Speed / 1.414f, y: Speed / 1.414f);
                }
                else
                {
                    currentPole = (int)Pole.WEST;
                    Play("GO_WEST");

                    //if (Position.X >= playerSpeed)
                    Move(x: -Speed);
                }
            }
            else
            if (KM.IsKeyDown(Keys.D))
            {
                if (KM.IsKeyDown(Keys.W))
                {
                    currentPole = (int)Pole.NORTH;
                    Play("GO_NORTH");

                    //if (Position.Y >= playerSpeed && Position.X <= Game1.Win.ClientBounds.Width - Width - playerSpeed)
                    Move(x: Speed / 1.414f, y: -Speed / 1.414f);
                }
                else
                if (KM.IsKeyDown(Keys.S))
                {
                    currentPole = (int)Pole.SOUTH;
                    Play("GO_SOUTH");

                    //if (Position.Y <= Game1.Win.ClientBounds.Height - Height - playerSpeed && Position.X <= Game1.Win.ClientBounds.Width - Width - playerSpeed)
                    Move(x: Speed / 1.414f, y: Speed / 1.414f);
                }
                else
                {
                    currentPole = (int)Pole.EAST;
                    Play("GO_EAST");

                    //if (Position.X <= Game1.Win.ClientBounds.Width - Width - playerSpeed)
                    Move(x: Speed);
                }
            }
            else
            {
                Stop();
                Speed = 0;
            }

            base.Update(gt);

            if (KM.IsKeyDown(Keys.OemPlus))
                Camera.ZoomIn(DeltaZoom);
            if (KM.IsKeyDown(Keys.OemMinus))
                Camera.ZoomOut(DeltaZoom);

            Camera.LookAt(Position);

/*            Console.WriteLine(Position);
                if (CurrentAnim != null) Console.WriteLine((int)Math.Floor(Frame));
            Console.WriteLine(Speed);*/
        }

        public override void Draw(SpriteBatch sb)
        {
            if (CurrentAnim != null)
                base.Draw(sb);
            else
                sb.Draw(Texture, Position, Rectangles[currentPole], Color.White);
        }

        public override void InitializeAnimations()
        {
            Animation[] _Animations =
            {
                new Animation(
                    "GO_EAST",
                    new Rectangle[] {new Rectangle(59, 76, 26, 52), new Rectangle(107, 78, 27, 51), new Rectangle(9, 77, 28, 52)},
                    Velocity,
                    false),
                new Animation(
                    "GO_WEST",
                    new Rectangle[] {new Rectangle(59, 204, 26, 52), new Rectangle(107, 205, 28, 51), new Rectangle(11, 206, 26, 50)},
                    Velocity,
                    false),
                new Animation(
                    "GO_NORTH",
                    new Rectangle[] {new Rectangle(56, 12, 32, 50), new Rectangle(104, 14, 31, 51), new Rectangle(9, 14, 31, 51)},
                    Velocity,
                    false),
                new Animation(
                    "GO_SOUTH",
                    new Rectangle[] {new Rectangle(56, 136, 32, 54), new Rectangle(105, 138, 31, 54), new Rectangle(8, 138, 31, 54)},
                    Velocity,
                    false)
            };
            Animations = new List<Animation>(_Animations);
        }
    }
}