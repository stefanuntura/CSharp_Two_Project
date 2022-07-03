using Microsoft.Xna.Framework;
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
        private TestMap.Map _map;
        private double _counter = 0;
        public GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager contentManager) : base(game, graphicsDevice, contentManager)
        {
            _camera = new Camera();
            _player = new Player(game, new Vector2(0, 0));
            _enemies = new List<Enemy>();
            _enemies.Add(new Walker(game, new Vector2(100, 100)));
            _map = new TestMap.Map();

            _map.addBox(new TestMap.Box(game, new Vector2(800, 100), new Vector2(0, 440), Color.DarkSlateGray));
            _map.addBox(new TestMap.Box(game, new Vector2(200, 700), new Vector2(780, 0), Color.DarkSlateGray));
            _map.addBox(new TestMap.Box(game, new Vector2(40, 400), new Vector2(740, 380), Color.DarkSlateGray));
            _map.addBox(new TestMap.Box(game, new Vector2(50, 400), new Vector2(690, 405), Color.DarkSlateGray));
            _map.addBox(new TestMap.Box(game, new Vector2(50, 15), new Vector2(100, 300), Color.DarkGray));
            _map.addBox(new TestMap.Box(game, new Vector2(50, 15), new Vector2(300, 350), Color.DarkGray));
            _map.addBox(new TestMap.Box(game, new Vector2(50, 15), new Vector2(200, 380), Color.DarkGray));
            _map.addBox(new TestMap.Box(game, new Vector2(60, 15), new Vector2(440, 400), Color.DarkGray));
            _map.LoadContent(game);
        }

        public override void Draw(GameTime gameTime, SpriteBatch _spriteBatch)
        {
            //_spriteBatch.Begin(SpriteSortMode.BackToFront, null);
            _spriteBatch.Begin(transformMatrix: _camera.Transform);
            _player.Draw(_spriteBatch, gameTime);
            foreach (Enemy enemy in _enemies)
            {
                enemy.Draw(_spriteBatch, gameTime);
            }
            _map.Draw(_spriteBatch, gameTime);

            _spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            if(_player.Health <= 0)
            {
                _counter += gameTime.ElapsedGameTime.TotalMilliseconds;
                if(_counter > 3000) { _game.ChangeState(new MenuState(_game, _graphicsDevice, _contentManager)); }
            }
            _player.Update(gameTime, _map);
            _camera.Follow(_player);
            foreach (Enemy enemy in _enemies)
            {
                enemy.Update(gameTime, _player, _map);
            }
        }
    }
}