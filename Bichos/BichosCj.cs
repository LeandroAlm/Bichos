using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bichos
{
    class BichosCj
    {
        public Dictionary<string, Bicho> bichos = new Dictionary<string, Bicho>();
        public Dictionary<string, Bicho> Adve = new Dictionary<string, Bicho>();
        public Dictionary<string, Bicho> Torneio = new Dictionary<string, Bicho>();

        public void adicionaIni()
        {
            Adve.Add("Rosaceae", new Bicho(10, 1, 3, 1, 1, "Rosaceae"));
            Adve.Add("Fizz", new Bicho(10, 1, 3, 1, 1, "Fizz"));
            Adve.Add("Drake", new Bicho(10, 1, 3, 1, 1, "Drake"));
            Adve.Add("Basaltes", new Bicho(10, 1, 3, 1, 1, "Basaltes"));
        }

        // BICHOS MAIS FORTES PARA O TORNEIO
        public void adicionaTorneio()
        {
            Torneio.Add("Rosaceae", new Bicho(19, 10, 5, 2, 1, "Rosaceae"));
            Torneio.Add("Fizz", new Bicho(16, 7, 4, 2, 1, "Fizz"));
            Torneio.Add("Drake", new Bicho(14, 4, 4, 2, 5, "Drake"));
            Torneio.Add("Basaltes", new Bicho(10, 2, 3, 3, 1, "Basaltes"));
        }


        public void adicionaErva()
        {
            if (!(bichos.ContainsKey("Rasaceae")))
                bichos.Add("Rosaceae", new Bicho(10, 1, 5, 1, 1, "Rosaceae"));
        }

        public void adicionaAgua()
        {
            if (!(bichos.ContainsKey("Fizz")))
                bichos.Add("Fizz", new Bicho(10, 1, 6, 1, 1, "Fizz"));
        }

        public void adicionaFogo()
        {
            if (!(bichos.ContainsKey("Drake")))
                bichos.Add("Drake", new Bicho(10, 1, 7, 1, 1, "Drake"));
        }

        public void adicionaPedra()
        {
            if (!(bichos.ContainsKey("Basaltes")))
                bichos.Add("Basaltes", new Bicho(10, 1, 4, 2, 1, "Basaltes"));
        }


        public void MudaVida(string name, int vida)
        {
            if (bichos.ContainsKey(name))
            {
                bichos[name].gereVida(vida);
            }
        }

        public void ReporVida(string name)
        {
            if (bichos.ContainsKey(name))
            {
                bichos[name].VidaRest(name, bichos[name].LevelAtual());
            }
        }

        public void ReporAdve()
        {
            foreach (var nome in Adve)
            {
                Adve[nome.Key].ReporAdve();
            }
        }

        public void ReporTorn()
        {
            Torneio.Remove("Rosaceae");
            Torneio.Remove("Fizz");
            Torneio.Remove("Drake");
            Torneio.Remove("Fizz");
            adicionaTorneio();
        }


        public int PoderSuper(string name)
        {
            if (bichos.ContainsKey(name))
            {
                return bichos[name].PoderEsp();
            }
            else return 0;
        }
    }
}
