using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Events;

public class MostrarEventos : MonoBehaviour
{
    public UnityEvent MiEventoUnity; 
    public event EventHandler OnSpacePressed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        OnSpacePressed += TriggerSpacePressed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnSpacePressed?.Invoke(this, EventArgs.Empty);
            MiEventoUnity?.Invoke();
        }    
    }

    public void TriggerSpacePressed(object sender, EventArgs e)
    {
        Debug.Log("Evento escuchado correctamente!");
    }


    ///Funcion para mi event de Unity   
    public void TriggerMiEventoUnity()
    {
  Debug.Log("Evento Unity disparado correctamente!");
    }
}
