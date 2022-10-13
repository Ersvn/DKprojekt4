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
    public class Player
    {
        public Vector2 position;
        public Texture2D texture;
        public Vector2 velocity;
        Vector2 destination;
        Vector2 direction;
        float speed = 100.0f;
        bool moving = false;
        bool hasJumped;


        public Player(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
            hasJumped = true;
        }

        public void Update(GameTime gameTime)
        {
            //If we're not already moving,pick a new direction and check if we can 
            //move in that direction
            //Otherwise,move towards the destination
            //if (!moving)
            //{
            position += velocity;

                if (Keyboard.GetState().IsKeyDown(Keys.D)) velocity.X = 3f;
                else if (Keyboard.GetState().IsKeyDown(Keys.A)) velocity.X = -3f; else velocity.X = 0f;

                if (Keyboard.GetState().IsKeyDown(Keys.W) && hasJumped == false)
                {
                    position.Y -= 10f;
                    velocity.Y = -5f;
                    hasJumped = true;
                }
                if (hasJumped == true)
                {
                    float i = 1;
                    velocity.Y += 0.23f * i;

                }
                if (position.Y + texture.Height >= 450)
                    hasJumped = false;

                if (hasJumped == false)
                {
                    velocity.Y = 0f;
                }
        
            if(moving == false)
            {
                position += direction * speed *
                (float)gameTime.ElapsedGameTime.TotalSeconds;



                if (Vector2.Distance(position, destination) < 1)
                {
                    position = destination;
                    moving = false;
                }
            }

            if (position.X <= 50) //<---|
                position.X = 50;//<---|

            if (position.X >= 550 - texture.Width) //|--->
                position.X = 550 - texture.Width; //|--->

            if (position.Y <= -20)
                position.Y = -20;

            if (position.Y >= 450 - texture.Height)
                position.Y = 450 - texture.Height;

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
        public void ChangeDirection(Vector2 dir)
        {
            direction = dir;
            Vector2 newDestination = position + direction * 50.0f;

            //Check if we can move in the desired direction, if not, do nothing
            if (!Game1.GetTileAtPosition(newDestination))
            {

                destination = newDestination;
                moving = true;
            }
        }
    }
}


//    Console.WriteLine(Keyboard.GetState().IsKeyDown(Keys.Left) + "");
//    if (Keyboard.GetState().IsKeyDown(Keys.A))
//    {
//        ChangeDirection(new Vector2(-1, 0));
//    }
//    else if (Keyboard.GetState().IsKeyDown(Keys.D))
//    {
//        ChangeDirection(new Vector2(1, 0));
//    }
//    else if (Keyboard.GetState().IsKeyDown(Keys.W))
//    {
//        ChangeDirection(new Vector2(0, -1));
//    }
//    if (Keyboard.GetState().IsKeyDown(Keys.W) && hasJumped == false)
//    {
//        position.Y -= 10f;
//        velocity.Y = -5f;
//        hasJumped = true;
//    }
//    if (hasJumped == true)
//    {
//        float i = 1;
//        velocity.Y += 0.23f * i;

//    }
//    if (position.Y + texture.Height >= 450)
//        hasJumped = false;

//    if (hasJumped == false)
//    {
//        velocity.Y = 0f;
//    }
//    else if (Keyboard.GetState().IsKeyDown(Keys.S))
//    {
//        ChangeDirection(new Vector2(0, 1));
//    }
//}
//else