using UnityEngine;
using UnityEngine.SceneManagement;

public static class GestorNiveles
{
    private static int bloquesRestantes;
    private static bool cargandoSiguienteNivel;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Inicializar()
    {
        Bloque.BloqueDestruido -= RegistrarBloqueDestruido;
        Bloque.BloqueDestruido += RegistrarBloqueDestruido;

        SceneManager.sceneLoaded -= AlCargarEscena;
        SceneManager.sceneLoaded += AlCargarEscena;

        bloquesRestantes = 0;
        cargandoSiguienteNivel = false;
    }

    private static void AlCargarEscena(Scene escena, LoadSceneMode modo)
    {
        cargandoSiguienteNivel = false;
        bloquesRestantes = Object.FindObjectsByType<Bloque>(FindObjectsSortMode.None).Length;

        Debug.Log($"Nivel '{escena.name}' cargado. Bloques restantes: {bloquesRestantes}.");
    }

    private static void RegistrarBloqueDestruido()
    {
        if (cargandoSiguienteNivel)
        {
            return;
        }

        bloquesRestantes = Mathf.Max(0, bloquesRestantes - 1);
        Debug.Log($"Bloques restantes: {bloquesRestantes}.");

        if (bloquesRestantes == 0)
        {
            AvanzarAlSiguienteNivel();
        }
    }

    private static void AvanzarAlSiguienteNivel()
    {
        int siguienteIndice = SceneManager.GetActiveScene().buildIndex + 1;

        if (siguienteIndice >= SceneManager.sceneCountInBuildSettings)
        {
            Debug.Log("¡Completaste todos los niveles!");
            return;
        }

        cargandoSiguienteNivel = true;
        SceneManager.LoadScene(siguienteIndice);
    }
}
