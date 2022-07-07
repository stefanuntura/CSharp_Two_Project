using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Graduation.Entities
{
    class DatabaseWeapon : Weapon
    {
        public DatabaseWeapon(Game game, Vector2 dimension) : base(game, dimension)
        {
            attackTimer = 500;
            Cooldown = 500;
            Speed = 7;
            Damage = 20;
        }

        public override void LoadContent(Game game)
        {
            device = game.Content.Load<Texture2D>("Weapon/ResitWeapon");
        }
    }
}
