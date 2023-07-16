using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Game1
{
    abstract class AnimatedSprite : Sprite
    {
        public List<Animation> Animations;
        public Animation CurrentAnim = null;
        private bool IsPaused = false;

        public float Frame { get; private set; } = 0;

        public AnimatedSprite(Texture2D Texture) : base(Texture)
        {

        }

        public virtual void InitializeAnimations()
        {
            Animations = new List<Animation>();
        }

        public virtual void Update(float DeltaTime)
        {
            if (CurrentAnim != null && !IsPaused)
            {
                Frame += CurrentAnim.FPS * DeltaTime;
                if (Math.Floor(Frame + (CurrentAnim.FPS * DeltaTime)) >= CurrentAnim.Rect.Length)
                {
                    if (!CurrentAnim.IsLooped)
                        Frame = 0;
                    else Stop();
                }
            }
        }

        public virtual void Update(GameTime gt)
        {
            Update(gt.ElapsedGameTime.Milliseconds);
        }

        public void Play(string AnimationName)
        {
            if (CurrentAnim == null || AnimationName != CurrentAnim.Name)
            {
                Stop();

                foreach (Animation a in Animations)
                {
                    if (a.Name == AnimationName)
                        CurrentAnim = a;
                }
            }
        }

        public void Stop()
        {
            Frame = 0;

            CurrentAnim = null;
        }

        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(Texture, Position, CurrentAnim.Rect[(int)Math.Floor(Frame)], Color.White);
        }
    }

    class Animation
    {
        public readonly string Name;
        public bool IsLooped;
        public float FPS;
        public readonly Rectangle[] Rect;

        public Animation(string Name, Rectangle[] Rect, float FPS = 0.1f, bool IsLooped = false)
        {
            this.Name = Name;
            this.IsLooped = false;
            this.FPS = FPS;
            this.Rect = Rect;
        }
    }
}
