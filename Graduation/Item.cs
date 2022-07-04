using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Graduation.Graphics;

namespace Graduation
{
    public class Item : DrawableGameComponent
    {
        public Vector2 Position;
        public Vector2 Dimensions;
        public bool Collected;
        private Texture2D _backpackSprite;

        public Item(Game game, Vector2 position, Vector2 dimensions) : base(game)
        {
            Position = position;
            Collected = false;
            Dimensions = dimensions;
            LoadContent(game);
        }

        public void Update() { }

        public void LoadContent(Game game)
        {
            _backpackSprite = game.Content.Load<Texture2D>("Item/backpack");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!Collected)
            {
                spriteBatch.Draw(_backpackSprite, Position, Color.White);
            }
        }
    }
}
