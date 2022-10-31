using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using Microsoft.Xna.Framework.Content;
using System.Security.Cryptography.X509Certificates;
using System.Diagnostics.Contracts;

namespace Övningar4
{
    public class Princess
    {
        public Vector2 position;
        public Texture2D texture;
        public Rectangle Rectangle;
        public Princess(Texture2D texture, Vector2 position, Rectangle rectangle)
        {
            this.texture = texture;
            this.position = position;
            this.Rectangle = rectangle;
        }

        public void Update(GameTime gameTime)
        {
            Rectangle = new Rectangle((int)(position.X), (int)(position.Y), (texture.Width / 2), (texture.Height / 2));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

    }
}
