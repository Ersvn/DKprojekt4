using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Övningar4
{
    public class Tile
    {
        public Vector2 position;
        public Texture2D texture;
        public bool wall;

        public Tile(Texture2D texture, Vector2 position, bool wall)
        {
            this.texture = texture;
            this.position = position;
            this.wall = wall;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
