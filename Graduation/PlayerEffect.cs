using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Graduation.Animations;
using Graduation.Entities;

namespace Graduation
{
    class PlayerEffect :  DrawableGameComponent
    {
        public String Title;
        public bool GoodEffect;
        public int TimeSpan;
        private AnimationSprite _animationSprite;
        public PlayerEffect(Game game, String effectString, bool goodEffect, int timeSpan) : base(game)
        {
            Title = effectString;
            GoodEffect = goodEffect;
            TimeSpan = timeSpan;
            LoadContent(game);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, Player player)
        {
            _animationSprite.SetActive(Title);
            _animationSprite.Update(gameTime);
            _animationSprite.Draw(spriteBatch,
                new Vector2(player.Position.X + (player.Dimensions.X / 2) - 20, player.Position.Y + 25));
        }

        public void LoadContent(Game game)
        {
            _animationSprite = new AnimationSprite(new Dictionary<string, Animation>()
            {
              { "-5 Health", new Animation(game.Content.Load<Texture2D>("Item/hpLoss"), 4) },
              { "10s Slowness", new Animation(game.Content.Load<Texture2D>("Item/slowEffect"), 3) },
              { "20s Speedboost", new Animation(game.Content.Load<Texture2D>("Item/speed"), 2) },
              { "+10 Health", new Animation(game.Content.Load<Texture2D>("Enemy/hpGain"), 7) },
            }, "20s Speedboost", Color.White);
        }
    }
}