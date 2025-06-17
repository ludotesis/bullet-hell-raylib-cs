using Raylib_cs;                  // Importa funciones de Raylib para gráficos y manejo de recursos
using System.Numerics;           // Importa estructuras matemáticas como Vector2

class Jugador                    // Declara una clase llamada Jugador
{
    Texture2D sprite;           // Variable para almacenar la imagen del jugador
    Vector2 posicion;           // Almacena la posición del jugador en la pantalla
    Rectangle hitboxJugador;    // Rectángulo que representa el área de colisión

    public Jugador(float posicionInicialX, float posicionInicialY) // Constructor: recibe coordenadas iniciales
    {
        posicion.X = posicionInicialX;         // Asigna coordenada X
        posicion.Y = posicionInicialY;         // Asigna coordenada Y
        hitboxJugador = new Rectangle(posicion,16, 16); // Crea hitbox de 16x16 en esa posición
    }

    public bool IsCollisionJugador(Rectangle otroHitbox) // Método para detectar colisiones
    {
        return Raylib.CheckCollisionRecs(hitboxJugador, otroHitbox); // Devuelve true si hay colisión
    }

    public void CargarSprite()              // Método para cargar la imagen del jugador
    {
        sprite = Raylib.LoadTexture("sprites/Jugador.png"); // Carga el sprite desde un archivo
    }

    public void DibujarSprite()            // Método para dibujar al jugador
    {
        Raylib.DrawTextureV(sprite, posicion, Color.White); // Dibuja el sprite en pantalla
    }
}




