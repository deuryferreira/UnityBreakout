using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bola : MonoBehaviour
{
    public bool GameIsStarted = false;
    [SerializeField] public float velocidadBola = 30.0f;

    private Rigidbody rb;
    private ControlBordes control;
    private bool gameOverIniciado;
    public UnityEvent BolaDestruida;

    void Start()
    {
        // 1. Obtener componente de física
        rb = GetComponent<Rigidbody>();

        // 2. Buscar ControlBordes localmente o en la escena
        control = GetComponent<ControlBordes>();
        if (control == null)
        {
            control = Object.FindFirstObjectByType<ControlBordes>();
        }

        if (control == null)
        {
            Debug.LogError("No se encontró ningún componente 'ControlBordes' en la escena ni en este GameObject.");
        }

        // 3. Vincular posición inicial al Jugador
        GameObject jugador = GameObject.FindGameObjectWithTag("Jugador");
        if (jugador != null)
        {
            Vector3 posicionInicial = jugador.transform.position;
            posicionInicial.y += 3f;
            transform.position = posicionInicial;
            transform.SetParent(jugador.transform);
        }
    }

    void Update()
    {
        // Manejo de colisiones de bordes
        if (control != null)
        {
            if (control.salioAbajo && !gameOverIniciado)
            {
                gameOverIniciado = true;
                GestorUIJuego.MostrarGameOver();
                BolaDestruida?.Invoke();
                Destroy(this.gameObject);
                return;
            }

            if (control.salioArriba)
            {
                Debug.Log("La Bola Tocó Arriba");
                Vector3 vel = rb.linearVelocity;
                vel.y = -Mathf.Abs(vel.y); // Rebote hacia abajo
                rb.linearVelocity = vel.normalized * velocidadBola;
                control.salioArriba = false;
            }

            if (control.salioIzquierda)
            {
                Debug.Log("La Bola Tocó la Izquierda");
                Vector3 vel = rb.linearVelocity;
                vel.x = Mathf.Abs(vel.x); // Rebote hacia la derecha
                rb.linearVelocity = vel.normalized * velocidadBola;
                control.salioIzquierda = false;
            }

            if (control.salioDerecha)
            {
                Debug.Log("La Bola Tocó la Derecha");
                Vector3 vel = rb.linearVelocity;
                vel.x = -Mathf.Abs(vel.x); // Rebote hacia la izquierda
                rb.linearVelocity = vel.normalized * velocidadBola;
                control.salioDerecha = false;
            }
        }

        // Lanzamiento inicial de la bola con clic izquierdo
        if (Input.GetMouseButtonDown(0) && !GameIsStarted)
        {
            GameIsStarted = true;
            transform.SetParent(null);
            if (rb != null)
            {
                rb.linearVelocity = Vector3.up * velocidadBola;
            }
        }
    }

    public void HabilitarControl()
    {
        if (control != null)
        {
            control.enabled = true;
        }
    }
}
