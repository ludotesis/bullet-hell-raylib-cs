using Raylib_cs;
using System.Numerics;

class Jugador
{
    Texture2D sprite;
    Vector2 posicion;

    public Jugador(float posicionInicialX, float posicionInicialY)
    {
        posicion.X = posicionInicialX;
        posicion.Y = posicionInicialY;
    }

    public void CargarSprite()
    {
        sprite = Raylib.LoadTexture("sprites/Jugador.png");
    }
    
    public void DibujarSprite()
    {
        Raylib.DrawTextureV(sprite, posicion, Color.White);
    }
}



