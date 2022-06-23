using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Graduation.Animations
{
    public class AnimationSprite
    {
        protected AnimationManager _animationManager;

        public Dictionary<string, Animation> Animations;

        public Animation CurrentAnimation;

        protected Vector2 _position;

        protected Texture2D _texture;

        public Vector2 Position
        {
            get { return _position; }
            set
            {
                _position = value;

                if (_animationManager != null)
                    _animationManager.Position = _position;
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            if (_texture != null)
                spriteBatch.Draw(_texture, Position, Color.White);
            else if (_animationManager != null)
                _animationManager.Draw(spriteBatch, position);
        }

        public AnimationSprite(Dictionary<string, Animation> animations, String startAnimation)
        {
            Animations = animations;
            CurrentAnimation = Animations[startAnimation];
            _animationManager = new AnimationManager(CurrentAnimation);
        }

        public AnimationSprite(Texture2D texture)
        {
            _texture = texture;
        }

        public virtual void Update(GameTime gameTime)
        {
            _animationManager.Play(CurrentAnimation);
            _animationManager.Update(gameTime);
        }

        public void SetActive(String animationName)
        {
            CurrentAnimation = Animations[animationName];
        }
    }
}