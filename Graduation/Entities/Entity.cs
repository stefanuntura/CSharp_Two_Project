using Microsoft.Xna.Framework;
using Graduation.Graphics;
using Graduation.TestMap;
using System.Diagnostics;

namespace Graduation.Entities
{
    public abstract class Entity : DrawableGameComponent
    {

        public Sprite Sprite { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Dimensions { get; set; }
        public double Health { get; set; }
        public double Speed { get; set; }
        public double VerticalSpeed { get; set; }
        public double Gravity { get; set; }
        public double Damage { get; set; }
        public Weapon Weapon { get; set; }

        public Entity(Game game, Vector2 position) : base(game)
        {
            Position = position;
        }

        public bool collided(Box box, Vector2 newPos)
        {
            if (newPos.Y <= box.Position.Y + box.Dimensions.Y && newPos.Y + Dimensions.Y >= box.Position.Y)
            {
                if (newPos.X <= box.Position.X + box.Dimensions.X && newPos.X + Dimensions.X >= box.Position.X)
                {
                    return true;
                }
            }

            return false;
        }

        public abstract void moveLeft(Map map);

        public abstract void moveRight(Map map);
    }
}