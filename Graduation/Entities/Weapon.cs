using Microsoft.Xna.Framework;
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
        public Vector2 NeutralPos; //weapon objects needs a neutral position, so the weapon won't interact after it stopped being drawn

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
            //Check for collision with an enemy
            foreach (Enemy enemy in enemies)
            {
                if (Util.weaponHitEnemy(this, enemy))
                {
                    Position = NeutralPos;
                    player.throwing = false;
                    enemy.Health -= Damage;
                }

                if(Util.weaponHitPlayer(this, player))
                {
                    Position = NeutralPos;
                    player.Health -= Damage;
                }
            }

            //Check for collision with any walls
            foreach (Box box in map.Boxes)
            {
                if (Util.weaponHitWall(this, box))
                {
                    player.throwing = false;
                    Position = NeutralPos;
                }
            }
        }

        public void playerAttack(GameTime gameTime, SpriteBatch spriteBatch, Player player)
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

                if (attackTimer > 500) //stops the weapon from drawing to the screen, even if it doesn't interact/hit anything
                {
                    player.throwing = false;
                    Position = NeutralPos;
                }
            }

            attackTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
        }

        public void bossRangedAttack(GameTime gameTime, SpriteBatch spriteBatch, BossLevelOne bossLevelOne)
        {
            if (bossLevelOne.attack)
            {
                _attackDirection = bossLevelOne._direction;
                attackTimer = 0;
                bossLevelOne.throwing = true;
                bossLevelOne.attack = false;

                Position.X = bossLevelOne.Position.X;
                Position.Y = bossLevelOne.Position.Y + (bossLevelOne.Dimensions.Y / 3);

            }
            else if (bossLevelOne.throwing)
            {
                Vector2 newPos = _attackDirection == "right" ? new Vector2(Position.X + Speed, Position.Y) :
                new Vector2(Position.X - Speed, Position.Y);
                Draw(spriteBatch, gameTime, newPos);

                if (attackTimer > 500) //stops the weapon from drawing to the screen, even if it doesn't interact/hit anything
                {
                    bossLevelOne.throwing = false;
                    Position = NeutralPos;
                }
            }

            attackTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
        }
    }
}
