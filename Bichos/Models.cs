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
        public Texture2D cama, livros, casa, tapete, mesa, pc, sofa, hosp;


        public void CarregaModels(ContentManager Content)
        {
            hosp = Content.Load<Texture2D>("hospital");
            casa = Content.Load<Texture2D>("casa");
            sofa = Content.Load<Texture2D>("sofa");
            pc = Content.Load<Texture2D>("pc");
            cama = Content.Load<Texture2D>("cama");
            mesa = Content.Load<Texture2D>("mesa");
            livros = Content.Load<Texture2D>("parteleira");
            tapete = Content.Load<Texture2D>("carpete");
        }


        public void Draw(SpriteBatch spriteBatch, Texture2D textura, Vector2 position)
        {
            spriteBatch.Draw(textura, position: position, color: Color.White);
        }
    }
}
