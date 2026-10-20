using UnityEngine;

public class ControlBordes : MonoBehaviour
{
    [Header("Configurar en el editor")]
    public float radio = 1f;
    public bool mantenerEnPantalla = false;

    [Header("Configuraciones dinamicas")]
    public bool estaEnPantalla = true;
    public float anchoCamara;
    public float altoCamara;
    public bool salioDerecha, salioIzquierda, salioArriba, salioAbajo;

    public void Awake()
    {
        if (Camera.main != null)
        {
            altoCamara = Camera.main.orthographicSize;
            anchoCamara = Camera.main.aspect * altoCamara;
        }
        else
        {
            Debug.LogError("No se encontró la MainCamera ortográfica en la escena.");
        }
    }

    private void LateUpdate()
    {
        Vector3 pos = transform.position;

        // Comprobación horizontal
        if (pos.x > anchoCamara - radio)
        {
            pos.x = anchoCamara - radio;
            salioDerecha = true;
        }
        else if (pos.x < -anchoCamara + radio)
        {
            pos.x = -anchoCamara + radio;
            salioIzquierda = true;
        }

        // Comprobación vertical
        if (pos.y > altoCamara - radio)
        {
            pos.y = altoCamara - radio;
            salioArriba = true;
        }
        else if (pos.y < -altoCamara + radio)
        {
            pos.y = -altoCamara + radio;
            salioAbajo = true;
        }

        // Restringir posición si mantenerEnPantalla está activo
        if (mantenerEnPantalla)
        {
            transform.position = pos;
        }

        // Estado general de visibilidad
        estaEnPantalla = !(salioDerecha || salioIzquierda || salioArriba || salioAbajo);
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;
        Gizmos.color = Color.green;
        Vector3 tamanoBorde = new Vector3(anchoCamara * 2, altoCamara * 2, 0);
        Gizmos.DrawWireCube(Vector3.zero, tamanoBorde);
    }
}