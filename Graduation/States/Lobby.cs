﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Graduation.Animations;

using Graduation.Entities;

namespace Graduation.States
{
    public class Lobby : State
    {
        private Player _player;
        private TestMap.Map _map;

        //Design Elements
        private Texture2D _wall;
        private Texture2D _floor;
        private Texture2D _reception;
        private Texture2D _drop;
        private Texture2D _blackBlock;
        private Texture2D _halfFloor;
        private Texture2D _weaponControls;
        private Texture2D _generalControls;
        private Texture2D _goodLuck;
        private Texture2D _couch;
        private Texture2D _year;

        private AnimationSprite _animationSprite;

        private bool moveState =  false;
        private double _displayTimer;
        private double _transitionTimer;

        public Lobby(Game1 game, GraphicsDevice graphicsDevice, ContentManager contentManager) : base(game, graphicsDevice, contentManager)
        {
            //Initialize map
            _map = new TestMap.Map();
            _player = new Player(game, new Vector2(50,50));
            LoadContent(game);

            for (int i = 0; i < 30; i++)
            {
                _map.addBox(new TestMap.Box(game, new Vector2(32, 32), new Vector2(i * 32, 0), Color.Black));
                for (int j = 0; j < 20; j++)
                {
                    if(i < 1 || i == 24)

                        _map.addBox(new TestMap.Box(game, new Vector2(32, 32), new Vector2(i * 32, j * 32), Color.Black));
                }

                // black floor blocks
                _map.addBox(new TestMap.Box(game, new Vector2(32 * 18, 150), new Vector2(0, 330), Color.Black));
                _map.addBox(new TestMap.Box(game, new Vector2(100, 150), new Vector2(32 * 21, 330), Color.Black));
            }
        }

        public override void Update(GameTime gameTime)
        {
            //update conversation timer
            _displayTimer += gameTime.ElapsedGameTime.TotalSeconds;

            //Move to next gamestate
            if (_player.Position.Y > 430)
            {
                _transitionTimer += gameTime.ElapsedGameTime.TotalSeconds;
                moveState = true;
                if (_transitionTimer > 2)
                    _game.ChangeState(new GameState(_game, _graphicsDevice, _contentManager));
            }

            // Place player back at spawn if they move outside map
            if (_player.Position.X < 0 || _player.Position.Y < 0)
            {
                _player.Position = new Vector2(50, 250);
            }

            _animationSprite.Update(gameTime);

            _player.Update(gameTime, _map, this);
            _player.weapon.Update(gameTime, _player, _map.Enemies, _map);

            _map.Update(gameTime, _player);
            _player.Update(gameTime, _map, this);

            if(_player.Health == 0)
            {
                _player.Health = 100;
            }
        }

        public void LoadContent(Game game)
        {
            _wall = game.Content.Load<Texture2D>("Lobby/wall3");
            _floor = game.Content.Load<Texture2D>("Lobby/floor");
            _reception = game.Content.Load<Texture2D>("Lobby/reception");
            _drop = game.Content.Load<Texture2D>("Lobby/dropWider");
            _blackBlock = game.Content.Load<Texture2D>("Lobby/wall");
            _halfFloor = game.Content.Load<Texture2D>("Lobby/floor3");
            _weaponControls = game.Content.Load<Texture2D>("Lobby/weapon");
            _generalControls = game.Content.Load<Texture2D>("Lobby/controlUpdated");
            _goodLuck = game.Content.Load<Texture2D>("Lobby/gl");
            _couch = game.Content.Load<Texture2D>("Lobby/couch");
            _year = game.Content.Load<Texture2D>("Lobby/Y1");

            _animationSprite = new AnimationSprite(new Dictionary<string, Animation>()
            {
              { "0", new Animation(game.Content.Load<Texture2D>("Lobby/conv0"), 1) },
              { "1", new Animation(game.Content.Load<Texture2D>("Lobby/conv1"), 1) },
              { "2", new Animation(game.Content.Load<Texture2D>("Lobby/conv2"), 1) },
              { "3", new Animation(game.Content.Load<Texture2D>("Lobby/conv3"), 1) },
              { "4", new Animation(game.Content.Load<Texture2D>("Lobby/conv4"), 1) },
              { "5", new Animation(game.Content.Load<Texture2D>("Lobby/conv5"), 1) },
              { "6", new Animation(game.Content.Load<Texture2D>("Lobby/conv6"), 1) },
              { "7", new Animation(game.Content.Load<Texture2D>("Lobby/conv7"), 1) },
            }, "0", Color.White * 0.75f);
        }

        public override void Draw(GameTime gameTime, SpriteBatch _spriteBatch)
        {

            _spriteBatch.Begin();
            if (!moveState)
            {
                for (int i = 0; i < 30; i++)
                {
                    for (int j = 0; j < 20; j++)
                    {
                        //Draw Backgroundpsprite
                        _spriteBatch.Draw(_wall, new Vector2(i * 32, j * 32), Color.White);
                    }
                }
                _map.Draw(_spriteBatch, gameTime);

                //Draw design elements
                _spriteBatch.Draw(_reception, new Vector2(120, 287), Color.White);
                _spriteBatch.Draw(_drop, new Vector2(18 * 32, 335), Color.White);
                _spriteBatch.Draw(_halfFloor, new Vector2(18 * 32, 330), Color.White);
                _spriteBatch.Draw(_couch, new Vector2(330, 330 - _couch.Bounds.Height), Color.White);
                _spriteBatch.Draw(_couch, new Vector2(390, 330 - _couch.Bounds.Height), Color.White);

                _player.Draw(_spriteBatch, gameTime, this, _map.Enemies.Count);

                for (int i = 0; i < 30; i++)
                {
                    if ((i != 0 && i < 18) || (i > 20 && i < 24))
                    {
                        // Draw floor sprites
                        _spriteBatch.Draw(_floor, new Vector2(i * 32, 330), Color.White);
                    }
                    for (int j = 0; j < 20; j++)
                    {
                        //Draw Black blocks in hole
                        if (i > 17 && i < 21 && j > 13)
                            _spriteBatch.Draw(_blackBlock, new Vector2(i * 32, j * 32), Color.White);
                    }
                }

                //Draw Dialogue bubble
                if (_displayTimer > 6 && _player.Position.X > 120 && _player.Position.X < 200)
                {
                    int sprite = (int)Math.Floor(Util.RandomDouble(0, 8));
                    _animationSprite.SetActive(sprite.ToString());
                    _displayTimer = 0;
                }
                else if (_displayTimer <= 6 && _player.Position.X > 120 && _player.Position.X < 200)
                {
                    _animationSprite.Draw(_spriteBatch, new Vector2(80, 80));
                }


                // display control explanation
                _spriteBatch.Draw(_player.Position.Y > 320 || _player.Position.X > 600 ? _goodLuck : _player.Position.X > 300 ? _weaponControls : _generalControls, new Vector2(430, 100), Color.Black * 0.5f);
            }
            else
            {
                for (int i = 0; i < 30; i++)
                    for (int j = 0; j < 20; j++)
                            _spriteBatch.Draw(_blackBlock, new Vector2(i * 32, j * 32), Color.Black);

                _spriteBatch.Draw(_year, new Vector2(280, 230), Color.White);
            }

                _spriteBatch.End();
        }
    }
}