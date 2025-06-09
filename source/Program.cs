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
float velocidadEnemigo = 400f;
int dificultad = 3;
velocidadEnemigo = velocidadEnemigo * dificultad;
Texture2D spriteEnemigo;
bool activoEnemigo = true;
bool upEnemigo = true;
Rectangle hitboxEnemigo;
// =========== Animacion Enemigo Reposo ===== //
float duracionAnimacion = 1.0f;
float velocidadAnimacion = 6f;
int cantidadFrames = 4;
float tiempoActualAnimacion = 0;
int frameActualAnimacion = 0;
Texture2D enemigoAnimamacionReposo;
Rectangle RectanguloAnimacion;
// =========== Enemigo Disparo ===== //
Vector2 posicionDisparo;
float velocidadDisparo = 700f;
bool activoDisparo = true;
float radioDisparo = 18f;
float refrescoDisparo = 3f;
float tiempoEsperaDisparo = 0;
// =========== Pared ===== //
Vector2 posicionPared;
Rectangle hitboxPared;
int anchoPared = 10;
// =========== Colisiones ===== //
bool collisionShurikenMapa = false;
bool collisionShurikenEnemigo = false;
bool collisionDisparoJugador = false;
#endregion

bool isDebugMode = false;
float deltaTime = 0f;

#region ==== CARGA DE JUEGO ====
Raylib.InitWindow(SCREEN_WIDTH, SCREEN_HEIGHT, "Bullet Hell con Raylib");
//Configurar ventana sin border
//Raylib.ToggleBorderlessWindowed();
// =========== Cargar Texturas ===== //
CargaTexturas();
// =========== Redimensionar Texturas ===== //
spriteShuriken.Width  *= ESCALA_ITEMS;
spriteShuriken.Height *= ESCALA_ITEMS;
spriteJugador.Width  *= ESCALA_ACTORES;
spriteJugador.Height *= ESCALA_ACTORES;
spriteEnemigo.Width  *= ESCALA_ACTORES;
spriteEnemigo.Height *= ESCALA_ACTORES;
//Escalar Spritesheet
enemigoAnimamacionReposo.Width *= ESCALA_ACTORES;
enemigoAnimamacionReposo.Height *= ESCALA_ACTORES;
//Definir rectángulo segun escala de spritesheet
RectanguloAnimacion = new Rectangle(
                        0f,
                        0f,
                      enemigoAnimamacionReposo.Width / cantidadFrames,
                      enemigoAnimamacionReposo.Height/ cantidadFrames);
