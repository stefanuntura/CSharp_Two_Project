using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graduation
{
    abstract class Weapon : DrawableGameComponent
    {
        public double Cooldown;
        public double Timer;

        public Texture2D device;
        public Vector2 Position;
        public Vector2 Dimension;

        public int Speed;


        public Weapon(Game game, Vector2 dimension) : base(game)
        {
            Dimension = dimension; 
            this.LoadContent(game);
            
        }

        public virtual void LoadContent(Game game)
        {
            device = game.Content.Load<Texture2D>("Weapon/Laptop");
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, Vector2 position)
        {
            Position = position;
            spriteBatch.Draw(device, new Rectangle((int)Position.X, (int)Position.Y, (int)Dimension.X, (int)Dimension.Y), Color.White);  
        }

        public override void Update(GameTime gameTime)
        {
            //
        }
    }
}
