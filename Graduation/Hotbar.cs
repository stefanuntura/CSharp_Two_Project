using Graduation.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graduation
{
    class Hotbar : DrawableGameComponent
    {
        Texture2D _hotbar;
        Texture2D _border;

        public int SelectedWeapon;

        public Hotbar(Game game) : base(game)
        {
            LoadContent(game);
            SelectedWeapon = 1; //default
        }


        public void Draw(GameTime gametime, SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Draw(_hotbar, new Rectangle((int)position.X - 45, (int)position.Y + 175, 160, 57), Color.White);

            //draw highlighted borders around the weapon slot of selected weapon
            if (SelectedWeapon == 1)
            {
                spriteBatch.Draw(_border, new Rectangle((int)position.X - 18, (int)position.Y + 220, 35, 1), Color.LightGray);
                spriteBatch.Draw(_border, new Rectangle((int)position.X - 18, (int)position.Y + 185, 1 , 35), Color.LightGray);
                spriteBatch.Draw(_border, new Rectangle((int)position.X - 18, (int)position.Y + 185, 35, 1), Color.LightGray);
                spriteBatch.Draw(_border, new Rectangle((int)position.X + 17, (int)position.Y + 185, 1, 35), Color.LightGray);
            } else if (SelectedWeapon == 2)
            {
                spriteBatch.Draw(_border, new Rectangle((int)position.X + 17, (int)position.Y + 220, 35, 1), Color.LightGray);
                spriteBatch.Draw(_border, new Rectangle((int)position.X + 17, (int)position.Y + 185, 1, 35), Color.LightGray);
                spriteBatch.Draw(_border, new Rectangle((int)position.X + 17, (int)position.Y + 185, 35, 1), Color.LightGray);
                spriteBatch.Draw(_border, new Rectangle((int)position.X + 52, (int)position.Y + 185, 1, 35), Color.LightGray);
            } else if(SelectedWeapon == 3)
            {
                spriteBatch.Draw(_border, new Rectangle((int)position.X + 52, (int)position.Y + 220, 35, 1), Color.LightGray);
                spriteBatch.Draw(_border, new Rectangle((int)position.X + 52, (int)position.Y + 185, 1, 35), Color.LightGray);
                spriteBatch.Draw(_border, new Rectangle((int)position.X + 52, (int)position.Y + 185, 35, 1), Color.LightGray);
                spriteBatch.Draw(_border, new Rectangle((int)position.X + 87, (int)position.Y + 185, 1, 35), Color.LightGray);
            }
            
        }

        public void LoadContent(Game game)
        {
            _hotbar = game.Content.Load<Texture2D>("Controls/Hotbar");
            
            _border = new Texture2D(GraphicsDevice, 1, 1);
            _border.SetData(new[] { Color.White });
        }
    }
}
