using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MovingPlatform : Mechanism
{
    [SerializeField] private Transform leftPoint;
    [SerializeField] private Transform rightPoint;
    [SerializeField] private float movingSpeed = 3;
    [SerializeField] private int direction = 1;
    private float adjustLength;
    public CharaMove charaMove;


    private void Start()
    {
        base.Start();
        adjustLength = transform.localScale.x / 2.0f;
        StartCoroutine(Moving());
    }


    IEnumerator Moving()
    {
        while(true)
        {
            if (transform.position.x + direction * adjustLength >= rightPoint.position.x) direction = -1;
            else if (transform.position.x + direction * adjustLength <= leftPoint.position.x) direction = 1;
            Vector3 moveDirection = direction == 1 ? Vector3.right : Vector3.left;
            transform.Translate(moveDirection * movingSpeed * Time.deltaTime);
            yield return null;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireCube(leftPoint.position, Vector3.one * 0.4f);

        Gizmos.DrawWireCube(rightPoint.position, Vector3.one * 0.4f);
    }

}
