using Raylib_cs;
using System.Numerics;

class Jugador
{
    Texture2D sprite;
    Vector2 posicion;
    Rectangle hitboxJugador;

    public Jugador(float posicionInicialX, float posicionInicialY)
    {
        posicion.X = posicionInicialX;
        posicion.Y = posicionInicialY;
        hitboxJugador = new Rectangle(posicion,16, 16);
    }

    public bool IsCollisionJugador(Rectangle otroHitbox)
    {
        return Raylib.CheckCollisionRecs(hitboxJugador, otroHitbox);
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




