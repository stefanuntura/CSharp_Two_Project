using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Graduation.Entities;
using Graduation.States;

namespace Graduation
{
    class Camera
    {
        public Matrix Transform { get; private set; }
        public Matrix HpTransform { get; private set; }

        public void Follow(Player target) 
        {
            var position = Matrix.CreateTranslation(
                -target.Position.X,
                -target.Position.Y,
                0);

            var offset = Matrix.CreateTranslation(
                Game1.ScreenWidth / 2,
                Game1.ScreenHeight / 2,
                0);
            Transform = position * offset;
        }

        public void snapHpBar(Healthbar bar, Player target) 
        {
            var position = Matrix.CreateTranslation(
               -target.Position.X,
               -target.Position.Y,
               0);

            var offset = Matrix.CreateTranslation(
                Game1.ScreenWidth / 2,
                Game1.ScreenHeight / 2,
                0);
            Transform = position * offset;
        }
    }
}
