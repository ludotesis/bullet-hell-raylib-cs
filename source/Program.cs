// Importar Raylib 
using Raylib_cs;
using System.Numerics;
// =========== Shuriken ===== //
Vector2 positionShuriken = new Vector2(100, 400);
float velocidadShuriken = 400f;
Texture2D spriteShuriken;
bool activoShuriken = false;
// Inicializar ventana
Raylib.InitWindow(320, 180, "Bullet Hell con Raylib");
//Configurar ventana sin border
Raylib.ToggleBorderlessWindowed();
// =========== Cargar Texturas ===== //
spriteShuriken = Raylib.LoadTexture("sprites/Shuriken.png");
// =========== Redimensionar Textura ===== //
spriteShuriken.Width  *= 4;
spriteShuriken.Height *= 4;
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
    if (Raylib.IsKeyPressed(KeyboardKey.F))
    {
        activoShuriken = true;
    }

    if (activoShuriken)
    {
        // Mover Shuriken horizontalmente en Sincronización delta
        positionShuriken.X += velocidadShuriken * deltaTime;
        // Reiniciar Posicion Shuriken si supera limite horizontal
        if (positionShuriken.X > Raylib.GetScreenWidth())
        {
            activoShuriken = false;
            positionShuriken.X = 100;
        }
        // Dibujar Textura en pantalla [textura2D, vector2, color]
        Raylib.DrawTextureV(spriteShuriken, positionShuriken, Color.White);
    }
  
    // Finalizar el dibujo del Canvas 
    Raylib.EndDrawing();
}
// Cerrar ventana
Raylib.CloseWindow();
