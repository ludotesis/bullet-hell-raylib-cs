using Raylib_cs;                  
using System.Numerics;

class Proyectil
{

    const int ANCHO = 8;
    const int ALTO = 8;

    Texture2D sprite;
    Vector2 posicion;
    public Rectangle hitbox;

    float velocidad = 100f;
    Vector2 margenOrigen = new Vector2(ANCHO, ALTO/2);
    bool activo;

    public Proyectil()
    {
        hitbox = new Rectangle(Vector2.Zero, ANCHO, ALTO);
        activo = false;
    }

    public void CargarSprite(string ruta)
    {
        sprite = Raylib.LoadTexture(ruta);
        Raylib.SetTextureFilter(sprite, TextureFilter.Point);
        sprite.Width = ANCHO;
        sprite.Height = ALTO;
    }

    public bool IsCollisionProyectil(Rectangle otroHitbox)
    {
        if (activo)
        {
          return Raylib.CheckCollisionRecs(hitbox, otroHitbox);  
        } 
        return false;
    }

    public void DibujarSprite()
    {
        if (activo)
        {
            Raylib.DrawTextureV(sprite, posicion, Color.White);
        }

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
    }

    void ActualizarHitbox()
    {
        hitbox.X = posicion.X;
        hitbox.Y = posicion.Y;
    }

    public bool puedoMoverDerecha(float limiteDerecha)
    {
        return activo && posicion.X < limiteDerecha;
    }

    public bool puedoDisparar()
    {
        return !activo;
    }


    public void Disparar(Vector2 origenDisparo)
    {
        posicion = origenDisparo + margenOrigen;
        activo = true;
    }
    
     public void Reiniciar()
    {
        activo = false;
    }
 
}