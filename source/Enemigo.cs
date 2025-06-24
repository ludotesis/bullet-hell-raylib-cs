using Raylib_cs;                        
using System.Numerics;

class Enemigo
{
    const int ANCHO = 16;
    const int ALTO = 16;

    //Texture2D sprite;
    Vector2 posicion;
    Vector2 posicionInicial;
    Animacion animacion;
    public Rectangle hitbox;

    float velocidad = 50f;

    public Enemigo(float posicionInicialX, float posicionInicialY)
    {
        posicion.X = posicionInicialX;
        posicion.Y = posicionInicialY;
        posicionInicial = posicion;
        hitbox = new Rectangle(posicion, ANCHO, ALTO);
        animacion = new Animacion(1f, 6f, 4, 2, 0);
    }

    public void CargarSprite()
    {
        //sprite = Raylib.LoadTexture("sprites/Enemigo.png");
        //Raylib.SetTextureFilter(sprite, TextureFilter.Point);
        animacion.CargarSpritesheet();
    }

    public void DibujarSprite()
    {
        //Raylib.DrawTextureV(sprite, posicion, Color.White);
        animacion.DibujarFrame(posicion, Color.White);
    }

    public void MoverHorizontal(bool haciaDerecha, float delta)
    {
        if (haciaDerecha)
        {
            posicion.X -= velocidad * delta;
        }
        else
        {
            posicion.X += velocidad * delta;
        }

        ActualizarHitbox();
        animacion.Actualizar(delta);
    }

    void ActualizarHitbox()
    {
        hitbox.X = posicion.X;
        hitbox.Y = posicion.Y;
    }

    public void Reiniciar()
    {
        posicion = posicionInicial;
    }

    public bool puedoMoverIzquierda(float limiteIzquierda)
    {
        return posicion.X > limiteIzquierda;
    }
}


