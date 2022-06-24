using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Graduation.Animations
{
    public class AnimationSprite
    {
        protected AnimationManager _animationManager;

        public Dictionary<string, Animation> Animations;

        public Animation CurrentAnimation;

        protected Vector2 _position;

        protected Texture2D _texture;

        private Color _color;

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

        public AnimationSprite(Dictionary<string, Animation> animations, String startAnimation, Color color)
        {
            Animations = animations;
            CurrentAnimation = Animations[startAnimation];
            _color = color;
            _animationManager = new AnimationManager(CurrentAnimation, color);
        }

        public AnimationSprite(Texture2D texture)
        {
            _texture = texture;
        }

        public virtual void Update(GameTime gameTime)
        {
            //Debug.WriteLine(CurrentAnimation.Texture);
            _animationManager.Play(CurrentAnimation, _color);
            _animationManager.Update(gameTime);
        }

        public void SetActive(String animationName)
        {
            Debug.WriteLine(Animations[animationName].Texture);
            CurrentAnimation = Animations[animationName];
        }
    }
}