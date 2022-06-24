using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Graduation.Graphics;
using Graduation.TestMap;
using Graduation.Animations;

namespace Graduation.Entities
{
    class Walker : Enemy
    {
        public Walker(Game game, Vector2 position) : base(game, position, 180, 100, 5) { }

        public override void Update(GameTime gameTime, Player player, Map map)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            FallDown(dt, map);
            if (Util.InRangeX(this, player, 200))
            {
                ChasePlayer(dt, player, map);
            }
            else
            {
                Stroll(dt, map);
            }
        }

        public override void LoadContent(Game game)
        {
            AnimationSprite = new AnimationSprite(new Dictionary<string, Animation>()
            {
              { "WalkRight", new Animation(game.Content.Load<Texture2D>("Player/WalkRight"), 2) },
              { "WalkLeft", new Animation(game.Content.Load<Texture2D>("Player/WalkLeft"), 2) },
              { "StandRight", new Animation(game.Content.Load<Texture2D>("Player/player"), 1) },
              { "StandLeft", new Animation(game.Content.Load<Texture2D>("Player/playerL"), 1) },
              { "UpRight", new Animation(game.Content.Load<Texture2D>("Player/UpRight"), 1) },
              { "UpLeft", new Animation(game.Content.Load<Texture2D>("Player/UpLeft"), 1) },
              { "DownRight", new Animation(game.Content.Load<Texture2D>("Player/DownRight"), 1) },
              { "DownLeft", new Animation(game.Content.Load<Texture2D>("Player/DownLeft"), 1) },
            }, "StandRight", Color.Red);
        }
    }
}
