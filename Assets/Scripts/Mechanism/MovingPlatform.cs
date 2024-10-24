using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngineInternal;

public class MovingPlatform : Mechanism
{
    [SerializeField] private Transform leftPoint;
    [SerializeField] private Transform rightPoint;
    [SerializeField] private float movingSpeed = 3;
    [SerializeField] private int direction = 1;
    [SerializeField] private Transform rightDetector;
    [SerializeField] private Transform leftDetector;
    [SerializeField] private bool startMoving = true;
    [SerializeField] private GameObject onPlaneObject;
    bool isMoving = false;
    private float adjustLength;
    public CharaMove charaMove;
    bool onPlane = false;

    private void Start()
    {
        base.Start();
        adjustLength = transform.localScale.x / 2.0f;
        if(startMoving) StartCoroutine(Moving());
    }

    public override void TimePause()
    {
        StopAllCoroutines(); isMoving = false;
        if(onPlane) charaMove.onPlaneVelocity = 0;
    }

    public override void TimeStart()
    {
        if(!isMoving)
        StartCoroutine(Moving());
    }


    IEnumerator Moving()
    {
        isMoving = true;
        while(true)
        {
            if (transform.position.x + direction * adjustLength >= rightPoint.position.x) direction = -1;
            else if (transform.position.x + direction * adjustLength <= leftPoint.position.x) direction = 1;
            //if (ColliderDetect()) direction = -direction;
            Vector3 moveDirection = direction == 1 ? Vector3.right : Vector3.left;
            if(onPlane) 
            {
                charaMove.onPlaneVelocity = movingSpeed * direction;
            }
            transform.Translate(moveDirection * movingSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.layer == 6 && collision.gameObject.transform.position.y > transform.position.y) onPlane = true;
        else if (collision.gameObject.transform.position.y > transform.position.y)
        {
            onPlaneObject = collision.gameObject;
            collision.gameObject.transform.SetParent(transform);
        }
        Debug.Log(onPlane);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6) 
        {
            onPlane = false;
            charaMove.onPlaneVelocity = 0;
        }
        else if(collision.gameObject == onPlaneObject)
        {
            onPlaneObject.transform.SetParent(null);
            onPlaneObject = null;
        }
    }
    public override void SetColliderOff()
    {
        return;
    }

    public override void SetColliderOn()
    {
        return;
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireCube(leftPoint.position, Vector3.one * 0.4f);

        Gizmos.DrawWireCube(rightPoint.position, Vector3.one * 0.4f);
    }

}
