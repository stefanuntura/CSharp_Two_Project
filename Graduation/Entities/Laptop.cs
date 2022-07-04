using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Graduation.Entities
{
    class Laptop : Weapon
    {
        public Laptop(Game game, Vector2 dimension) : base(game, dimension)
        {
            attackTimer = 750;
            Cooldown = 750;
            Speed = 5;
            Damage = 25;
        }

        public override void LoadContent(Game game)
        {
            device = game.Content.Load<Texture2D>("Weapon/Laptop");
        }
    }
}
