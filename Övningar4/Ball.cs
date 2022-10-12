using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Color = Microsoft.Xna.Framework.Color;

namespace Övningar4
{
    public class Ball
    {
        public Vector2 position;
        public Texture2D ballTex;

        public Ball(Texture2D ballTex, Vector2 position)
        {
            this.ballTex = ballTex;
            this.position = position;
        }

        public void Update(GameTime gameTime)
        {

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ballTex, position, Color.White);
        }
    }
}
