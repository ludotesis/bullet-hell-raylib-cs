using Raylib_cs;
using System.Numerics;

class Enemigo
{
    Texture2D sprite;
    Vector2 posicion = new Vector2(50, 0);

    public void CargarSprite()
    {
        sprite = Raylib.LoadTexture("sprites/Enemigo.png");
    }
    
    public void DibujarSprite()
    {
        Raylib.DrawTextureV(sprite, posicion, Color.White);
    }
}



