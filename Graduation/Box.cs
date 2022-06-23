/*using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Graduation
{
    class Box
    {
        double height;
        double width;
        Vector2 position;

        public Box(double height, double width, int x, int y)
        {
            this.height = height;
            this.width = width;
            this.position = new Vector2(x, y);
        }
        public bool isPlayerStandingOn(Entities.Player player)
        {
            if (player.Position.X <= position.X + width && player.Position.X >= position.X && player.Position.Y >= position.Y && player.Position.Y < position.Y + height)
            {
                return true;
            }
            return false;
        }

        public bool XAxisCollision(Entities.Player player)
        {
            if (player.Position.X <= position.X + width && player.Position.X + player.Width >= position.X && player.Position.Y + player.Height < position.Y + height && player.Position.Y > position.Y)
            {
                return true;
            }
            return false;
        }

        // playerstandson, collides -- array in game mit boxes und dann check für array ob player standing on wenn ja nicht down, draw boxes in array
    }
}
*/