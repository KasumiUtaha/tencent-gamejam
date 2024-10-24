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
    int onIceCount;
    public float speedX;

    void Start()
    {

        iceScale = iceGround.transform.lossyScale; 
        icePosition = iceGround.transform.position;
        onIceCount = 0;
        dir = Vector2.zero;
    }
    
    void Update()
    {
        

        playerPosition = player.transform.position;
        isOnice();
   
    }

    public void isOnice()
    {
        if (playerPosition.y > icePosition.y && playerPosition.y <= icePosition.y + cM.jumpHeight + 1f)
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
    //public void isDash()
    //{
    //    if (playerPosition.y > icePosition.y)
    //    {
    //        if (icePosition.x + (iceScale.x / 2) == playerPosition.x || icePosition.x - (iceScale.x / 2) == playerPosition.x)
    //        {
    //            //if (onIceCount == 0)
    //            //{
    //            //    //Dash( );
    //            //}
    //            onIceCount++;
    //            //if (onIceCount == 1)
    //            //{
    //            //    rb.constraints = RigidbodyConstraints2D.FreezePosition;
    //            //    rb.constraints = ~RigidbodyConstraints2D.FreezePosition;
    //            //}
    //            //if(onIceCount > 1 )
    //            //{
    //            //    onIceCount = 0;
    //            //}
    //        }

    //    }
    //}

    
}
