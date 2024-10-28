using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Ice : MonoBehaviour
{
    public CharaMove cM;
    public GameObject iceGround;
    public GameObject player;
    public Rigidbody2D rb;
   


    Vector3 iceScale;
    Vector3 icePosition;
    Vector3 playerPosition;
    Vector2 dir;
    public float speedX;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        iceScale = iceGround.transform.lossyScale; 
        icePosition = iceGround.transform.position;
    }
    
    void Update()
    {
        playerPosition = player.transform.position;
        isOnice();
    }

    public void isOnice()
    {
        
        if (playerPosition.y > icePosition.y && playerPosition.y < icePosition.y + 2f)
        {
            if (icePosition.x + (iceScale.x / 2) >= playerPosition.x && icePosition.x - (iceScale.x / 2) <= playerPosition.x)
            {
                cM.onIce = true;
                cM.canMove = false;
                cM.rb.velocityX = speedX * cM.direction;
            }
            else
            {
                Debug.Log(cM.onIce);
                if (cM.onIce)
                {
                    cM.onIce = false;
                    cM.canMove = true;
                }
            }
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireCube(iceGround.transform.position, Vector3.one * 2f);
    }
}
