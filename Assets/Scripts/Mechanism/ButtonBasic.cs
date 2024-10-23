using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBasic : MonoBehaviour
{
    private Collider2D trigger;
    public ButtonSet buttonSet;
    private Animator animator;

    public bool isPressed;
    

    private void Awake()
    {
        trigger = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        if (trigger == null || trigger.isTrigger == false)
        {
            Debug.LogWarning("No Trigger");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isPressed = true;
        AudioManager.instance.Play("click");
        animator.SetBool("isPressed", true);
        buttonSet.CheckAndInvoke(this);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isPressed = false;
        animator.SetBool("isPressed", false);
        AudioManager.instance.Play("click");
        buttonSet.CheckAndInvoke(this);
    }


}
