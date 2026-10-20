using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GestorUIJuego : MonoBehaviour
{
    private static GestorUIJuego instancia;

    private GameObject panelPausa;
    private GameObject panelGameOver;
    private bool juegoPausado;
    private bool juegoTerminado;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void CrearInstancia()
    {
        if (instancia != null)
        {
            return;
        }

        GameObject gestor = new GameObject("GestorUIJuego");
        gestor.AddComponent<GestorUIJuego>();
    }

    private void Awake()
    {
        if (instancia != null && instancia != this)
        {
            Destroy(gameObject);
            return;
        }

        instancia = this;
        DontDestroyOnLoad(gameObject);
        CrearInterfaz();
        SceneManager.sceneLoaded += AlCargarEscena;
    }

    private void OnDestroy()
    {
        if (instancia == this)
        {
            SceneManager.sceneLoaded -= AlCargarEscena;
            instancia = null;
        }
    }

    public static void MostrarGameOver()
    {
        if (instancia != null)
        {
            instancia.ActivarGameOver();
        }
    }

    private void CrearInterfaz()
    {
        GameObject canvasObject = new GameObject("CanvasMenusJuego", typeof(RectTransform), typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
        canvasObject.transform.SetParent(transform, false);

        Canvas canvas = canvasObject.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 100;

        CanvasScaler scaler = canvasObject.GetComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080);
        scaler.matchWidthOrHeight = 0.5f;

        panelPausa = CrearPanel(canvas.transform, "PanelPausa");
        CrearTexto(panelPausa.transform, "TituloPausa", "JUEGO EN PAUSA", 150, 54);
        CrearBoton(panelPausa.transform, "BotonContinuar", "CONTINUAR", new Vector2(0, 35), new Vector2(360, 62), Continuar);
        CrearBoton(panelPausa.transform, "BotonReiniciar", "REINICIAR NIVEL", new Vector2(0, -45), new Vector2(360, 62), ReiniciarNivel);
        CrearBoton(panelPausa.transform, "BotonMenu", "MENU PRINCIPAL", new Vector2(0, -125), new Vector2(360, 62), VolverAlMenu);

        panelGameOver = CrearPanel(canvas.transform, "PanelGameOver");
        CrearTexto(panelGameOver.transform, "TituloGameOver", "GAME OVER", 105, 64);
        CrearBoton(panelGameOver.transform, "BotonReintentar", "REINTENTAR", new Vector2(0, 0), new Vector2(360, 62), ReiniciarNivel);
        CrearBoton(panelGameOver.transform, "BotonMenuGameOver", "MENU PRINCIPAL", new Vector2(0, -80), new Vector2(360, 62), VolverAlMenu);

        panelPausa.SetActive(false);
        panelGameOver.SetActive(false);
    }

    private GameObject CrearPanel(Transform parent, string nombre)
    {
        GameObject panel = new GameObject(nombre, typeof(RectTransform), typeof(Image));
        panel.transform.SetParent(parent, false);

        RectTransform rect = panel.GetComponent<RectTransform>();
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;

        Image fondo = panel.GetComponent<Image>();
        fondo.color = new Color(0.025f, 0.04f, 0.09f, 0.88f);
        return panel;
    }

    private Button CrearBoton(Transform parent, string nombre, string etiqueta, Vector2 posicion, Vector2 tamano, UnityAction accion)
    {
        GameObject botonObject = new GameObject(nombre, typeof(RectTransform), typeof(Image), typeof(Button));
        botonObject.transform.SetParent(parent, false);

        RectTransform rect = botonObject.GetComponent<RectTransform>();
        rect.anchorMin = new Vector2(0.5f, 0.5f);
        rect.anchorMax = new Vector2(0.5f, 0.5f);
        rect.pivot = new Vector2(0.5f, 0.5f);
        rect.anchoredPosition = posicion;
        rect.sizeDelta = tamano;

        Image imagen = botonObject.GetComponent<Image>();
        imagen.color = new Color(0.12f, 0.35f, 0.72f, 1f);

        Button boton = botonObject.GetComponent<Button>();
        boton.targetGraphic = imagen;
        boton.onClick.AddListener(accion);

        GameObject textoObject = new GameObject("Texto", typeof(RectTransform), typeof(TextMeshProUGUI));
        textoObject.transform.SetParent(botonObject.transform, false);
        RectTransform textoRect = textoObject.GetComponent<RectTransform>();
        textoRect.anchorMin = Vector2.zero;
        textoRect.anchorMax = Vector2.one;
        textoRect.offsetMin = new Vector2(8, 4);
        textoRect.offsetMax = new Vector2(-8, -4);

        TextMeshProUGUI texto = textoObject.GetComponent<TextMeshProUGUI>();
        texto.text = etiqueta;
        texto.font = Resources.Load<TMP_FontAsset>("Fonts & Materials/LiberationSans SDF");
        texto.fontSize = 28;
        texto.fontStyle = FontStyles.Bold;
        texto.alignment = TextAlignmentOptions.Center;
        texto.color = Color.white;
        texto.raycastTarget = false;

        return boton;
    }

    private void CrearTexto(Transform parent, string nombre, string contenido, float posicionY, float tamanoFuente)
    {
        GameObject textoObject = new GameObject(nombre, typeof(RectTransform), typeof(TextMeshProUGUI));
        textoObject.transform.SetParent(parent, false);

        RectTransform rect = textoObject.GetComponent<RectTransform>();
        rect.anchorMin = new Vector2(0.5f, 0.5f);
        rect.anchorMax = new Vector2(0.5f, 0.5f);
        rect.pivot = new Vector2(0.5f, 0.5f);
        rect.anchoredPosition = new Vector2(0, posicionY);
        rect.sizeDelta = new Vector2(900, 90);

        TextMeshProUGUI texto = textoObject.GetComponent<TextMeshProUGUI>();
        texto.text = contenido;
        texto.font = Resources.Load<TMP_FontAsset>("Fonts & Materials/LiberationSans SDF");
        texto.fontSize = tamanoFuente;
        texto.fontStyle = FontStyles.Bold;
        texto.alignment = TextAlignmentOptions.Center;
        texto.color = Color.white;
        texto.raycastTarget = false;
    }

    private void AlCargarEscena(Scene escena, LoadSceneMode modo)
    {
        juegoPausado = false;
        juegoTerminado = false;
        Time.timeScale = 1f;

        panelPausa.SetActive(false);
        panelGameOver.SetActive(false);

        if (escena.name == "MenuPrincipal")
        {
            VincularBotonMenu("Play", IniciarJuego);
            VincularBotonMenu("Exit", SalirDelJuego);
        }
        else
        {
            VincularBotonMenu("Button", AlternarPausa);
        }
    }

    private void VincularBotonMenu(string nombre, UnityAction accion)
    {
        GameObject botonObject = GameObject.Find(nombre);
        if (botonObject == null)
        {
            Debug.LogWarning($"No se encontró el botón '{nombre}' en el menú principal.");
            return;
        }

        Button boton = botonObject.GetComponent<Button>();
        if (boton == null)
        {
            Debug.LogWarning($"El objeto '{nombre}' no tiene un componente Button.");
            return;
        }

        boton.onClick.RemoveListener(accion);
        boton.onClick.AddListener(accion);
    }

    private void AlternarPausa()
    {
        if (juegoTerminado)
        {
            return;
        }

        juegoPausado = !juegoPausado;
        Time.timeScale = juegoPausado ? 0f : 1f;
        panelPausa.SetActive(juegoPausado);
    }

    private void Continuar()
    {
        juegoPausado = false;
        Time.timeScale = 1f;
        panelPausa.SetActive(false);
    }

    private void ActivarGameOver()
    {
        juegoTerminado = true;
        juegoPausado = false;
        Time.timeScale = 0f;
        panelPausa.SetActive(false);
        panelGameOver.SetActive(true);
    }

    private void ReiniciarNivel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void VolverAlMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuPrincipal");
    }

    private void IniciarJuego()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Nivel 1");
    }

    private void SalirDelJuego()
    {
        Application.Quit();
    }
}
