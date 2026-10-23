using UnityEngine;

public class Bloque : MonoBehaviour
{
    public static event System.Action BloqueDestruido;

    private bool yaDestruido;
    public int resistencia = 1;
    public UnityEngine.Events.UnityEvent AumentarPuntaje;

    protected virtual void Start()
    {
        OpcionesJuego opciones = OpcionesJuego.Instancia;
        if (opciones != null)
        {
            resistencia = opciones.CalcularResistencia(resistencia);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (yaDestruido || !collision.gameObject.CompareTag("Bola"))
        {
            return;
        }

        RebotarBola(collision);
        resistencia--;

        if (resistencia <= 0)
        {
            yaDestruido = true;
            AumentarPuntaje?.Invoke();
            BloqueDestruido?.Invoke();
            Destroy(gameObject);
        }
    }

    public virtual void RebotarBola(Collision collision)
    {
        if (collision.contacts.Length == 0)
        {
            return;
        }

        Vector3 direccion = (collision.contacts[0].point - transform.position).normalized;
        Bola bolaScript = collision.gameObject.GetComponent<Bola>();
        if (bolaScript != null && collision.rigidbody != null)
        {
            collision.rigidbody.linearVelocity = bolaScript.velocidadBola * direccion;
        }
    }
}
