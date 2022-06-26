using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Graduation
{
    class Healthbar : DrawableGameComponent
    {
        private Texture2D _healthbar;
        private Texture2D _bar;
        private Vector2 _position;
        public Healthbar(Game game, Vector2 position) : base(game)
        {
            LoadContent(game);
            _position = position;
        }

        public void Update()
        {

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, double health)
        {
            spriteBatch.Draw(_healthbar, _position, Color.White);
            Vector2 barPos = new Vector2(_position.X + 15, _position.Y + 6);
            for(int i = 0; i <= health; i++)
            {
                spriteBatch.Draw(_bar, new Vector2(barPos.X++, barPos.Y), Color.White);
            }
            
        }


        public virtual void LoadContent(Game game)
        {
            _healthbar = game.Content.Load<Texture2D>("Health/healthbar");
            _bar = game.Content.Load<Texture2D>("Health/bar");
        }
    }
}
