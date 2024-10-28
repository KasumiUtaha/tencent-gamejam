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
    public IceCollider iceCollider;
   


    Vector3 iceScale;
    Vector3 icePosition;
    Vector3 playerPosition;
    Vector2 dir;
    int onIceCount;
    public float speedX;
    public float height;
    float width;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        iceScale = iceGround.transform.lossyScale;
        width = iceScale.x;
        icePosition = iceGround.transform.position;
    }
    
    void Update()
    {
        onIceCount = iceCollider.onIceCount;
        Debug.Log(onIceCount);
        playerPosition = player.transform.position;
        isOnice();
    }
    public void isOnice()
    {
        if (onIceCount > 0)
        {
            if (playerPosition.y > icePosition.y && playerPosition.y < icePosition.y + height)
            {
                if (icePosition.x + (iceScale.x / 2) >= playerPosition.x && icePosition.x - (iceScale.x / 2) <= playerPosition.x)
                {
                    cM.onIce = true;
                    cM.canMove = false;
                    cM.rb.velocityX = speedX * cM.direction;
                }
                else
                {
                    //Debug.Log(cM.onIce);
                    if (cM.onIce)
                    {
                        cM.onIce = false;
                        cM.canMove = true;
                    }
                }
            }
        }
    }
    void OnDrawGizmos()
    {
        Vector3 ice = new Vector3(iceGround.transform.position.x, iceGround.transform.position.y + iceScale.y / 2,0);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(ice, new Vector3(width, height, 0));
    }
}
