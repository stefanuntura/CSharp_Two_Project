using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

using Graduation.Entities;

namespace Graduation.States
{
    public class GameState : State
    {
        private Camera _camera;
        private Player _player;
        private List<Enemy> _enemies;
        public List<Item> Items;
        private BossLevelOne _bossLevelOne;
        private TestMap.Map _map;
        private double _counter = 0;
        public GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager contentManager) : base(game, graphicsDevice, contentManager)
        {
            //Initialize map
            _map = new TestMap.Map();

            _camera = new Camera();
            _player = new Player(game, new Vector2(1, 400));
            _bossLevelOne = new BossLevelOne(game, new Vector2(3000, -400));
            _enemies = new List<Enemy>();
            Items = new List<Item>();

            // comment this in to generate map, commented out to not screw with test map
            _map.Generate(game, new int[,] {
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
                {0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
                {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,},
                {0,1,0,0,1,0,0,1,0,0,0,0,0,1,0,0,0,0,0,0,0,0,1,0,1,0,1,0,0,0,0,0,0,1,0,1,1,0,0,0,0,0,0,0,},
                {0,0,0,1,1,0,0,1,1,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,},
                {1,1,1,1,1,0,0,1,1,1,1,1,1,0,3,4,3,3,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,0,0,0,0,0,0,1,1,1,1,},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
                {0,0,0,0,0,0,0,0,0,0,4,4,0,0,0,0,2,0,0,0,2,0,0,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
            }, 32);


            _map.addBox(new TestMap.Box(game, new Vector2(800, 100), new Vector2(0, 440), Color.DarkSlateGray));
            _map.addBox(new TestMap.Box(game, new Vector2(200, 700), new Vector2(780, 0), Color.DarkSlateGray));
            _map.addBox(new TestMap.Box(game, new Vector2(40, 400), new Vector2(740, 380), Color.DarkSlateGray));
            _map.addBox(new TestMap.Box(game, new Vector2(50, 400), new Vector2(690, 405), Color.DarkSlateGray));
            
            //Add walker quadrant one left area
            _enemies.Add(new Walker(game, new Vector2(100, 400)));

            //Add walkers quadrant one right area
            _enemies.Add(new Walker(game, new Vector2(1000, 400)));
            _enemies.Add(new Walker(game, new Vector2(1200, 400)));

            //Add walkers quadrant two
            _enemies.Add(new Walker(game, new Vector2(2500, -100)));

            //Add item box quadrant one
            Items.Add(new Item(game, new Vector2(300, 412), new Vector2(25, 26)));

            //Add item box lootbox area quadrant two
            Items.Add(new Item(game, new Vector2(2150, -80), new Vector2(25, 26)));

            //Quadrant One
            //Add floor quadrant one
            _map.addBox(new TestMap.Box(game, new Vector2(1500, 100), new Vector2(0, 440), Color.DarkSlateGray));

            //Add wall on right and left quadrant one
            _map.addBox(new TestMap.Box(game, new Vector2(200, 700), new Vector2(1500, 0), Color.DarkSlateGray));
            _map.addBox(new TestMap.Box(game, new Vector2(200, 540), new Vector2(-200, 0), Color.DarkSlateGray));

            //Add ceiling quadrant one
            _map.addBox(new TestMap.Box(game, new Vector2(1000, 100), new Vector2(0, 0), Color.DarkSlateGray));

            //Add vertical obstacle quadrant one
            _map.addBox(new TestMap.Box(game, new Vector2(200, 300), new Vector2(900, 0), Color.DarkSlateGray));

            //Add horizontal obstacle qudrant one
            _map.addBox(new TestMap.Box(game, new Vector2(40, 100), new Vector2(740, 380), Color.DarkSlateGray));
            _map.addBox(new TestMap.Box(game, new Vector2(50, 100), new Vector2(690, 405), Color.DarkSlateGray));

            //Add platforms in left area of quadrant one
            _map.addBox(new TestMap.Box(game, new Vector2(50, 15), new Vector2(100, 300), Color.DarkGray));
            _map.addBox(new TestMap.Box(game, new Vector2(50, 15), new Vector2(300, 350), Color.DarkGray));
            _map.addBox(new TestMap.Box(game, new Vector2(50, 15), new Vector2(200, 380), Color.DarkGray));
            _map.addBox(new TestMap.Box(game, new Vector2(60, 15), new Vector2(440, 400), Color.DarkGray));

            //Spikes
            _map.addBox(new TestMap.Spike(game, new Vector2(25, 25), new Vector2(615, 415), Color.Red));
            _map.addBox(new TestMap.Spike(game, new Vector2(25, 25), new Vector2(640, 415), Color.Red));
            _map.addBox(new TestMap.Spike(game, new Vector2(25, 25), new Vector2(665, 415), Color.Red));

            //Add platforms in right area of quadrant one
            _map.addBox(new TestMap.Box(game, new Vector2(50, 15), new Vector2(1150, 380), Color.DarkGray));
            _map.addBox(new TestMap.Box(game, new Vector2(50, 15), new Vector2(1300, 200), Color.DarkGray));
            _map.addBox(new TestMap.Box(game, new Vector2(50, 15), new Vector2(1400, 350), Color.DarkGray));
            _map.addBox(new TestMap.Box(game, new Vector2(50, 15), new Vector2(1220, 280), Color.DarkGray));
            _map.addBox(new TestMap.Box(game, new Vector2(50, 15), new Vector2(1450, 150), Color.DarkGray));
            _map.addBox(new TestMap.Box(game, new Vector2(50, 15), new Vector2(1250, 110), Color.DarkGray));
            _map.addBox(new TestMap.Box(game, new Vector2(50, 15), new Vector2(1150, 60), Color.DarkGray));
            _map.addBox(new TestMap.Box(game, new Vector2(75, 15), new Vector2(1370, 50), Color.DarkGray));

            //Extra quadrant
            //Add wall on left in extra quadrant
            _map.addBox(new TestMap.Box(game, new Vector2(200, 300), new Vector2(-200, -300), Color.DarkSlateGray));

            //Add ceiling extra quadrant
            _map.addBox(new TestMap.Box(game, new Vector2(1500, 100), new Vector2(0, -300), Color.DarkSlateGray));


            //Quadrant Two
            //Add floor quadrant two
            _map.addBox(new TestMap.Box(game, new Vector2(1000, 100), new Vector2(1500, 100), Color.DarkSlateGray));

            //Add wall on left and right in quadrant two
            _map.addBox(new TestMap.Box(game, new Vector2(200, 500), new Vector2(1500, -700), Color.DarkSlateGray));
            _map.addBox(new TestMap.Box(game, new Vector2(200, 500), new Vector2(2500, -300), Color.DarkSlateGray));

            //Add platforms in quadrant two
            _map.addBox(new TestMap.Box(game, new Vector2(50, 15), new Vector2(1800, -70), Color.DarkGray));
            _map.addBox(new TestMap.Box(game, new Vector2(75, 15), new Vector2(2000, -105), Color.DarkGray));
            _map.addBox(new TestMap.Box(game, new Vector2(25, 15), new Vector2(2325, -125), Color.DarkGray));
            _map.addBox(new TestMap.Box(game, new Vector2(25, 15), new Vector2(2420, -140), Color.DarkGray));
            _map.addBox(new TestMap.Box(game, new Vector2(50, 15), new Vector2(2450, -205), Color.DarkGray));

            //Add lootbox area in quadrant two
            _map.addBox(new TestMap.Box(game, new Vector2(75, 25), new Vector2(2100, -120), Color.DarkSlateGray));
            _map.addBox(new TestMap.Box(game, new Vector2(25, 25), new Vector2(2100, -105), Color.DarkSlateGray));
            _map.addBox(new TestMap.Box(game, new Vector2(25, 25), new Vector2(2100, -90), Color.DarkSlateGray));
            _map.addBox(new TestMap.Box(game, new Vector2(25, 25), new Vector2(2100, -75), Color.DarkSlateGray));
            _map.addBox(new TestMap.Box(game, new Vector2(25, 25), new Vector2(2100, -60), Color.DarkSlateGray));
            _map.addBox(new TestMap.Box(game, new Vector2(25, 25), new Vector2(2100, -45), Color.DarkSlateGray));
            _map.addBox(new TestMap.Box(game, new Vector2(100, 25), new Vector2(2100, -30), Color.DarkSlateGray));

            //Quadrant Three
            //Add floor quadrant three
            _map.addBox(new TestMap.Box(game, new Vector2(1000, 100), new Vector2(2500, -300), Color.DarkSlateGray));

            //Add wall on right in quadrant three
            _map.addBox(new TestMap.Box(game, new Vector2(100, 500), new Vector2(3500, -700), Color.DarkSlateGray));


            _map.LoadContent(game);
        }

        public override void Draw(GameTime gameTime, SpriteBatch _spriteBatch)
        {   
            _spriteBatch.Begin(transformMatrix: _camera.Transform);

            _map.Draw(_spriteBatch, gameTime);

            _player.Draw(_spriteBatch, gameTime);

           /* foreach (Enemy enemy in _enemies)
            {
                enemy.Draw(_spriteBatch, gameTime);
            }*/

            _bossLevelOne.Draw(_spriteBatch, gameTime);

            _spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            if(_player.Health <= 0)
            {
                _counter += gameTime.ElapsedGameTime.TotalMilliseconds;
				
                if(_counter > 1000) { _game.ChangeState(new GameOverState(_game, _graphicsDevice, _contentManager)); }
            }
            _player.Update(gameTime, _map, this);
            _camera.Follow(_player);
            _player.weapon.Update(gameTime, _player, _map.Enemies, _map);

            _map.Update(gameTime, _player);
			_player.Update(gameTime, _map, this);
            _camera.Follow(_player);
            _bossLevelOne.Update(gameTime, _map);
        }
    }
}