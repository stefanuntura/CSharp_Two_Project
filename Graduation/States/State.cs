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
    public abstract class State
    {

        protected Game1 _game;
        protected GraphicsDevice _graphicsDevice;
        protected ContentManager _contentManager;

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        public State(Game1 game, GraphicsDevice graphicsDevice, ContentManager contentManager)
        {
            _game = game;
            _graphicsDevice = graphicsDevice;
            _contentManager = contentManager;
        }

        public abstract void Update(GameTime gameTime);
    }
}
