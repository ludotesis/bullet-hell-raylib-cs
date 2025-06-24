/*
// Importar Raylib 
using Raylib_cs;
using System.Numerics;
// =========== Constantes ===== //
const int SCREEN_WIDTH = 1920;
const int SCREEN_HEIGHT = 1080;
const int ESCALA_ACTORES = 10;
const int ESCALA_ITEMS = 6;
#region ==== PROPIEDADES ====
// =========== Jugador ===== //
Vector2 posicionJugador = new Vector2(50, 400);
Texture2D spriteJugador;
Rectangle hitboxJugador;
float velocidadJugador = 500f;
bool activoJugador = true;
// =========== Shuriken ===== //
Vector2 posicionShuriken = new Vector2(100, 400);
float velocidadShuriken = 1000f;
Texture2D spriteShuriken;
bool activoShuriken = false;
Rectangle hitboxShuriken;
// =========== Enemigo ===== //
Vector2 posicionEnemigo ;
float velocidadEnemigo = 150f;
int dificultad = 3;
velocidadEnemigo = velocidadEnemigo * dificultad;
bool activoEnemigo = true;
bool upEnemigo = true;
Rectangle hitboxEnemigo;
// =========== Animacion Enemigo Reposo ===== //
float duracionAnimacion = 1.0f;
float velocidadAnimacion = 5f;
int cantidadFrames = 4;
float tiempoActualAnimacion = 0;
int frameActualAnimacion = 0;
Texture2D enemigoAnimamacionReposo;
Rectangle RectanguloAnimacion;
// =========== Enemigo Disparo ===== //
Vector2 posicionDisparo;
float velocidadDisparo = 800f;
bool activoDisparo = false;
float radioDisparo = 18f;
float refrescoDisparo = 4f;
float tiempoEsperaDisparo = 0;
// =========== Pared ===== //
Vector2 posicionPared;
Rectangle hitboxPared;
int anchoPared = 10;
// =========== Sonido ===== //


enemigoAnimamacionReposo.Width *= ESCALA_ACTORES;
enemigoAnimamacionReposo.Height *= ESCALA_ACTORES;
//Definir rectángulo segun escala de spritesheet
RectanguloAnimacion = new Rectangle(
                        0f,
                        0f,
                      enemigoAnimamacionReposo.Width / cantidadFrames,
                      enemigoAnimamacionReposo.Height/ cantidadFrames);

#region ==== AUDIO ====

Raylib.SetSoundVolume(sonidoDisparo, volumenSonidos);
Raylib.SetSoundVolume(sonidoColision, volumenMusica);
Raylib.PlayMusicStream(musicaFondo);
#endregion
#region ==== GAME LOOP ====
while (!Raylib.WindowShouldClose())
{

    //========= TEMPORIZADORES ======
    ActualizarTemporizadores();
    //========= MOVIMIENTO =========
    MoverJugador();
    MoverEnemigo();
    // ======== DISPAROS ===========
    DispararShuriken();
    DispararEnemigo();
    MoverShuriken();
    MoverDisparoEnemigo();
    //========= PROCESAR =========
    ActualizarHitboxes();
    DetectarColisiones();
    GestionarColisiones();
    AnimarEnemigo();
    //========= DIBUJAR =========
    Raylib.BeginDrawing();
    Raylib.ClearBackground(Color.Beige);
    DibujarTexto();
    DibujarObjetos();
    GestionarDebugMode();
    Raylib.EndDrawing();
}
// Descargar Musica
Raylib.UnloadMusicStream(musicaFondo);
// Descargar Sonidos
Raylib.UnloadSound(sonidoColision);
Raylib.UnloadSound(sonidoDisparo);
// Cerrar dispositivo de audio
Raylib.CloseAudioDevice();
// Cerrar ventana
Raylib.CloseWindow();
#endregion

#region ==== METODOS ====
void ActualizarTemporizadores()
{
    // Obtener el tiempo en segundos para el último fotograma dibujado (tiempo delta)

    // Sumamos tiempo trascurrido al disparo
    tiempoEsperaDisparo += deltaTime;
    // Se acumula tiempo para determinar cuando cambiar el frame
    tiempoActualAnimacion += deltaTime;    
}
void MoverJugador()
{
    if (activoJugador)
    {
        if (Raylib.IsKeyDown(KeyboardKey.Up) && (posicionJugador.Y > limiteUp))
        {
            // Mover Jugador hacia arriba en Sincronización delta
            posicionJugador.Y -= velocidadJugador * deltaTime;
        }
        // Si el jugador presiona la tecla Down y no supera el limite inferior
        if (Raylib.IsKeyDown(KeyboardKey.Down) && (posicionJugador.Y < limiteDown))
        {
            // Mover Jugador hacia abajo en Sincronización delta
            posicionJugador.Y += velocidadJugador * deltaTime;
        }
    }

    
}
void DispararShuriken()
{
    if (!activoJugador) return;

    // Si el jugador presiona la tecla F y aún no disparó Shuriken
    if (Raylib.IsKeyPressed(KeyboardKey.F) && !activoShuriken)
    {
        //Disparar Efecto de Sonido
      
        //Asignar a la shuriken la posicion de origen en X
        posicionShuriken.X = origenShurikenX;
        //Calcula de la posicion en Y luego de moverse y segun la altura del personaje
        posicionShuriken.Y = posicionJugador.Y + (spriteJugador.Height * 0.4f);
        activoShuriken = true;
    }
}
void MoverShuriken()
{
    if (activoShuriken) posicionShuriken.X += velocidadShuriken * deltaTime;
}
void ActualizarHitboxes()
{
    hitboxJugador.X = posicionJugador.X;
    hitboxJugador.Y = posicionJugador.Y;
    hitboxShuriken.X = posicionShuriken.X;
    hitboxShuriken.Y = posicionShuriken.Y;
    hitboxEnemigo.X = posicionEnemigo.X;
    hitboxEnemigo.Y = posicionEnemigo.Y;
}
void MoverEnemigo()
{
    if (activoEnemigo && !activoDisparo)
    {
        // Si el enemigo se mueve hacia arriba y no supera el limite superior 
        if (upEnemigo && (posicionEnemigo.Y > limiteUp))
        {
            posicionEnemigo.Y -= velocidadEnemigo * deltaTime;
        }
        else
        {
            upEnemigo = false;
        }
        // Si el enemigo se mueve hacia abajo y no supera el limite inferior
        if (!upEnemigo && (posicionEnemigo.Y < limiteDown))
        {
            posicionEnemigo.Y += velocidadEnemigo * deltaTime;
        }
        else
        {
            upEnemigo = true;
        }
    }
}
void DispararEnemigo()
{
    // Si tiempo espera disparo es mayo o igual que el refresco de disparo
    if (activoEnemigo && (tiempoEsperaDisparo >= refrescoDisparo))
    {
        // Configuro el disparo como activado
        activoDisparo = true;
        // Reinicio el tiempo de espera para el siguiente disparo
        tiempoEsperaDisparo = 0;
        // Posicion Disparo en X segun Posicion Enemigo
        posicionDisparo.X = posicionEnemigo.X - 50f;
        // Posicion Disparo en Y segun Posicion Enemigo
        posicionDisparo.Y = posicionEnemigo.Y + (RectanguloAnimacion.Height * 0.4f);
    }
}
void AnimarEnemigo()
{
    if (activoEnemigo)
    {
        if (tiempoActualAnimacion >= tiempoEntreFrame)
        {
            //Reiniciar el conteo para el siguiente cuadro
            tiempoActualAnimacion = 0f;
            //Incrementar el INDICE del frame
            frameActualAnimacion++;
            //Si se supera el último frame

            if (frameActualAnimacion >= cantidadFrames)
            {
                //Reiniciar la animacion
                frameActualAnimacion = 0;
            }
            RectanguloAnimacion.X = RectanguloAnimacion.Width * columnaAnimacionY;
            //Posicionar el rectángulo en el siguiente cuadro
            RectanguloAnimacion.Y = frameActualAnimacion * RectanguloAnimacion.Height;
        }
    }
}
void MoverDisparoEnemigo()
{
    
    // Si el Disparo esta activo
    if (activoDisparo)
    {
        // Mover Disparo horizontalmente en Sincronización delta
        posicionDisparo.X -= velocidadDisparo * deltaTime;
        // Detectamos colision entre hibtox Disparo y hibox Jugador
        
    }
    // Si el Disparo supera limite izquierdo
    if (posicionDisparo.X < 0)
    {
        activoDisparo = false;
        posicionDisparo.X = posicionEnemigo.X;
    }
}
void DetectarColisiones()
{
    collisionShurikenMapa = Raylib.CheckCollisionRecs(hitboxShuriken, hitboxPared);
    collisionShurikenEnemigo = Raylib.CheckCollisionRecs(hitboxShuriken, hitboxEnemigo);
    collisionDisparoJugador = Raylib.CheckCollisionCircleRec(posicionDisparo, radioDisparo, hitboxJugador);
}
void GestionarColisiones()
{
    if (collisionShurikenMapa)
    {
        activoShuriken = false;
        posicionShuriken.X = origenShurikenX;
        Raylib.PlaySound(sonidoColision);
    }

    if (collisionShurikenEnemigo)
    {
        activoShuriken = false;
        posicionShuriken.X = origenShurikenX;
        activoEnemigo = false;
        Raylib.PlaySound(sonidoColision);
    }

    if (collisionDisparoJugador)
    {
        activoJugador = false;
        activoDisparo = false;
        tiempoEsperaDisparo = 0;
        Raylib.PlaySound(sonidoColision);
    }
}
void DibujarObjetos()
{
    if (activoJugador) Raylib.DrawTextureV(spriteJugador, posicionJugador, Color.White);
    if (activoEnemigo) Raylib.DrawTextureRec(enemigoAnimamacionReposo, RectanguloAnimacion, posicionEnemigo, Color.Red);
    if (activoShuriken) Raylib.DrawTextureV(spriteShuriken, posicionShuriken, Color.White);
    if (activoDisparo) Raylib.DrawCircleV(posicionDisparo, radioDisparo, Color.Black);
}
void DibujarTexto()
{
    if (collisionDisparoJugador) Raylib.DrawText("GAME OVER", 50, 100, 75, Color.Red);
    if (collisionShurikenEnemigo) Raylib.DrawText("VICTORIA", 50, 100, 75, Color.DarkGreen);
    if (activoEnemigo) Raylib.DrawText("FRAME ENEMIGO " + frameActualAnimacion.ToString(), 10, 1000, 30, Color.DarkBrown);
}
void CargarTexturas()
{
    spriteShuriken = Raylib.LoadTexture("sprites/Shuriken.png");
    spriteJugador = Raylib.LoadTexture("sprites/Jugador.png");
    enemigoAnimamacionReposo = Raylib.LoadTexture("sprites/Enemigo/Walk.png");
}
void DibujarHitboxs()
{
    Raylib.DrawRectangleRec(hitboxPared, Color.Brown);
    Raylib.DrawRectangleRec(hitboxShuriken, Raylib.ColorAlpha(Color.Red, 0.3f));
    Raylib.DrawRectangleRec(hitboxJugador, Raylib.ColorAlpha(Color.Blue, 0.3f));
    Raylib.DrawRectangleRec(hitboxEnemigo, Raylib.ColorAlpha(Color.Black, 0.3f));
}
void GestionarDebugMode()
{
    // Finalizar el dibujo del Canvas 
    if (Raylib.IsKeyDown(KeyboardKey.F12))
    {
        isDebugMode = true;
    }

    if (Raylib.IsKeyDown(KeyboardKey.F11))
    {
        isDebugMode = false;
    }

    if (isDebugMode)
    {
        DibujarHitboxs();
    }
}
#endregion
*/
using System.Numerics;
using Raylib_cs;                    

