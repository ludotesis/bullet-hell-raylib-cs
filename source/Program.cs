// Importar Raylib 
using Raylib_cs;
// Inicializar ventana
Raylib.InitWindow(320, 180, "Bullet Hell con Raylib");
//Configurar ventana sin border
Raylib.ToggleBorderlessWindowed();
// Hasta que la aplicacion no se cierre
while (!Raylib.WindowShouldClose())
{
    // Establecer el color de fondo
    Raylib.ClearBackground(Color.Beige);
    // Configurar el canvas (framebuffer) para comenzar a dibujar
    Raylib.BeginDrawing();
    // Dibujar un texto en pantalla [posicionX,posicionY, size, color]
    Raylib.DrawText("HOLA MAIN LOOP!", 50, 100,75, Color.Brown);
    // Finalizar el dibujo del Canvas 
    Raylib.EndDrawing();
}
// Cerrar ventana
Raylib.CloseWindow();
