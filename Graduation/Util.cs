using System;
using Graduation.Entities;
using Graduation.TestMap;
using Graduation.Animations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Graduation.Graphics;

namespace Graduation
{
    class Util
    {
        public static bool InRangeX(Entity e1, Entity e2, float maxDistance)
        {
            float distance = Math.Abs(e1.Position.X - e2.Position.X);
            return distance < maxDistance;
        }

        public static double RandomDouble(float min, float max)
        {
            if (min < max)
            {
                float saved = min;
                min = max;
                max = saved;
            }

            Random rand = new Random();
            return min + rand.NextDouble() * (max - min);
        }

        public static bool areOverlapping(Entity e1, Entity e2)
        {
            if (e1.Position.Y <= e2.Position.Y + e2.Dimensions.Y && e1.Position.Y + e1.Dimensions.Y >= e2.Position.Y)
            {
                if (e1.Position.X <= e2.Position.X + e2.Dimensions.X && e1.Position.X + e1.Dimensions.X >= e2.Position.X)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool newPositionBoxCollision(Entity e, Map map, Vector2 newPos)
        {
            foreach (Box box in map.Boxes)
            {
                if (e.collided(box, newPos))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool InRange(Entity e1, Entity e2, float maxDistance)
        {
            float distanceX = Math.Abs(e1.Position.X + (e1.Dimensions.X / 2) - e2.Position.X);
            float distanceY = Math.Abs(e1.Position.Y + (e1.Dimensions.Y / 2) - e2.Position.Y);
            return distanceX < maxDistance && distanceY < maxDistance;
        }
    }
}
