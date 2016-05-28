using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bichos
{
    class Combate
    {
        SpriteBatch spriteBatch;
        SpriteFont ComicSans;
        BichosCj BichosDoCj = new BichosCj();

        // ** BONUS **
        // agua -> fogo; fogo-> pedra; pedra -> erva; erva -> agua;

        public Combate ()
        {
            
        }

        public void inicombate(string ata, string def, int Tipo)
        {
            int dano;

            if (ata == "Drake")
            {
                if (def == "Fizz")
                {

                }
                else if (def == "Basaltes")
                {

                }
                else
                {
                    if (Tipo == 1)
                    {
                        // ATAQUE BASICO
                    }
                    else if (Tipo == 2 && BichosDoCj.bichos[ata].PoderEsp() > 0)
                    {
                        // ataque especial 


                    }
                }

            }
        }

    }
}
