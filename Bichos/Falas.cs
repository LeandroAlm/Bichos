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
        BichosCj BichosDoCj;
        CJ cj;
        private ContentManager content;
        int cont = 1, bonus = 0, vitorias = 0;
        bool upper = true, vida = false;
        string atacante = "", defensor = "", BichoVer;

        public Falas(ContentManager content, BichosCj b, Game1 g, CJ c)
        {
            this.content = content;
            game = g;
            BichosDoCj = b;
            cj = c;


        }

        public void TipoBicho(string bicho, string enemy)
        {
            game.tipo = bicho;
            game.tipoAdv = enemy;
        }

        public void TornBicho(string bicho, string enemy)
        {
            game.Ttipo = bicho;
            game.TtipoAdv = enemy;
        }

        // FALAS DO TORNEIO!!!!
        public void Torneio(SpriteBatch spriteBatch, SpriteFont ComicSans, int height)
        {
            KeyboardState keys = Keyboard.GetState();
            if (vitorias == 0)
            {
                defensor = "Basaltes";
            }
            else if (vitorias == 1)
            {
                defensor = "Drake";
            }
            else if (vitorias == 2)
            {
                defensor = "Fizz";
            }
            else if (vitorias == 3)
            {
                defensor = "Rosaceae";
            }


            if (cont == 1)
            {
                spriteBatch.DrawString(ComicSans, "Ola, o torneio e para lutadores de alto nivel!", new Vector2(10, height * 64 + 3), Color.Black);
                spriteBatch.DrawString(ComicSans, "Tem a certeza que quer entrar? (s/n)", new Vector2(10, height * 64 + 27), Color.Black);
                if (keys.IsKeyDown(Keys.S))
                {
                    game.level = 5;
                    game.loadLevel();
                    cont = 2;
                }
                if (keys.IsKeyDown(Keys.N)) game.torneio = false;
            }
            else if (cont == 2)
            {
                int a = 1;

                
                foreach (var bicho in BichosDoCj.bichos)
                {
                    spriteBatch.DrawString(ComicSans, "Escolha o seu Bicho:", new Vector2(10, height * 64 + 3), Color.Black);
                    if (a == 1)
                    {
                        spriteBatch.DrawString(ComicSans, a + " - " + bicho.Key + "     ", new Vector2(0 + (10), height * 64 + 25), Color.Black);

                    }
                    if (a == 2)
                        spriteBatch.DrawString(ComicSans, a + " - " + bicho.Key, new Vector2(0 + (150), height * 64 + 25), Color.Black);
                    if (a == 3)
                        spriteBatch.DrawString(ComicSans, a + " - " + bicho.Key, new Vector2(0 + (240), height * 64 + 25), Color.Black);
                    if (a == 4)
                        spriteBatch.DrawString(ComicSans, a + " - " + bicho.Key, new Vector2(0 + (360), height * 64 + 25), Color.Black);
                    a++;
                }
                a = 1;
                if (keys.IsKeyDown(Keys.NumPad1))
                {
                    Console.WriteLine("Chegou!!!!");
                    upper = false;
                    foreach (var bicho in BichosDoCj.bichos)
                    {
                        if (a == 1)
                        {
                            atacante = bicho.Key;
                            TornBicho(bicho.Key, defensor);
                        }
                        a++;
                    }
                    cont = 3;
                }
                else if (keys.IsKeyDown(Keys.NumPad2) && BichosDoCj.bichos.Count() >= 2)
                {
                    upper = false;
                    foreach (var bicho in BichosDoCj.bichos)
                    {
                        if (a == 2)
                        {
                            atacante = bicho.Key;
                            TornBicho(bicho.Key, defensor);
                        }
                        a++;
                    }
                    cont = 3;
                }
                else if (keys.IsKeyDown(Keys.NumPad3) && BichosDoCj.bichos.Count() >= 3)
                {
                    upper = false;
                    foreach (var bicho in BichosDoCj.bichos)
                    {
                        if (a == 3)
                        {
                            atacante = bicho.Key;
                            TornBicho(bicho.Key, defensor);
                        }
                        a++;
                    }
                    cont = 3;
                }
                else if (keys.IsKeyDown(Keys.NumPad4) && BichosDoCj.bichos.Count() == 4)
                {
                    upper = false;
                    foreach (var bicho in BichosDoCj.bichos)
                    {
                        if (a == 4)
                        {
                            atacante = bicho.Key;
                            TornBicho(bicho.Key, defensor);
                        }
                        a++;
                    }
                    cont = 3;
                }
                if (keys.IsKeyDown(Keys.NumPad1) && keys.IsKeyDown(Keys.NumPad2) && keys.IsKeyDown(Keys.NumPad3) && keys.IsKeyDown(Keys.NumPad4)) upper = true;
            }
            else if (cont == 3)
            {
                spriteBatch.DrawString(ComicSans, "O que pretende fazer?", new Vector2(10, height * 64 + 3), Color.Black);
                spriteBatch.DrawString(ComicSans, "1-Ataque basico", new Vector2(0 + (10), height * 64 + 25), Color.Black);
                if (BichosDoCj.PoderSuper(atacante) > 0)
                    spriteBatch.DrawString(ComicSans, "2-Especial", new Vector2(0 + (250), height * 64 + 25), Color.Black);
                else
                    spriteBatch.DrawString(ComicSans, "2-Especial", new Vector2(0 + (250), height * 64 + 25), Color.Red);
                spriteBatch.DrawString(ComicSans, "3-Defender           Vida do Rival: " + BichosDoCj.Torneio[defensor].VidaAtual(), new Vector2(0 + (400), height * 64 + 25), Color.Black);

                if (keys.IsKeyDown(Keys.NumPad1) && upper)
                {
                    upper = false;
                    BichosDoCj.Torneio[defensor].gereVida(BichosDoCj.bichos[atacante].ValorAtaque());

                    // atacar basicamente, descontar a vida do def
                    if (BichosDoCj.Torneio[defensor].VidaAtual() > 0)
                    {
                        cont = 4;
                    }
                    else cont = 5;

                }
                else if (keys.IsKeyDown(Keys.NumPad2) && BichosDoCj.bichos[atacante].PoderEsp() > 0 && upper)
                {
                    upper = false;
                    // atacar especial, descontar o ataque + level
                    BichosDoCj.Torneio[defensor].gereVida(BichosDoCj.bichos[atacante].ValorAtaque() + BichosDoCj.bichos[atacante].LevelAtual());
                    BichosDoCj.bichos[atacante].gereSuper();

                    if (BichosDoCj.Adve[defensor].VidaAtual() > 0)
                    {

                        cont = 4;
                    }
                    else cont = 5;

                }
                else if (keys.IsKeyDown(Keys.NumPad3) && upper)
                {
                    upper = false;
                    if (BichosDoCj.bichos[atacante].LevelAtual() < 3) bonus = BichosDoCj.bichos[atacante].LevelAtual();
                    if (BichosDoCj.bichos[atacante].LevelAtual() >= 3) bonus = 3;
                    // defender, ganha defesa = level

                    cont = 4;
                }
                if (keys.IsKeyUp(Keys.NumPad1) && keys.IsKeyUp(Keys.NumPad2) && keys.IsKeyUp(Keys.NumPad3) && keys.IsKeyUp(Keys.NumPad4)) upper = true;
            }
            else if (cont == 4)
            {
                spriteBatch.DrawString(ComicSans, "O adversario atacou! (ENTER)", new Vector2(10, height * 64 + 3), Color.Black);
                if (keys.IsKeyDown(Keys.Enter) && upper && !vida)
                {
                    if (BichosDoCj.Adve[atacante].VidaAtual() > 0)
                    {
                        upper = false;
                        vida = true;
                        BichosDoCj.bichos[atacante].gereVida(BichosDoCj.Torneio[defensor].ValorAtaque() - bonus);
                        bonus = 0;
                    }
                }
                if (vida)
                {
                    spriteBatch.DrawString(ComicSans, "O adversario atacou! (ENTER)    Vida do seu Bicho: " + BichosDoCj.bichos[atacante].VidaAtual(), new Vector2(10, height * 64 + 3), Color.Black);
                    spriteBatch.DrawString(ComicSans, "1-Prosseguir       0-Sair", new Vector2(0 + (10), height * 64 + 25), Color.Black);
                }
                if (BichosDoCj.bichos[atacante].VidaAtual() > 0)
                {
                    if (keys.IsKeyDown(Keys.NumPad1) && upper)
                    {
                        vida = false;
                        upper = false;
                        cont = 3;
                    }
                    else if (keys.IsKeyDown(Keys.NumPad0))
                    {
                        game.numero = 0;
                        game.contador = 0;
                        vida = false;
                        game.EmCombate = false;
                        game.EmCombate1 = false;
                        game.aux = false;
                        cont = 1;
                        BichosDoCj.Adve[defensor].ReporAdve();
                        TipoBicho("", "");
                    }
                    if (keys.IsKeyUp(Keys.NumPad1) && keys.IsKeyUp(Keys.Enter) && (keys.IsKeyUp(Keys.NumPad0))) upper = true;
                }
                else cont = 6;
            }
            else if (cont == 5)
            {
                BichosDoCj.Torneio[defensor].ReporAdve();
                // VITORIA
                spriteBatch.DrawString(ComicSans, "Voce ganhou-me desta vez, o torneio nao e", new Vector2(10, height * 64 + 3), Color.Black);
                spriteBatch.DrawString(ComicSans, "para qualquer um, parabens! (Enter)", new Vector2(10, height * 64 + 27), Color.Black);
                if (keys.IsKeyDown(Keys.Enter))
                {
                    if (vitorias == 3)
                    {
                        game.exit = cj.Position();
                        game.contador = 0;
                        game.numero = 0;
                        game.torneio = false;
                        cont = 1;
                        TipoBicho("", "");
                        BichosDoCj.bichos[atacante].GereLevel();
                    }
                    else
                    {
                        vitorias++;
                        cont = 2;
                    }
                }
            }
            else if (cont == 6)
            {
                BichosDoCj.Torneio[defensor].ReporAdve();
                // DERROTA    
                spriteBatch.DrawString(ComicSans, "Foi facil ganhar, nao volte aqui ou eu", new Vector2(10, height * 64 + 3), Color.Black);
                spriteBatch.DrawString(ComicSans, "terei de ganhar novamente! (Enter)", new Vector2(10, height * 64 + 27), Color.Black);
                if (keys.IsKeyDown(Keys.Enter))
                {
                    game.exit = cj.Position();
                    game.contador = 0;
                    game.numero = 0;
                    game.torneio = false;
                    cont = 1;
                    TipoBicho("", "");
                }
            }

        }





        //----------------------------------------------------------------------------------------------------------------------------------





        // FALAS DE UM COMBATE
        public void Combate(SpriteBatch spriteBatch, SpriteFont ComicSans, int height, char letra, Vector2 pos, bool Conquista)
        {
            KeyboardState keys = Keyboard.GetState();

            // define defensor
            if (letra == 'x')
                defensor = "Rosaceae";
            if (letra == 'f')
                defensor = "Drake";
            if (letra == 'a')
                defensor = "Fizz";
            if (letra == 'b')
                defensor = "Basaltes";


            if (cont == 1)
            {
                if (!Conquista)
                {
                    spriteBatch.DrawString(ComicSans, "Ola, quer combater? (s/n)", new Vector2(10, height * 64 + 3), Color.Black);
                    if (keys.IsKeyDown(Keys.S))
                    {
                        if (defensor == "Rosaceae" || defensor == "Drake")
                            game.prep = true;
                        if (defensor == "Fizz" || defensor == "Basaltes")
                            game.prep1 = true;
                        game.contador = 0;
                        cont = 2;
                    }
                    else if (keys.IsKeyDown(Keys.N))
                    {
                        game.numero = 0;
                        game.EmCombate = false;
                        game.EmCombate1 = false;
                    }
                }
                else cont = 2;
            }
            else if (cont == 2)
            {
                game.prep2 = false;
                upper = true;
                int a = 1;
                foreach (var bicho in BichosDoCj.bichos)
                {
                    spriteBatch.DrawString(ComicSans, "Escolha o seu Bicho:", new Vector2(10, height * 64 + 3), Color.Black);
                    if (a == 1)
                    {
                        spriteBatch.DrawString(ComicSans, a + " - " + bicho.Key+"     ", new Vector2(0 + (10), height * 64 + 25), Color.Black);
                            
                    }
                    if (a == 2)
                        spriteBatch.DrawString(ComicSans, a + " - " + bicho.Key, new Vector2(0 + (150), height * 64 + 25), Color.Black);
                    if (a == 3)
                        spriteBatch.DrawString(ComicSans, a + " - " + bicho.Key, new Vector2(0 + (240), height * 64 + 25), Color.Black);
                    if (a == 4)
                        spriteBatch.DrawString(ComicSans, a + " - " + bicho.Key, new Vector2(0 + (360), height * 64 + 25), Color.Black);
                    a++;
                }
                a = 1;
                if (keys.IsKeyDown(Keys.NumPad1) && upper)
                {
                    upper = false;
                    foreach (var bicho in BichosDoCj.bichos)
                    {
                        if (a == 1)
                        {
                            atacante = bicho.Key;
                            TipoBicho(bicho.Key, defensor);
                        }
                        a++;
                    }
                    cont = 3;
                }
                else if (keys.IsKeyDown(Keys.NumPad2) && BichosDoCj.bichos.Count() >= 2 && upper)
                {
                    upper = false;
                    foreach (var bicho in BichosDoCj.bichos)
                    {
                        if (a == 2) 
                        {
                            atacante = bicho.Key;
                            TipoBicho(bicho.Key, defensor);
                        }
                        a++;
                    }
                    cont = 3;
                }
                else if (keys.IsKeyDown(Keys.NumPad3) && BichosDoCj.bichos.Count() >= 3 && upper)
                {
                    upper = false;
                    foreach (var bicho in BichosDoCj.bichos)
                    {
                        if (a == 3)
                        {
                            atacante = bicho.Key;
                            TipoBicho(bicho.Key, defensor);
                        }
                        a++;
                    }
                    cont = 3;
                }
                else if (keys.IsKeyDown(Keys.NumPad4) && BichosDoCj.bichos.Count() == 4 && upper)
                {
                    upper = false;
                    foreach (var bicho in BichosDoCj.bichos)
                    {
                        if (a == 4)
                        {
                            atacante = bicho.Key;
                            TipoBicho(bicho.Key, defensor);
                        }
                        a++;
                    }
                    cont = 3;
                }
                if (keys.IsKeyDown(Keys.NumPad1) && keys.IsKeyDown(Keys.NumPad2) && keys.IsKeyDown(Keys.NumPad3) && keys.IsKeyDown(Keys.NumPad4)) upper = true;
            }

            else if (cont == 3)
            {

                spriteBatch.DrawString(ComicSans, "O que pretende fazer?", new Vector2(10, height * 64 + 3), Color.Black);
                spriteBatch.DrawString(ComicSans, "1-Ataque basico", new Vector2(0 + (10), height * 64 + 25), Color.Black);
                if (BichosDoCj.PoderSuper(atacante) > 0)
                    spriteBatch.DrawString(ComicSans, "2-Especial", new Vector2(0 + (250), height * 64 + 25), Color.Black);
                else
                    spriteBatch.DrawString(ComicSans, "2-Especial", new Vector2(0 + (250), height * 64 + 25), Color.Red);
                spriteBatch.DrawString(ComicSans, "3-Defender", new Vector2(0 + (400), height * 64 + 25), Color.Black);
                spriteBatch.DrawString(ComicSans, "4-desistir           Vida do Rival: " + BichosDoCj.Adve[defensor].VidaAtual(), new Vector2(0 + (600), height * 64 + 25), Color.Black);

                if (keys.IsKeyDown(Keys.NumPad1) && upper)
                {
                    upper = false;
                    BichosDoCj.Adve[defensor].gereVida(BichosDoCj.bichos[atacante].ValorAtaque());

                    // atacar basicamente, descontar a vida do def
                    if (BichosDoCj.Adve[defensor].VidaAtual() > 0)
                    {
                        cont = 4;
                    }
                    else cont = 5;

                }
                else if (keys.IsKeyDown(Keys.NumPad2) && BichosDoCj.bichos[atacante].PoderEsp() > 0 && upper)
                {
                    upper = false;
                    // atacar especial, descontar o ataque + level
                    BichosDoCj.Adve[defensor].gereVida(BichosDoCj.bichos[atacante].ValorAtaque() + BichosDoCj.bichos[atacante].LevelAtual());
                    BichosDoCj.bichos[atacante].gereSuper();

                    if (BichosDoCj.Adve[defensor].VidaAtual() > 0)
                    {
                        
                        cont = 4;
                    }
                    else cont = 5;

                }
                else if (keys.IsKeyDown(Keys.NumPad3) && upper)
                {
                    upper = false;
                    if (BichosDoCj.bichos[atacante].LevelAtual() < 3) bonus = BichosDoCj.bichos[atacante].LevelAtual();
                    if (BichosDoCj.bichos[atacante].LevelAtual() >= 3) bonus = 3;
                    // defender, ganha defesa = level

                    cont = 4;
                }
                else if (keys.IsKeyDown(Keys.NumPad4) && upper)
                {
                    upper = false;
                    game.numero = 0;
                    game.contador = 0;
                    // desiste da partida
                    game.EmCombate = false;
                    game.EmCombate1 = false;
                    game.aux = false;
                    cont = 1;
                    TipoBicho("", "");
                    BichosDoCj.Adve[defensor].ReporAdve();
                }

                if (keys.IsKeyUp(Keys.NumPad1) && keys.IsKeyUp(Keys.NumPad2) && keys.IsKeyUp(Keys.NumPad3) && keys.IsKeyUp(Keys.NumPad4)) upper = true;
            }
            else if (cont == 4)
            {
                spriteBatch.DrawString(ComicSans, "O adversario atacou! (ENTER)", new Vector2(10, height * 64 + 3), Color.Black);
                if (keys.IsKeyDown(Keys.Enter) && upper && !vida)
                {
                    if (BichosDoCj.Adve[atacante].VidaAtual() > 0)
                    {
                        upper = false;
                        vida = true;
                        BichosDoCj.bichos[atacante].gereVida(BichosDoCj.Adve[defensor].ValorAtaque() - bonus);
                        bonus = 0;
                    }
                }
                if (vida)
                {
                    spriteBatch.DrawString(ComicSans, "O adversario atacou! (ENTER)    Vida do seu Bicho: " + BichosDoCj.bichos[atacante].VidaAtual(), new Vector2(10, height * 64 + 3), Color.Black);
                    spriteBatch.DrawString(ComicSans, "1-Prosseguir       0-Sair", new Vector2(0 + (10), height * 64 + 25), Color.Black);
                }
                if (keys.IsKeyDown(Keys.NumPad1) && upper)
                {
                    vida = false;
                    upper = false;
                    cont = 3;
                }
                else if (keys.IsKeyDown(Keys.NumPad0))
                {
                    game.numero = 0;
                    game.contador = 0;
                    vida = false;
                    game.EmCombate = false;
                    game.EmCombate1 = false;
                    game.aux = false;
                    cont = 1;
                    BichosDoCj.Adve[defensor].ReporAdve();
                    TipoBicho("", "");
                }
                if (keys.IsKeyUp(Keys.NumPad1) && keys.IsKeyUp(Keys.Enter) && (keys.IsKeyUp(Keys.NumPad0))) upper = true;
            }
            else if (cont == 5)
            {
                BichosDoCj.Adve[defensor].ReporAdve();
                // VITORIA OU DERROTA    
                if (!Conquista)
                    spriteBatch.DrawString(ComicSans, "Voce ganhou-me desta vez, ate uma proxima! (Enter)", new Vector2(10, height * 64 + 3), Color.Black);
                else
                {
                    if (defensor == "Drake")
                    {
                        BichosDoCj.adicionaFogo();
                    }
                    else if (defensor == "Rosaceae")
                    {
                        BichosDoCj.adicionaErva();
                    }
                    else if (defensor == "Basaltes")
                    {
                        BichosDoCj.adicionaAgua();
                    }
                    else if (defensor == "Fizz")
                    {
                        BichosDoCj.adicionaAgua();
                    }
                    spriteBatch.DrawString(ComicSans, "Parabens, ganhou este bicho! (Enter)", new Vector2(10, height * 64 + 3), Color.Black);
                }
                if (keys.IsKeyDown(Keys.Enter))
                {

                    game.contador = 0;
                    game.numero = 0;
                    game.EmCombate = false;
                    game.EmCombate1 = false;
                    game.aux = false;
                    cont = 1;
                    TipoBicho("", "");
                    BichosDoCj.bichos[atacante].GereLevel();
                }
            }
        }





        // FALAS NO HOSPITAL
        public void NoHospital(SpriteBatch spriteBatch, SpriteFont ComicSans, int height)
        {
            KeyboardState keys = Keyboard.GetState();

            if (cont == 1)
            {
                spriteBatch.DrawString(ComicSans, "Bem-Vindo, quer restaurar os seus bichos? (s/n)", new Vector2(10, height * 64 + 3), Color.Black);
                if (keys.IsKeyDown(Keys.S)) cont = 2;
                if (keys.IsKeyDown(Keys.N)) game.EmCura = false;
            }
            else if (cont == 2)
            {
                spriteBatch.DrawString(ComicSans, "Escolha o bicho:", new Vector2(10, height * 64 + 3), Color.Black);
                int a = 1;
                foreach (var bicho in BichosDoCj.bichos)
                {
                    if (a == 1)
                        spriteBatch.DrawString(ComicSans, a + " - " + bicho.Key + "   ", new Vector2(0 + (10), height * 64 + 25), Color.Black);
                    if (a == 2)
                        spriteBatch.DrawString(ComicSans, a + " - " + bicho.Key, new Vector2(0 + (150), height * 64 + 25), Color.Black);
                    if (a == 3)
                        spriteBatch.DrawString(ComicSans, a + " - " + bicho.Key, new Vector2(0 + (240), height * 64 + 25), Color.Black);
                    if (a == 4)
                        spriteBatch.DrawString(ComicSans, a + " - " + bicho.Key, new Vector2(0 + (360), height * 64 + 25), Color.Black);
                    a++;
                }
                // escolhe o bicho no numpad e dps funçao!!

                a = 1;
                if (keys.IsKeyDown(Keys.NumPad1))
                {
                    foreach (var bicho in BichosDoCj.bichos)
                    {
                        if (a == 1) BichosDoCj.ReporVida(bicho.Key);
                        a++;
                    }
                    cont++;
                }
                else if (keys.IsKeyDown(Keys.NumPad2) && BichosDoCj.bichos.Count() >= 2)
                {
                    foreach (var bicho in BichosDoCj.bichos)
                    {
                        if (a == 2) BichosDoCj.ReporVida(bicho.Key);
                        a++;
                    }
                    cont=3;
                }
                else if (keys.IsKeyDown(Keys.NumPad3) && BichosDoCj.bichos.Count() >= 3)
                {
                    foreach (var bicho in BichosDoCj.bichos)
                    {
                        if (a == 3) BichosDoCj.ReporVida(bicho.Key);
                        a++;
                    }
                    cont = 3;
                }
                else if (keys.IsKeyDown(Keys.NumPad4) && BichosDoCj.bichos.Count() == 4)
                {
                    foreach (var bicho in BichosDoCj.bichos)
                    {
                        if (a == 4) BichosDoCj.ReporVida(bicho.Key);
                        a++;
                    }
                    cont = 3;
                }
            }

            else if (cont == 3)
            {
                spriteBatch.DrawString(ComicSans, "Muito bem, a vida do seu bicho foi reposta!", new Vector2(10, height * 64 + 3), Color.Black);
                spriteBatch.DrawString(ComicSans, "Deseja restaurar outro bicho? (s/n)", new Vector2(0 + (10), height * 64 + 25), Color.Black);
                if (keys.IsKeyDown(Keys.S)) cont = 1;
                if (keys.IsKeyDown(Keys.N))
                {
                    cont = 1;
                    game.EmCura = false;
                }
            }
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        // NO PC!!!
        public void NoPc(SpriteBatch spriteBatch, SpriteFont ComicSans, int height)
        {
            
            KeyboardState keys = Keyboard.GetState();

            if (cont == 1)
            {

                if (keys.IsKeyUp(Keys.NumPad1) && keys.IsKeyUp(Keys.NumPad2) && keys.IsKeyUp(Keys.NumPad3)) upper = true;

                spriteBatch.DrawString(ComicSans, "O que pretende fazer:", new Vector2(10, height * 64 + 3), Color.Black);
                spriteBatch.DrawString(ComicSans, "1-Meus Bichos   2-Todos os bichos   0-Sair", new Vector2(0 + (10), height * 64 + 25), Color.Black);

                if (keys.IsKeyDown(Keys.NumPad1) && upper)
                {
                    upper = false;
                    cont = 2;
                }
                else if (keys.IsKeyDown(Keys.NumPad2) && upper)
                {
                    upper = false;
                    cont = 4;
                }
                else if (keys.IsKeyDown(Keys.NumPad0) && upper)
                {
                    upper = false;
                    game.UsaPc = false;
                    cont = 1;
                }
            }

            else if (cont == 2)
            {
                int a = 1;

                if (keys.IsKeyUp(Keys.NumPad1) && keys.IsKeyUp(Keys.NumPad2) && keys.IsKeyUp(Keys.NumPad3)) upper = true;

                spriteBatch.DrawString(ComicSans, "Escolha o bicho:", new Vector2(10, height * 64 + 3), Color.Black);
                foreach (var bicho in BichosDoCj.bichos)
                {
                    if (a == 1)
                        spriteBatch.DrawString(ComicSans, a + " - " + bicho.Key + "     ", new Vector2(0 + (10), height * 64 + 25), Color.Black);
                    if (a == 2)
                        spriteBatch.DrawString(ComicSans, a + " - " + bicho.Key, new Vector2(0 + (150), height * 64 + 25), Color.Black);
                    if (a == 3)
                        spriteBatch.DrawString(ComicSans, a + " - " + bicho.Key, new Vector2(0 + (240), height * 64 + 25), Color.Black);
                    if (a == 4)
                        spriteBatch.DrawString(ComicSans, a + " - " + bicho.Key, new Vector2(0 + (360), height * 64 + 25), Color.Black);
                    a++;
                }
                
                a = 1;
                if (keys.IsKeyDown(Keys.NumPad1) && upper)
                {
                    upper = false;
                    foreach (var bicho in BichosDoCj.bichos)
                    {
                        if (a == 1)
                            BichoVer = bicho.Key;
                        a++;
                    }
                    cont=3;
                }
                else if (keys.IsKeyDown(Keys.NumPad2) && upper)
                {
                    upper = false;
                    foreach (var bicho in BichosDoCj.bichos)
                    {
                        if (a == 2)
                            BichoVer = bicho.Key;
                        a++;
                    }
                    cont = 3;
                }
                else if (keys.IsKeyDown(Keys.NumPad3) && upper)
                {
                    upper = false;
                    foreach (var bicho in BichosDoCj.bichos)
                    {
                        if (a == 3)
                            BichoVer = bicho.Key;
                        a++;
                    }
                    cont = 3;
                }
                else if (keys.IsKeyDown(Keys.NumPad4) && upper)
                {
                    upper = false;
                    foreach (var bicho in BichosDoCj.bichos)
                    {
                        if (a == 4)
                            BichoVer = bicho.Key;
                        a++;
                    }
                    cont = 3;
                }
            }
            else if (cont == 3)
            {
                spriteBatch.DrawString(ComicSans, "Nome: "+ BichoVer + ", vida: " + BichosDoCj.bichos[BichoVer].VidaAtual() + ", ataque: "+ BichosDoCj.bichos[BichoVer].ValorAtaque()
                    + ", level: " + BichosDoCj.bichos[BichoVer].LevelAtual(), new Vector2(10, height * 64 + 3), Color.Black);
                spriteBatch.DrawString(ComicSans, "(Enter)", new Vector2(10, height * 64 + 25), Color.Black);
                if (keys.IsKeyDown(Keys.Enter))
                    cont = 1;
            }
            else if (cont == 4)
            {
                spriteBatch.DrawString(ComicSans, "Agua: Fizz;          Fogo: Drake,", new Vector2(10, height * 64 + 3), Color.Black);
                spriteBatch.DrawString(ComicSans, "Erva: Rosaceae;      Pedra: Basaltes   (Enter)", new Vector2(10, height * 64 + 25), Color.Black);
                if (keys.IsKeyDown(Keys.Enter))
                    cont = 1;
            }
        }

        //---------------------------------------------------------------------------------------------------------------------------------------




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
                spriteBatch.DrawString(ComicSans, "Se quiser mais bichos basta ir a jungle,", new Vector2(10, height * 64 + 3), Color.Black);
                spriteBatch.DrawString(ComicSans, "procure por la!(Enter)", new Vector2(10, height * 64 + 25), Color.Black);
                if (keys.IsKeyDown(Keys.Enter) && upper)
                {
                    upper = false;
                    cont = 5;

                }
                if (keys.IsKeyUp(Keys.Enter) && !upper) upper = true;
            }
            else if (cont == 5)
            {
                spriteBatch.DrawString(ComicSans, "Boa escolha, sempre que quiser recarregar", new Vector2(10, height * 64 + 3), Color.Black);
                spriteBatch.DrawString(ComicSans, "o seu bicho passe no Hospital!(Enter)", new Vector2(10, height * 64 + 25), Color.Black);
                if (keys.IsKeyDown(Keys.Enter) && upper)
                {
                    upper = false;
                    game.inicio = false;
                    cont = 1;
                    
                }
                if (keys.IsKeyUp(Keys.Enter) && !upper) upper = true;
            }
        }
    }
}
