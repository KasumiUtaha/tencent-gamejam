using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : Mechanism
{
    public Rigidbody2D boxRb;
    public bool shouldStop = false;

    private new void Start()
    {
        boxRb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.position.y < transform.position.y)
        {
            shouldStop = true;
        }
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
