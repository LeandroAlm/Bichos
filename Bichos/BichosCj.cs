using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bichos
{
    class BichosCj
    {
        Dictionary<string, Bicho> bichos = new Dictionary<string, Bicho>();

        public void adicionaBA()
        {
            bichos.Add("A", new Bicho(10, 1, 5, 5, "A"));
        }

        public void MudaVida(string name, int vida)
        {
            if (bichos.ContainsKey(name))
            {
                bichos[name].gereVida(vida);
            }
        }
    }
}
