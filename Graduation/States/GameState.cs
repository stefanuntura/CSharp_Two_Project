﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

using Graduation.Entities;

namespace Graduation.States
{
    public class GameState : State
    {
        private Camera _camera;
        private Player _player;
        private List<Enemy> _enemies;
        public List<Item> Items;
        private Entities.BossLevelOne _bossLevelOne;
        private TestMap.Map _map;
        private double _counter = 0;
        public GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager contentManager) : base(game, graphicsDevice, contentManager)
        {
            _camera = new Camera();
            _player = new Player(game, new Vector2(1, 400));
            _bossLevelOne = new Entities.BossLevelOne(game, new Vector2(0, 15));
            _enemies = new List<Enemy>();
            Items = new List<Item>();

            //Add walkers
            _enemies.Add(new Walker(game, new Vector2(100, 400)));
            _enemies.Add(new Walker(game, new Vector2(1000, 400)));
            _enemies.Add(new Walker(game, new Vector2(1500, 500)));

            //Add item boxes
            Items.Add(new Item(game, new Vector2(300, 412), new Vector2(25, 26)));
            Items.Add(new Item(game, new Vector2(380, 300), new Vector2(25, 26)));
            Items.Add(new Item(game, new Vector2(430, 300), new Vector2(25, 26)));
            _map = new TestMap.Map();

            _map.addBox(new TestMap.Box(game, new Vector2(1500, 100), new Vector2(0, 440), Color.DarkSlateGray));
            _map.addBox(new TestMap.Box(game, new Vector2(200, 300), new Vector2(900, 0), Color.DarkSlateGray));
            //Add wall on right and left
            _map.addBox(new TestMap.Box(game, new Vector2(200, 700), new Vector2(1500, 0), Color.DarkSlateGray));
            _map.addBox(new TestMap.Box(game, new Vector2(200, 1500), new Vector2(-200, 0), Color.DarkSlateGray));

            //Add ceiling
            _map.addBox(new TestMap.Box(game, new Vector2(1000, 100), new Vector2(0, 0), Color.DarkSlateGray));

            //Add platforms in left area
            _map.addBox(new TestMap.Box(game, new Vector2(40, 400), new Vector2(740, 380), Color.DarkSlateGray));
            _map.addBox(new TestMap.Box(game, new Vector2(50, 400), new Vector2(690, 405), Color.DarkSlateGray));
            _map.addBox(new TestMap.Box(game, new Vector2(50, 15), new Vector2(100, 300), Color.DarkGray));
            _map.addBox(new TestMap.Box(game, new Vector2(50, 15), new Vector2(300, 350), Color.DarkGray));
            _map.addBox(new TestMap.Box(game, new Vector2(50, 15), new Vector2(200, 380), Color.DarkGray));
            _map.addBox(new TestMap.Box(game, new Vector2(60, 15), new Vector2(440, 400), Color.DarkGray));

            //Add platforms in right area
            _map.addBox(new TestMap.Box(game, new Vector2(50, 15), new Vector2(1150, 380), Color.DarkGray));
            _map.addBox(new TestMap.Box(game, new Vector2(50, 15), new Vector2(1300, 200), Color.DarkGray));
            _map.addBox(new TestMap.Box(game, new Vector2(50, 15), new Vector2(1400, 350), Color.DarkGray));
            _map.addBox(new TestMap.Box(game, new Vector2(50, 15), new Vector2(1220, 280), Color.DarkGray));

            _map.LoadContent(game);
        }

        public override void Draw(GameTime gameTime, SpriteBatch _spriteBatch)
        {   
            _spriteBatch.Begin(transformMatrix: _camera.Transform);

            _map.Draw(_spriteBatch, gameTime);

            _player.Draw(_spriteBatch, gameTime);

            foreach (Enemy enemy in _enemies)
            {
                enemy.Draw(_spriteBatch, gameTime);
            }

            foreach( Item item in Items)
            {
                item.Draw(_spriteBatch);
            }

            _bossLevelOne.Draw(_spriteBatch, gameTime);

            _spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            if(_player.Health <= 0)
            {
                _counter += gameTime.ElapsedGameTime.TotalMilliseconds;
				
                if(_counter > 1500) { _game.ChangeState(new GameOverState(_game, _graphicsDevice, _contentManager)); }
            }
            _player.Update(gameTime, _map, this);
            _camera.Follow(_player);
            _player.weapon.Update(gameTime, _player, _enemies, _map);

            foreach (Enemy enemy  in _enemies)
            {
                enemy.Update(gameTime, _player, _map);                
            }

            foreach (Enemy enemy in _enemies)
            {
                if (enemy.Health <= 0)
                {
                    _enemies.Remove(enemy);
                    break;
                }
            }
			
			_player.Update(gameTime, _map, this);
            _camera.Follow(_player);
        }
    }
}