using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Graduation.Entities
{
    class ProjectileWeapon : Weapon
    {
        public ProjectileWeapon(Game game, Vector2 dimension) : base(game, dimension) 
        {
            attackTimer = 500;
            Cooldown = 2500;
            Speed = 7;
            Damage = 1;
        }
    }
}
