using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBasic : MonoBehaviour
{
    private Collider2D trigger;
    public ButtonSet buttonSet;

    public bool isPressed;
    

    private void Awake()
    {
        trigger = GetComponent<Collider2D>();
        if (trigger == null || trigger.isTrigger == false)
        {
            Debug.LogWarning("No Trigger");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isPressed = true;
        buttonSet.CheckAndInvoke(this);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isPressed = false;
        buttonSet.CheckAndInvoke(this);
    }


}
