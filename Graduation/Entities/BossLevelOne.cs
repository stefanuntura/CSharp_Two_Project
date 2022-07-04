using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Graduation.Graphics;
using Graduation.Animations;

namespace Graduation.Entities
{
    public class BossLevelOne : Boss
    {
        public AnimationSprite _animationSprite;
        public EntityState State { get; private set; }
        public Vector2 Position { get; set; }
        public double Health { get; set; }
        public double Speed { get; set; }
        public double Gravity { get; set; }
        public double Damage { get; set; }
        public int DrawOder { get; set; }

        public BossLevelOne(Game game, Vector2 position) : base(game, position)
        {
            LoadContent(game);
        }
         


        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _animationSprite.Draw(spriteBatch, Position);
        }

        public void LoadContent(Game game)
        {
            _animationSprite = new AnimationSprite(new Dictionary<string, Animation>()
            {
              { "WalkRight", new Animation(game.Content.Load<Texture2D>("Player/WalkRight"), 2) },
              { "WalkLeft", new Animation(game.Content.Load<Texture2D>("Player/WalkLeft"), 2) },
              { "StandRight", new Animation(game.Content.Load<Texture2D>("Bosses/BossLevelOneIdle"), 1) },
              { "StandLeft", new Animation(game.Content.Load<Texture2D>("Player/playerL"), 1) },
              { "UpRight", new Animation(game.Content.Load<Texture2D>("Player/UpRight"), 1) },
              { "UpLeft", new Animation(game.Content.Load<Texture2D>("Player/UpLeft"), 1) },
              { "DownRight", new Animation(game.Content.Load<Texture2D>("Player/DownRight"), 1) },
              { "DownLeft", new Animation(game.Content.Load<Texture2D>("Player/DownLeft"), 1) },
              {"MeleeAttack", new Animation(game.Content.Load<Texture2D>("Bosses/BossLeveLoneMeleeAttack"), 1) }
            }, "StandRight", Color.White);

            changePlayerDimensions();
        }

        public void changePlayerDimensions()
        {
            Animation currAnim = _animationSprite.CurrentAnimation;
            Dimensions = new Vector2(currAnim.FrameWidth, currAnim.FrameHeight);
        }
    }
}
