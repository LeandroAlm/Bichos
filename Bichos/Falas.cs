using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bichos
{
    class Falas
    {
        Game1 game = new Game1();
        BichosCj BichosDoCj = new BichosCj();


        public void Combate (SpriteBatch spriteBatch, SpriteFont ComicSans, int height)
        {
            int cont = 1;
            KeyboardState keys = Keyboard.GetState();

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
            if (cont == 2)
            {
                spriteBatch.DrawString(ComicSans, "Escolha o seu Bicho:", new Vector2(10, height * 64 + 3), Color.Black);
            }
        }

        // INICIO DO JOGO ONDE SE ESCOLHE O PRIMEIRO BICHO
        public void Inicio(SpriteBatch spriteBatch, SpriteFont ComicSans, int height)
        {
            int cont = 1;
            KeyboardState keys = Keyboard.GetState();
            //cj.position = (cj.position + new Vector2 (2,2));  Colocar no inicializate do GMAE1

            if (cont == 1)
            {
                spriteBatch.DrawString(ComicSans, "Ola, eu Sou o Professor X, estou aqui para entregar o seu novo Bicho! (Enter)", new Vector2(10, height * 64 + 3), Color.Black);
                if (keys.IsKeyDown(Keys.Enter)) cont++;
            }
            if (cont == 2)
            {
                spriteBatch.DrawString(ComicSans, "Os bichos evoluem a cada vez que luta, por isso treine o seu bicho! (Enter)", new Vector2(10, height * 64 + 3), Color.Black);
                if (keys.IsKeyDown(Keys.Enter)) cont++;
            }
            if (cont == 3)
            {
                spriteBatch.DrawString(ComicSans, "Escolha: 1-erva; 2-pedra; 3-fogo; 4-agua;", new Vector2(10, height * 64 + 3), Color.Black);
                if (keys.IsKeyDown(Keys.NumPad1))
                {
                    BichosDoCj.adicionaErva();
                    cont++;
                }
                if (keys.IsKeyDown(Keys.NumPad2))
                {
                    BichosDoCj.adicionaPedra();
                    cont++;
                }
                if (keys.IsKeyDown(Keys.NumPad3))
                {
                    BichosDoCj.adicionaFogo();
                    cont++;
                }
                if (keys.IsKeyDown(Keys.NumPad4))
                {
                    BichosDoCj.adicionaAgua();
                    cont++;
                }
            }

            if (cont == 3)
            {
                spriteBatch.DrawString(ComicSans, "Boa escolha, sempre que quiser recarregar o seu bicho passe no Hospital!(Enter)", new Vector2(10, height * 64 + 3), Color.Black);
                if (keys.IsKeyDown(Keys.Enter)) cont++;
            }
        }
    }
}
