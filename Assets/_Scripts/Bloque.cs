using System;
using UnityEngine;
using UnityEngine.Events;

public class Bloque : MonoBehaviour
{
    public static event Action BloqueDestruido;
    private bool yaDestruido;
    public int resistencia = 1;
    public UnityEvent AumentarPuntaje;

    public void OnCollisionEnter(Collision collision)
    {
        if (yaDestruido)
        {
            return;
        }

        if (collision.gameObject.CompareTag("Bola"))
        {
            RebotarBola(collision);
            RebotarBola(); // Llamada a la sobrecarga sin parámetros

            resistencia--;

            if (resistencia <= 0)
            {
                yaDestruido = true;
                AumentarPuntaje?.Invoke();
                BloqueDestruido?.Invoke();
                Destroy(this.gameObject);
            }
        }
    }

    public virtual void RebotarBola(Collision collision)
    {
        if (collision.contacts.Length > 0)
        {
            Vector3 direccion = collision.contacts[0].point - transform.position;
            direccion = direccion.normalized;

            Bola bolaScript = collision.gameObject.GetComponent<Bola>();
            if (bolaScript != null && collision.rigidbody != null)
            {
                collision.rigidbody.linearVelocity = bolaScript.velocidadBola * direccion;
            }
        }
    }

    // Método virtual sin parámetros para que las clases derivadas lo puedan sobrescribir
    public virtual void RebotarBola()
    {
       resistencia--;
    }
}