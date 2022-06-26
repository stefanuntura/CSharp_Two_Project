using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graduation.States
{
    public class GameState : State
    {
        private Entities.Player _player;
        private Entities.BossLevelOne _bossLevelOne;
        private TestMap.Map _map;
        public GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager contentManager) : base(game, graphicsDevice, contentManager)
        {
            _player = new Entities.Player(game, new Vector2(0, 0));
            _bossLevelOne = new Entities.BossLevelOne(game, new Vector2(0, 15));
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
            _spriteBatch.Begin();

            _player.Draw(_spriteBatch, gameTime);

            _bossLevelOne.Draw(_spriteBatch, gameTime);
            
            _map.Draw(_spriteBatch, gameTime);
        
            _spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            _player.Update(gameTime, _map);
        }
    }
}