//=========== Generar Posiciones ===== //
posicionPared = new Vector2(Raylib.GetScreenWidth() - anchoPared, 0);
posicionEnemigo = new Vector2(Raylib.GetScreenWidth() - (spriteEnemigo.Width * 2f), posicionJugador.Y);
posicionDisparo = new Vector2(posicionEnemigo.X, posicionEnemigo.Y);
//=========== Generar Hiboxes ===== //
hitboxShuriken = new Rectangle(posicionShuriken.X, posicionShuriken.Y, spriteShuriken.Width, spriteShuriken.Height);
hitboxPared = new Rectangle(posicionPared.X, posicionPared.Y, anchoPared, Raylib.GetScreenHeight());
hitboxJugador = new Rectangle(posicionJugador.X, posicionJugador.Y, spriteJugador.Width, spriteJugador.Height);
hitboxEnemigo = new Rectangle(posicionEnemigo.X, posicionEnemigo.Y, spriteEnemigo.Width, spriteEnemigo.Height);
// Configuracion de limite superior 
float limiteUp = 0;
// Calculo de limite inferior segun la altura del jugador
float limiteDown = Raylib.GetScreenHeight() - spriteJugador.Height;
// Calculo de origen proyectil segun ancho jugador
float origenShurikenX = posicionJugador.X + spriteJugador.Width;
// Hasta que la aplicacion no se cierre
float tiempoEntreFrame = duracionAnimacion / velocidadAnimacion;
int columnaAnimacionY = 0;
#endregion
#region ==== GAME LOOP ====
while (!Raylib.WindowShouldClose())
{
    // Obtener el tiempo en segundos para el último fotograma dibujado (tiempo delta)
    deltaTime = Raylib.GetFrameTime();
    // Sumamos tiempo trascurrido al disparo
    tiempoEsperaDisparo += deltaTime;
    // Se acumula tiempo para determinar cuando cambiar el frame
    tiempoActualAnimacion += deltaTime;
    // Si hay que cambiar de cuandro de animacion (segun duracion y velocidad)
    //velocidadAnimacion++;
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
    Raylib.BeginDrawing();
    // Establecer el color de fondo
    Raylib.ClearBackground(Color.Beige);
    // Establecer mensaje para ver frame actual del enemigo
    Raylib.DrawText("FRAME ENEMIGO " + frameActualAnimacion.ToString(), 10, 1000, 30, Color.DarkBrown);
    // Si el jugador presiona la tecla UP y no supera el limite superior
    MoverJugador();
    DispararShuriken();
    // Si el Shuriken esta activo
    if (activoShuriken)
    {
        // Mover Shuriken horizontalmente en Sincronización delta
        posicionShuriken.X += velocidadShuriken * deltaTime;
        // Actualizar Posición Hitbox segun posicion Disparo
        hitboxShuriken.X = posicionShuriken.X;
        hitboxShuriken.Y = posicionShuriken.Y;
        // Detectamos colision entre hibtox Shuriken y hibox pared luego de moverlo
        collisionShurikenMapa = Raylib.CheckCollisionRecs(hitboxShuriken, hitboxPared);
        // Reiniciar Posicion Shuriken si toca pared
        if (collisionShurikenMapa)
        {
            activoShuriken = false;
            posicionShuriken.X = origenShurikenX;
        }
        // Dibujar Textura en pantalla [textura2D, vector2, color]
        Raylib.DrawTextureV(spriteShuriken, posicionShuriken, Color.White);
        // Dibujar Hibox en pantalla [Rectangulo, color] (Modo Debug)

    }
    // Si el Jugador esta activo
    if (activoJugador)
    {
        // Actualizar Posición Hitbox segun posicion Jugador
        hitboxJugador.X = posicionJugador.X;
        hitboxJugador.Y = posicionJugador.Y;
        // Dibujar Textura en pantalla [textura2D, vector2, color]
        Raylib.DrawTextureV(spriteJugador, posicionJugador, Color.White);
        // Dibujar Hibox en pantalla [Rectangulo, color] (Modo Debug)

    }
    else
    {
        // Dibujar un texto en pantalla [posicion en X,posicion en Y, tamaño, color]
        Raylib.DrawText("GAME OVER", 50, 100, 75, Color.Red);
    }

    // Si el Enemigo esta activo
    if (activoEnemigo)
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
        // Actualizar Posición Hitbox segun posicion Enemigo
        hitboxEnemigo.X = posicionEnemigo.X;
        hitboxEnemigo.Y = posicionEnemigo.Y;
        // Detectamos colision entre hibtox Shuriken y hibox Enemigo 
        collisionShurikenEnemigo = Raylib.CheckCollisionRecs(hitboxShuriken, hitboxEnemigo);
        // Reiniciar Posicion Shuriken si toca pared
        if (collisionShurikenEnemigo)
        {
            activoShuriken = false;
            posicionShuriken.X = origenShurikenX;
            activoEnemigo = false;
        }
        // Dibujar una porción de la textura usando un rectángulo
        //Raylib.DrawTextureV(spriteEnemigo, posicionEnemigo, Color.DarkBlue);
        Raylib.DrawTextureRec(enemigoAnimamacionReposo, RectanguloAnimacion, posicionEnemigo, Color.Red);
    
    }
    else
    {
        // Dibujar un texto en pantalla [posicion en X,posicion en Y, tamaño, color]
        Raylib.DrawText("VICTORIA", 50, 100, 75, Color.DarkGreen);
    }
    // Si tiempo espera disparo es mayo o igual que el refresco de disparo
    if ((tiempoEsperaDisparo >= refrescoDisparo) && activoEnemigo)
    {
        // Configuro el disparo como activado
        activoDisparo = true;
        // Reinicio el tiempo de espera para el siguiente disparo
        tiempoEsperaDisparo = 0;
        // Posicion Disparo en X segun Posicion Enemigo
        posicionDisparo.X = posicionEnemigo.X;
        // Posicion Disparo en Y segun Posicion Enemigo
        posicionDisparo.Y = posicionEnemigo.Y + (spriteEnemigo.Height * 0.4f);
        //
        columnaAnimacionY = 2;
    }
    // Si el Disparo esta activo
    if (activoDisparo)
    {
        // Mover Disparo horizontalmente en Sincronización delta
        posicionDisparo.X -= velocidadDisparo * deltaTime;
        // Detectamos colision entre hibtox Disparo y hibox Jugador
        collisionDisparoJugador = Raylib.CheckCollisionCircleRec(posicionDisparo, radioDisparo, hitboxJugador);
        // Si colision Disparo Jugador
        if (collisionDisparoJugador)
        {
            activoJugador = false;
            activoDisparo = false;
            tiempoEsperaDisparo = 0;
        }
        Raylib.DrawCircleV(posicionDisparo, radioDisparo, Color.Black);
    }
    // Si el Disparo supera limite izquierdo
    if (posicionDisparo.X < 0)
    {
        activoDisparo = false;
        posicionDisparo.X = posicionEnemigo.X;
    }

    GestionDebugMode();
    Raylib.EndDrawing();
}
// Cerrar ventana
Raylib.CloseWindow();
#endregion

#region ==== METODOS ====
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
        //Asignar a la shuriken la posicion de origen en X
        posicionShuriken.X = origenShurikenX;
        //Calcula de la posicion en Y luego de moverse y segun la altura del personaje
        posicionShuriken.Y = posicionJugador.Y + (spriteJugador.Height * 0.4f);
        activoShuriken = true;
    }
}
void MoverShuriken()
{

}
void ActulizarHitboxes()
{
    
}
void CargaTexturas()
{
    spriteShuriken = Raylib.LoadTexture("sprites/Shuriken.png");
    spriteJugador = Raylib.LoadTexture("sprites/Jugador.png");
    spriteEnemigo = Raylib.LoadTexture("sprites/Enemigo.png");
    enemigoAnimamacionReposo = Raylib.LoadTexture("sprites/Enemigo/Walk.png");
}
void DibujarHitboxs()
{
    // Dibujar Pantalla en pantalla [Rectangulo, color] (Modo Debug)
    Raylib.DrawRectangleRec(hitboxPared, Color.Brown);
    Raylib.DrawRectangleRec(hitboxShuriken, Raylib.ColorAlpha(Color.Red, 0.3f));
    Raylib.DrawRectangleRec(hitboxJugador, Raylib.ColorAlpha(Color.Blue, 0.3f));
    // Dibujar Hibox en pantalla [Rectangulo, color] (Modo Debug)
    Raylib.DrawRectangleRec(hitboxEnemigo, Raylib.ColorAlpha(Color.Black, 0.3f));
}
void GestionDebugMode()
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