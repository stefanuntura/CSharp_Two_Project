using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Graduation.TestMap
{
    public class Map
    {
        public List<Box> Boxes { get; set; }
        private List<CollisionTiles> collisionTiles = new List<CollisionTiles>();
        private int width, height;
        public Map()
        {
            Boxes = new List<Box>();
        }

        public Map(List<Box> boxes)
        {
            Boxes = boxes;
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
        }

        //Draw for map generation
        public void Draw(SpriteBatch spriteBatch) 
        {
            foreach (CollisionTiles tile in collisionTiles) 
            {
                tile.Draw(spriteBatch);
            }
        }
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (Box box in Boxes)
            {
                box.Draw(spriteBatch, gameTime);
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
                    if (number > 0)
                        Boxes.Add(new Box(game, new Vector2(x * size, y * size), new Vector2(size, size), Color.Black));

                    width = (x + 1) * size;
                    height = (y + 1) * size;
                }
            }
        }
    }
}