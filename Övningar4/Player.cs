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
    public class Player
    {
        public Vector2 position;
        Vector2 velocity;
        Vector2 destination;
        Vector2 direction;
        Vector2 origin;
        public Texture2D texture, runTexture, fireballTexture;
        public Texture2D runningTex;
        public Rectangle boundingBox; 
        float speed = 450.0f;
        static bool moving = false;
        public bool isColliding;
        public float fireballDelay;
        
        public float bulletDelay;
        static bool shooting = false;


        public Player(Texture2D texture, Texture2D runTexture, Vector2 position)
        {
            
            this.texture = texture;
            this.runTexture = runTexture;
            this.position = position;
            bulletDelay = 20;
            
        }
        
        public void LoadContent(ContentManager Content)
        {
            fireballTexture = Content.Load<Texture2D>("fireball");
        }
        public void Update(GameTime gameTime)
        {
            

            if (!moving)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.A))
                {
                    ChangeDirection(new Vector2(-1, 0));
                }
                if (Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    ChangeDirection(new Vector2(1, 0));
                }

                if (Keyboard.GetState().IsKeyDown(Keys.S))
                {
                    ChangeDirection(new Vector2(0, 1));
                }

                if (Keyboard.GetState().IsKeyDown(Keys.W))
                {
                    ChangeDirection(new Vector2(0, -1));
                }
                
            }

            else
            {
                position += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds / MathHelper.Pi;
                //Check if we are near enough to the destination
                if (Vector2.Distance(position, destination) < 1)
                {
                    position = destination;
                    moving = false;
                }
            }
            //if (Keyboard.GetState().IsKeyDown(Keys.Space))
            //{
            //    Shoot();
            //}

            //UpdateFireballs();  
            //Fireball//



                //if (position.X <= 1) //<---|
                //    position.X = 1;//<---|

                //if (position.X >= 900 - texture.Width) //|--->
                //    position.X = 900 - texture.Width; //|--->

                //if (position.Y <= -12)
                //    position.Y = -12;

                //if (position.Y >= 660 - texture.Height)
                //    position.Y = 660 - texture.Height;


        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //if (!moving) spriteBatch.Draw(texture, position, Color.White);
            //if (moving) spriteBatch.Draw(runTexture, position, Color.White);
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                spriteBatch.Draw(runTexture, position, Color.White);
            }
            //if (Keyboard.GetState().IsKeyDown(Keys.D))
            //{
            //    spriteBatch.Draw(runningTex, position, Color.White);
            //}
            else
            {
                spriteBatch.Draw(texture, position, Color.White);
                
            }
        }

       

        public void ChangeDirection(Vector2 dir)
        {
            direction = dir;
            Vector2 newDestination = position + direction * 40.0f;

            if (!Game1.GetTileAtPosition(newDestination))
            {
                destination = newDestination;
                moving = true;
            }
        }

        //public void Shoot()
        //{
        //    if (bulletDelay >= 0)
        //        bulletDelay--;

        //    if (bulletDelay <= 0)
        //    {
        //        Fireball newFireball = new Fireball(fireballTexture);
        //        newFireball.position = new Vector2(origin.X - newFireball.texture.Width / 2, origin.Y);

        //        newFireball.isVisible = true;

        //        if (fireballList.Count() < 20)
        //            fireballList.Add(newFireball);
        //    }
        //    if (bulletDelay == 0)
        //        bulletDelay = 20;
        //}

        //public void UpdateFireballs()
        //{
        //    foreach (Fireball f in fireballList)
        //    {
        //        f.position.X = f.position.X - f.speed;

        //        if (f.position.X <= 1200)
        //            f.isVisible = false;
        //    }
        //    for (int i = 0; i < fireballList.Count; i++)
        //    {
        //        if (!fireballList[i].isVisible)
        //        {
        //            fireballList.RemoveAt(i);
        //            i--;
        //        }
        //    }

        //}
        //public void JumpDirection(Vector2 vel)
        //{

        //}
    }
}
