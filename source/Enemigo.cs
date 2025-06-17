using Raylib_cs;                        // Importa funciones de Raylib (dibujar, cargar texturas, etc.)
using System.Numerics;                 // Importa estructuras como Vector2 para manejar posiciones

class Enemigo                          // Declara una clase llamada Enemigo
{
    Texture2D sprite;                  // Variable para almacenar la imagen del enemigo
    Vector2 posicion = new Vector2(50, 0);  // Posición inicial del enemigo (por defecto en (50,0))
    public Rectangle hitboxEnemigo;         // Área de colisión (pública para poder acceder desde fuera)

    public Enemigo(float posicionInicialX, float posicionInicialY) // Constructor con posición inicial
    {
        posicion.X = posicionInicialX;     // Asigna la coordenada X
        posicion.Y = posicionInicialY;     // Asigna la coordenada Y
        hitboxEnemigo = new Rectangle(posicion,16, 16); // Crea un hitbox de 16x16 en esa posición
    }

    public void CargarSprite()           // Método para cargar el sprite del enemigo
    {
        sprite = Raylib.LoadTexture("sprites/Enemigo.png"); // Carga la imagen desde archivo
    }
    
    public void DibujarSprite()         // Método para dibujar al enemigo en pantalla
    {
        Raylib.DrawTextureV(sprite, posicion, Color.White); // Dibuja el sprite en la posición actual
    }
}


