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
    class Nave
    {
        private int altoVentana = 480;
        private int anchoVentana= 800;
        private const int anchoImagen = 42;
        private const int altoImagen = 44;
        private Rectangle rectangulo;
        private int ancho = 42;
        private int alto = 44;
        private Texture2D imagen;
        private int height;
        private int width;
        private ContentManager _content;
        private int frameCounter = 0;

        public Texture2D Imagen  
    {  
        get { return imagen; }  
    }  
    //Este rectangulo representa la posicion dentro de la ventana.
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
  
    private List<Disparo>disparos;  
    public List<Disparo> Disparos  
    {  
        get { return disparos; }  
    }  

        public Nave(int height, int width)
        {
            this.height = height;
            this.width = width;
            posicion = new Vector2(height - alto * 2, (width - ancho)/2);
            CrearRectangulo(anchoImagen, altoImagen * 2);
            disparos = new List<Disparo>();
        }
        public void LoadContent(ContentManager Content)
        {
            this._content = Content;
            imagen = Content.Load<Texture2D>("Imagenes/sin");
        }

        public void Update()
        {
            UpdateShots();
            UpdatePosition();
            UpdateRectangle();
        }

        private void UpdateShots()
        {
            frameCounter++;
            if (Keyboard.GetState().IsKeyDown(Keys.Z) && disparos.Count < 6 && frameCounter > 7)
            {
                Disparo s = new Disparo(posicion, anchoImagen, _content);
                disparos.Add(s);
                s.FueraDePantalla += new EventHandler(FueraDePantallaHandler);
                frameCounter = 0;
            }
            disparos.ForEach(x => x.Update());
        }

        private void UpdatePosition()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Left) && posicion.X > 5)
                posicion.X -= 7;
            if (Keyboard.GetState().IsKeyDown(Keys.Right) && posicion.X < (anchoVentana - anchoImagen))
                posicion.X += 7;
            if (Keyboard.GetState().IsKeyDown(Keys.Up) && posicion.Y > 5)
                posicion.Y -= 5;
            if (Keyboard.GetState().IsKeyDown(Keys.Down) && posicion.Y < (altoVentana - altoImagen))
                posicion.Y += 5;
            bounds = new Rectangle((int)Posicion.X, (int)Posicion.Y, anchoImagen, altoImagen);  
        }
        private void UpdateRectangle()
        {

            if (Keyboard.GetState().IsKeyDown(Keys.Left) && Keyboard.GetState().IsKeyDown(Keys.Up)
            )
            {
                CrearRectangulo(0, altoImagen);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right) && Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                CrearRectangulo(anchoImagen * 2, altoImagen);

            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                CrearRectangulo(anchoImagen, altoImagen);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left) && Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                CrearRectangulo(0, 0);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right) && Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                CrearRectangulo(anchoImagen * 2, 0);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                CrearRectangulo(anchoImagen, 0);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                CrearRectangulo(0, altoImagen * 2);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                CrearRectangulo(anchoImagen * 2, altoImagen * 2);
            }
            else
            {
                CrearRectangulo(anchoImagen, altoImagen * 2);
            }
        }
         void CrearRectangulo(int x, int y)
{
    rectangulo = new Rectangle(x, y, anchoImagen, altoImagen);
}
    

        public void Draw(SpriteBatch spbtch)
        {
            spbtch.Draw(imagen, posicion, rectangulo, Color.White);
            DrawShots(spbtch);
        }
        private void DrawShots(SpriteBatch spbtch)
        {
            foreach (Disparo s in disparos)
            {
                s.Draw(spbtch);
            }
        }
        private void FueraDePantallaHandler(Object sender, EventArgs args)
        {
            disparos.Remove((Disparo)sender);
        }
        }
    }




