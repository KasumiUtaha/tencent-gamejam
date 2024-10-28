        using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaAnim : MonoBehaviour
{
    public CharaMove charaMove;
    public Animator animator;
    public bool isMoving = false;
    public bool sit = false;

    public float standTime = 5f;
    public float nowTime = 0f;

    Coroutine countCoroutine = null;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    IEnumerator CountTime()
    {
        while(true)
        {
            nowTime += Time.deltaTime;
            if(nowTime > standTime)
            {
                animator.SetBool("sit", true);
                break;
            }
            yield return null;
        }
    }

    private void Update()
    {
        Vector3 scale = transform.parent.transform.localScale;
        scale.x = Mathf.Abs(scale.x);
        scale.x = scale.x * charaMove.direction;
        transform.parent.transform.localScale = scale;


        animator.SetBool("isMoving", isMoving);

        if (!isMoving)
        {
            if (countCoroutine == null)
            {
                nowTime = 0f;
                countCoroutine = StartCoroutine(CountTime());
            }
        }
        else
        {
            if(countCoroutine != null)
            {
                StopCoroutine(countCoroutine);
            }
        }
    }
}
