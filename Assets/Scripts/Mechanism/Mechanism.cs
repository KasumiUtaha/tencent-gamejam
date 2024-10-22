using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Mechanism : MonoBehaviour
{
    public ButtonSet buttonSet;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private float setColliderTime = 0.5f;
    [SerializeField] private float setColliderAlpha = 0.1f;
    protected Collider2D mechanismCollider;
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
        if (buttonSet == null) return;
        if(bindingColliderOnPress)
        {
            buttonSet.buttonPressEvents += MechanismController.instance.SetColliderOn;
            buttonSet.buttonReleaseEvents += MechanismController.instance.SetColliderOff;
        }
        if (bindingColliderOnRelease)
        {
            buttonSet.buttonPressEvents += MechanismController.instance.SetColliderOff;
            buttonSet.buttonReleaseEvents += MechanismController.instance.SetColliderOn;
        }
        if(bindingTimePausePress)
        {
            buttonSet.buttonPressEvents += MechanismController.instance.SetTimePause;
            buttonSet.buttonReleaseEvents += MechanismController.instance.SetTimeStart;
        }
        if (bindingTimePauseRelease)
        {
            buttonSet.buttonPressEvents += MechanismController.instance.SetTimeStart;
            buttonSet.buttonReleaseEvents += MechanismController.instance.SetTimePause;
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
        if (!mechanismCollider) return;
        mechanismCollider.isTrigger = true;
        if(changeColoerCoroutine != null )
        {
            StopCoroutine( changeColoerCoroutine );
        }
        changeColoerCoroutine = StartCoroutine(ChangeColor(setColliderAlpha));
    }

    virtual public void SetColliderOn()
    {
        if (!mechanismCollider) return;
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
        while (t < 1)
        {
            t += Time.deltaTime / setColliderTime;

            spriteRenderer.color = Color.Lerp(spriteRenderer.color, targetColor, t);
            yield return null;
        }
        changeColoerCoroutine = null;
    }
}
