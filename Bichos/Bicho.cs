using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bichos
{
    class Bicho
    {
        int heal, level, ata, def;
        string name;

        public Bicho(int vida, int nivel, int ataque, int defesa, string nome)
        {
            name = nome;
            heal = vida;
            level = nivel;
            ata = ataque;
            def = defesa;
        }

        public void gereVida(int dano)
        {
            heal -= dano;
        }

    }
}
