using Graduation.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graduation.States
{
    class GameOverState : State
    {
        private List<Component> _components;

        public GameOverState(Game1 game, GraphicsDevice graphicsDevice, ContentManager contentManager) : base(game, graphicsDevice, contentManager)
        {
            var gameOverTexture = _contentManager.Load<Texture2D>("Controls/GameOver");
            var buttonTexture = _contentManager.Load<Texture2D>("Controls/button");
            var buttonFont = _contentManager.Load<SpriteFont>("Fonts/Font");

            var GameOver = new Img(gameOverTexture)
            {
                Position = new Vector2(175, 100)
            };

            var restart = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(250, 250),
                Text = "Restart game",
            };

            restart.Click += restart_Click;

            var quitGame = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(250, 350),
                Text = "Quit Game",
            };

            quitGame.Click += quitGame_Click;

            _components = new List<Component>()
            {
                GameOver,
                restart,
                quitGame,
            };
        }

        private void restart_Click(Object sender, EventArgs e)
        {
            _game.ChangeState(new GameState(_game, _graphicsDevice, _contentManager));
        }

        private void quitGame_Click(Object sender, EventArgs e)
        {
            _game.Exit();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _components)
                component.Update(gameTime);
        }
    }
}
