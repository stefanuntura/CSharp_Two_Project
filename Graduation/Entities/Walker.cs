using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Graduation.Graphics;
using Graduation.TestMap;
using Graduation.Animations;
using System.Diagnostics;


namespace Graduation.Entities
{
    class Walker : Enemy
    {
        public Walker(Game game, Vector2 position) : base(game, position, 180, 100, 10, 3, 40) {
        }

        public override void Update(GameTime gameTime, Player player, Map map)
        {
            _collisionDamageCooldown += gameTime.ElapsedGameTime.TotalMilliseconds;
            _attackCooldown += gameTime.ElapsedGameTime.TotalMilliseconds;
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            FallDown(dt, map);
            DealCollisionDamage(player, map);
            HitPlayer(player);

            if (Util.InRangeX(this, player, 200) && player.Health > 0)
            {
                ChasePlayer(dt, player, map);
            }
            else //if(player.Health > 0)
            {
                Stroll(dt, map);
            }
            /*else
            {
                Roam(dt, map);
            }*/
        }

        public override void LoadContent(Game game)
        {
            //Debug.WriteLine("Test");
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

        public void HitPlayer(Player player)
        {
            if (Util.InRange(player, this, (float)_attackRange) && _attackCooldown >= 4000)
            {
                player.Health -= Damage;
                _attackCooldown = 0;
            }
        }
    }
}
