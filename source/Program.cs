// Importar Raylib 
using Raylib_cs;
using System.Numerics;
// =========== Shuriken ===== //
Vector2 posicionShuriken = new Vector2(100, 400);
float velocidadShuriken = 400f;
Texture2D spriteShuriken;
bool activoShuriken = false;
Rectangle hitboxShuriken;
int escalaShuriken = 4;
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
// =========== Redimensionar Texturas ===== //
spriteShuriken.Width  *= escalaShuriken;
spriteShuriken.Height *= escalaShuriken;
//=========== Generar Posiciones ===== //
posicionPared = new Vector2(Raylib.GetScreenWidth() - anchoPared, 0);
//=========== Generar Hiboxes ===== //
hitboxShuriken = new Rectangle(posicionShuriken.X, posicionShuriken.Y, spriteShuriken.Width, spriteShuriken.Height);
hitboxPared = new Rectangle(posicionPared.X, posicionPared.Y, anchoPared, Raylib.GetScreenHeight());
// Hasta que la aplicacion no se cierre
while (!Raylib.WindowShouldClose())
{
    // Obtener el tiempo en segundos para el último fotograma dibujado (tiempo delta)
    float deltaTime = Raylib.GetFrameTime();
    // Configurar el Canvas para comenzar a dibujar
    Raylib.BeginDrawing();
    // Establecer el color de fondo
    Raylib.ClearBackground(Color.Beige);
    // Dibujar un texto en pantalla [posicion en X,posicion en Y, tamaño, color]
    Raylib.DrawText("SUSCRIBETE A LUDOTESIS >.<", 50, 100, 75, Color.Brown);
    // Si el jugador presiona la tecla F
    if (Raylib.IsKeyPressed(KeyboardKey.F))
    {
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
        // Reiniciar Posicion Shuriken si supera limite horizontal
        if (posicionShuriken.X > Raylib.GetScreenWidth())
        {
            activoShuriken = false;
            posicionShuriken.X = 100;
        }
        // Dibujar Textura en pantalla [textura2D, vector2, color]
        Raylib.DrawTextureV(spriteShuriken, posicionShuriken, Color.White);
        // Dibujar Hibox en pantalla [Rectangulo, color] (Modo Debug)
        Raylib.DrawRectangleRec(hitboxShuriken, Raylib.ColorAlpha(Color.Red, 0.3f));
    }
    // Dibujar Pantalla en pantalla [Rectangulo, color] (Modo Debug)
    Raylib.DrawRectangleRec(hitboxPared, Color.Brown);
    // Finalizar el dibujo del Canvas 
    Raylib.EndDrawing();
}
// Cerrar ventana
Raylib.CloseWindow();
