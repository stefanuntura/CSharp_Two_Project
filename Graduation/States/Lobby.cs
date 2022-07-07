using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

using Graduation.Entities;

namespace Graduation.States
{
    public class Lobby : State
    {
        private Player _player;
        private TestMap.Map _map;

        private Texture2D _wall;
        private Texture2D _floor;
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

        public override void Draw(GameTime gameTime, SpriteBatch _spriteBatch)
        {

            _spriteBatch.Begin();
            for (int i = 0; i < 30; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    //Draw Backgroundpsprite
                    _spriteBatch.Draw(_wall, new Vector2(i * 32, j * 32), Color.White);
                }
            }
            _map.Draw(_spriteBatch, gameTime);

            _player.Draw(_spriteBatch, gameTime, this);

            for (int i = 0; i < 30; i++)
            {
                if (( i != 0 && i < 18) || ( i > 20 && i < 24))
                {
                    // Draw floor sprites
                    _spriteBatch.Draw(_floor, new Vector2(i * 32, 330), Color.White);
                }
                for (int j = 0; j < 20; j++)
                {
                }
            }

            /* foreach (Enemy enemy in _enemies)
             {
                 enemy.Draw(_spriteBatch, gameTime);
             }*/

            _spriteBatch.End();
        }

        public void LoadContent(Game game)
        {
            _wall = game.Content.Load<Texture2D>("Lobby/wall3");
            _floor = game.Content.Load<Texture2D>("Lobby/floor");
        }

        public override void Update(GameTime gameTime)
        {

            if (_player.Position.Y > 410)
            {
                _game.ChangeState(new GameState(_game, _graphicsDevice, _contentManager));
            }

            if (_player.Position.X < 0 || _player.Position.Y < 0)
            {
                _player.Position = new Vector2(50, 250);
            }

            _player.Update(gameTime, _map, this);
            _player.weapon.Update(gameTime, _player, _map.Enemies, _map);

            _map.Update(gameTime, _player);
            _player.Update(gameTime, _map, this);
        }
    }
}