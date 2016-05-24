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
        public Texture2D casaInt, casa, hosp, hospital;


        public void CarregaModels(ContentManager Content)
        {
            hosp = Content.Load<Texture2D>("hospital");
            hospital = Content.Load<Texture2D>("hosp");
            casa = Content.Load<Texture2D>("casa");
            casaInt = Content.Load<Texture2D>("casaInt");
        }
    }
}
