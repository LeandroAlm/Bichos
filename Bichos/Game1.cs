using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Bichos
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        CJ cj;
        Vector2 exit;
        Combate combate = new Combate();

        char[,] board;

        int size = 64;

        int width, height;

        int level = 2;

        bool msgon = false, inifala = false;

        Texture2D wall, box, pixel, inimigo;
        
        SpriteFont ComicSans;
        

        public Game1()
       {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }


        protected override void Initialize()
        {
            loadLevel();
            
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {

            spriteBatch = new SpriteBatch(GraphicsDevice);

            wall = Content.Load<Texture2D>("CrateDark_Gray");

            box = Content.Load<Texture2D>("Box");

            inimigo = Content.Load<Texture2D>("inimigo");

            pixel = Content.Load<Texture2D>("Ground_Sand");

            ComicSans = Content.Load<SpriteFont>("ComicSansMS_20");


        }


        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Vector2 movement = Vector2.Zero;
            KeyboardState keys = Keyboard.GetState();

            

                if (!cj.isMoving())
            {
                if (keys.IsKeyDown(Keys.Down))

                    movement = Vector2.UnitY;


                else if (keys.IsKeyDown(Keys.Up))

                    movement = -Vector2.UnitY;


                else if (keys.IsKeyDown(Keys.Left))

                    movement = -Vector2.UnitX;


                else if (keys.IsKeyDown(Keys.Right))

                    movement = Vector2.UnitX;


                if (keys.IsKeyDown(Keys.E))
                {
                    if (isCrate(cj.Position() + movement))
                        msgon = true;
                    if ((cj.Position() + movement) == positionIni())
                    {
                        inifala = true;
                        if (keys.IsKeyDown(Keys.Enter))
                        {
                            inifala = false;
                            combate.inicombate();
                        }
                    }


                }
                if (keys.IsKeyDown(Keys.Enter))
                {
                    msgon = false;

                }

            }

            if (keys.IsKeyDown(Keys.R))
                loadLevel();
            
            
            if (movement != Vector2.Zero)
            {
                if (!isCrate(cj.Position() + movement) && !isWall(cj.Position() + movement) && ((cj.Position() + movement != positionIni())))
                    cj.Move(movement);
            }
            cj.Update(gameTime);

            if (cj.Position() == exit)
            {
                level = 1;
                loadLevel();
            }

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            spriteBatch.Begin();
            

            // pintar chao
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    spriteBatch.Draw(pixel, new Vector2(x * size, y * size), Color.White);
                }
            }


                    // desenhar objetos
                    for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    switch (board[x, y])
                    {
                        case '#':
                            spriteBatch.Draw(wall, new Vector2(x * size, y * size), Color.White);
                            break;
                        case 'x':
                            spriteBatch.Draw(inimigo, new Vector2(x * size, y * size), Color.White);
                            break;
                        default:
                            break;
                    }
                }
            }
            cj.Draw(spriteBatch);
            
            if (msgon)
                spriteBatch.DrawString(ComicSans, "UNS TRABALHAM OUTROS VAO A NET", new Vector2(128, 192), Color.Black);
            if (inifala)
                spriteBatch.DrawString(ComicSans, "Quer combater?", new Vector2(128, 192), Color.Black);


            spriteBatch.End();
            base.Draw(gameTime);
        }

        static char[,] readLevel(string filename)
        {
            string[] lines = File.ReadAllLines(filename);
            int width = lines.Select(x => x.Length).Max();
            int height = lines.Length;

            char[,] board = new char[width, height];

            for (int line = 0; line < height; line++)
            {
                for (int c = 0; c < width; c++)
                {
                    board[c, line] = c < lines[line].Length ? lines[line][c] : ' ';
                }
            }
            return board;
        }

        Vector2 OutHome()
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (board[x, y] == '*')
                    {
                        board[x, y] = ' ';
                        return new Vector2(x, y);
                    }
                }
            }
            return Vector2.Zero;
        }

        Vector2 positionCj()
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (board[x, y] == '@')
                    {
                        board[x, y] = ' ';
                        return new Vector2(x, y);
                    }
                }
            }
            return Vector2.Zero;
        }

        Vector2 positionIni()
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (board[x, y] == 'x')
                    {
                        return new Vector2(x, y);
                    }
                }
            }
            return Vector2.Zero;
        }


        bool isCrate(Vector2 pos)
        {
            return board[(int)pos.X, (int)pos.Y] == '$';
        }

        bool isWall(Vector2 coord)
        {
            return board[(int)coord.X, (int)coord.Y] == '#';

        }

        void loadLevel()
        {
            board = readLevel(@"Content\level" + level + ".sok");
            width = board.GetLength(0);
            height = board.GetLength(1);
            cj = new CJ(Content, positionCj());
            graphics.PreferredBackBufferHeight = height * size + 30;
            graphics.PreferredBackBufferWidth = width * size;
            graphics.ApplyChanges();
            exit = OutHome();

        }
        
        
    }
}





