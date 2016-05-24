using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bichos
{
    class BichosCj
    {
        public Dictionary<string, Bicho> bichos = new Dictionary<string, Bicho>();

        public void adicionaErva()
        {
            bichos.Add("Rosaceae", new Bicho(10, 1, 5, 4, "Rosaceae"));
        }

        public void adicionaAgua()
        {
            bichos.Add("Fizz", new Bicho(10, 1, 6, 5, "Fizz"));
        }

        public void adicionaFogo()
        {
            bichos.Add("Drake", new Bicho(10, 1, 7, 4, "Drake"));
        }

        public void adicionaPedra()
        {
            bichos.Add("Basaltes", new Bicho(12, 1, 4, 8, "Basaltes"));
        }

        public void MudaVida(string name, int vida)
        {
            if (bichos.ContainsKey(name))
            {
                bichos[name].gereVida(vida);
            }
        }

        //public string DizNome()
        //{
        //    string nome = "";
        //    nome = bicho;
        //    return nome;
        //}
    }
}
