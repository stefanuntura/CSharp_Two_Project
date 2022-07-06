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
                Position = new Vector2(200, 50)
            };

            var restart = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(275, 175),
                Text = "Restart Game",
            };

            var menu = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(275, 275),
                Text = "Back to Main Menu",
            };


            var quitGame = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(275, 375),
                Text = "Quit Game",
            };

            //button Events
            restart.Click += restart_Click;
            menu.Click += menu_Click;
            quitGame.Click += quitGame_Click;

            _components = new List<Component>()
            {
                GameOver,
                restart,
                menu,
                quitGame,
            };
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

        private void restart_Click(Object sender, EventArgs e)
        {
            _game.ChangeState(new GameState(_game, _graphicsDevice, _contentManager));
        }

        private void menu_Click(Object sender, EventArgs e)
        {
            _game.ChangeState(new MenuState(_game, _graphicsDevice, _contentManager));
        }

        private void quitGame_Click(Object sender, EventArgs e)
        {
            _game.Exit();
        }
    }
}
