using System.Numerics;
using Raylib_cs;

class Animacion
{
    float duracion;
    float velocidad;
    int frames;
    int bloquearX;
    int bloquearY;


    float tiempoActual = 0;
    int frameActual = 0;

    Texture2D spritesheet;
    Rectangle frame;

    public Animacion(float duracion, float velocidad, int frames, int bloquearX, int bloquearY)
    {
        this.duracion = duracion;
        this.velocidad = velocidad;
        this.frames = frames;
        this.bloquearX = bloquearX;
        this.bloquearY = bloquearY;
    }

    public void CargarSpritesheet()
    {
        spritesheet = Raylib.LoadTexture("sprites/Enemigo/Walk.png");
        Raylib.SetTextureFilter(spritesheet, TextureFilter.Point);
        frame = new Rectangle(
                        0f,
                        0f,
                      spritesheet.Width / frames,
                      spritesheet.Height / frames);
    }

    public void Actualizar(float delta)
    {
        tiempoActual += delta;
        Console.WriteLine(tiempoActual);

        if (tiempoActual >= duracion / velocidad)
        {
            tiempoActual = 0f;
            frameActual++;

            if (frameActual >= frames) frameActual = 0;

            if (bloquearX == 0)
            {
                frame.X = frameActual * frame.Width;
            }
            else
            {
                frame.X = bloquearX * frame.Width;
            }

            if (bloquearY == 0)
            {
                frame.Y = frameActual * frame.Width;
            }
            else
            {
                frame.Y = bloquearY * frame.Width;
            }
        }
    }

    public void DibujarFrame(Vector2 posicion, Color color)
    {
        Raylib.DrawTextureRec(spritesheet, frame, posicion, color);
    }

}