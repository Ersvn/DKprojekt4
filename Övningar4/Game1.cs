using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using SharpDX.MediaFoundation;
using System;
using System.Collections.Generic;
using System.IO;



namespace Övningar4
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        KeyboardState keyState;
        private string text;
        SpriteFont font;
        List<string> strings = new List<string>();        
        
        Player player;
        static Tile[,] tiles;
        Princess princess;
        Texture2D princessTex;
        //Enemies
        Enemy enemy;
        public List<Enemy> enemyList;
        Texture2D enemyTex;
        // Texture2D playerTex1;
        // Texture2D playerTex2;
        Texture2D runningTex;
        Texture2D ballTex;
        Texture2D runTexture;
        Texture2D wallTileTex;
        Texture2D floorTileTex;
        Texture2D bridgeTex;
        Texture2D bridgeLadderTex;
        Texture2D ladderTex;
        Vector2 position;
        Vector2 velocity;
        static int tileSize;
        //int timer;
        Texture2D fireballTex;
        public static bool isVisible;
        public float speed;
        int lives;
        /*
         * Gör om mappen till gridsystem och bestäm vektor.
         */


        enum GameState { Input, Start, Play, GameOver, } GameState gState;
        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1200;
            graphics.PreferredBackBufferHeight = 800;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            graphics.IsFullScreen = false;
            tileSize = 40;
            this.Window.Title = "Donkey Kong";
            lives = 5;
            isVisible = false;

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
            ballTex = Content.Load<Texture2D>("SuperMarioFront");
            runTexture = Content.Load<Texture2D>("SuperMarioBack");
           // runningTex = Content.Load<Texture2D>("mario-pauline");
            StreamReader sr = new StreamReader("map.txt");
            bridgeTex = Content.Load<Texture2D>("bridge");
            ladderTex = Content.Load <Texture2D>("ladder");
            enemyTex = Content.Load<Texture2D>("enemy");
            enemyList = new List<Enemy>();
            princessTex = Content.Load<Texture2D>("pauline");
            //enemy = new Enemy(Content.Load < Texture2D > ("enemy"), new Vector2(400, 400), 150);
            
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
                        tiles[i, j] = new Tile(wallTileTex, new Vector2(tileSize * j, tileSize * i), true);
                    }
                    if (strings[j][i] == '-')
                    {
                        tiles[i, j] = new Tile(floorTileTex, new Vector2(tileSize * j, tileSize * i), true);
                    }
                    if (strings[j][i] == '+')
                    {
                        tiles[i, j] = new Tile(floorTileTex, new Vector2(tileSize * j, tileSize * i), false);
                    }
                    if (strings[j][i] == 'l')
                    {
                        tiles[i, j] = new Tile(ladderTex, new Vector2(tileSize * j, tileSize * i), false);
                    }   
                    if (strings[j][i] == 'r')
                    {
                        tiles[i, j] = new Tile(bridgeTex, new Vector2(tileSize * j, tileSize * i), true);
                    }
                    if (strings[j][i] == 'b')
                    {
                        tiles[i, j] = new Tile(floorTileTex, new  Vector2(tileSize * j, tileSize * i), false);
                        player = new Player(ballTex, runTexture, new Vector2(tileSize * i, tileSize * j));
                    }
                    if (strings[j][i] == 'e')
                    {
                        tiles[i, j] = new Tile(floorTileTex, new Vector2(tileSize * j, tileSize * i), false);
                        
                        enemy = new Enemy(enemyTex, new Vector2(tileSize * i, tileSize * j));
                        enemyList.Add(enemy);
                    }
                    if (strings[j][i] == 'p')
                    {
                        tiles[i, j] = new Tile(princessTex, new Vector2(tileSize * j, tileSize * i), false);
                    }

                }
            }
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)) Exit();

            keyState = Keyboard.GetState();
            switch (gState)
            {
                case GameState.Input:
                    if (keyState.IsKeyDown(Keys.Enter))
                    {
                        gState = GameState.Play;
                    }
                    break;

                case GameState.Play:
                    //timer--;
                    {
                        player.Update(gameTime);
                        foreach (Enemy enemy in enemyList)
                        {
                            enemy.Update(gameTime);
                            if (player.boundingBox.Contains(enemy.enemyRec.X, enemy.enemyRec.Y))
                            {
                                lives--;
                            }
                        }

                        
                    }

                    break;
                case GameState.GameOver:
                    {
                        //.
                    }
                    break;
            }            

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            for (int i = 0; i < strings.Count; i++)
            {
                for(int j = 0; j < strings[i].Length; j++)
                {
                    if (strings[i][j] == 'w')
                    {
                        spriteBatch.Draw(wallTileTex, new Vector2(tileSize * j, tileSize * i), Color.DarkViolet);
                    }

                    else if (strings[i][j] == '-')
                    {
                        spriteBatch.Draw(floorTileTex, new Vector2(tileSize * j, tileSize * i), Color.DarkViolet);
                    }

                    else if (strings[i][j] == '+')
                    {
                        spriteBatch.Draw(floorTileTex, new Vector2(tileSize * j, tileSize * i), Color.DarkViolet);
                    }
                    else if (strings[i][j] == 'r')
                    {
                        spriteBatch.Draw(bridgeTex, new Vector2(tileSize * j, tileSize * i), Color.BlueViolet);
                    }

                    else if (strings[i][j] == 'b')
                    {
                        spriteBatch.Draw(floorTileTex, new Vector2(tileSize * j, tileSize * i), Color.DarkViolet);
                    }
                    else if (strings[i][j] == 'l')
                    {
                        spriteBatch.Draw(ladderTex, new Vector2(tileSize * j, tileSize * i), Color.YellowGreen);
                    }
                    else if (strings[i][j] == 'e')
                    {
                        
                        spriteBatch.Draw(floorTileTex, new Vector2(tileSize * j, tileSize * i), Color.DarkViolet);
                        //spriteBatch.Draw(enemyTex, new Vector2(tileSize * j, tileSize * i), Color.Red);
                        enemy.Draw(spriteBatch);
                    }
                    else if (strings[i][j] == 'p')
                    {
                        spriteBatch.Draw(floorTileTex, new Vector2(tileSize * j, tileSize * i), Color.DarkViolet);
                        spriteBatch.Draw(princessTex, new Vector2(tileSize * j, tileSize * i), Color.White);
                    }
                } 
            }
            foreach (Enemy enemy in enemyList)
            {
                enemy.Draw(spriteBatch);
            }

            player.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
        


        public static bool GetTileAtPosition(Vector2 vec)
        {
            return tiles[(int)vec.X / tileSize, (int)vec.Y / tileSize].wall;
        }
        //public static bool GetLadderAtPosition(Vector2 ladderVec)
        //{
        //    return tiles[(int)ladderVec.X / 64, (int)ladderVec.Y / 64].ladder;
        //}
        //public static bool GetLadderAtPositionInvisible(Vector2 ladderVec)
        //{
        //    return tiles[(int)ladderVec.X / 64, (int)ladderVec.Y / 64].invisible;
        //}
        //public void UpdateEnemy()
        //{
        //    foreach (Enemy e in enemyList)
        //    {
        //        e.position.X = e.position.X - e.speed;

        //        if (e.position.X <= 1200)
        //            e.isVisible = false;
        //    }
        //    for (int i = 0; i < enemyList.Count; i++)
        //    {
        //        if (!enemyList[i].isVisible)
        //        {
        //            enemyList.RemoveAt(i);
        //            i--;
        //        }
        //    }

        //}
    }
}
