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

        public Map()
        {
            Boxes = new List<Box>();
        }

        public Map(List<Box> boxes)
        {
            Boxes = boxes;
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

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (Box box in Boxes)
            {
                box.Draw(spriteBatch, gameTime);
            }
        }
    }
}