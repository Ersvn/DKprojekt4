using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.IO;
using static System.Net.Mime.MediaTypeNames;


namespace Övningar4
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private string text;
        SpriteFont font;
        bool start;
        List<string> strings = new List<string>();
        //Ball[,] balls;
        Ball ball;
        Player player;
        static Tile[,] tiles;
        Texture2D texture;
        Texture2D ballTex;
        Texture2D wallTileTex;
        Texture2D floorTileTex;
        Vector2 position;
        KeyboardState keyState;
        static int tileSize;
        enum GameState { LoadingScreen, StartScreen, PlayState, GameOver, } GameState gState = GameState.LoadingScreen;
        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            tileSize = 50;

        }
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>(@"Font");
            wallTileTex = Content.Load<Texture2D>("walltile");
            floorTileTex = Content.Load<Texture2D>("floortile");
            ballTex = Content.Load<Texture2D>("ball");
            texture = Content.Load<Texture2D>("ball");

          
            StreamReader sr = new StreamReader("map.txt");

            //StreamReader sr = new StreamReader(@"MyText.txt");
            text = sr.ReadLine();

            while (!sr.EndOfStream)
            {
                strings.Add(sr.ReadLine());
            }
            sr.Close();

            tiles = new Tile[strings[0].Length, strings.Count];

            for (int i = 0; i < tiles.GetLength(0); i++)
            {

                for (int j = 0; j < tiles.GetLength(1); j++)
                {

                    if (strings[j][i] == 'w')
                    {
                        tiles[i, j] = new Tile(wallTileTex, new Vector2(wallTileTex.Width * i, wallTileTex.Height * j), true);
                    }

                    else if (strings[j][i] == '-')
                    {
                        tiles[i, j] = new Tile(floorTileTex, new Vector2(floorTileTex.Width * i, floorTileTex.Height * j), false);
                    }

                    else if (strings[j][i] == 'b')
                    {
                        tiles[i, j] = new Tile(floorTileTex, new Vector2(floorTileTex.Width * i, floorTileTex.Height * j), false);
                        //ball = new Ball(ballTex, new Vector2(floorTileTex.Width * i, floorTileTex.Height * j));
                        player = new Player(texture, new Vector2(texture.Width * i, texture.Height * j));
                    }
                }
            }
        }
        protected override void Update(GameTime gameTime)
        {
            keyState = Keyboard.GetState();

            
            //ball.Update(gameTime);
            player.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            spriteBatch.DrawString(font, text, new Vector2(100, 100), Color.Black);
            
            for (int i = 0; i < strings.Count; i++)
            {
                for(int j = 0; j < strings[i].Length; j++)
                {
                    //if (strings[i][j] == 'w')
                    //{
                    //    spriteBatch.Draw(wallTileTex, new Vector2(tileSize * i, tileSize * j), Color.White);
                    //}

                    //else if (strings[i][j] == '-')
                    //{
                    //    spriteBatch.Draw(floorTileTex, new Vector2(tileSize * i, tileSize * j), Color.White);
                    //}
                    //else if (strings[i][j] == 'b')
                    //{
                    //    spriteBatch.Draw(floorTileTex, new Vector2(tileSize * i, tileSize * j), Color.White);
                    //    ball.Draw(spriteBatch);
                    //}

                    if (strings[i][j] == 'w')
                    {
                        spriteBatch.Draw(wallTileTex, new Vector2(tileSize * j, tileSize * i), Color.White);
                    }

                    else if (strings[i][j] == '-')
                    {
                        spriteBatch.Draw(floorTileTex, new Vector2(tileSize * j, tileSize * i), Color.White);
                    }
                    else if (strings[i][j] == 'b')
                    {
                        spriteBatch.Draw(floorTileTex, new Vector2(tileSize * j, tileSize * i), Color.White);
                        //ball.Draw(spriteBatch);
                    }
                }
            }
            player.Draw(spriteBatch);
            //ball.Draw(spriteBatch);
           /* for (int i = 0; i < strings.Count; i++)
            {
                for (int j = 0; j < strings[i].Length; j++)
                {
                    
                    if (strings[i][j] == 'b')
                    {
                        ball.Draw(spriteBatch);
                    }
                }
            }*/

            //ball.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
        public static bool GetTileAtPosition(Vector2 vec)
        {
            return tiles[(int)vec.X / tileSize, (int)vec.Y / tileSize].wall;
        }
    }
}
