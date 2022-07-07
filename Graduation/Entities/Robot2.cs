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
    class Robot2 : Enemy
    {
        private AnimationSprite _attackItem;
        private double _attackTimer = 0;
        Weapon weapon;
        
        public Robot2(Game game, Vector2 position) : base(game, position, 180, 70, 10, 3, 40)
        {
            weapon = new ProjectileWeapon(game, new Vector2(10, 10));
            LoadContent(game);
            weapon.NeutralPos = new Vector2(0, 0);

        }

        public override void Update(GameTime gameTime, Player player, Map map)
        {
            _collisionDamageCooldown += gameTime.ElapsedGameTime.TotalMilliseconds;
            _attackCooldown += gameTime.ElapsedGameTime.TotalMilliseconds;
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _attackTimer += gameTime.ElapsedGameTime.TotalSeconds;

            if (Util.weaponHitPlayer(weapon, player))
            {
                weapon.Position = weapon.NeutralPos;
                player.Health -= Damage;
            }

            AnimationSprite.Update(gameTime);
            FallDown(dt, map);
            DealCollisionDamage(player, map);
            HitPlayer(player);

            if (_attackTimer < 2)
                AnimationSprite.SetActive(_direction == "right" ? "AttackRight" : "AttackLeft");

            if (Util.InRangeX(this, player, 400) && player.Health > 0 && _attackTimer > 1)
            {
                RangedChasePlayer(dt, player, map);
            }
            else if (_attackTimer > 1)//if(player.Health > 0)
            {
                Stroll(dt, map);
            }

            //Check for collision with any walls
            foreach (Box box in map.Boxes)
            {
                if (Util.weaponHitWall(weapon, box))
                {
                    weapon.Position = weapon.NeutralPos;
                }
            }
        }

        public override void LoadContent(Game game)
        {
            AnimationSprite = new AnimationSprite(new Dictionary<string, Animation>()
            {
              { "WalkRight", new Animation(game.Content.Load<Texture2D>("Enemy/Robot2WalkRight"), 1) },
              { "WalkLeft", new Animation(game.Content.Load<Texture2D>("Enemy/Robot2WalkLeft"), 1) },
              { "StandRight", new Animation(game.Content.Load<Texture2D>("Enemy/Robot2WalkRight"), 1) },
              { "StandLeft", new Animation(game.Content.Load<Texture2D>("Enemy/Robot2WalkLeft"), 1) },
              { "AttackLeft", new Animation(game.Content.Load<Texture2D>("Enemy/Robot2MeleeLeft"), 1) },
              { "AttackRight", new Animation(game.Content.Load<Texture2D>("Enemy/Robot2MeleeRight"), 1) },
              { "DownRight", new Animation(game.Content.Load<Texture2D>("Enemy/Robot2WalkRight"), 1) },
              { "DownLeft", new Animation(game.Content.Load<Texture2D>("Enemy/Robot2MeleeLeft"), 1) },
              { "RangedRight", new Animation(game.Content.Load<Texture2D>("Enemy/Robot2RangedRight"), 1) },
              { "RangedLeft", new Animation(game.Content.Load<Texture2D>("Enemy/Robot2RangedLeft"), 1) },
            }, "StandRight", Color.White);

           
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
                _attackTimer = 0;
            }
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(Texture, new Rectangle((int)(Position.X + (Dimensions.X / 2) - (Health / 2)), (int)Position.Y - 20, (int)Health, 2), Color.Red);

            /*if (_attackItem != null && _attackTimer < 1)
            {
                _attackItem.Draw(spriteBatch, _direction == "right" ? new Vector2(Position.X + Dimensions.X - 2, Position.Y - 12) : new Vector2(Position.X - 38, Position.Y - 15));
            }*/

            spriteBatch.Draw(Texture, new Rectangle((int)(Position.X + (Dimensions.X / 2) - (Health / 2)), (int)Position.Y - 20, (int)Health, 2), Color.Red);

            AnimationSprite.Draw(spriteBatch, Position);

            weapon.RangedAttack(gameTime, spriteBatch, this);

            AnimationSprite.Draw(spriteBatch, Position);
        }

        public void RangedChasePlayer(float dt, Player player, Map map)
        {
            this.dt = dt;
            Speed = 110;
            if (!Util.InRangeX(player, this, 3))
            {
                if (player.Position.X > Position.X)
                {
                    moveRight(map, (float)Speed);

                }
                else
                {
                    moveLeft(map, (float)Speed);
                }
            }

            if (Util.InRangeX(this, player, 350) && player.Health > 0)
            {
                if (weapon.attackTimer >= weapon.Cooldown)
                {
                    attack = true;
                    if (_direction == "right")
                    {
                        AnimationSprite.SetActive("RangedRight");
                    }
                    else 
                    {
                        AnimationSprite.SetActive("RangedLeft");
                    }

                    throwWeapon(this.weapon, player);
                    if (Util.weaponHitPlayer(weapon, player)) player.Health -= weapon.Damage;
                    throwing = false;

                }
            }
        }

        public void throwWeapon(Weapon weapon, Player player)
        {
            //Called by the InputController, only starts the attack method once cooldown of weapon is down

            if (weapon.attackTimer >= weapon.Cooldown)
            {
                attack = true;
                if (Util.weaponHitPlayer(weapon, player)) player.Health -= weapon.Damage;
            }
        }


    }
}
