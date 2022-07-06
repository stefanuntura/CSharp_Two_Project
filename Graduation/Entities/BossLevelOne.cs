using System;
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
        public Vector2 Position { get; set; }
        public double Health { get; set; }
        public double Speed { get; set; }
        public double Gravity { get; set; }
        public double Damage { get; set; }
        public int DrawOder { get; set; }

        float dt;
        float Time;
        BossLevelOneController controller;
        String _direction = "right";

        public BossLevelOne(Game game, Vector2 position) : base(game, position, 100, 100, 30, 30, 40)
        {
            _collisionDamage = 30;
            Speed = 300;
            Position = position;
            LoadContent(game);
            controller = new BossLevelOneController(this);
        }

        public override void Update(GameTime gameTime, Player player, Map map)
        {
            _collisionDamageCooldown += gameTime.ElapsedGameTime.TotalMilliseconds;
            Time += (float)gameTime.ElapsedGameTime.TotalSeconds;
            dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            moveY(map);
            controller.handleAttackPatterns(Gravity, Time, player, map);
            DealCollisionDamage(player, map);
            _animationSprite.Update(gameTime);
        }

        public override void LoadContent(Game game)
        {
            _animationSprite = new AnimationSprite(new Dictionary<string, Animation>()
            {
              { "DashRight", new Animation(game.Content.Load<Texture2D>("Bosses/BossLevelOneDashRight"), 1) },
              { "DashLeft", new Animation(game.Content.Load<Texture2D>("Bosses/BossLevelOneDashLeft"), 1) },
              { "StandRight", new Animation(game.Content.Load<Texture2D>("Player/player"), 1) },
              { "StandLeft", new Animation(game.Content.Load<Texture2D>("Bosses/BossLevelOneIdle"), 1) },
              { "UpRight", new Animation(game.Content.Load<Texture2D>("Player/UpRight"), 1) },
              { "UpLeft", new Animation(game.Content.Load<Texture2D>("Player/UpLeft"), 1) },
              { "DownRight", new Animation(game.Content.Load<Texture2D>("Player/DownRight"), 1) },
              { "DownLeft", new Animation(game.Content.Load<Texture2D>("Player/DownLeft"), 1) },
              {"MeleeAttack", new Animation(game.Content.Load<Texture2D>("Bosses/BossLeveLoneMeleeAttack"), 1) }
            }, "StandRight", Color.White);
            Texture = new Texture2D(GraphicsDevice, 1, 1);
            Texture.SetData(new[] { Color.White });

            changePlayerDimensions();
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(Texture, new Rectangle((int)(Position.X + (Dimensions.X / 2) - (Health / 2)), (int)Position.Y - 20, (int)Health, 2), Color.Red);

            _animationSprite.Draw(spriteBatch, Position);
        }

        public void changePlayerDimensions()
        {
            Animation currAnim = _animationSprite.CurrentAnimation;
            Dimensions = new Vector2(currAnim.FrameWidth, currAnim.FrameHeight);
        }

        public void moveY(Map map)
        {
            float newY;
            Gravity = Gravity < Speed * 1.5 ? Gravity + 10 : Gravity;
            changePlayerDimensions();


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
                }
            }
        }

        public void dashRight(Map map)
        {
            _animationSprite.SetActive("DashRight");
            _direction = "right";
            changePlayerDimensions();
            if (Position.X < 3500)
            {
                bool collision = false;
                Box collidedBox = null;
                Vector2 newPos = new Vector2(Position.X + (float)Speed * dt, Position.Y);

                foreach (Box box in map.Boxes)
                {
                    if (collided(box, newPos))
                    {
                        collision = true;
                        collidedBox = box;
                        break;
                    }
                }

                if (!collision)
                {
                    Position = newPos;
                }
                else
                {
                    Position = new Vector2(collidedBox.Position.X - Dimensions.X - 1, Position.Y);
                }
            }
        }

        public void dashLeft(Map map)
        {
            _animationSprite.SetActive("DashLeft");
            _direction = "left";
            changePlayerDimensions();
            if (Position.X > 0)
            {
                bool collision = false;
                Box collidedBox = null;
                Vector2 newPos = new Vector2(Position.X - (float)Speed * dt, Position.Y);

                foreach (Box box in map.Boxes)
                {
                    if (collided(box, newPos))
                    {
                        collision = true;
                        collidedBox = box;
                        break;
                    }
                }

                if (!collision)
                {
                    Position = newPos;
                }
                else
                {
                    Position = new Vector2(collidedBox.Position.X + collidedBox.Dimensions.X + 1, Position.Y);
                }
            }
        }

        public new void ChasePlayer(float dt, Player player, Map map)
        {
            this.dt = dt;
            Speed = 110;
            if (!Util.InRangeX(player, this, 3))
            {
                if (player.Position.X > Position.X)
                {
                    dashRight(map);
                }
                else
                {
                    dashLeft(map);
                }
            }
        }

        public void chase(Player player, Map map)
        {
            if (Util.InRangeX(this, player, 200) && player.Health > 0)
            {
                ChasePlayer(dt, player, map);
            }
        }

        public void idle(Map map)
        {
            _animationSprite.SetActive("StandLeft");
            _direction = "left";
            changePlayerDimensions();
        }
    }
}
