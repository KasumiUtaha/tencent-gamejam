using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBasic : MonoBehaviour
{
    private Collider2D trigger;
    public ButtonSet buttonSet;
    public GameObject goOnButton;
    public GameObject Image1;
    public GameObject Image2;
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
        if (isPressed == true) return; 
        isPressed = true;
        goOnButton = collision.gameObject;
        AudioManager.instance.Play("click");
        Image1.SetActive(false);
        Image2.SetActive(true);
        animator.SetBool("isPressed", true);
        buttonSet.CheckAndInvoke(this);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject != goOnButton) return;
        isPressed = false;
        goOnButton = null;
        animator.SetBool("isPressed", false);
        AudioManager.instance.Play("click");
        buttonSet.CheckAndInvoke(this);
        Image1.SetActive(true);
        Image2.SetActive(false);
    }


}
