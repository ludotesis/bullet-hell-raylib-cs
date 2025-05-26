// Importar Raylib 
using Raylib_cs;
using System.Numerics;
// =========== Shuriken ===== //
Vector2 posicionJugador = new Vector2(50, 400);
Texture2D spriteJugador;
Rectangle hitboxJugador;
float velocidadJugador = 500f;
int escalaJugador = 8;
bool activoJugador = true;
// =========== Jugador ===== //
Vector2 posicionShuriken = new Vector2(100, 400);
float velocidadShuriken = 1000f;
Texture2D spriteShuriken;
bool activoShuriken = false;
Rectangle hitboxShuriken;
int escalaShuriken = 4;
// =========== Enemigo ===== //
Vector2 posicionEnemigo ;
float velocidadEnemigo = 1000f;
Texture2D spriteEnemigo;
bool activoEnemigo = true;
Rectangle hitboxEnemigo;
int escalaEnemigo = 8;
// =========== Pared ===== //
Vector2 posicionPared;
Rectangle hitboxPared;
int anchoPared = 10;
// =========== Colisiones ===== //
bool collisionShurikenMapa = false;
// Inicializar ventana
Raylib.InitWindow(320, 180, "Bullet Hell con Raylib");
//Configurar ventana sin border
Raylib.ToggleBorderlessWindowed();
// =========== Cargar Texturas ===== //
spriteShuriken = Raylib.LoadTexture("sprites/Shuriken.png");
spriteJugador = Raylib.LoadTexture("sprites/Jugador.png");
spriteEnemigo = Raylib.LoadTexture("sprites/Enemigo.png");
// =========== Redimensionar Texturas ===== //
spriteShuriken.Width  *= escalaShuriken;
spriteShuriken.Height *= escalaShuriken;
spriteJugador.Width  *= escalaJugador;
spriteJugador.Height *= escalaJugador;
spriteEnemigo.Width  *= escalaEnemigo;
spriteEnemigo.Height *= escalaEnemigo;
//=========== Generar Posiciones ===== //
posicionPared = new Vector2(Raylib.GetScreenWidth() - anchoPared, 0);
posicionEnemigo = new Vector2(Raylib.GetScreenWidth() - (spriteEnemigo.Width * 2f) , posicionJugador.Y);
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
while (!Raylib.WindowShouldClose())
{
    // Obtener el tiempo en segundos para el último fotograma dibujado (tiempo delta)
    float deltaTime = Raylib.GetFrameTime();
    // Configurar el Canvas para comenzar a dibujar
    Raylib.BeginDrawing();
    // Establecer el color de fondo
    Raylib.ClearBackground(Color.Beige);
    // Si el jugador presiona la tecla UP y no supera el limite superior
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
    // Si el jugador presiona la tecla F y aún no disparó Shuriken
    if (Raylib.IsKeyPressed(KeyboardKey.F) && !activoShuriken)
    {
        //Asignar a la shuriken la posicion de origen en X
        posicionShuriken.X = origenShurikenX;
        //Calcula de la posicion en Y luego de moverse y segun la altura del personaje
        posicionShuriken.Y = posicionJugador.Y + (spriteJugador.Height * 0.4f);
        activoShuriken = true;
    }
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
        Raylib.DrawRectangleRec(hitboxShuriken, Raylib.ColorAlpha(Color.Red, 0.3f));
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
        Raylib.DrawRectangleRec(hitboxJugador, Raylib.ColorAlpha(Color.Blue, 0.3f));
    }
    else
    {
        // Dibujar un texto en pantalla [posicion en X,posicion en Y, tamaño, color]
        Raylib.DrawText("GAME OVER", 50, 100, 75, Color.Red);
    }

    // Si el Jugador esta activo
    if (activoEnemigo)
    {
        // Dibujar Textura en pantalla [textura2D, vector2, color]
        Raylib.DrawTextureV(spriteEnemigo, posicionEnemigo, Color.White);
        // Dibujar Hibox en pantalla [Rectangulo, color] (Modo Debug)
        Raylib.DrawRectangleRec(hitboxEnemigo, Raylib.ColorAlpha(Color.Black, 0.3f));
    }
    // Dibujar Pantalla en pantalla [Rectangulo, color] (Modo Debug)
    Raylib.DrawRectangleRec(hitboxPared, Color.Brown);
    // Finalizar el dibujo del Canvas 
    Raylib.EndDrawing();
}
// Cerrar ventana
Raylib.CloseWindow();
