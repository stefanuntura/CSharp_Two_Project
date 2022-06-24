using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Graduation.Graphics;
using Graduation.TestMap;
using Graduation.Animations;

namespace Graduation.Entities
{
    abstract class Enemy : Entity
    {
        public AnimationSprite AnimationSprite;
        private bool _isWalking;
        private Vector2 _destination;
        private String _direction;
        private bool _canJump = false;
        private bool _strollLeft = false;
        float dt;

        public Enemy(Game game, Vector2 position, float speed, float health, float damage) : base(game, position)
        {
            LoadContent(game);
            Speed = speed;
            Health = health;
            Damage = Damage;
        }

        public void moveLeft(Map map, float distance)
        {
            AnimationSprite.SetActive("WalkLeft");
            _direction = "left";
            changeDimensions();

            if (Position.X > 0)
            {
                bool collision = false;
                Box collidedBox = null;
                Vector2 newPos = new Vector2(Position.X - (float)distance * dt, Position.Y);

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

        public void moveRight(Map map, float distance)
        {
            AnimationSprite.SetActive("WalkRight");
            _direction = "right";
            changeDimensions();
            if (Position.X < 1265)
            {
                bool collision = false;
                Box collidedBox = null;
                Vector2 newPos = new Vector2(Position.X + (float)distance * dt, Position.Y);

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

        public void Roam(float dt, Map map)
        {
            this.dt = dt;
            if (!_isWalking)
            {
                Random rand = new Random();
                if (rand.NextDouble() < 0.02)
                {
                    double randomX = rand.NextDouble() < 0.5 ? Util.RandomDouble(-50, -100) : Util.RandomDouble(50, 100);
                    _destination = new Vector2(Position.X + (float)randomX, Position.Y);
                    _isWalking = true;
                }
            }
            else
            {
                if (_destination.X < Position.X)
                {
                    if (_destination.X >= Position.X - Speed * dt)
                    {
                        moveLeft(map, Position.X - _destination.X);
                        _isWalking = false;
                    }
                    else
                    {
                        moveLeft(map, (float)Speed);
                    }
                }
                else
                {
                    if (_destination.X <= Position.X + Speed * dt)
                    {
                        moveRight(map, _destination.X - Position.X);
                        _isWalking = false;
                    }
                    else
                    {
                        moveRight(map, (float)Speed);
                    }
                }
            }
        }

        public void Stroll(float dt, Map map)
        {
            this.dt = dt;
            Speed = 90;
            if (_strollLeft)
            {
                moveLeft(map, (float)Speed);
            }
            else
            {
                moveRight(map, (float)Speed);
            }
        }

        public void ChasePlayer(float dt, Player player, Map map)
        {
            this.dt = dt;
            Speed = 140;
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
        }

        public void changeDimensions()
        {
            Animation currAnim = AnimationSprite.CurrentAnimation;
            Dimensions = new Vector2(currAnim.FrameWidth, currAnim.FrameHeight);
        }

        public abstract void Update(GameTime gameTime, Player player, Map map);

        public abstract void LoadContent(Game game);

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            //Sprite.Draw(spriteBatch, Position);
            AnimationSprite.Draw(spriteBatch, Position);
        }
    }
}
