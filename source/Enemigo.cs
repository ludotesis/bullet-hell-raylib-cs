using Raylib_cs;
using System.Numerics;

class Enemigo
{
    Texture2D sprite;
    Vector2 posicion = new Vector2(50, 0);
    public Rectangle hitboxEnemigo;

    public Enemigo(float posicionInicialX, float posicionInicialY)
    {
        posicion.X = posicionInicialX;
        posicion.Y = posicionInicialY;
        hitboxEnemigo = new Rectangle(posicion,16, 16);
    }

    public void CargarSprite()
    {
        sprite = Raylib.LoadTexture("sprites/Enemigo.png");
    }
    
    public void DibujarSprite()
    {
        Raylib.DrawTextureV(sprite, posicion, Color.White);
    }
}



