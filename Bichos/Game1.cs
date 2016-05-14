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
        Models models;
        Vector2 exit;
        Combate combate = new Combate();

        char[,] board;

        int size = 64;

        int width, height;

        int level = 2;
        
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
            models = new Models();

            models.CarregaModels(Content);

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

            Vector2 movement = Vector2.Zero;
            KeyboardState keys = Keyboard.GetState();

            

                if (!cj.isMoving())
            {
                if (keys.IsKeyDown(Keys.Down) && cj.Position().Y != height-1)

                    movement = Vector2.UnitY;


                else if (keys.IsKeyDown(Keys.Up) && cj.Position().Y != 0)

                    movement = -Vector2.UnitY;


                else if (keys.IsKeyDown(Keys.Left) && cj.Position().X != 0)

                    movement = -Vector2.UnitX;


                else if (keys.IsKeyDown(Keys.Right) && cj.Position().X != width - 1)

                    movement = Vector2.UnitX;


                if (keys.IsKeyDown(Keys.E))
                {
                    IsLevel(cj.Position() + movement);


                }

            }

            if (keys.IsKeyDown(Keys.R))
                loadLevel();
            
            
            if (movement != Vector2.Zero)
            {
                if (Nobejetos(cj.Position() + movement))
                    cj.Move(movement);
            }
            cj.Update(gameTime);

            if (cj.Position() == exit)
            {
                level = 1;
                loadLevel();

                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        if (board[x, y] == '2')
                        {
                            cj.position = new Vector2(x, y+1);
                        }
                    }
                }

                
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
                        case 'c':
                            spriteBatch.Draw(models.cama, new Vector2(x * size, y * size), Color.White);
                            break;
                        case 'l':
                            spriteBatch.Draw(models.livros, new Vector2(x * size, y * size), Color.White);
                            break;
                        case '2':
                            spriteBatch.Draw(models.casa, new Vector2(x * size, y * size), Color.White);
                            break;
                        case '3':
                            spriteBatch.Draw(models.hosp, new Vector2(x * size, y * size), Color.White);
                            break;
                        case 'm':
                            spriteBatch.Draw(models.mesa, new Vector2(x * size, y * size), Color.White);
                            break;
                        case 'p':
                            spriteBatch.Draw(models.pc, new Vector2(x * size, y * size), Color.White);
                            break;
                        case 's':
                            spriteBatch.Draw(models.sofa, new Vector2(x * size, y * size), Color.White);
                            break;
                        default:
                            break;
                    }
                }
            }
            cj.Draw(spriteBatch);
            

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
                    if (board[x, y] == '1')
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


        bool Nobejetos(Vector2 pos)
        {
            if ((board[(int)pos.X, (int)pos.Y] == 'p') || (board[(int)pos.X, (int)pos.Y] == 'l') || (board[(int)pos.X, (int)pos.Y] == 's') || (board[(int)pos.X, (int)pos.Y] == ' '))
                return true;
            else return false;
        }

        void IsLevel(Vector2 vetor)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (new Vector2 (x, y) == vetor)
                    {
                        if (board[x, y] == '1')
                        {
                            level = 1;
                            loadLevel();
                        }
                        else if (board[x, y] == '2')
                        {
                            level = 2;
                            loadLevel();
                        }
                    }
                }
            }
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





