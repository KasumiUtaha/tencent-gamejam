using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCollider : MonoBehaviour
{
    public Ice ice;
    public int onIceCount;
    void Start()
    {
        onIceCount = 0;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Collider2D>().name == "Collider")
            onIceCount++;
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Collider2D>().name == "Collider")
            onIceCount=0;
    }

    void Update()
    {
        Debug.Log(onIceCount);
        
        

    }
}
