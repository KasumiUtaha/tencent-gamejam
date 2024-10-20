using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mechanism : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;
    [SerializeField] private float setColliderTime = 0.5f;
    [SerializeField] private float setColliderAlpha = 0.1f;
    private Collider2D mechanismCollider;

    Coroutine changeColoerCoroutine = null;

    protected void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameObject.TryGetComponent<Collider2D>(out mechanismCollider);
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
