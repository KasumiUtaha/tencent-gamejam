using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : Mechanism
{
    [SerializeField] private Transform destination;
    [SerializeField] private Transform middlePoint;
    [SerializeField] private float springTime;
    [SerializeField] private float idleTime;
    [SerializeField] private ConfigReader configReader;
    [SerializeField] private Transform topTransform;
    public Animator animator;
    bool triggered = false;
    public CharaMove charaMove;
    private List<Vector3> bezierPoint = new List<Vector3>();
    Coroutine coroutine = null;

    private Vector3 GetNextPosition(Vector3 startPosition, Vector3 endPosition, Vector3 middlePosition,float t)
    {
        Vector3 v1 = Vector3.Lerp(startPosition, middlePoint.position, t);
        Vector3 v2 = Vector3.Lerp(middlePoint.position, destination.position, t);
        var find = Vector3.Lerp(v1, v2,t);
        return find;
    }

    void GetBezierPoint(Vector3 startPosition)
    {
        bezierPoint.Clear();
        float num = 200;
        for(float i=0;i < num; i = i + 1)
        {
            Vector3 v1 = Vector3.Lerp(startPosition, middlePoint.position, i / 100f);
            Vector3 v2 = Vector3.Lerp(middlePoint.position, destination.position, i / 100f);
            var find = Vector3.Lerp(v1, v2, i / 100f);
            bezierPoint.Add(find);
        }
    }

    IEnumerator Compress(GameObject go)
    {
        float t = 0;
        Vector3 delta = new Vector3(go.transform.position.x - topTransform.position.x, go.transform.localScale.y / 2f, 0);
        if (go.tag == "Player") delta.y += 0.5f;
        while(t < idleTime)
        {
            t += Time.deltaTime;
            
            go.transform.position = topTransform.position + delta;
            yield return null;
        }
        
        
    }

    IEnumerator MoveGameObject(GameObject go)
    {
        if (go.tag == "Player") charaMove.canMove = false;
        float t = 0;
        //GetBezierPoint(go.transform.position);
        Vector3 startPosition = go.transform.position;
        animator.SetBool("isCompressed", true);
        coroutine = StartCoroutine(Compress(go));
        yield return new WaitForSeconds(idleTime);
        if (coroutine != null) StopCoroutine(coroutine); 
        animator.SetBool("isCompressed", false);
        //int i = -1;
        while (t < 1)   
        {
            //Debug.Log(configReader.time_pause);
            while(configReader.time_pause == true && go.tag != "Player") yield return null;
            while(configReader.time_pause == true && go.tag == "Player" && configReader.player_move == false) yield return null;
            t += Time.deltaTime / springTime;
            //i++;
            //if (i >= bezierPoint.Count - 1) break;
            //go.transform.position = Vector3.Lerp(bezierPoint[i], bezierPoint[i + 1], 1);
            //Debug.Log(t + "  " + go.transform.position);
            go.transform.position = GetNextPosition(startPosition, destination.position, middlePoint.position, t);
            yield return null;
        }
        if (go.tag == "Player") charaMove.canMove = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        StartCoroutine(MoveGameObject(collision.gameObject));
    }


    public override void SetColliderOff()
    {
        return;
    }

    public override void SetColliderOn()
    {
        return;
    }

    private void OnDrawGizmos()
    {
        
        Gizmos.color = Color.green;

        Gizmos.DrawWireCube(destination.position, Vector3.one * 0.5f);

        Gizmos.color = Color.yellow;

        Gizmos.DrawWireCube(middlePoint.position, Vector3.one * 0.5f);
        
        GetBezierPoint(gameObject.transform.position);
        for(int i= 0; i < bezierPoint.Count - 1; i++)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(bezierPoint[i], bezierPoint[i + 1]);
        }
        
    }
}
