using System;

using Graduation.Entities;

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
            if (e1.Position.Y <= e2.Position.Y + e2.Position.Y && e1.Position.Y + e1.Dimensions.Y >= e2.Position.Y)
            {
                if (e1.Position.X <= e2.Position.X + e2.Position.X && e1.Position.X + e2.Dimensions.X >= e2.Position.X)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
