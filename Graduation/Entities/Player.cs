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
    public class Player : Entity
    {

        private bool _canJump = false;
        InputController controller;
        AnimationSprite _animationSprite;
        String _direction = "right";
        float dt;
        String _attackDirection = "right";
        Boolean attack = false;
        Boolean throwing = false;
        private SpriteFont font;
        public Healthbar _healthbar { get; set}


        Laptop weapon_1;
        PC weapon_2;
        MacBook weapon_3;
        Weapon weapon;

        public Player(Game game, Vector2 position) : base(game, position)
        {
            LoadContent(game);
            Speed = 180;
            controller = new InputController(this);
            

            weapon_1 = new Laptop(game, new Vector2(14, 10));
            weapon_2 = new PC(game, new Vector2(14, 11));
            weapon_3 = new MacBook(game, new Vector2(14, 10));
            weapon = weapon_1;
            Health = 100;
            _healthbar = new Healthbar(game,new Vector2(60,20));
        }

        public void Update(GameTime gameTime, Map map)
        {
            if (Health <= 0)
            {
                //Debug.WriteLine("Accessed");
                _animationSprite.SetActive("Dead");
            }
            else
            {
                // delta time
                dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (_canJump)
                    _animationSprite.SetActive(_direction == "right" ? "StandRight" : "StandLeft");

                controller.handleInput(map);
                moveY(map);
                _animationSprite.Update(gameTime);
                //Debug.WriteLine(Dimensions.Y);
            }
        }

        public void moveRight(Map map)
        {
            _animationSprite.SetActive("WalkRight");
            _direction = "right";
            changePlayerDimensions();
            if (Position.X < 1265)
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

        public void moveLeft(Map map)
        {
            _animationSprite.SetActive("WalkLeft");
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

        public void moveY(Map map)
        {
            float newY;
            Gravity = Gravity < Speed * 1.5 ? Gravity + 10 : Gravity;
            changePlayerDimensions();


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

        public void jump()
        {
            if (_canJump)
            {
                Gravity = -Speed * 1.7f;
                _canJump = false;
            }
        }

        public void throwWeapon()
        {
            if (weapon.Timer >= weapon.Cooldown)
            {
                attack = true;
            }
            
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _animationSprite.Draw(spriteBatch, Position);
            // Health Number Log for testing 
            spriteBatch.DrawString(font, this.Health >= 0 ? this.Health.ToString() : "0", new Vector2(30, 30), Color.Black);
            _healthbar.Draw(gameTime,spriteBatch, Health);

            if (attack)
            {
                _attackDirection = _direction;
                weapon.Timer = 0;
                throwing = true;
                attack = false;

                weapon.Position.X = Position.X;
                weapon.Position.Y = Position.Y + (Dimensions.Y / 3);

            } else if (throwing) {
                
                    Vector2 newPos = _attackDirection == "right" ? new Vector2(weapon.Position.X + weapon.Speed, weapon.Position.Y) :
                    new Vector2(weapon.Position.X - weapon.Speed, weapon.Position.Y);
                    weapon.Draw(spriteBatch, gameTime, newPos);

                if (weapon.Timer > 500) // or collided
                {
                    throwing = false;
                }
            }

            weapon.Timer += gameTime.ElapsedGameTime.TotalMilliseconds;
        }

        public void LoadContent(Game game)
        {
            _animationSprite = new AnimationSprite(new Dictionary<string, Animation>()
            {
              { "WalkRight", new Animation(game.Content.Load<Texture2D>("Player/WalkRight"), 2) },
              { "WalkLeft", new Animation(game.Content.Load<Texture2D>("Player/WalkLeft"), 2) },
              { "StandRight", new Animation(game.Content.Load<Texture2D>("Player/player"), 1) },
              { "StandLeft", new Animation(game.Content.Load<Texture2D>("Player/playerL"), 1) },
              { "UpRight", new Animation(game.Content.Load<Texture2D>("Player/UpRight"), 1) },
              { "UpLeft", new Animation(game.Content.Load<Texture2D>("Player/UpLeft"), 1) },
              { "DownRight", new Animation(game.Content.Load<Texture2D>("Player/DownRight"), 1) },
              { "DownLeft", new Animation(game.Content.Load<Texture2D>("Player/DownLeft"), 1) },
              { "Dead", new Animation(game.Content.Load<Texture2D>("Player/PlayerDead"), 1) },
            }, "StandRight", Color.White);
            font = game.Content.Load<SpriteFont>("Fonts/Font");
            changePlayerDimensions();
        }

        public void changePlayerDimensions()
        {
            Animation currAnim = _animationSprite.CurrentAnimation;
            Dimensions = new Vector2(currAnim.FrameWidth, currAnim.FrameHeight);
        }

        public void swichWeapon(int i)
        {
            switch (i)
            {
                case 1:
                    weapon = weapon_1;
                    break;
                case 2:
                    weapon = weapon_2;
                    break;
                case 3:
                    weapon = weapon_3;
                    break;
            }
        }
    }
}
