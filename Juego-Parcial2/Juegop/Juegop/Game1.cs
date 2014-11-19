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
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Nave _nave;
        Vector2 posText, postext2, postpuntaje, postscore,felicitacion,Upwned,Pvidas,coordes_menu;
        Fondo _fondo;
        public Texture2D fondos;
        public int vidasM=106;
        int min = 20, PP = 590, LL = 540, ÑÑ=590, seg = 0,Y=0, I = 0, J = 600, P = 2280, K = 0, min2 = 20; 
        int min3 = 20, seg3 = 0, seg2 = 0, min4 = 20, seg4 = 0, postScore, menu = 0,T=0;
        SpriteFont texto, texto2, texto3, textoPuntaje, Felicitacion, pwned, vidas, menuf, titulos;
        string tiempo = "Tiempo", tiempo2 = "Tiempo2";
        private int frameCounter = 0;
        Texture2D Perdio, Gano, mapa0;
        private List<Enemigo> enemigos;
        private Song MusicaF;
        private SoundEffect Buu, explosion2, explosion3, explosion4, yeah,Pierde;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            enemigos = new List<Enemigo>(); 
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            _nave = new Nave(graphics.PreferredBackBufferHeight, graphics.PreferredBackBufferWidth);
            _fondo = new Fondo(graphics.PreferredBackBufferHeight, graphics.PreferredBackBufferWidth);
            //Nombre del juego.
            this.Window.Title = "Jimmy's Game";
            //Posiciones.
            posText = new Vector2(10, 10);
            postext2 = new Vector2(10,450);
            postpuntaje = new Vector2(700, 450);
            postscore = new Vector2(15, 350);
            felicitacion = new Vector2(560, 200);
            Upwned = new Vector2(450, 350);
            Pvidas = new Vector2(340, 450);
            coordes_menu = new Vector2(230, 250);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            _fondo.LoadContent(Content);
            _nave.LoadContent(Content);
            //Control del volumen general.
            SoundEffect.MasterVolume = 0.4f;
            //Puntaje inicial.
            postScore = 0;
            //Fuentes.
            texto = this.Content.Load<SpriteFont>("Fuentes/texto");
            texto2 = Content.Load<SpriteFont>("Fuentes/Nivel_1");
            texto3 = Content.Load<SpriteFont>("Fuentes/Nivel_2");
            pwned = Content.Load<SpriteFont>("Fuentes/PWNED");
            vidas = Content.Load<SpriteFont>("Fuentes/Vidas");
            menuf = Content.Load<SpriteFont>("Fuentes/fuentemenu");
            Felicitacion = this.Content.Load<SpriteFont>("Fuentes/Felicitacion");
            textoPuntaje = Content.Load<SpriteFont>("Fuentes/PUNTAJE");
            titulos = Content.Load<SpriteFont>("Fuentes/titulos");
            //Efectos de sonido.
            MusicaF = Content.Load<Song>("Sonidos/sports_card");
            explosion2 = Content.Load<SoundEffect>("Sonidos/explosion");
            explosion3 = Content.Load<SoundEffect>("Sonidos/movimientoavion");
            explosion4 = Content.Load<SoundEffect>("Sonidos/gun_ricochet");
            yeah = Content.Load<SoundEffect>("Sonidos/yes");
            Pierde = Content.Load<SoundEffect>("Sonidos/Pierde");
            Buu = Content.Load<SoundEffect>("Sonidos/Buu");
            //Imagenes.
            Perdio = Content.Load<Texture2D>("Imagenes/Perdiste");
            Gano = Content.Load<Texture2D>("Imagenes/Gano");
            mapa0 = Content.Load<Texture2D>("Imagenes/Menu/Fondo");
            //Musica de fondo.
            MediaPlayer.Play(MusicaF);
            MediaPlayer.IsRepeating = true;
        }
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            //Guarda la tecla presionada.
            KeyboardState teclado = Keyboard.GetState();
            if (teclado.IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }
            if (teclado.IsKeyDown(Keys.F1))
            {
                menu = 1; //Jugar
            }
            if (teclado.IsKeyDown(Keys.F2)) // Comandos
            {
                menu = 2; 
            }
            if (teclado.IsKeyDown(Keys.F3)) // Creditos
            {
                menu = 3;
            }
            if (teclado.IsKeyDown(Keys.Space)) //  Retrocede
            {
                menu = 0;
            }
            //Control de la pausa si el usuario esta en el menu.
            if ((menu == 1) && (vidasM > 0) && (P>0) && (postScore < 30))
            {
                //Retraso en el sonido del disparo si deja apretada la Z.
                //Si alguna se cumple igualo las otras a 0 para que no continue el update.
                if (vidasM == 0)
                    P = 0;
                if (P == 0)
                    vidasM = 0;
                if (postScore >= 30)
                {
                    P = 0;
                    vidasM = 0;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Z))
                {
                    I = I + 1;
                    if (I > 5)
                    {
                        explosion4.Play();
                        I = 0;
                    }
                }
            }
            //Abucheo si pierde el jugador.
            if ((vidasM == 0) || (P == 0))
            {
                T = T + 1;
                if (T > 5)
                {
                    Buu.Play();
                    T = -12346;
                }
            }
            if (postScore >= 30)
            {
                T = T + 1;
                if (T > 5)
                {
                    yeah.Play();
                    T = -12346;
                }
            }
            //Control de la pausa si el usuario esta en el menu.
            if ((menu == 1) && (vidasM > 0) && (P > 0) && (postScore < 30))
            { //Si alguna se cumple igualo las otras a 0 para que no continue el update.
                if (vidasM == 0)
                    P = 0;
                if (P == 0)
                    vidasM = 0;
                if (postScore >= 30)
                {
                    P = 0;
                    vidasM = 0;
                }
                _fondo.Update();
                _nave.Update();
                UpdateEnemigos();
                UpdateColisiones();
            }
            //Control de la pausa si el usuario esta en el menu.
            if ((menu == 1) && (vidasM > 0) && (P>0) && (postScore < 30))
            {
                if (vidasM == 0)
                    P = 0;
                if (P == 0)
                    vidasM = 0;
                if (postScore >= 30)
                {
                    P = 0;
                    vidasM = 0;
                }
                //Tiempo en primer nivel.
                if ((seg == 0) && (min > 0))
                {
                    seg = 30;
                    min--;
                }
                if (seg >= 1)
                {
                    seg--;
                    tiempo = String.Concat(min, ":", seg);
                }
                //Tiempo en el segundo nivel.
                if (seg == 0)
                {
                    if ((seg2 == 0) && (min2 > 0))
                    {
                        seg2 = 30;
                        min2--;
                    }
                    if (seg2 >= 1)
                    {
                        seg2--;
                        tiempo2 = String.Concat(min2, ":", seg2);
                    }
                }
                //Tiempo en el tercer nivel.
                if (seg2 == 0)
                {
                    if ((seg3 == 0) && (min3 > 0))
                    {
                        seg3 = 30;
                        min3--;
                    }
                    if (seg3 >= 1)
                    {
                        seg3--;
                        tiempo2 = String.Concat(min3, ":", seg3);
                    }
                }
                //Tiempo en el cuarto nivel.
                if (seg3 == 0)
                {
                    if ((seg4 == 0) && (min4 > 0))
                    {
                        seg4 = 30;
                        min4--;
                    }
                    if (seg4 >= 1)
                    {
                        seg4--;
                        tiempo2 = String.Concat(min4, ":", seg4);
                    }
                }
                //Control en el dibujado de nivel 1 a 2, 3 y 4.
                if (J > 0)
                {
                    J = (J + 1) - 2;
                }
                if (J == 0)
                {
                    if (PP>0)
                    PP = (PP + 1) - 2;
                }
                if (PP==0)
                {
                    if (LL > 0)
                        LL = (LL + 1) - 2;
                }
                if (LL == 0)
                {
                    if (ÑÑ > 0)
                        ÑÑ = (ÑÑ + 1) - 2;
                }
                //Control fin del tiempo total del juego.
                if (P > 0)
                {
                    P = (P + 1) - 2;
                }
                //Control sonido "YES" cuando mata +10.
                if (((postScore >= 10) && (postScore <= 12)) || (postScore >= 22) && (postScore <= 24))
                {
                    K = K + 1;
                    if (K > 55)
                    {
                        yeah.Play();
                        K = 0;
                    }
                }
            }
            base.Update(gameTime);
        }
        //Borra enemigos fuera de pantalla.
        private void FueraDePantallaHandler(Object sender, EventArgs args)
        {
            //Contador de vidas, (si el enemigo no es eliminado y te pasa).
            if (vidasM > 0)
            {
                vidasM = vidasM - 1;
            }
            //Sonido -1 vida.
            Pierde.Play();
            enemigos.Remove((Enemigo)sender);
        }

        private void UpdateColisiones()
        {
            //Elimina cualquier enemigo que haya colisionado contra cualquier disparo en la pantalla.  
            bool colision = false;
            Enemigo enemigoABorrar = null;
            Disparo disparoABorrar = null;

            foreach (Enemigo e in enemigos)
            {
                foreach (Disparo d in _nave.Disparos)
                {
                    if (ColisionEnemigoDisparo(e, d))
                    {
                        postScore = postScore+1;
                        explosion2.Play();
                        SoundEffect.MasterVolume = 0.5f;
                        //si hay colision el tag pasa a true, almacena el disparo y el enemigo y rompe el primer bucle.  
                        colision = true;
                        enemigoABorrar = e;
                        disparoABorrar = d;
                        break;
                    }
                }
                //si el tag esta activo se rompe el segundo bucle.  
                if (colision)
                    break;
            }
            //si el tag esta activo se borra el enemigo y el disparo.  
            if (colision)
            {
                enemigos.Remove(enemigoABorrar);
                _nave.Disparos.Remove(disparoABorrar);
            }
        }
        private bool ColisionEnemigoDisparo(Enemigo e, Disparo d)
        {
            //Si los rectangle de e y de d se intersectan se comprueba la colision.  
            if (e.Bounds.Intersects(d.Bounds))
                return ColisionPixel(d.Imagen, e.Imagen, d.Posicion, e.Posicion);
            return false;
        }

        public static bool ColisionPixel(Texture2D texturaA, Texture2D texturaB, Vector2 posicionA, Vector2 posicionB)
        {
            bool colisionPxAPx = false;

            uint[] bitsA = new uint[texturaA.Width * texturaA.Height];
            uint[] bitsB = new uint[texturaB.Width * texturaB.Height];

            Rectangle rectanguloA = new Rectangle(Convert.ToInt32(posicionA.X), Convert.ToInt32(posicionA.Y), texturaA.Width, texturaA.Height);
            Rectangle rectanguloB = new Rectangle(Convert.ToInt32(posicionB.X), Convert.ToInt32(posicionB.Y), texturaB.Width, texturaB.Height);
            //almacena los datos de los pixeles en las variables locales bitsA y bitsB  
            texturaA.GetData<uint>(bitsA);
            texturaB.GetData<uint>(bitsB);

            //almacena las coordenadas que delimitaran la zona en la que trabajaremos  
            int x1 = Math.Max(rectanguloA.X, rectanguloB.X);
            int x2 = Math.Min(rectanguloA.X + rectanguloA.Width, rectanguloB.X + rectanguloB.Width);

            int y1 = Math.Max(rectanguloA.Y, rectanguloB.Y);
            int y2 = Math.Min(rectanguloA.Y + rectanguloA.Height, rectanguloB.Y + rectanguloB.Height);

            for (int y = y1; y < y2; ++y)
            {
                for (int x = x1; x < x2; ++x)
                {
                    if (((bitsA[(x - rectanguloA.X) + (y - rectanguloA.Y) * rectanguloA.Width] & 0xFF000000) >> 24) > 20 &&
                        ((bitsB[(x - rectanguloB.X) + (y - rectanguloB.Y) * rectanguloB.Width] & 0xFF000000) >> 24) > 20)
                    {
                        //Se comprueba el canal alpha de las dos imagenes en el mismo pixel. Si los dos son visibles hay colision.  
                        colisionPxAPx = true;
                        break;
                    }
                }

                // Rompe el bucle si la condicion ya se ha cumplido.  
                if (colisionPxAPx)
                {
                    break;
                }
            }

            return colisionPxAPx;
        }

        private void UpdateEnemigos()
        {
            frameCounter++;
            if (frameCounter > 60)
            {
                Random r = new Random();
                Enemigo e = new Enemigo(
                new Vector2(r.Next(graphics.PreferredBackBufferWidth-50), -57), graphics.PreferredBackBufferHeight, graphics.PreferredBackBufferWidth, Content);
                enemigos.Add(e);
                e.FueraDePantalla += new EventHandler(FueraDePantallaHandler);
                frameCounter = 0;
            }
            enemigos.ForEach(x => x.Update());
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            if (menu == 0) //Menu
            {
                spriteBatch.Draw(mapa0, Vector2.Zero, Color.White);
                coordes_menu.X = 140;
                coordes_menu.Y = 60;
                spriteBatch.DrawString(titulos, "Jimmy's Game®", coordes_menu, Color.OrangeRed);
                coordes_menu.X = 180;
                coordes_menu.Y = 140;
                spriteBatch.DrawString(menuf, "F1 : Jugar", coordes_menu, Color.Yellow);
                coordes_menu.X = 180;
                coordes_menu.Y = 180;
                spriteBatch.DrawString(menuf, "F2 : Comandos", coordes_menu, Color.Violet);
                coordes_menu.X = 180;
                coordes_menu.Y = 220;
                spriteBatch.DrawString(menuf, "F3 : Creditos", coordes_menu, Color.Coral);
                coordes_menu.X = 440;
                coordes_menu.Y = 450;
                spriteBatch.DrawString(menuf, "PRESIONA ESCAPE PARA SALIR.", coordes_menu, Color.Gold);
            }
            if (menu == 2) // Intrucciones
            {
                spriteBatch.Draw(mapa0, Vector2.Zero, Color.White);
                coordes_menu.X = 210;
                coordes_menu.Y = 40;
                spriteBatch.DrawString(titulos, "Instrucciones", coordes_menu, Color.OrangeRed);
                coordes_menu.X = 110;
                coordes_menu.Y = 80;
                spriteBatch.DrawString(menuf, "1 )  La flechas desplazaran a la nave.", coordes_menu, Color.Orange);
                coordes_menu.X = 110;
                coordes_menu.Y = 140;
                spriteBatch.DrawString(menuf, "2 )  Con la tecla Z disparas el rayo laser.", coordes_menu, Color.Violet);
                coordes_menu.X = 110;
                coordes_menu.Y = 200;
                spriteBatch.DrawString(menuf, "3 )  CUAL ES LA META: Tienes que eliminar 30", coordes_menu, Color.Gold);
                coordes_menu.X = 110;
                coordes_menu.Y = 240;
                spriteBatch.DrawString(menuf, "enemigos para ganar. Debes evitar que te pasen", coordes_menu, Color.Gold);
                coordes_menu.X = 110;
                coordes_menu.Y = 280;
                spriteBatch.DrawString(menuf, "y destruyan la tierra, sino perderas. Hazlo!!", coordes_menu, Color.Gold);
                coordes_menu.X = 110;
                coordes_menu.Y = 340;
                spriteBatch.DrawString(menuf, "4 ) Si deseas pausar el juego presiona tecla espacio.", coordes_menu, Color.Green);
                coordes_menu.X = 300;
                coordes_menu.Y = 390;
                spriteBatch.DrawString(menuf, "USE TECLA ESPACIO PARA VOLVER ATRAS", coordes_menu, Color.Red);
            }
            if (menu == 3) // Creditos
            {
                spriteBatch.Draw(mapa0, Vector2.Zero, Color.White);
                coordes_menu.X = 300;
                coordes_menu.Y = 60;
                spriteBatch.DrawString(titulos, "Creditos", coordes_menu, Color.OrangeRed);
                coordes_menu.X = 100;
                coordes_menu.Y = 100;
                spriteBatch.DrawString(menuf, "Diseno y Programacion: The Code Crab y Leandro Merlino", coordes_menu, Color.Violet);
                coordes_menu.X = 100;
                coordes_menu.Y = 140;
                spriteBatch.DrawString(menuf, "Sprites:  The Code Crab http://codecrab.blogspot.com.ar/", coordes_menu, Color.Yellow);
                coordes_menu.X = 100;
                coordes_menu.Y = 180;
                spriteBatch.DrawString(menuf, "Tema musical:  Freesfx http://www.freesfx.co.uk/", coordes_menu, Color.Coral);
                coordes_menu.X = 100;
                coordes_menu.Y = 220;
                spriteBatch.DrawString(menuf, "Efectos de Sonido:  Freesfx http://www.freesfx.co.uk/", coordes_menu, Color.Pink);
                coordes_menu.X = 300;
                coordes_menu.Y = 380;
                spriteBatch.DrawString(menuf, "USE TECLA ESPACIO PARA VOLVER ATRAS", coordes_menu, Color.Red);
            }
            if (menu == 1) //Arranca el juego
            {
                //Dibuja el fondo.
                _fondo.Draw(spriteBatch);
                //Dibuja KILLS + los enemigos abatidos por el jugador.
                spriteBatch.DrawString(textoPuntaje, "KILLS " + postScore, postpuntaje, Color.Aquamarine);
                //Cambia de nivel 1 a nivel 2.
                if (J > 0)
                {
                    spriteBatch.DrawString(texto3, "NIVEL 1", postext2, Color.Yellow);
                }
                if ((PP > 0) && (J==0))
                {
                    spriteBatch.DrawString(texto2, "NIVEL 2", postext2, Color.Crimson);
                }
                if ((LL > 0) && (PP == 0) && (J == 0))
                {
                    spriteBatch.DrawString(texto2, "NIVEL 3", postext2, Color.Yellow);
                }
                if((ÑÑ> 0) && (PP == 0) && (J == 0)&& (LL==0))
                {
                    spriteBatch.DrawString(texto2, "NIVEL 4", postext2, Color.Crimson);
                }
                //Dibuja bonus SIGUE ASI!.
                if (((postScore >= 10) && (postScore <= 12)) || (postScore >= 22) && (postScore <= 24))
                {
                    spriteBatch.DrawString(Felicitacion, "SIGUE ASI!!", felicitacion, Color.Green);
                }
                //Dibuja el tiempo que lleva cada nivel en juego.
                if (seg > 0)
                {
                    spriteBatch.DrawString(texto, tiempo, posText, Color.Violet);
                }
                if (seg==0)
                {
                    spriteBatch.DrawString(texto, tiempo2, posText, Color.Violet);
                }
                if (seg2==0)
                {
                    spriteBatch.DrawString(texto, tiempo2, posText, Color.Violet);
                }
                if (seg3 == 0)
                {
                    spriteBatch.DrawString(texto, tiempo2, posText, Color.Violet);
                }
                //Dibuja bonus PWNED!.
                if (((postScore >= 20) && (postScore < 21)) || ((postScore >= 29) && (postScore < 30)))
                {
                    spriteBatch.DrawString(pwned, "PWNED!", Upwned, Color.Blue);
                }
                spriteBatch.DrawString(vidas, "VIDAS " + vidasM, Pvidas, Color.GreenYellow);
                //Remueve enemigo abatido.
                foreach (Enemigo e in enemigos)
                {
                    //Produce camuflaje del enemigo en nivel 3 y 4.
                    if ((seg2==0) || (seg3==0))
                    {
                        Y = Y + 1;
                        if (Y > 2)
                        {
                            e.Draw(spriteBatch);
                            Y = 0;
                        }
                    }
                    else
                        e.Draw(spriteBatch);

                }
                //Dibuja la nave.
                _nave.Draw(spriteBatch);
                //Dibuja carteles de gano/perdio.
                if ((vidasM == 0) || (P == 0))
                {
                    MediaPlayer.Stop();
                    spriteBatch.Draw(Perdio, Vector2.Zero, Color.White);
                }
                if (postScore>= 30)
                {
                    MediaPlayer.Stop();
                    spriteBatch.Draw(Gano, Vector2.Zero, Color.White);
                }
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
