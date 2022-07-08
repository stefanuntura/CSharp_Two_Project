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

        public static bool weaponHitEnemy(Weapon e1, Entity e2)
        {
            if (e1.Position.Y <= e2.Position.Y + e2.Dimensions.Y && e1.Position.Y + e1.Dimension.Y >= e2.Position.Y)
            {
                if (e1.Position.X <= e2.Position.X + e2.Dimensions.X && e1.Position.X + e1.Dimension.X >= e2.Position.X)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool weaponHitPlayer(Weapon e1, Player player)
        {
            if (e1.Position.Y <= player.Position.Y + player.Dimensions.Y && e1.Position.Y + e1.Dimension.Y >= player.Position.Y)
            {
                if (e1.Position.X <= player.Position.X + player.Dimensions.X && e1.Position.X + e1.Dimension.X >= player.Position.X)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool weaponHitWall(Weapon e1, Box e2)
        {
            if (e1.Position.Y <= e2.Position.Y + e2.Dimensions.Y && e1.Position.Y + e1.Dimension.Y >= e2.Position.Y)
            {
                if (e1.Position.X <= e2.Position.X + e2.Dimensions.X && e1.Position.X + e1.Dimension.X >= e2.Position.X)
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

        public static bool CollectedItem(Entity e1, Item i)
        {
            if (e1.Position.Y <= i.Position.Y + i.Dimensions.Y && e1.Position.Y + e1.Dimensions.Y >= i.Position.Y)
            {
                if (e1.Position.X <= i.Position.X + i.Dimensions.X && e1.Position.X + e1.Dimensions.X >= i.Position.X)
                {
                    return true;
                }
            }
            return false;
        }

        public static void blockDmg(Box box, Entity entity)
        {
            if (box is Spike)
            {
                entity.Health -= 50;
            }
        }

        public static void changePlayerDimensions(AnimationSprite _animationSprite, Entity entity)
        {
            Animation currAnim = _animationSprite.CurrentAnimation;
            entity.Dimensions = new Vector2(currAnim.FrameWidth, currAnim.FrameHeight);
        }

        public static void checkAndMoveRight(Map map, Entity entity, float dt)
        {
            if (entity.Position.X< 3500)
            {
                bool collision = false;
                Box collidedBox = null;
                Vector2 newPos = new Vector2(entity.Position.X + (float)entity.Speed * dt, entity.Position.Y);

                foreach (Box box in map.Boxes)
                {
                    if (entity.collided(box, newPos))
                    {
                        collision = true;
                        collidedBox = box;
                        break;
                    }
                }

                if (!collision)
                {
                    entity.Position = newPos;
                }
                else
                {
                    entity.Position = new Vector2(collidedBox.Position.X - entity.Dimensions.X - 1, entity.Position.Y);
                }
            }
        }


        public static void checkAndMoveLeft(Map map, Entity entity, float dt)
        {
            if (entity.Position.X > 0)
            {
                bool collision = false;
                Box collidedBox = null;
                Vector2 newPos = new Vector2(entity.Position.X - (float)entity.Speed * dt, entity.Position.Y);

                foreach (Box box in map.Boxes)
                {
                    if (entity.collided(box, newPos))
                    {
                        collision = true;
                        collidedBox = box;
                        break;
                    }
                }

                if (!collision)
                {
                    entity.Position = newPos;
                }
                else
                {
                    entity.Position = new Vector2(collidedBox.Position.X + collidedBox.Dimensions.X + 1, entity.Position.Y);
                }
            }
        }

        public static void playerCheckAndMoveLeft(Map map, Entity entity, float dt)
        {
            if (entity.Position.X > 0)
            {
                bool collision = false;
                Box collidedBox = null;
                Vector2 newPos = new Vector2(entity.Position.X - (float)entity.Speed * dt, entity.Position.Y);

                foreach (Box box in map.Boxes)
                {
                    if (entity.collided(box, newPos))
                    {
                        collision = true;
                        collidedBox = box;
                        if (box is Spike)
                        {
                            entity.Health -= 50;
                        }
                        break;
                    }
                }

                if (!collision)
                {
                    entity.Position = newPos;
                }
                else
                {
                    entity.Position = new Vector2(collidedBox.Position.X + collidedBox.Dimensions.X + 1, entity.Position.Y);
                }
            }
        }

        public static void playerCheckAndMoveRight(Map map, Entity entity, float dt)
        {
            if (entity.Position.X < 3500)
            {
                bool collision = false;
                Box collidedBox = null;
                Vector2 newPos = new Vector2(entity.Position.X + (float)entity.Speed * dt, entity.Position.Y);

                foreach (Box box in map.Boxes)
                {
                    if (entity.collided(box, newPos))
                    {
                        collision = true;
                        collidedBox = box;
                        if (box is Spike)
                        {
                            entity.Health -= 50;
                        }
                        break;
                    }
                }

                if (!collision)
                {
                    entity.Position = newPos;
                }
                else
                {
                    entity.Position = new Vector2(collidedBox.Position.X - entity.Dimensions.X - 1, entity.Position.Y);
                }
            }
        }
    }
}
