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

        public void ReporAdve()
        {
            heal = 10;
        }
        

        public void GereLevel()
        {
            level++;
            ata++;
        }

        public void gereSuper()
        {
            hardAta--;
        }

        public void gereVida(int dano)
        {
            heal = heal-(dano- def);
        }

        public int PoderEsp()
        {
            return hardAta;
        }

        public int VidaAtual()
        {
            return heal;
        }

<<<<<<< HEAD
        public int LevelAtual()
        {
            return level;
        }

        public int ValorAtaque()
        {
            return ata;
        }

        public void VidaRest(string name, int nivel)
        {
            if (name == "Basaltes") heal = 10 + nivel;
            else heal = 9 + nivel;
            if (nivel < 3)
                hardAta = 0 + nivel;
            else hardAta = 3;
        }

=======
>>>>>>> origin/master
    }
}
