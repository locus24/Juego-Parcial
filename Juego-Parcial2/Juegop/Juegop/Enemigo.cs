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
    class Enemigo
    {
        private const int anchoImagen = 40;  
    private const int altoImagen = 57;  
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
    int altoVentana;  
    int anchoVentana;  
    public event EventHandler FueraDePantalla;  
    public Enemigo(Vector2 posicion, int altoVentana, int anchoVentana, ContentManager Content)  
    {  
        this.altoVentana = altoVentana;  
        this.anchoVentana = anchoVentana;  
        this.posicion = posicion;  
        //En vez de eso las cargaremos en el constructor.  
        imagen = Content.Load<Texture2D>("Imagenes/navemala");  
    }  
    public void Update()  
    {  
        posicion.Y += 4;  
        bounds = new Rectangle((int)posicion.X-2, (int)posicion.Y, anchoImagen, altoImagen);
        if (posicion.Y >= altoVentana)
        {
            FueraDePantalla(this, null);
        }
    }  
    public void Draw(SpriteBatch spbtch)  
    {  
        spbtch.Draw(imagen, posicion, new Rectangle(0,0,anchoImagen, altoImagen), Color.White);  
    } 
    }
}
