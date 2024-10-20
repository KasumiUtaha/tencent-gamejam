using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderBox : Mechanism
{
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            SetColliderOff();
        }
        else if(Input.GetMouseButtonDown(1))
        {
            SetColliderOn();
        }
    }
}
