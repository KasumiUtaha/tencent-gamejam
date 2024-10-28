using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : Mechanism
{
    public Rigidbody2D boxRb;

    private new void Start()
    {
        boxRb = GetComponent<Rigidbody2D>();
    }
    public override void TimePause()
    {
        boxRb.constraints = RigidbodyConstraints2D.FreezeAll ;

    }

    public override void TimeStart()
    {
        boxRb.constraints = RigidbodyConstraints2D.FreezeRotation;
        boxRb.AddForce(Vector2.down * 0.1f);
    }
}
