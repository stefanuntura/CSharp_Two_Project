﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Text;
using Graduation.Entities;
using Graduation.TestMap;

namespace Graduation
{
    public abstract class Weapon : DrawableGameComponent
    {
        public double Cooldown;
        public double attackTimer;
    
        public Texture2D device;
        public Vector2 Position;
        public Vector2 Dimension;

        public int Speed;
        public double Damage;
        public Vector2 NeutralPos;

        String _attackDirection = "right";
       
        public Weapon(Game game, Vector2 dimension) : base(game)
        {
            Dimension = dimension;
            this.LoadContent(game);
            NeutralPos = new Vector2(0, 0);
        }

        public virtual void LoadContent(Game game)
        {
            device = game.Content.Load<Texture2D>("Weapon/Laptop");
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, Vector2 position)
        {
            Position = position;
            spriteBatch.Draw(device, new Rectangle((int)Position.X, (int)Position.Y, (int)Dimension.X, (int)Dimension.Y), Color.White);  
        }

        public void Update(GameTime gameTime, Player player, List<Enemy> enemies, Map map)
        {
            foreach (Enemy enemy in enemies)
            {
                if (Util.weaponHitEnemy(this, enemy))
                {
                    Position = NeutralPos;
                    player.throwing = false;
                    enemy.Health -= Damage;
                }
            }

            foreach (Box box in map.Boxes)
            {
                if (Util.weaponHitWall(this, box))
                {
                    player.throwing = false;
                    Position = NeutralPos;
                }
            }
        }

        public void attack(GameTime gameTime, SpriteBatch spriteBatch, Player player)
        {
            if (player.attack)
            {
                _attackDirection = player._direction;
                attackTimer = 0;
                player.throwing = true;
                player.attack = false;

                Position.X = player.Position.X;
                Position.Y = player.Position.Y + (player.Dimensions.Y / 3);

            }
            else if (player.throwing)
            {

                Vector2 newPos = _attackDirection == "right" ? new Vector2(Position.X + Speed, Position.Y) :
                 new Vector2(Position.X - Speed, Position.Y);
                Draw(spriteBatch, gameTime, newPos);

                if (attackTimer > 500)
                {
                    player.throwing = false;
                    Position = NeutralPos;
                }
            }

            attackTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
        }
    }
}
