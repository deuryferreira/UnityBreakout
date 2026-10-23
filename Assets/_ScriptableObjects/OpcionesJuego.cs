using UnityEngine;

public enum DificultadJuego
{
    Facil,
    Normal,
    Dificil
}

[CreateAssetMenu(fileName = "OpcionesJuego", menuName = "Herramientas/Opciones de juego", order = 2)]
public class OpcionesJuego : PuntajePersistente
{
    private static OpcionesJuego instancia;

    [Header("Partida")]
    public DificultadJuego dificultad = DificultadJuego.Normal;
    [Min(1f)] public float velocidadBola = 30f;

    [Header("Multiplicador de resistencia por dificultad")]
    [Min(0.1f)] public float resistenciaFacil = 0.75f;
    [Min(0.1f)] public float resistenciaNormal = 1f;
    [Min(0.1f)] public float resistenciaDificil = 1.5f;

    public static OpcionesJuego Instancia
    {
        get
        {
            if (instancia == null)
            {
                instancia = Resources.Load<OpcionesJuego>("OpcionesJuego");
            }

            return instancia;
        }
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void CargarOpcionesGuardadas()
    {
        instancia = Resources.Load<OpcionesJuego>("OpcionesJuego");
        if (instancia != null)
        {
            instancia.LoadScore();
        }
    }

    public int CalcularResistencia(int resistenciaBase)
    {
        float multiplicador = dificultad switch
        {
            DificultadJuego.Facil => resistenciaFacil,
            DificultadJuego.Dificil => resistenciaDificil,
            _ => resistenciaNormal
        };

        return Mathf.Max(1, Mathf.CeilToInt(resistenciaBase * multiplicador));
    }

    public void EstablecerDificultad(DificultadJuego nuevaDificultad)
    {
        dificultad = nuevaDificultad;
        SaveScore();
    }

    public void EstablecerVelocidadBola(float nuevaVelocidad)
    {
        velocidadBola = Mathf.Max(1f, nuevaVelocidad);
        SaveScore();
    }
}
