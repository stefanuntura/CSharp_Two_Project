using Graduation.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduation.States
{
    public class MenuState : State
    {
        private List<Component> _components;

        public MenuState(Game1 game, GraphicsDevice graphicsDevice, ContentManager contentManager) : base(game, graphicsDevice, contentManager)
        {
            var gameTitleTexture = _contentManager.Load<Texture2D>("Controls/GameTitle");
            var buttonTexture = _contentManager.Load<Texture2D>("Controls/button");
            var buttonFont = _contentManager.Load<SpriteFont>("Fonts/Font");

            var newGameTitlePic = new Img(gameTitleTexture)
            {
                Position = new Vector2(200, 75)
            };

            var newGameBtn = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(275, 250),
                Text = "Start Game",
            };

            var quitGameBtn = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(275, 350),
                Text = "Quit Game",
            };
            
            //button events
            newGameBtn.Click += newGameBtn_Click;
            quitGameBtn.Click += quitGameBtn_Click;

            //Make collection of all components
            _components = new List<Component>()
            {
                newGameTitlePic,
                newGameBtn,
                quitGameBtn,
            };
        }

        public override void Draw(GameTime gameTime, SpriteBatch _spriteBatch)
        {
            _spriteBatch.Begin();
            
            foreach(var component in _components)
                component.Draw(gameTime, _spriteBatch);

            _spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _components)
                component.Update(gameTime);
        }

        private void newGameBtn_Click(Object sender, EventArgs e)
        {
            _game.ChangeState(new Lobby(_game, _graphicsDevice, _contentManager));
        }

        private void quitGameBtn_Click(Object sender, EventArgs e)
        {
            _game.Exit();
        }
    }
}
