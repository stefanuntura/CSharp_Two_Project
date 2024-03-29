﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Graduation.Graphics;
using Graduation.Animations;
using Graduation.TestMap;

namespace Graduation.Entities
{
    public class BossLevelOne : Enemy
    {
        public AnimationSprite _animationSprite;
        public EntityState State { get; private set; }
        public int DrawOder { get; set; }

        float dt;
        float Time;

        private bool _canJump = false;

        public Weapon weapon;
        public Boolean attack = false;
        public Boolean throwing = false;

        BossLevelOneController controller;

        public BossLevelOne(Game game, Vector2 position) : base(game, position, 300, 300, 30, 30, 40)
        {
            weapon = new DatabaseWeapon(game, new Vector2(42, 10));
            LoadContent(game);
            weapon.NeutralPos = new Vector2(0, 0);
            controller = new BossLevelOneController(this);
        }

        public override void Update(GameTime gameTime, Player player, Map map)
        {
            _collisionDamageCooldown += gameTime.ElapsedGameTime.TotalMilliseconds;
            dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (this.Position.Y != player.Position.Y)
            {
                Time = 0;
            }
            else
            {
                Time += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if(Util.weaponHitPlayer(weapon, player))
            {
                weapon.Position = weapon.NeutralPos;
                player.Health -= Damage;
            }

            DealCollisionDamage(player, map);
            controller.handleAttackPatterns(Time, player, map);
            _animationSprite.Update(gameTime);
        }

        public override void LoadContent(Game game)
        {
            _animationSprite = new AnimationSprite(new Dictionary<string, Animation>()
            {
              { "DashRight", new Animation(game.Content.Load<Texture2D>("Bosses/BossLevelOneDashRight"), 1) },
              { "DashLeft", new Animation(game.Content.Load<Texture2D>("Bosses/BossLevelOneDashLeft"), 1) },
              { "StandRight", new Animation(game.Content.Load<Texture2D>("Bosses/BossLevelOneIdleRight"), 1) },
              { "StandLeft", new Animation(game.Content.Load<Texture2D>("Bosses/BossLevelOneIdle"), 1) },
              { "UpRight", new Animation(game.Content.Load<Texture2D>("Player/UpRight"), 1) },
              { "UpLeft", new Animation(game.Content.Load<Texture2D>("Player/UpLeft"), 1) },
              { "DownRight", new Animation(game.Content.Load<Texture2D>("Player/DownRight"), 1) },
              { "DownLeft", new Animation(game.Content.Load<Texture2D>("Player/DownLeft"), 1) },
              {"MeleeAttack", new Animation(game.Content.Load<Texture2D>("Bosses/BossLeveLoneMeleeAttack"), 1) },
              { "ThrowAttackRight", new Animation(game.Content.Load<Texture2D>("Bosses/BossThrowAttackRight"), 1) },
              {"ThrowAttackLeft", new Animation(game.Content.Load<Texture2D>("Bosses/BossThrowAttackLeft"), 1) }
            }, "StandRight", Color.White);
            Texture = new Texture2D(GraphicsDevice, 1, 1);
            Texture.SetData(new[] { Color.White });

            Util.changePlayerDimensions(_animationSprite, this);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(Texture, new Rectangle((int)(Position.X + (Dimensions.X / 2) - (Health / 2)), (int)Position.Y - 20, (int)Health, 2), Color.Red);

            _animationSprite.Draw(spriteBatch, Position);

            weapon.bossRangedAttack(gameTime, spriteBatch, this);
        }

        public void moveY(Map map)
        {
            float newY;
            Gravity = Gravity < Speed * 1.5 ? Gravity + 10 : Gravity;
            Util.changePlayerDimensions(_animationSprite, this);


            bool collision = false;
            Box collidedBox = null;
            Vector2 newPos = new Vector2(Position.X, Position.Y + (float)Gravity * Time);

            foreach (Box box in map.Boxes)
            {
                if (collided(box, newPos))
                {
                    collision = true;
                    collidedBox = box;
                    Gravity = 0;
                    break;
                }
            }

            if (!collision)
            {
                Position = newPos;
                if (Gravity > 0)
                    _animationSprite.SetActive(_direction == "right" ? "DownRight" : "DownLeft");
                else
                    _animationSprite.SetActive(_direction == "right" ? "UpRight" : "UpLeft");
            }
            else
            {
                if (Gravity < 0)
                {
                    Position = new Vector2(Position.X, collidedBox.Position.Y + collidedBox.Dimensions.Y + 1);
                    Gravity = 0;
                    _animationSprite.SetActive(_direction == "right" ? "DownRight" : "DownLeft");
                }
                else
                {
                    Position = new Vector2(Position.X, collidedBox.Position.Y - Dimensions.Y - 1);
                    _canJump = true;
                }
            }
        }

        //Override moveRight method in entity
        public override void moveRight(Map map)
        {
            _animationSprite.SetActive("DashRight");
            _direction = "right";
            Util.changePlayerDimensions(_animationSprite, this);
            Util.checkAndMoveRight(map, this, dt);
        }

        public override void moveLeft(Map map)
        {
            _animationSprite.SetActive("DashLeft");
            _direction = "left";
            Util.changePlayerDimensions(_animationSprite, this);
            Util.checkAndMoveLeft(map, this, dt);
        }

        public void throwWeapon()
        {
            //Called by the InputController, only starts the attack method once cooldown of weapon is down
            if (weapon.attackTimer >= weapon.Cooldown)
            {
                attack = true;
            }
        }

        public new void ChasePlayer(float dt, Player player, Map map)
        {
            Speed = 150;
            if (!Util.InRangeX(player, this, 3))
            {
                if (player.Position.X > Position.X)
                {
                    moveRight(map);
                }
                else
                {
                    moveLeft(map);
                }
            }
        }

        public void chase(Player player, Map map)
        {
            if (Util.InRangeX(this, player, 200) && player.Health > 0)
            {
                ChasePlayer(dt, player, map);
            }
            if (Util.InRangeX(this, player, 250) && player.Health > 0)
            {

                if (_direction == "right")
                {
                    _animationSprite.SetActive("ThrowAttackRight");
                }
                else
                {
                    _animationSprite.SetActive("ThrowAttackLeft");
                }

                throwWeapon();
            }
            if (!Util.InRangeX(this, player, 350) && player.Health > 0)
            {
                if(player.Position.X - 100 > 0)
                {
                    this.Position = new Vector2(player.Position.X - 100, player.Position.Y);
                    Speed = 200;
                }
            }
        }
    }
}
