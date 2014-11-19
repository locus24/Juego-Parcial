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
    class Disparo
    {
        private const int anchoImagen = 10;
        private const int altoImagen = 22;
        private Texture2D imagen;
        public Texture2D Imagen
        {
            get { return imagen; }
        }
        private Rectangle bounds;
        public Rectangle Bounds
        {
            get { return bounds; }
        }
        private Vector2 posicion;
        public Vector2 Posicion
        {
            get { return posicion; }
        }  

        public event EventHandler FueraDePantalla;

        public Disparo(Vector2 posicion, int anchoNave, ContentManager Content)
        {
            //Mueve un poco la posicion deldisparo, para que salga desde el centro de lanave y no desde una esquina.
            posicion.X += (anchoNave) / 2;
            //Situaremos el centro alineado con el centro de la imagen los 3 pixeles extra es por que la imagen del disparo no esta perfectamente centrada.
            posicion.X += (anchoImagen / 2 - 16);
            this.posicion = posicion; 
            //En vez de eso las cargaremos en el constructor.
            imagen = Content.Load<Texture2D>("Imagenes/balas");
        }
        public void Update()
        {
            posicion.Y -= 5;
            bounds = new Rectangle((int)posicion.X, (int)posicion.Y, anchoImagen, altoImagen);
            if (posicion.Y <= 0)
                FueraDePantalla(this, null);
        }
        public void Draw(SpriteBatch spbtch)
        {
            spbtch.Draw(imagen, posicion, new Rectangle(0, 0, anchoImagen, altoImagen), Color.White);
        }
    }
}
