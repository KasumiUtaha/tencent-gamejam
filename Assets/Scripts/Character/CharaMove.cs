
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaMove : MonoBehaviour
{
    public float jumpHeight;
    public float moveSpeed;
    public float moveSpeedAir;

    public Transform groundDetector1;
    public Transform groundDetector2;
    public LayerMask groundLayer;
    public float groundDetectDis;
    [HideInInspector]
    public float onPlaneVelocity = 0f;

    [HideInInspector]
    public bool canMove  = true;
    public bool player_move = false;

    private Rigidbody2D rb;
    private Collider2D circleCollider;

    [HideInInspector]
    public bool onGround { get; private set; } = false;
    [HideInInspector]
    public int direction { get; private set; } = 1;
    [HideInInspector]
    public bool walking { get; private set; } = false;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        PhysicsDetect();
        //Debug.Log(rb.velocity);
        //Debug.Log(canMove);
        if(canMove && Input.GetButton("Jump"))
        {
            //Debug.Log(onGround);
            //Debug.Log(rb.velocityY);
            if(onGround && rb.velocityY <= 0.001f)
            {
                rb.velocity = new Vector2(rb.velocity.x, Mathf.Sqrt(-Physics2D.gravity.y * rb.gravityScale * 2 * jumpHeight));
                //Debug.Log("Jump " + rb.velocityY);
            }
        }
        //Debug.Log(rb.velocityY);
        float inputVelocity = 0f;
        if(canMove)
        {
            if(onGround)
            {
                inputVelocity = Input.GetAxisRaw("Horizontal") * moveSpeed;
            }
            else
            {
                inputVelocity = Input.GetAxisRaw("Horizontal") * moveSpeedAir;
            }
            direction =
                direction > 0 ?
                (inputVelocity > -0.01 ? 1 : -1) :
                (inputVelocity > 0.01 ? 1 : -1) ;
            walking = inputVelocity > 0.1f || inputVelocity < -0.1f;
        }
        rb.velocityX = inputVelocity + onPlaneVelocity;
    }

    private void PhysicsDetect()
    {
        RaycastHit2D hit1 = Physics2D.Raycast(groundDetector1.position, Vector2.down, groundDetectDis, groundLayer);
        RaycastHit2D hit2 = Physics2D.Raycast(groundDetector2.position, Vector2.down, groundDetectDis, groundLayer);

        if(hit1 || hit2)
        {
            onGround = true;
        }
        else
        {
            onGround = false;
        }
        //Debug.Log(onGround);
    }
}
