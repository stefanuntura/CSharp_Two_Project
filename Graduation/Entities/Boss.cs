using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Graduation.Graphics;
using Graduation.Animations;

namespace Graduation.Entities
{
    public abstract class Boss : Entity
    {
        public AnimationSprite _animationSprite;
        public EntityState State { get; private set; }
        public Vector2 Position { get; set; }
        public double Health { get; set; }
        public double Speed { get; set; }
        public double Gravity { get; set; }
        public double Damage { get; set; }
        public int DrawOder { get; set; }

        public Boss(Game game, Vector2 position) : base(game, position)
        {
        }
    }
}
