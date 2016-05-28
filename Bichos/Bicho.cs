using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bichos
{
    class Bicho
    {
        int heal, level, ata, def, hardAta;
        string name;

        public Bicho(int vida, int nivel, int ataque, int defesa, int especial, string nome)
        {
            name = nome;
            heal = vida;
            level = nivel;
            ata = ataque;
            def = defesa;
            hardAta = especial;
        }

        public void gereVida(int dano)
        {
            heal -= dano;
        }

        public int PoderEsp()
        {
            return hardAta;
        }

    }
}
