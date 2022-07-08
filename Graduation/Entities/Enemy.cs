using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Graduation.Graphics;
using Graduation.TestMap;
using Graduation.Animations;

namespace Graduation.Entities
{
    public abstract class Enemy : Entity
    {
        public AnimationSprite AnimationSprite;
        private bool _isWalking;
        private Vector2 _destination;
        public String _direction;
        private bool _canJump = false;
        private bool _strollLeft = false;
        public bool attack = false;
        public bool throwing = false;
        protected float dt;
        protected double _attackCooldown = 0;
        protected double _collisionDamageCooldown = 0;
        protected double _collisionDamage;
        protected double _attackRange;
        public Texture2D Texture;

        public Enemy(Game game, Vector2 position, float speed, float health, float damage, double collisionDamage, double attackRange) : base(game, position)
        {
            Speed = speed;
            Health = health;
            Damage = damage;
            _collisionDamage = collisionDamage;
            _attackRange = attackRange;
            Position = position;
            LoadContent(game);
        }

        public abstract void Update(GameTime gameTime, Player player, Map map);

        public virtual void LoadContent(Game game)
        {
            // Texture for healthbar

            /*Texture = new Texture2D(GraphicsDevice, 1, 1);
            Texture.SetData(new[] { Color.White });*/
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            // Draw Healthbar
            AnimationSprite.Draw(spriteBatch, Position);
        }

        public void changeDimensions()
        {
            Animation currAnim = AnimationSprite.CurrentAnimation;
            Dimensions = new Vector2(currAnim.FrameWidth, currAnim.FrameHeight);
        }

        public override void moveLeft(Map map)
        {
            AnimationSprite.SetActive("WalkLeft");
            _direction = "left";
            changeDimensions();

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
                    _isWalking = false;
                    _strollLeft = !_strollLeft;
                }
            }
            else
            {
                _isWalking = false;
                _strollLeft = !_strollLeft;
            }
        }

        public override void moveRight(Map map)
        {
            AnimationSprite.SetActive("WalkRight");
            _direction = "right";
            changeDimensions();
            if (Position.X < 2500)
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
                    _isWalking = false;
                    _strollLeft = !_strollLeft;
                }
            }
            else
            {
                _isWalking = false;
                _strollLeft = !_strollLeft;
            }
        }

        public void FallDown(float dt, Map map)
        {
            this.dt = dt;
            changeDimensions();
            float newY;
            AnimationSprite.SetActive(_direction == "right" ? "DownRight" : "DownLeft");
            Gravity = Gravity < Speed * 1.5 ? Gravity + 10 : Gravity;

            bool collision = false;
            Box collidedBox = null;
            Vector2 newPos = new Vector2(Position.X, Position.Y + (float)Gravity * dt);

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
                if (Gravity < 0)
                {
                    Position = new Vector2(Position.X, collidedBox.Position.Y + collidedBox.Dimensions.Y + 1);
                    Gravity = 0;
                }
                else
                {
                    Position = new Vector2(Position.X, collidedBox.Position.Y - Dimensions.Y - 1);
                    _canJump = true;
                }
            }
        }

        public void Stroll(float dt, Map map)
        {
            this.dt = dt;
            Speed = 70;
            if (_strollLeft)
            {
                moveLeft(map);
            }
            else
            {
                moveRight(map);
            }
        }

        public void ChasePlayer(float dt, Player player, Map map)
        {
            this.dt = dt;
            Speed = 110;
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


        public void DealCollisionDamage(Player player, Map map)
        {
            if(Util.areOverlapping(this, player) && _collisionDamageCooldown >= 2000)
            {
                player.Health -= this._collisionDamage;
                _collisionDamageCooldown = 0;
            }
        }
    }
}
