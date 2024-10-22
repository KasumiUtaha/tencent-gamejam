using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSet : MonoBehaviour
{
    public delegate void ButtonEvent();
    public ButtonEvent buttonPressEvents;
    public ButtonEvent buttonReleaseEvents;
    public List<ButtonBasic> buttonBasics = new List<ButtonBasic>();

    private void Start()
    {
        foreach (var button in buttonBasics)
        {
            button.buttonSet = this;
        }
    }

    public void CheckAndInvoke(ButtonBasic button)
    {
        bool flag = true;
        foreach (var b in buttonBasics)
        {
            if(b.isPressed == false)
            {
                if(b == button)
                {
                    flag = false;
                }
                else
                {
                    return;
                }
            }
        }
        if (flag) buttonPressEvents?.Invoke();
        else buttonReleaseEvents?.Invoke();
    }
}
