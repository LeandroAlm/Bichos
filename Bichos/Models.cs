using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bichos
{
    class Models
    {
        public Texture2D casaInt, hospital, mundo, jungler, medico, torneio;


        public void CarregaModels(ContentManager Content)
        {
            torneio = Content.Load<Texture2D>("ginasio");
            medico = Content.Load<Texture2D>("infermeira");
            jungler = Content.Load<Texture2D>("jung");
            mundo = Content.Load<Texture2D>("world");
            hospital = Content.Load<Texture2D>("hosp");
            casaInt = Content.Load<Texture2D>("casaInt");
        }
    }
}
