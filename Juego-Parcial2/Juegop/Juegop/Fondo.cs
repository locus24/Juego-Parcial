using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Juegop
{
    class Fondo
    {
        private const int anchoImagen = 930;
        private const int altoImagen = 1700;
        private int altoVentana;
        private Texture2D imagen1;
        private Texture2D imagen2, imagen3, imagen4;
        private Texture2D imagent;
        private Rectangle rectangulo;
        int J = 600, L = 540, P = 590;

        public Fondo(int altoVentana, int anchoVentana)
        {
            rectangulo = new Rectangle(0, altoImagen - altoVentana, anchoVentana, altoVentana);
            this.altoVentana = altoVentana;
        }
        public void LoadContent(ContentManager Content)
        {
            //Carga los dos fondos.
            imagen1 = Content.Load<Texture2D>("Imagenes/fondodeljuego");
            imagen2 = Content.Load<Texture2D>("Imagenes/Fondo2");
            imagen3 = Content.Load<Texture2D>("Imagenes/Fondo3");
            imagen4 = Content.Load<Texture2D>("Imagenes/Fondo4");
        }
        public void Update()
        {
            //Movimiento de la imagen.
            rectangulo.Y -= 1;
            if (rectangulo.Y <= 0)
                rectangulo.Y = ((altoImagen - altoVentana) - 15);
            //Paso de un fondo al otro (cambio de nivel).
            if (J > 0)
            {
                J = (J + 1) - 2;
                if (J == 0)
                    imagent = imagen2;
                if (J > 0)
                    imagent = imagen1;
            }
            if (J == 0)
            {
                if (P > 0)
                    P = (P + 1) - 2;
                if (P == 0)
                    imagent = imagen3;
            }
            if (P == 0)
            {
                if (L > 0)
                    L = (L + 1) - 2;
                if (L == 0)
                    imagent = imagen4;
            }
        }
        public void Draw(SpriteBatch spbtch)
        {
            spbtch.Draw(imagent, new Vector2(0, 0), rectangulo, Color.White);
        }

    }
}

