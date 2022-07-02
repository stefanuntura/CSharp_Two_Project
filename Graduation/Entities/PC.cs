using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graduation.Entities
{
    class PC : Weapon
    {
        public PC(Game game, Vector2 dimension) : base(game, dimension)
        {
            Timer = 1000;
            Cooldown = 1000;
            Speed = 3;
        }

        public override void LoadContent(Game game)
        {
            device = game.Content.Load<Texture2D>("Weapon/PC");
        }
    }
}
