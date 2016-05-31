using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace Bichos
{
    class Falas
    {
        Game1 game;
        BichosCj BichosDoCj = new BichosCj();
        private ContentManager content;
        int cont = 1;
        bool upper = true;


        public Falas(ContentManager content, Game1 g)
        {
            this.content = content;
            game = g;
        }

        public void Combate (SpriteBatch spriteBatch, SpriteFont ComicSans, int height)
        {
            KeyboardState keys = Keyboard.GetState();
            string atacante = "";

            if (cont == 1)
            {
                spriteBatch.DrawString(ComicSans, "Ola, quer combater? (s/n)", new Vector2(10, height * 64 + 3), Color.Black);
                if (keys.IsKeyDown(Keys.S))
                {
                    game.prep = true;
                    game.contador = 0;
                    cont++;
                }
                else if (keys.IsKeyDown(Keys.N)) game.EmCombate = false;
            }
            else if (cont == 2)
            {
                int a = 1;
                foreach (var bicho in BichosDoCj.bichos)
                {
                    spriteBatch.DrawString(ComicSans, "Escolha o seu Bicho:", new Vector2(10, height * 64 + 3), Color.Black);
                    if (a == 1)
                        spriteBatch.DrawString(ComicSans, a + " - " + bicho.Key, new Vector2(0 + (10), height * 64 + 25), Color.Black);
                    if (a == 2)
                        spriteBatch.DrawString(ComicSans, a + " - " + bicho.Key, new Vector2(0 + (110), height * 64 + 25), Color.Black);
                    if (a == 3)
                        spriteBatch.DrawString(ComicSans, a + " - " + bicho.Key, new Vector2(0 + (240), height * 64 + 25), Color.Black);
                    if (a == 4)
                        spriteBatch.DrawString(ComicSans, a + " - " + bicho.Key, new Vector2(0 + (360), height * 64 + 25), Color.Black);
                    a ++;
                }
                if (keys.IsKeyDown(Keys.NumPad1))
                {
                    // defenir o bicho??

                    //desenhar o bicho
                    cont++;
                }
                if (a>=2 && keys.IsKeyDown(Keys.NumPad2))
                {

                    cont++;
                }
                
            }

            // colocar no ponta direita o health do bicho ->> hp: BichosDoCj.bichos[atacante].VidaAtual()

            else if (cont == 3)
            {
                spriteBatch.DrawString(ComicSans, "O que pretende fazer?", new Vector2(10, height * 64 + 3), Color.Black);
                spriteBatch.DrawString(ComicSans, "1-Ataque basico", new Vector2(0 + (10), height * 64 + 25), Color.Black);
                //if (BichosDoCj.bichos[atacante].PoderEsp() > 0)
                    spriteBatch.DrawString(ComicSans, "2-Especial", new Vector2(0 + (250), height * 64 + 25), Color.Black);
                spriteBatch.DrawString(ComicSans, "3-Defender", new Vector2(0 + (400), height * 64 + 25), Color.Black);
                spriteBatch.DrawString(ComicSans, "4-desistir", new Vector2(0 + (600), height * 64 + 25), Color.Black);

                if (keys.IsKeyDown(Keys.NumPad1))
                {
                    // atacar basicamente, descontar a vida do def


                }
                else if (keys.IsKeyDown(Keys.NumPad2) && BichosDoCj.bichos[atacante].PoderEsp() > 0)
                {
                    // atacar especial, descontar o ataque + level


                }
                else if (keys.IsKeyDown(Keys.NumPad3))
                {
                    // defender, ganha defesa = level


                }
                else if (keys.IsKeyDown(Keys.NumPad4))
                {
                    // desiste da partida
                    game.EmCombate = false;

                }

            }
            
        }



        // INICIO DO JOGO ONDE SE ESCOLHE O PRIMEIRO BICHO
        public void Inicio(SpriteBatch spriteBatch, SpriteFont ComicSans, int height)
        {
            
            KeyboardState keys = Keyboard.GetState();            

            if (cont == 1)
            {
                spriteBatch.DrawString(ComicSans, "Ola, eu Sou o Professor X, estou aqui para", new Vector2(10, height * 64 + 3), Color.Black);
                spriteBatch.DrawString(ComicSans, "entregar o seu novo Bicho! (Enter)", new Vector2(10, height * 64 + 25), Color.Black);
                if (keys.IsKeyDown(Keys.Enter) && upper)
                {
                    upper = false;
                    cont = 2;
                }
                if (keys.IsKeyUp(Keys.Enter) && !upper) upper = true;
            }
            else if (cont == 2)
            {
                spriteBatch.DrawString(ComicSans, "Os bichos evoluem a cada vez que luta,", new Vector2(10, height * 64 + 3), Color.Black);
                spriteBatch.DrawString(ComicSans, "por isso treine o seu bicho! (Enter)", new Vector2(10, height * 64 + 25), Color.Black);
                if (keys.IsKeyDown(Keys.Enter) && upper)
                {
                    upper = false;
                    cont = 3;
                }
                if (keys.IsKeyUp(Keys.Enter) && !upper) upper = true;
            }
            else if (cont == 3)
            {
                spriteBatch.DrawString(ComicSans, "Escolha: 1-erva; 2-pedra; 3-fogo; 4-agua;", new Vector2(10, height * 64 + 3), Color.Black);
                if (keys.IsKeyDown(Keys.NumPad1))
                {
                    upper = false;
                    BichosDoCj.adicionaErva();
                    cont = 4;
                }
                if (keys.IsKeyDown(Keys.NumPad2))
                {
                    upper = false;
                    BichosDoCj.adicionaPedra();
                    cont = 4;
                }
                if (keys.IsKeyDown(Keys.NumPad3))
                {
                    upper = false;
                    BichosDoCj.adicionaFogo();
                    cont = 4;
                }
                if (keys.IsKeyDown(Keys.NumPad4))
                {
                    upper = false;
                    BichosDoCj.adicionaAgua();
                    cont = 4;
                }
                if (keys.IsKeyUp(Keys.Enter) && !upper)
                {
                    upper = true;
                }
            }

            else if (cont == 4)
            {
                spriteBatch.DrawString(ComicSans, "Boa escolha, sempre que quiser recarregar", new Vector2(10, height * 64 + 3), Color.Black);
                spriteBatch.DrawString(ComicSans, "o seu bicho passe no Hospital!(Enter)", new Vector2(10, height * 64 + 25), Color.Black);
                if (keys.IsKeyDown(Keys.Enter))
                {
                    game.inicio = false;
                    cont = 1;
                }
            }
        }
    }
}
