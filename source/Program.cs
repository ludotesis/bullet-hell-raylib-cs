using System.Numerics;
using Raylib_cs;                    

class Program
{
    public static void Main()
    {
        Jugador jugador = new Jugador(50f, 50f);
        Proyectil shuriken = new Proyectil();
        Enemigo enemigo1 = new Enemigo(300, 10);
        Enemigo enemigo2 = new Enemigo(300, 60);
        Enemigo enemigo3 = new Enemigo(300, 110);
        Enemigo enemigo4 = new Enemigo(300, 160);

        int anchoVentana = 320;
        int altoVentana = 180;
        
        Raylib.InitWindow(anchoVentana, altoVentana, "Camara2D y Zoom");
        Raylib.ToggleBorderlessWindowed();
        int pantallaAncho = Raylib.GetScreenWidth();
        int pantallaAlto = Raylib.GetScreenHeight();

        float zoomAncho = pantallaAncho / (float)anchoVentana;
        float zoomAlto = pantallaAlto / (float)altoVentana;
        float zoom = MathF.Floor(MathF.Min(zoomAncho, zoomAlto));
        float deltaTime = 0f;
        
        Camera2D camera = new Camera2D();
        camera.Target = Vector2.Zero;   
        camera.Offset = Vector2.Zero;   
        camera.Rotation = 0f;           
        camera.Zoom = zoom;             
        
        jugador.CargarSprite();
        shuriken.CargarSprite("sprites/Shuriken.png");
        enemigo1.CargarSprite();
        enemigo2.CargarSprite();
        enemigo3.CargarSprite();
        enemigo4.CargarSprite();

        Music musicaFondo;
        float volumenMusica  = 0.8f;
    
        Raylib.InitAudioDevice();
        musicaFondo = Raylib.LoadMusicStream("sonidos/Dungeon.ogg");
        Raylib.SetMusicVolume(musicaFondo, volumenMusica);
        shuriken.InicializarSFX();
        Raylib.PlayMusicStream(musicaFondo);

        while (!Raylib.WindowShouldClose())
        {
            deltaTime = Raylib.GetFrameTime();
            //========= MUSICA ======
            Raylib.UpdateMusicStream(musicaFondo);

            if (Raylib.IsKeyDown(KeyboardKey.Up) && jugador.puedoMoverArriba(0))
            {
                jugador.MoverVertical(true, deltaTime);
            }

            if (Raylib.IsKeyDown(KeyboardKey.Down) && jugador.puedoMoverAbajo(altoVentana))
            {
                jugador.MoverVertical(false, deltaTime);
            }

            if (Raylib.IsKeyPressed(KeyboardKey.F) && shuriken.puedoDisparar())
            {
                shuriken.Disparar(jugador.ObtenerPosicionJugador());
                shuriken.DisparoSFX();
            }

            if (shuriken.puedoMoverDerecha(anchoVentana))
            {
                shuriken.MoverHorizontal(false, deltaTime);
            }
            else
            {
                shuriken.Reiniciar();
            }

            if (enemigo1.puedoMoverIzquierda(0))
            {
                enemigo1.MoverHorizontal(true, deltaTime);
            }
            else
            {
                enemigo1.Reiniciar();
            }

            if (enemigo2.puedoMoverIzquierda(0))
            {
                enemigo2.MoverHorizontal(true, deltaTime);
            }
            else
            {
                enemigo2.Reiniciar();
            }

            if (enemigo3.puedoMoverIzquierda(0))
            {
                enemigo3.MoverHorizontal(true, deltaTime);
            }
            else
            {
                enemigo3.Reiniciar();
            }

            if (enemigo4.puedoMoverIzquierda(0))
            {
                enemigo4.MoverHorizontal(true, deltaTime);
            }
            else
            {
                enemigo4.Reiniciar();
            }

            if (jugador.IsCollisionJugador(enemigo1.hitbox))
            {
                enemigo1.Reiniciar();
            }

            if (jugador.IsCollisionJugador(enemigo2.hitbox))
            {
                enemigo2.Reiniciar();
            }

            if (jugador.IsCollisionJugador(enemigo3.hitbox))
            {
                enemigo3.Reiniciar();
            }

            if (jugador.IsCollisionJugador(enemigo4.hitbox))
            {
                enemigo4.Reiniciar();
            }

            if (shuriken.IsCollisionProyectil(enemigo1.hitbox))
            {
                shuriken.Reiniciar();
                shuriken.HitSFX();
                enemigo1.Reiniciar();
            }

            if (shuriken.IsCollisionProyectil(enemigo2.hitbox))
            {
                shuriken.Reiniciar();
                shuriken.HitSFX();
                enemigo2.Reiniciar();
            }

            if (shuriken.IsCollisionProyectil(enemigo3.hitbox))
            {
                shuriken.Reiniciar();
                shuriken.HitSFX();
                enemigo3.Reiniciar();
            }

            if (shuriken.IsCollisionProyectil(enemigo4.hitbox))
            {
                shuriken.Reiniciar();
                shuriken.HitSFX();
                enemigo4.Reiniciar();
            }

            Raylib.BeginDrawing();
            Raylib.BeginMode2D(camera);

            Raylib.DrawRectangle(0, 0, 320, 180, Color.DarkBlue);

            jugador.DibujarSprite();
            shuriken.DibujarSprite();
            enemigo1.DibujarSprite();
            enemigo2.DibujarSprite();
            enemigo3.DibujarSprite();
            enemigo4.DibujarSprite();

            Raylib.DrawRectangleRec(jugador.hitbox, Raylib.ColorAlpha(Color.Blue, 0.5f));
            Raylib.DrawRectangleRec(shuriken.hitbox, Raylib.ColorAlpha(Color.Green, 0.5f));
            Raylib.DrawRectangleRec(enemigo1.hitbox, Raylib.ColorAlpha(Color.Red, 0.5f));
            Raylib.DrawRectangleRec(enemigo2.hitbox, Raylib.ColorAlpha(Color.Red, 0.5f));
            Raylib.DrawRectangleRec(enemigo3.hitbox, Raylib.ColorAlpha(Color.Red, 0.5f));
            Raylib.DrawRectangleRec(enemigo4.hitbox, Raylib.ColorAlpha(Color.Red, 0.5f));

            //Raylib.DrawFPS(10, 150);
            Raylib.EndMode2D();
            Raylib.EndDrawing();
        }

        // Descargar Musica
        Raylib.UnloadMusicStream(musicaFondo);
        // Descargar Sonidos
        shuriken.DeInicializarSFX();
        // Cerrar dispositivo de audio
        Raylib.CloseAudioDevice();
        // Cerrar Ventana
        Raylib.CloseWindow();
    }
}
