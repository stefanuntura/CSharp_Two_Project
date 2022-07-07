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
        private AnimationSprite _attackItem;
        private double _attackTimer = 0;
        public Walker(Game game, Vector2 position) : base(game, position, 180, 70, 10, 3, 40) {
        }

        public override void Update(GameTime gameTime, Player player, Map map)
        {
            _collisionDamageCooldown += gameTime.ElapsedGameTime.TotalMilliseconds;
            _attackCooldown += gameTime.ElapsedGameTime.TotalMilliseconds;
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _attackTimer += gameTime.ElapsedGameTime.TotalSeconds;

            AnimationSprite.Update(gameTime);
            FallDown(dt, map);
            DealCollisionDamage(player, map);
            HitPlayer(player);

            _attackItem.Update(gameTime);
            if(_attackTimer < 2)
                AnimationSprite.SetActive(_direction == "right" ? "AttackRight" : "AttackLeft");

            if (Util.InRangeX(this, player, 200) && player.Health > 0 && _attackTimer > 2)
            {
                ChasePlayer(dt, player, map);
            }
            else if(_attackTimer > 2)//if(player.Health > 0)
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
              { "WalkRight", new Animation(game.Content.Load<Texture2D>("Enemy/WalkRight"), 2) },
              { "WalkLeft", new Animation(game.Content.Load<Texture2D>("Enemy/WalkLeft"), 2) },
              { "StandRight", new Animation(game.Content.Load<Texture2D>("Player/player"), 1) },
              { "StandLeft", new Animation(game.Content.Load<Texture2D>("Player/playerL"), 1) },
              { "AttackLeft", new Animation(game.Content.Load<Texture2D>("Enemy/AttackLeft"), 1) },
              { "AttackRight", new Animation(game.Content.Load<Texture2D>("Enemy/AttackRight"), 1) },
              { "DownRight", new Animation(game.Content.Load<Texture2D>("Enemy/FallRight"), 1) },
              { "DownLeft", new Animation(game.Content.Load<Texture2D>("Enemy/FallLeft"), 1) },
            }, "StandRight", Color.White);

            _attackItem = new AnimationSprite(new Dictionary<string, Animation>()
            {
              { "poolLeft", new Animation(game.Content.Load<Texture2D>("Enemy/poolLeft"), 5) },
              { "poolRight", new Animation(game.Content.Load<Texture2D>("Enemy/attackPool"), 5) },
            }, "poolRight", Color.White * 0.5f);
            Texture = new Texture2D(GraphicsDevice, 1, 1);
            Texture.SetData(new[] { Color.White });

            // animationsprite erstellen
        }

        public void HitPlayer(Player player)
        {
            float range = (float)_attackRange;
            if (player.Position.X > Position.X)
                range += Dimensions.X;
            if (Util.InRange(player, this, (float)range) && _attackCooldown >= 4000)
            {
                player.Health -= Damage;
                _attackCooldown = 0;
                _attackItem.SetActive(_direction == "right" ? "poolRight" :  "poolLeft");
                _attackTimer = 0;
            }
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(Texture, new Rectangle((int)(Position.X + (Dimensions.X / 2) - (Health / 2)), (int)Position.Y - 20, (int)Health, 2), Color.Red);
            if(_attackItem != null && _attackTimer < 1)
            {
                _attackItem.Draw(spriteBatch, _direction == "right" ? new Vector2(Position.X + Dimensions.X - 2, Position.Y - 12) : new Vector2(Position.X - 38, Position.Y - 15));
            }

            AnimationSprite.Draw(spriteBatch, Position);
        }
    }
}
