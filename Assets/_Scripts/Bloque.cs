using UnityEngine;

public class Bloque : MonoBehaviour
{

    public int resistencia = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (resistencia <= 0) {
            Destroy(this.gameObject);
        }
    }


    public virtual void RebotarBola()
    {
       /// resistencia--;
    }

}
