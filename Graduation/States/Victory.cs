using Graduation.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graduation.States
{
    class Victory : State
    {
        private List<Component> _components;
        private SpriteFont Font;

        public Victory(Game1 game, GraphicsDevice graphicsDevice, ContentManager contentManager) : base(game, graphicsDevice, contentManager)
        {
            var congrats = _contentManager.Load<Texture2D>("Controls/Congrats");
            var buttonTexture = _contentManager.Load<Texture2D>("Controls/button");
            Font  = _contentManager.Load<SpriteFont>("Fonts/Font");

            var victory = new Img(congrats)
            {
                Position = new Vector2(180, 100)
            };


            var menu = new Button(buttonTexture, Font)
            {
                Position = new Vector2(100, 300),
                Text = "Back to Main Menu",
            };


            var quitGame = new Button(buttonTexture, Font)
            {
                Position = new Vector2(450, 300),
                Text = "Quit Game",
            };

            //button Events
            menu.Click += menu_Click;
            quitGame.Click += quitGame_Click;

            _components = new List<Component>()
            {
                victory,
                menu,
                quitGame,
            };
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.DrawString(Font, "You have completed your first year! take a rest and stay tuned for your second year...", new Vector2(100, 200), Color.Black);

            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _components)
                component.Update(gameTime);
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