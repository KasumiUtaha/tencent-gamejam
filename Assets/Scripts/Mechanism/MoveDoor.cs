using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDoor : Mechanism
{
    public Transform destination;
    public float moveTime = 2f;
    private Vector3 originPosition;
    bool isTimePause = false;
    Coroutine coroutine = null;
    public bool bindingMoveToDestinationOnPress = false;
    public bool bindingMoveToDestinationOnRelease = false;

    void Awake()
    {
        originPosition = transform.position;
        if (destination.position.x == transform.position.x)
        {
            Vector3 align = new Vector3(0, -transform.localScale.y / 2.0f, 0);
            destination.position += align;
        }
        else
        {
            Vector3 align = new Vector3(-transform.localScale.x / 2.0f, 0, 0);
            destination.position += align;
        }
    }

    private void Start()
    {
        if (bindingMoveToDestinationOnPress) 
        {
            buttonSet.buttonPressEvents += MoveToDestination;
            buttonSet.buttonReleaseEvents += MoveToOriginPosition;
        }
        else if(bindingMoveToDestinationOnRelease)
        {
            buttonSet.buttonPressEvents += MoveToOriginPosition;
            buttonSet.buttonReleaseEvents += MoveToDestination;
        }
    }

    public override void TimePause()
    {
        isTimePause = true;
    }

    public override void TimeStart()
    {
        isTimePause = false;
    }
    void MoveToDestination()
    {
        if(coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        StartCoroutine(Move(destination.position));
    }

    void MoveToOriginPosition()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        StartCoroutine(Move(originPosition));
    }

    IEnumerator Move(Vector3 targetPositon)
    {
        float t = 0f;
        Vector3 start = transform.position;
        while (t < 1)
        {
            while (isTimePause) yield return null;
            t += Time.deltaTime / moveTime;
            transform.position = Vector3.Lerp(start, targetPositon, t);
            yield return null;
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireCube(destination.position, Vector3.one * 0.5f);
    }
}
