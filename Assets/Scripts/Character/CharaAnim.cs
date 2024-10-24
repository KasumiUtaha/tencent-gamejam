using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaAnim : MonoBehaviour
{
    public CharaMove charaMove;
    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x);
        scale.x = scale.x * charaMove.direction;
        transform.localScale = scale;
    }
}
