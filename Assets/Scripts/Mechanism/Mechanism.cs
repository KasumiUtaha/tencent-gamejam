using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Mechanism : MonoBehaviour
{
    public ButtonBasic button;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private float setColliderTime = 0.5f;
    [SerializeField] private float setColliderAlpha = 0.1f;
    private Collider2D mechanismCollider;
    public bool bindingTimePausePress = false;
    public bool bindingTimePauseRelease = false;
    public bool bindingColliderOnPress = false;
    public bool bindingColliderOnRelease = false;

    Coroutine changeColoerCoroutine = null;

    protected void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameObject.TryGetComponent<Collider2D>(out mechanismCollider);
        BindButton();
    }

    void BindButton()
    {
        if(bindingColliderOnPress)
        {
            button.AddPressDelegate(MechanismController.instance.SetColliderOn);
            button.AddReleaseDelegate(MechanismController.instance.SetColliderOff);
        }
        if (bindingColliderOnRelease)
        {
            button.AddPressDelegate(MechanismController.instance.SetColliderOff);
            button.AddReleaseDelegate(MechanismController.instance.SetColliderOn);
        }
        if(bindingTimePausePress)
        {
            button.AddPressDelegate(MechanismController.instance.SetTimePause);
            button.AddReleaseDelegate(MechanismController.instance.SetTimeStart);
        }
        if (bindingTimePauseRelease)
        {
            button.buttonPressEvents += MechanismController.instance.SetTimeStart;
            button.buttonReleaseEvents += MechanismController.instance.SetTimePause;
        }
    }


    virtual public void TimePause()
    {
        return;
    }

    virtual public void TimeStart()
    {
        return;
    }

    virtual public void SetColliderOff()
    {
        mechanismCollider.isTrigger = true;
        if(changeColoerCoroutine != null )
        {
            StopCoroutine( changeColoerCoroutine );
        }
        changeColoerCoroutine = StartCoroutine(ChangeColor(setColliderAlpha));
    }

    virtual public void SetColliderOn()
    {
        mechanismCollider.isTrigger = false;
        if (changeColoerCoroutine != null)
        {
            StopCoroutine(changeColoerCoroutine);
        }
        changeColoerCoroutine = StartCoroutine(ChangeColor(1f));
    }

    IEnumerator ChangeColor(float targetAlpha)
    {
        Color targetColor = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, targetAlpha);
        float t = 0;
        while (Mathf.Abs(spriteRenderer.color.a - targetAlpha) > 0.01f)
        {
            t = Time.deltaTime / setColliderTime;

            spriteRenderer.color = Color.Lerp(spriteRenderer.color, targetColor, t);
            yield return null;
        }
        changeColoerCoroutine = null;
    }
}
