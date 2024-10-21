using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBasic : MonoBehaviour
{
    private Collider2D trigger;
    public delegate void ButtonEvent();
    public ButtonEvent buttonPressEvents;
    public ButtonEvent buttonReleaseEvents;
    public bool isPressed;
    public List<ButtonBasic> buttonBasics = new List<ButtonBasic>();

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
        CheckAndInvoke();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isPressed = false;
        CheckAndInvoke();
    }

    public void AddPressDelegate(ButtonEvent buttonEvent)
    {
        foreach (var b in buttonBasics)
        {
            b.buttonPressEvents += buttonEvent;
        }
    }

    public void AddReleaseDelegate(ButtonEvent buttonEvent)
    {
        foreach (var b in buttonBasics)
        {
            b.buttonReleaseEvents += buttonEvent;
        }
    }

    virtual protected void CheckAndInvoke()
    {
        bool type = true;
        foreach(var b in buttonBasics)
        {
            if (b.isPressed == false)
            {
                if (b.gameObject != gameObject) return;
                else type = false;
            }
        }
        Debug.Log(1);
        if(type) buttonPressEvents?.Invoke();
        else buttonReleaseEvents?.Invoke();
    }

}