class Program
{
    public static void Main()
    {
        Jugador jugador = new Jugador(50f, 50f);
        Proyectil shuriken = new Proyectil();
        Enemigo enemigo1 = new Enemigo(300, 10);
        Enemigo enemigo2 = new Enemigo(300, 60);
        Enemigo enemigo3 = new Enemigo(300, 110);
        Enemigo enemigo4 = new Enemigo(300, 160);

        int anchoVentana = 320;
        int altoVentana = 180;
        
        Raylib.InitWindow(anchoVentana, altoVentana, "Camara2D y Zoom");
        Raylib.ToggleBorderlessWindowed();
        int pantallaAncho = Raylib.GetScreenWidth();
        int pantallaAlto = Raylib.GetScreenHeight();

        float zoomAncho = pantallaAncho / (float)anchoVentana;
        float zoomAlto = pantallaAlto / (float)altoVentana;
        float zoom = MathF.Floor(MathF.Min(zoomAncho, zoomAlto));
        float deltaTime = 0f;
        
        Camera2D camera = new Camera2D();
        camera.Target = Vector2.Zero;   
        camera.Offset = Vector2.Zero;   
        camera.Rotation = 0f;           
        camera.Zoom = zoom;             
        
        jugador.CargarSprite();
        shuriken.CargarSprite("sprites/Shuriken.png");
        enemigo1.CargarSprite();
        enemigo2.CargarSprite();
        enemigo3.CargarSprite();
        enemigo4.CargarSprite();

        Music musicaFondo;
        float volumenMusica  = 0.8f;
    
        Raylib.InitAudioDevice();
        musicaFondo = Raylib.LoadMusicStream("sonidos/Dungeon.ogg");
        Raylib.SetMusicVolume(musicaFondo, volumenMusica);
        shuriken.InicializarSFX();
        Raylib.PlayMusicStream(musicaFondo);

        while (!Raylib.WindowShouldClose())
        {
            deltaTime = Raylib.GetFrameTime();
            //========= MUSICA ======
            Raylib.UpdateMusicStream(musicaFondo);

            if (Raylib.IsKeyDown(KeyboardKey.Up) && jugador.puedoMoverArriba(0))
            {
                jugador.MoverVertical(true, deltaTime);
            }

            if (Raylib.IsKeyDown(KeyboardKey.Down) && jugador.puedoMoverAbajo(altoVentana))
            {
                jugador.MoverVertical(false, deltaTime);
            }

            if (Raylib.IsKeyPressed(KeyboardKey.F) && shuriken.puedoDisparar())
            {
                shuriken.Disparar(jugador.ObtenerPosicionJugador());
                shuriken.DisparoSFX();
            }

            if (shuriken.puedoMoverDerecha(anchoVentana))
            {
                shuriken.MoverHorizontal(false, deltaTime);
            }
            else
            {
                shuriken.Reiniciar();
            }

            if (enemigo1.puedoMoverIzquierda(0))
            {
                enemigo1.MoverHorizontal(true, deltaTime);
            }
            else
            {
                enemigo1.Reiniciar();
            }

            if (enemigo2.puedoMoverIzquierda(0))
            {
                enemigo2.MoverHorizontal(true, deltaTime);
            }
            else
            {
                enemigo2.Reiniciar();
            }

            if (enemigo3.puedoMoverIzquierda(0))
            {
                enemigo3.MoverHorizontal(true, deltaTime);
            }
            else
            {
                enemigo3.Reiniciar();
            }

            if (enemigo4.puedoMoverIzquierda(0))
            {
                enemigo4.MoverHorizontal(true, deltaTime);
            }
            else
            {
                enemigo4.Reiniciar();
            }

            if (jugador.IsCollisionJugador(enemigo1.hitbox))
            {
                enemigo1.Reiniciar();
            }

            if (jugador.IsCollisionJugador(enemigo2.hitbox))
            {
                enemigo2.Reiniciar();
            }

            if (jugador.IsCollisionJugador(enemigo3.hitbox))
            {
                enemigo3.Reiniciar();
            }

            if (jugador.IsCollisionJugador(enemigo4.hitbox))
            {
                enemigo4.Reiniciar();
            }

            if (shuriken.IsCollisionProyectil(enemigo1.hitbox))
            {
                shuriken.Reiniciar();
                shuriken.HitSFX();
                enemigo1.Reiniciar();
            }

            if (shuriken.IsCollisionProyectil(enemigo2.hitbox))
            {
                shuriken.Reiniciar();
                shuriken.HitSFX();
                enemigo2.Reiniciar();
            }

            if (shuriken.IsCollisionProyectil(enemigo3.hitbox))
            {
                shuriken.Reiniciar();
                shuriken.HitSFX();
                enemigo3.Reiniciar();
            }

            if (shuriken.IsCollisionProyectil(enemigo4.hitbox))
            {
                shuriken.Reiniciar();
                shuriken.HitSFX();
                enemigo4.Reiniciar();
            }

            Raylib.BeginDrawing();
            Raylib.BeginMode2D(camera);

            Raylib.DrawRectangle(0, 0, 320, 180, Color.DarkBlue);

            jugador.DibujarSprite();
            shuriken.DibujarSprite();
            enemigo1.DibujarSprite();
            enemigo2.DibujarSprite();
            enemigo3.DibujarSprite();
            enemigo4.DibujarSprite();

            Raylib.DrawRectangleRec(jugador.hitbox, Raylib.ColorAlpha(Color.Blue, 0.5f));
            Raylib.DrawRectangleRec(shuriken.hitbox, Raylib.ColorAlpha(Color.Green, 0.5f));
            Raylib.DrawRectangleRec(enemigo1.hitbox, Raylib.ColorAlpha(Color.Red, 0.5f));
            Raylib.DrawRectangleRec(enemigo2.hitbox, Raylib.ColorAlpha(Color.Red, 0.5f));
            Raylib.DrawRectangleRec(enemigo3.hitbox, Raylib.ColorAlpha(Color.Red, 0.5f));
            Raylib.DrawRectangleRec(enemigo4.hitbox, Raylib.ColorAlpha(Color.Red, 0.5f));

            //Raylib.DrawFPS(10, 150);
            Raylib.EndMode2D();
            Raylib.EndDrawing();
        }

        // Descargar Musica
        Raylib.UnloadMusicStream(musicaFondo);
        // Descargar Sonidos
        shuriken.DeInicializarSFX();
        // Cerrar dispositivo de audio
        Raylib.CloseAudioDevice();
        // Cerrar Ventana
        Raylib.CloseWindow();
    }
}
