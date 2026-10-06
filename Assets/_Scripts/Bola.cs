using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bola : MonoBehaviour
{ 

    public bool GameIsStarted = false;
    [SerializeField] public float velocidadBola = 13.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        Vector3 posicionInicial = GameObject.FindGameObjectWithTag("Jugador").transform.position;
        posicionInicial.y += 3;
        this.transform.position = posicionInicial;
        this.transform.SetParent(GameObject.FindGameObjectWithTag("Jugador").transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !GameIsStarted)
        {
            GameIsStarted = true;
            this.transform.SetParent(null);
            GetComponent<Rigidbody>().linearVelocity = velocidadBola * Vector3.up;
        }
    }
}
