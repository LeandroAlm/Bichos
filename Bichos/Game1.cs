using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
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
        Falas fala;
        Models models;
        public Vector2 exit;

        BichosCj BichosDoCj = new BichosCj();
        Combate combate = new Combate();

        char[,] board;

        int size = 64;

        public int numero;

        int width, height;

        public int level = 2;

        Random r = new Random();

        public string tipo = "", tipoAdv ="", Ttipo = "", TtipoAdv = "";

        char aux1;

        public int contador = 0;

        public bool prep = false, prep1 = false, prep2 = false, EmCombate = false, EmCombate1 = false, inicio = true, EmCura = false, UsaPc = false, aux = false, torneio = false;

        Texture2D wall, pixel, fogo, agua, pedra, erva;
        
        SpriteFont ComicSans;
        

        public Game1()
       {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }


        protected override void Initialize()
        {
            loadLevel();
            BichosDoCj.adicionaTorneio();
            BichosDoCj.adicionaIni();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            fala = new Falas(Content, BichosDoCj ,this, cj);
            models = new Models();

            models.CarregaModels(Content);

            spriteBatch = new SpriteBatch(GraphicsDevice);

            wall = Content.Load<Texture2D>("CrateDark_Gray");

            pixel = Content.Load<Texture2D>("Ground_Sand");

            ComicSans = Content.Load<SpriteFont>("ComicSansMS_20");

            fogo = Content.Load<Texture2D>("fogo");
            agua = Content.Load<Texture2D>("agua");
            erva = Content.Load<Texture2D>("erva");
            pedra = Content.Load<Texture2D>("pedra");


        }


        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            Vector2 movement = Vector2.Zero;

            KeyboardState keys = Keyboard.GetState();



            if (!cj.isMoving() && !EmCombate && !inicio && !EmCura && !UsaPc && !torneio)
            {
                if (keys.IsKeyDown(Keys.Down) && cj.Position().Y != height - 1)
                {
                    movement = Vector2.UnitY;
                    if (level == 4)
                        numero = r.Next(1, 10);
                }

                else if (keys.IsKeyDown(Keys.Up) && cj.Position().Y != 0)
                {
                    movement = -Vector2.UnitY;
                    if (level == 4)
                        numero = r.Next(1, 10);
                }

                else if (keys.IsKeyDown(Keys.Left) && cj.Position().X != 0)
                {
                    movement = -Vector2.UnitX;
                    if (level == 4)
                        numero = r.Next(1, 10);
                }

                else if (keys.IsKeyDown(Keys.Right) && cj.Position().X != width - 1)
                {
                    movement = Vector2.UnitX;
                    if (level == 4)
                        numero = r.Next(1, 10);
                }

                if (keys.IsKeyDown(Keys.E) && level != 4)
                {
                    IsLevel(cj.Position() + movement);
                    if (board[(int)cj.Position().X, (int)cj.Position().Y] == '4')
                    {
                        level = 4;
                        loadLevel();
                    }

                    Vector2 next = cj.Position() + movement;

                    if (board[(int)next.X, (int)next.Y] == 'x' )
                    {
                        EmCombate = true;
                    }
                    if (board[(int)next.X, (int)next.Y] == 'f')
                    {
                        EmCombate = true;
                    }
                    if (board[(int)next.X, (int)next.Y] == 'a')
                    {
                        EmCombate1 = true;
                    }
                    if (board[(int)next.X, (int)next.Y] == 'b')
                    {
                        EmCombate1 = true;
                    }
                    if (board[(int)next.X, (int)next.Y] == '$')
                    {
                        EmCura = true;
                    }
                    // Entrar no torneio
                    if (board[(int)cj.Position().X, (int)cj.Position().Y] == '5')
                    {
                        torneio = true;
                    }

                    // USA PC
                    if (board[(int)cj.Position().X, (int)cj.Position().Y] == 'p')
                    {
                        UsaPc = true;
                    }

                }

                if (level == 4 && !prep2)
                {
                    if (board[(int)cj.Position().X, (int)cj.Position().Y] == 'x')
                    {
                        if (numero == 5)
                        {
                            aux1 = 'x';
                            numero = 0;
                            prep2 = true;
                        }
                        

                    }
                    else if (board[(int)cj.Position().X, (int)cj.Position().Y] == 'f')
                    {
                        if (numero == 5)
                        {
                            aux1 = 'f';
                            numero = 0;
                            prep2 = true;
                        }
                    }
                    else if (board[(int)cj.Position().X, (int)cj.Position().Y] == 'a')
                    {
                        if (numero == 5)
                        {
                            aux1 = 'a';
                            numero = 0;
                            prep2 = true;
                        }
                    }
                    else if (board[(int)cj.Position().X, (int)cj.Position().Y] == 'b')
                    {
                        if (numero == 5)
                        {
                            aux1 = 'b';
                            numero = 0;
                            prep2 = true;
                        }
                    }
                }
                
            }


            // PREPARA PARA LUTAR
            if (prep == true)
            {
                if (!cj.isMoving() && contador < 3)
                {
                    movement = -Vector2.UnitX;
                    cj.Move(movement);
                    contador++;
                }
                else if (!cj.isMoving() && contador == 3)
                {
                    movement = Vector2.UnitX;
                    cj.Move(movement);
                    prep = false;
                }
            }
            if (prep1 == true)
            {
                if (!cj.isMoving() && contador < 3)
                {
                    movement = Vector2.UnitX;
                    cj.Move(movement);
                    contador++;
                }
                else if (!cj.isMoving() && contador == 3)
                {
                    movement = -Vector2.UnitX;
                    cj.Move(movement);
                    prep1 = false;
                }
            }
            if (prep2)
            {
                aux = true;
                if (cj.Position().X > 3)
                {
                    if (!cj.isMoving() && contador < 2)
                    {
                        movement = -Vector2.UnitX;
                        cj.Move(movement);
                        contador++;
                    }
                    else if (!cj.isMoving() && contador == 2)
                    {
                        movement = Vector2.UnitX;
                        //contador = 3;
                        prep2 = false;
                    }

                }
                else
                {
                    if (!cj.isMoving() && contador < 2)
                    {
                        movement = Vector2.UnitX;
                        cj.Move(movement);
                        contador++;
                    }
                    else if (!cj.isMoving() && contador == 2)
                    {
                        movement = -Vector2.UnitX;
                        contador = 3;
                        prep2 = false;
                    }
                }
            }

            if (movement != Vector2.Zero)
            {
                if (Nobejetos(cj.Position() + movement))
                    cj.Move(movement);
            }
            cj.Update(gameTime);


            // sair de idificios para o MAPA
            if (cj.Position() == exit)
            {
                int oldLevel = level;
                level = 1;
                loadLevel();
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        if (board[x, y] == '0' + oldLevel)
                        {
                            if (oldLevel == 4)
                                cj.position = new Vector2(x, y);
                            else
                                cj.position = new Vector2(x, y + 1);
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


            if (inicio == true)
            {
                // DESENHAR MEDICO, MAS N DESENHA
                spriteBatch.Draw(models.medico, new Vector2((cj.Position().X)* size, (cj.Position().Y - 1) * size), Color.White);
                fala.Inicio(spriteBatch, ComicSans, height);

            }

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
                        case 'c':
                            spriteBatch.Draw(models.casaInt, new Vector2(x * size, y * size), Color.White);
                            break;
                        case 'z':
                            spriteBatch.Draw(models.jungler, new Vector2(x * size, y * size), Color.White);
                            break;
                        case 'h':
                            spriteBatch.Draw(models.hospital, new Vector2(x * size, y * size), Color.White);
                            break;
                        case 'm':
                            spriteBatch.Draw(models.mundo, new Vector2(x * size, y * size), Color.White);
                            break;
                        case 't':
                            spriteBatch.Draw(models.torneio, new Vector2(x * size, y * size), Color.White);
                            break;
                        default:
                            break;
                    }
                }
            }
            cj.Draw(spriteBatch);

            // DESENHAR BICHOS
            if (tipoAdv != "")
            {
                if (tipoAdv == "Drake")
                    spriteBatch.Draw(fogo, (cj.position + new Vector2(2, 0)) * 64, Color.White);
                else if (tipoAdv == "Rosaceae")
                    spriteBatch.Draw(erva, (cj.position + new Vector2(2, 0)) * 64, Color.White);
                else if (tipoAdv == "Fizz")
                    spriteBatch.Draw(agua, (cj.position - new Vector2(2, 0)) * 64, Color.White);
                else if (tipoAdv == "Basaltes")
                    spriteBatch.Draw(pedra, (cj.position - new Vector2(2, 0)) * 64, Color.White);

                if (tipoAdv == "Rosaceae" || tipoAdv == "Drake")
                {
                    if (tipo == "Rosaceae")
                        spriteBatch.Draw(erva, (cj.position + new Vector2(1, 0)) * 64, Color.White);
                    if (tipo == "Drake")
                        spriteBatch.Draw(fogo, (cj.position + new Vector2(1, 0)) * 64, Color.White);
                    if (tipo == "Fizz")
                        spriteBatch.Draw(agua, (cj.position + new Vector2(1, 0)) * 64, Color.White);
                    if (tipo == "Basaltes")
                        spriteBatch.Draw(pedra, (cj.position + new Vector2(1, 0)) * 64, Color.White);
                }
                else if (tipoAdv == "Basaltes" || tipoAdv == "Fizz")
                {
                    if (tipo == "Rosaceae")
                        spriteBatch.Draw(erva, (cj.position - new Vector2(1, 0)) * 64, Color.White);
                    if (tipo == "Drake")
                        spriteBatch.Draw(fogo, (cj.position - new Vector2(1, 0)) * 64, Color.White);
                    if (tipo == "Fizz")
                        spriteBatch.Draw(agua, (cj.position - new Vector2(1, 0)) * 64, Color.White);
                    if (tipo == "Basaltes")
                        spriteBatch.Draw(pedra, (cj.position - new Vector2(1, 0)) * 64, Color.White);
                }
            }

            //DESENHAR BICHOS NO TORNEIO
            if (Ttipo != "")
            {
                if (TtipoAdv == "Drake")
                    spriteBatch.Draw(fogo, (cj.position + new Vector2(5, 0)) * 64, Color.White);
                else if (TtipoAdv == "Rosaceae")
                    spriteBatch.Draw(erva, (cj.position + new Vector2(5, 0)) * 64, Color.White);
                else if (TtipoAdv == "Fizz")
                    spriteBatch.Draw(agua, (cj.position + new Vector2(5, 0)) * 64, Color.White);
                else if (TtipoAdv == "Basaltes")
                    spriteBatch.Draw(pedra, (cj.position + new Vector2(5, 0)) * 64, Color.White);

                // cj
                if (Ttipo == "Drake")
                    spriteBatch.Draw(fogo, (cj.position + new Vector2(2, 0)) * 64, Color.White);
                else if (Ttipo == "Rosaceae")
                    spriteBatch.Draw(erva, (cj.position + new Vector2(2, 0)) * 64, Color.White);
                else if (Ttipo == "Fizz")
                    spriteBatch.Draw(agua, (cj.position + new Vector2(2, 0)) * 64, Color.White);
                else if (Ttipo == "Basaltes")
                    spriteBatch.Draw(pedra, (cj.position + new Vector2(2, 0)) * 64, Color.White);
            }

            if (aux)
            {
                fala.Combate(spriteBatch, ComicSans, height, aux1, cj.position, true);
            }

            if (UsaPc)
            {
                //enviar o oponente, o caracter X pertence ao De Erva
                fala.NoPc(spriteBatch, ComicSans, height);
            }

            if (torneio)
            {
                fala.Torneio(spriteBatch, ComicSans, height);
            }

            if (EmCombate)
            {
                //enviar o oponente, FOGO E ERVA
                fala.Combate(spriteBatch, ComicSans, height, board[(int)(cj.Position().X + 1), (int)(cj.Position().Y)], cj.position, false);
            }
            if (EmCombate1)
            {
                //enviar o oponente, AGUA E PEDRA
                fala.Combate(spriteBatch, ComicSans, height, board[(int)(cj.Position().X - 1), (int)(cj.Position().Y)], cj.position, false);
            }

            if (EmCura)
            {
                fala.NoHospital(spriteBatch, ComicSans, height);
            }

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
            if (level != 4)
            {
                if ((board[(int)pos.X, (int)pos.Y] == '#')
                    || (board[(int)pos.X, (int)pos.Y] == '-') || (board[(int)pos.X, (int)pos.Y] == '2') || (board[(int)pos.X, (int)pos.Y] == '3')
                    || (board[(int)pos.X, (int)pos.Y] == 'x') || (board[(int)pos.X, (int)pos.Y] == '$') || (board[(int)pos.X, (int)pos.Y] == 'f')
                    || (board[(int)pos.X, (int)pos.Y] == 'a') || (board[(int)pos.X, (int)pos.Y] == 'b') || (board[(int)pos.X, (int)pos.Y] == 'm'))
                    return false;
                else return true;
            }
            else if (level == 4)
                if (board[(int)pos.X, (int)pos.Y] == '-')
                    return false;
                else return true;
            else
                return true;
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
                        else if (board[x, y] == '3')
                        {
                            level = 3;
                            loadLevel();
                        }
                    }
                }
            }
        }



        public void loadLevel()
        {
            board = readLevel(@"Content\level" + level + ".sok");
            width = board.GetLength(0);
            height = board.GetLength(1);
            cj = new CJ(Content, positionCj());
            graphics.PreferredBackBufferHeight = height * size + 80;
            graphics.PreferredBackBufferWidth = width * size;
            graphics.ApplyChanges();
            exit = OutHome();

        }
        
        
    }
}





