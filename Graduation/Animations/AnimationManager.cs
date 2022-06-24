using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace Graduation.Animations
{
    public class AnimationManager
    {
        private Animation _animation;
        private Color _color;

        private float _timer;

        public Vector2 Position { get; set; }

        public AnimationManager(Animation animation, Color color)
        {
            _animation = animation;
            _color = color;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            Debug.WriteLine(_animation.Texture);
            spriteBatch.Draw(
                _animation.Texture,
                position,
                new Rectangle(
                    _animation.CurrentFrame * _animation.FrameWidth,
                    0,
                    _animation.FrameWidth,
                    _animation.FrameHeight),
                    _color);
        }

        public void Play(Animation animation, Color color)
        {
            if (_animation == animation)
                return;

            _animation = animation;
            _color = color;
            _animation.CurrentFrame = 0;

            _timer = 0;
        }

        public void Stop()
        {
            _timer = 0f;

            _animation.CurrentFrame = 0;
        }

        public void Update(GameTime gameTime)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_timer > _animation.FrameSpeed)
            {
                _timer = 0f;

                _animation.CurrentFrame++;

                if (_animation.CurrentFrame >= _animation.FrameCount)
                    _animation.CurrentFrame = 0;
            }
        }
    }
}