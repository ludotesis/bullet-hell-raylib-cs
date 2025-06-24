using Raylib_cs;                  
using System.Numerics;           

class Jugador                    
{

    const int ANCHO  = 16;
    const int ALTO = 16;

    Texture2D sprite;           
    Vector2 posicion;           
    public Rectangle hitbox;           
  
    float velocidad = 500f;
    bool activo;

    public Jugador(float posicionInicialX, float posicionInicialY)
    {
        posicion.X = posicionInicialX;
        posicion.Y = posicionInicialY;
        hitbox = new Rectangle(posicion, ANCHO, ALTO);
        activo = true;
    }

    public bool IsCollisionJugador(Rectangle otroHitbox) // Método para detectar colisiones
    {
        return Raylib.CheckCollisionRecs(hitbox, otroHitbox); // Devuelve true si hay colisión
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




