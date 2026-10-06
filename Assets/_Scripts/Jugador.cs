using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;

public class Jugador : MonoBehaviour {



    [SerializeField] public int limiteX = 23;
    [SerializeField] public int velocidadPaddle = 7;

    Vector3 mousePosition2D;
    Vector3 mousePosition3D;
    



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition2D = Input.mousePosition;
        mousePosition2D.z = -Camera.main.transform.position.z;
        mousePosition3D = Camera.main.ScreenToWorldPoint(mousePosition2D);


        if (Input.GetKey(KeyCode.RightArrow))
        {
           /// MoverJugador();
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            /// MoverJugador();
        }



        Vector3 pos = this.transform.position;
        pos.x = mousePosition3D.x;
        
        if (pos.x < -limiteX)
        {
            pos.x = -limiteX;
        }   else if (pos.x > limiteX)
        {  pos.x = limiteX;}

        this.transform.position = pos;

    }
}
