using UnityEngine;
using System;


public class MuestraSubscriptor : MonoBehaviour
{
    MostrarEventos subscriptor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        subscriptor = GetComponent <MostrarEventos>();
        subscriptor.OnSpacePressed += MensajeEscuchadoSubscriptor;
    }

private void MensajeEscuchadoSubscriptor(object sender, EventArgs e)
    {
        Debug.Log("Mensaje o Evento escuchado por el subscriptor desde otra clase!");
        subscriptor.OnSpacePressed -= MensajeEscuchadoSubscriptor;
    }
}
