using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BrightnessManager : UIManager
{
    [SerializeField]
    private Image brightnessImage;
    /// <summary>
    /// �ص�ʱ������
    /// </summary>
    [SerializeField]
    private float lightOffLevel;
    // ���ɳ���ʱ�䣨�룩
    private float transitionDuration = 1f;

    // ��ǰ�������е�Э��
    private Coroutine currentTransition = null;
    
    void Start()
    {
        SetLightOn();

    }
    /// <summary>
    /// ���ô˷����Կ����ƹ⣨���ȴ�0.2��Ϊ1��
    /// </summary>
    public void SetLightOn()
    {
        // ��������ڽ��еĹ��ɣ���ֹͣ��
        if (currentTransition != null)
        {
            StopCoroutine(currentTransition);
        }

        // ��ʼ�����ƹ�Ĺ���
        currentTransition = StartCoroutine(LightOn(brightnessImage.color.a));
    }

    /// <summary>
    /// ���ô˷����Թرյƹ⣨���ȴ�1��Ϊ0.2��
    /// </summary>
    public void SetLightOff()
    {
        // ��������ڽ��еĹ��ɣ���ֹͣ��
        if (currentTransition != null)
        {
            StopCoroutine(currentTransition);
        }

        // ��ʼ�رյƹ�Ĺ���
        currentTransition = StartCoroutine(LightOff(brightnessImage.color.a));
    }

    /// <summary>
    /// Э�̣������ȴ�0.2���䵽1
    /// </summary>
    private IEnumerator LightOn(float currentLight)
    {
        float elapsedTime = 0f;
        float startAlpha = currentLight;
        float targetAlpha = 0f; 

        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / transitionDuration);
            brightnessImage.color = new Color(brightnessImage.color.r, brightnessImage.color.g, brightnessImage.color.b, newAlpha);
            yield return null;
        }

        // ȷ������ֵ׼ȷ
        brightnessImage.color = new Color(brightnessImage.color.r, brightnessImage.color.g, brightnessImage.color.b, targetAlpha);
        currentTransition = null;
    }

    /// <summary>
    /// Э�̣������ȴ�1���䵽0.2
    /// </summary>
    private IEnumerator LightOff(float currentLight)
    {
        float elapsedTime = 0f;
        float startAlpha = brightnessImage.color.a;
        float targetAlpha = 0.8f; // ���ȴ�1��Ϊ0.2���൱�����ֵ�Alpha��0��Ϊ0.8


        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / transitionDuration);
            brightnessImage.color = new Color(brightnessImage.color.r, brightnessImage.color.g, brightnessImage.color.b, newAlpha);
            yield return null;
        }

        // ȷ������ֵ׼ȷ
        brightnessImage.color = new Color(brightnessImage.color.r, brightnessImage.color.g, brightnessImage.color.b, targetAlpha);
        currentTransition = null;
    }
}