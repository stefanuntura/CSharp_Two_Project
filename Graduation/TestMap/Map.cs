using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Graduation.Entities;
using System.Diagnostics;

namespace Graduation.TestMap
{
    public class Map
    {
        public List<Box> Boxes { get; set; }
        public List<Enemy> Enemies;
        public List<Item> Items;
        private List<CollisionTiles> collisionTiles = new List<CollisionTiles>();
        private int width, height;
        public Map()
        {
            Boxes = new List<Box>();
            Items = new List<Item>();
            Enemies = new List<Enemy>();
        }

        /*public Map(List<Box> boxes)
        {
            Boxes = boxes;
        }*/

        public void Update(GameTime gameTime, Player player)
        {
            foreach (Enemy enemy in Enemies)
            {
                enemy.Update(gameTime, player, this);
                if (enemy.Health <= 0)
                {
                    Enemies.Remove(enemy);
                    break;
                }
            }

            Debug.WriteLine(player.Position);
        }

        private List<CollisionTiles> CollisionTiles
        {
            get { return collisionTiles; }
        }

        public int Width 
        {
            get { return width; }
        }

        public int Heihght
        {
            get { return height; }
        }

        public void addBox(Box box)
        {
            Boxes.Add(box);
        }

        public void LoadContent(Game game)
        {
            foreach (Box box in Boxes)
            {
                box.LoadContent(game);
            }
            /*foreach (Enemy enemy in Enemies)
            {
                enemy.LoadContent(game);
            }*/
            foreach (Item item in Items)
            {
                item.LoadContent(game);
            }
                
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (Box box in Boxes)
            {
                box.Draw(spriteBatch, gameTime);
            }
            foreach (Enemy enemy in Enemies)
            {
                enemy.Draw(spriteBatch, gameTime);
            }    
            foreach (Item item in Items)
            {
                item.Draw(spriteBatch);
            }
                
        }

        public void Generate(Game game, int[,] map, int size) 
        {
            for (int x = 0; x < map.GetLength(1); x++) 
            {
                for (int y = 0; y < map.GetLength(0); y++) 
                {
                    int number = map[y, x];

                    //if number number = 0, blank space
                    if (number == 1) { Boxes.Add(new Box(game, new Vector2(size, size), new Vector2((x * size), (y * size)), Color.Black)); }
                    else if (number == 2) { Boxes.Add(new Spike(game, new Vector2(35,35), new Vector2((x * size), (y * size)), Color.Red)); }
                    else if (number == 3) { Enemies.Add(new Walker(game, new Vector2((x * size), (y * size)))); }
                    else if (number == 4) { Items.Add(new Item(game, new Vector2((x * size), (y * size)), new Vector2(26, 25))); }
                    else if (number == 5) { Enemies.Add(new BossLevelOne(game, new Vector2((x * size), ((y * size)-20)))); }  
                    else if (number == 6) { Enemies.Add(new Robot1(game, new Vector2((x * size), (y * size)))); }
                    width = (x + 1) * size;
                    height = (y + 1) * size;
                }
            }
        }
        
    }
}