﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Graduation.Graphics;
using Graduation.TestMap;
using Graduation.Animations;
using System.Diagnostics;
using Graduation.States;

namespace Graduation.Entities
{
    public class Player : Entity
    {
        private bool _canJump = false;
        InputController controller;
        AnimationSprite _animationSprite;
        public String _direction = "right";
        float dt;
        public Boolean attack = false;
        public Boolean throwing = false;
        private Healthbar _healthbar;

        //Effect 
        private List<PlayerEffect> _effects;
        private SpriteFont _font;
        private int _currentEffect;
        private bool _effectActivated;
        private double _effectTimer;
        private double _defaultSpeed;

        //weapons & hotbar
        Laptop weapon_1;
        MacBook weapon_2;
        PC weapon_3;
        public Weapon weapon;
        private Hotbar _hotbar;

        public Player(Game game, Vector2 position) : base(game, position)
        {
            LoadContent(game);
            Speed = 100;
            VerticalSpeed = 205;
            controller = new InputController(this);
            
            weapon_1 = new Laptop(game, new Vector2(14, 10));
            weapon_2 = new MacBook(game, new Vector2(14, 10));
            weapon_3 = new PC(game, new Vector2(14, 11));
            weapon = weapon_1; //set default
            _hotbar = new Hotbar(game);
            
            Health = 100;
            _healthbar = new Healthbar(game,new Vector2(60,20));
            
            _effectTimer = 0;
            _effects = new List<PlayerEffect>();
            _defaultSpeed = Speed;
            _effectActivated = true;

            //Creating effects
            _effects.Add(new PlayerEffect(game, "", true, 0));
            _effects.Add(new PlayerEffect(game, "20s Speedboost", true, 20));
            _effects.Add(new PlayerEffect(game, "10s Slowness", false, 10));
            _effects.Add(new PlayerEffect(game, "+10 Health", true, 3));
            _effects.Add(new PlayerEffect(game, "-5 Health", false, 2));
        }

        public void Update(GameTime gameTime, Map map, State gs)
        {
            if (Health <= 0)
            {
                _animationSprite.SetActive("Dead");
                _animationSprite.Update(gameTime);
                moveY(map);
                Util.changePlayerDimensions(_animationSprite, this);
            }
            else
            {
                // delta time
                dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (_canJump)
                    _animationSprite.SetActive(_direction == "right" ? "StandRight" : "StandLeft");

                controller.handleInput(map, gs);

                moveY(map);
                _animationSprite.Update(gameTime);

                foreach (Item item in map.Items)
                {
                    if (Util.CollectedItem(this, item))
                    {
                        _effectTimer = 0;
                        _currentEffect = (int)Math.Floor(Util.RandomDouble(1, _effects.Count));
                        _effectActivated = false;
                        map.Items.Remove(item);
                        break;
                    }
                }
                _effectTimer += _effectActivated ? gameTime.ElapsedGameTime.TotalSeconds : 0;
                HandleEffects();
            }
        }

        public override void moveRight(Map map)
        {
            _animationSprite.SetActive("WalkRight");
            _direction = "right";
            Util.changePlayerDimensions(_animationSprite, this);
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

                Util.blockDmg(collidedBox, this);
            }
        }

        public override void moveLeft(Map map)
        {
            _animationSprite.SetActive("WalkLeft");
            _direction = "left";
            Util.changePlayerDimensions(_animationSprite, this);
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

                Util.blockDmg(collidedBox, this);
            }
        }

        public void moveY(Map map)
        {
            float newY;
            Gravity = Gravity < VerticalSpeed * 1.5 ? Gravity + 10 : Gravity;
            Util.changePlayerDimensions(_animationSprite, this);


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

                Util.blockDmg(collidedBox, this);
            }
        }

        public void jump()
        {
            if (_canJump)
            {
                Gravity = -VerticalSpeed * 1.7f;
                _canJump = false;
            }
        }

        public void throwWeapon()
        {
            //Called by the InputController, only starts the attack method once cooldown of weapon is down
            if (weapon.attackTimer >= weapon.Cooldown)
            {
                attack = true;
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime,  State gs, int i)
        {
            // Disable healthbar and fix position of hotbar in lobby 
            if (gs.GetType() != typeof(Lobby))
            {
                _healthbar.Draw(gameTime, spriteBatch, Health, Position);
                _hotbar.Draw(gameTime, spriteBatch, Position);
                String remainingEnemies = "Enemies remaining: " + i;
                spriteBatch.DrawString(_font, remainingEnemies, new Vector2(Position.X + 200, Position.Y - 225), Color.Crimson);
            }
            else
                _hotbar.Draw(gameTime, spriteBatch, new Vector2(350, 230));

            _animationSprite.Draw(spriteBatch, Position);

            weapon.playerAttack(gameTime, spriteBatch, this);
            if (_effectTimer < _effects[_currentEffect].TimeSpan)
                _effects[_currentEffect].Draw(spriteBatch, gameTime, this);

            if (0 < _effectTimer && _effectTimer <= 2)
            {
                float xPlacement = Position.X + 15 - (_font.MeasureString(_effects[_currentEffect].Title).X / 2);
                spriteBatch.DrawString(_font, _effects[_currentEffect].Title, new Vector2(xPlacement, Position.Y - 20), _effects[_currentEffect].GoodEffect ? Color.Green : Color.Red);
            }
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

            _font = game.Content.Load<SpriteFont>("Fonts/Font");
            Util.changePlayerDimensions(_animationSprite, this);
        }



        public void switchWeapon(int i)
        {
            //Changes active weapon
            switch (i)
            {
                case 1:
                    weapon = weapon_1;
                    _hotbar.SelectedWeapon = 1;
                    break;
                case 2:
                    weapon = weapon_2;
                    _hotbar.SelectedWeapon = 2;
                    break;
                case 3:
                    weapon = weapon_3;
                    _hotbar.SelectedWeapon = 3;
                    break;
            }
        }

        public void HandleEffects()
        {
            if (!_effectActivated && _effects[_currentEffect].TimeSpan > _effectTimer)
            {
                switch (_currentEffect)
                {
                    case 1:
                        Speed += 30;
                        break;
                    case 2:
                        Speed -= 30;
                        break;
                    case 3:
                        Health = Health + 10 >= 100 ? 100 : Health + 10;
                        break;
                    case 4:
                        Health = Health - 5 <= 0 ? 0 : Health -  5;
                        break;
                }
                _effectActivated = true;
            }
            else if(_effects[_currentEffect].TimeSpan <= _effectTimer && _effectActivated)
            {
                Speed = _defaultSpeed;
            }
        }

        public void Restart()
        {
            this.Health = 0;
        }
    }
}
