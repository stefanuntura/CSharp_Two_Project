using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graduation.TestMap
{
    class Spike : Box
    {
        public Spike(Game game, Vector2 dimensions, Vector2 position, Color color) : base(game, dimensions, position, color)
        {
        }

        public override void LoadContent(Game game)
        {
            Texture = game.Content.Load<Texture2D>("Blocks/Spike");
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, new Rectangle((int)Position.X, (int)Position.Y, 25, 25), Color.White);
        }
    }
}
