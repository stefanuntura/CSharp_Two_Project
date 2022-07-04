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
        private SpriteFont font;
        public Healthbar(Game game, Vector2 position) : base(game)
        {
            LoadContent(game);
        }

        public void Update(Game game)
        {

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, double health, Vector2 pos)
        {
            Vector2 newPos = new Vector2(pos.X - 360, pos.Y - 220);
            spriteBatch.DrawString(font, health >= 0 ? health.ToString() : "0", new Vector2(newPos.X - 23, newPos.Y + 8), Color.Brown);
            spriteBatch.Draw(_healthbar, newPos , Color.White);
            Vector2 barPos = new Vector2(newPos.X + 15, newPos.Y + 6);
            for(int i = 0; i <= health; i++)
            {
                spriteBatch.Draw(_bar, new Vector2(barPos.X++, barPos.Y), Color.White);
            }    
        }


        public virtual void LoadContent(Game game)
        {

            font = game.Content.Load<SpriteFont>("Fonts/EffectFont");
            _healthbar = game.Content.Load<Texture2D>("Health/healthbar");
            _bar = game.Content.Load<Texture2D>("Health/bar");
        }
    }
}
