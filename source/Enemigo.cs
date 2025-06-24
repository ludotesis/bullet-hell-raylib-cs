using Raylib_cs;                        
using System.Numerics;                 

class Enemigo                          
{
    const int ANCHO  = 16;
    const int ALTO = 16;

    Texture2D sprite;                  
    Vector2 posicion = new Vector2(50, 0);  
    public Rectangle hitbox;         

    public Enemigo(float posicionInicialX, float posicionInicialY)
    {
        posicion.X = posicionInicialX;     
        posicion.Y = posicionInicialY;     
        hitbox = new Rectangle(posicion, ANCHO, ALTO); 
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


