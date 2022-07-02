using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Graduation.Graphics;
using Graduation.Animations;
using Graduation.TestMap;

namespace Graduation.Entities
{
    public class BossLevelOne : Boss
    {
        public AnimationSprite _animationSprite;
        public EntityState State { get; private set; }
        public Vector2 Position { get; set; }
        public double Health { get; set; }
        public double Speed { get; set; }
        public double Gravity { get; set; }
        public double Damage { get; set; }
        public int DrawOder { get; set; }

        float Time = 0;
        BossLevelOneController controller;
        String _direction = "right";
        SpriteFont font = null;

        public BossLevelOne(Game game, Vector2 position) : base(game, position)
        {
            LoadContent(game);
            controller = new BossLevelOneController(this);
        }
         
        public void Update(GameTime gameTime, Map map)
        {
            Time += (float)gameTime.ElapsedGameTime.TotalSeconds;
            _animationSprite.Update(gameTime);
            controller.handleAttackPatterns(Time, map);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _animationSprite.Draw(spriteBatch, Position);
            //spriteBatch.DrawString(font, "The timer is:" + Time.ToString("0.00"), new Vector2(300, 50), Color.Black);
        }

        public void LoadContent(Game game)
        {
            //font = ContentManager.Load<SpriteFont>("Fonts/Font.spritefont");
            _animationSprite = new AnimationSprite(new Dictionary<string, Animation>()
            {
              { "WalkRight", new Animation(game.Content.Load<Texture2D>("Player/WalkRight"), 2) },
              { "WalkLeft", new Animation(game.Content.Load<Texture2D>("Player/WalkLeft"), 2) },
              { "StandRight", new Animation(game.Content.Load<Texture2D>("Bosses/BossLevelOneIdle"), 1) },
              { "StandLeft", new Animation(game.Content.Load<Texture2D>("Player/playerL"), 1) },
              { "UpRight", new Animation(game.Content.Load<Texture2D>("Player/UpRight"), 1) },
              { "UpLeft", new Animation(game.Content.Load<Texture2D>("Player/UpLeft"), 1) },
              { "DownRight", new Animation(game.Content.Load<Texture2D>("Player/DownRight"), 1) },
              { "DownLeft", new Animation(game.Content.Load<Texture2D>("Player/DownLeft"), 1) },
              {"MeleeAttack", new Animation(game.Content.Load<Texture2D>("Bosses/BossLeveLoneMeleeAttack"), 1) }
            }, "StandRight", Color.White);

            changePlayerDimensions();
        }

        public void changePlayerDimensions()
        {
            Animation currAnim = _animationSprite.CurrentAnimation;
            Dimensions = new Vector2(currAnim.FrameWidth, currAnim.FrameHeight);
        }

        public void dashRight(Map map)
        {
            _animationSprite.SetActive("WalkRight");
            _direction = "right";
            changePlayerDimensions();
            if (Position.X < 1265)
            {
                bool collision = false;
                Box collidedBox = null;
                Vector2 newPos = new Vector2(Position.X + (float)Speed * Time, Position.Y);

                foreach (Box box in map.Boxes)
                {
                    if (collided(box, newPos))
                    {
                        collision = true;
                        collidedBox = box;
                        break;
                    }
                }

                if (!collision)
                {
                    Position = newPos;
                }
                else
                {
                    Position = new Vector2(collidedBox.Position.X - Dimensions.X - 1, Position.Y);
                }
            }
        }

        public void dashLeft(Map map)
        {
            _animationSprite.SetActive("WalkLeft");
            _direction = "left";
            changePlayerDimensions();
            if (Position.X > 0)
            {
                bool collision = false;
                Box collidedBox = null;
                Vector2 newPos = new Vector2(Position.X - (float)Speed * Time, Position.Y);

                foreach (Box box in map.Boxes)
                {
                    if (collided(box, newPos))
                    {
                        collision = true;
                        collidedBox = box;
                        break;
                    }
                }

                if (!collision)
                {
                    Position = newPos;
                }
                else
                {
                    Position = new Vector2(collidedBox.Position.X + collidedBox.Dimensions.X + 1, Position.Y);
                }
            }
        }

        public void dashAttack(Map map) 
        {
            if(_direction == "right")
            {
                dashLeft(map);
                dashRight(map);
            }

            if(_direction == "left")
            {
                dashRight(map);
                dashLeft(map);
            }
        }
    }
}
