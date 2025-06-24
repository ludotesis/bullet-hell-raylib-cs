using Raylib_cs;                  
using System.Numerics;

class Jugador
{

    const int ANCHO = 16;
    const int ALTO = 16;

    Texture2D sprite;
    Vector2 posicion;
    public Rectangle hitbox;

    float velocidad = 50f;

    public Jugador(float posicionInicialX, float posicionInicialY)
    {
        posicion.X = posicionInicialX;
        posicion.Y = posicionInicialY;
        hitbox = new Rectangle(posicion, ANCHO, ALTO);
    }

    public bool IsCollisionJugador(Rectangle otroHitbox)
    {
        return Raylib.CheckCollisionRecs(hitbox, otroHitbox);
    }

    public void CargarSprite()
    {
        sprite = Raylib.LoadTexture("sprites/Jugador.png");
        Raylib.SetTextureFilter(sprite, TextureFilter.Point);
    }

    public void DibujarSprite()
    {
        Raylib.DrawTextureV(sprite, posicion, Color.White);
    }

    public void MoverVertical(bool haciaArriba, float delta)
    {
        if (haciaArriba)
        {
            posicion.Y -= velocidad * delta;
        }
        else
        {
            posicion.Y += velocidad * delta;
        }

        ActualizarHitbox();
    }

    void ActualizarHitbox()
    {
        hitbox.X = posicion.X;
        hitbox.Y = posicion.Y;
    }

    public bool puedoMoverArriba(float limiteArriba)
    {
        return posicion.Y > limiteArriba;
    }

    public bool puedoMoverAbajo(float limiteAbajo)
    {
        return posicion.Y < (limiteAbajo - ALTO);
    }

    public Vector2 ObtenerPosicionJugador()
    {
        return posicion;
    }
}




