using UnityEngine;

public class Bloque_Fuego : Bloque
{

 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        resistencia = 1;
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    public override void RebotarBola()
    {
        base.RebotarBola();
    }
}
