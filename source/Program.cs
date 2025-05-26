// Importar Raylib 
using Raylib_cs;
// Inicializar ventana
Raylib.InitWindow(320, 180, "Bullet Hell con Raylib");
//Configurar ventana sin border
Raylib.ToggleBorderlessWindowed();
// Hasta que la aplicacion no se cierre
while (!Raylib.WindowShouldClose())
{
    // Configurar el Canvas para comenzar a dibujar
    Raylib.BeginDrawing();
    // Establecer el color de fondo
    Raylib.ClearBackground(Color.Beige);
    // Dibujar un texto en pantalla [posicion en X,posicion en Y, tamaño, color]
    Raylib.DrawText("HOLA MAIN LOOP!", 50, 100,75, Color.Brown);
    // Dibujar circulo en el centro de la pantalla [X, Y, tamaño, color]
    Raylib.DrawCircle(Raylib.GetScreenWidth()/2, Raylib.GetScreenHeight()/2, 50, Color.Red);
    // Finalizar el dibujo del Canvas 
    Raylib.EndDrawing();
}
// Cerrar ventana
Raylib.CloseWindow();
