using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BrightnessManager : UIManager
{
    [SerializeField]
    private Image brightnessImage;
    /// <summary>
    /// 关灯时的亮度
    /// </summary>
    [SerializeField]
    private float lightOffLevel;
    // 过渡持续时间（秒）
    private float transitionDuration = 1f;

    public float targetAlpha = 0f;

    // 当前正在运行的协程
    private Coroutine currentTransition = null;
    
    void Start()
    {
        //SetLightOn();
    }
    /// <summary>
    /// 调用此方法以开启灯光（亮度从0.2变为1）
    /// </summary>
    public void SetLightOn()
    {
        // 如果有正在进行的过渡，先停止它
        if (currentTransition != null)
        {
            StopCoroutine(currentTransition);
        }

        // 开始开启灯光的过渡
        currentTransition = StartCoroutine(LightOn(brightnessImage.color.a));
    }

    public void SetLight(float alpha)
    {
        targetAlpha = alpha;
        if (brightnessImage.color.a > alpha) SetLightOn();
        else SetLightOff();
    }
    /// <summary>
    /// 调用此方法以关闭灯光（亮度从1变为0.2）
    /// </summary>
    public void SetLightOff()
    {
        // 如果有正在进行的过渡，先停止它
        if (currentTransition != null)
        {
            StopCoroutine(currentTransition);
        }

        // 开始关闭灯光的过渡
        currentTransition = StartCoroutine(LightOff(brightnessImage.color.a));
    }

    /// <summary>
    /// 协程：将亮度从0.2渐变到1
    /// </summary>
    private IEnumerator LightOn(float currentLight)
    {
        float elapsedTime = 0f;
        float startAlpha = currentLight;
        Debug.Log(startAlpha + " " + targetAlpha);
        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / transitionDuration);
            brightnessImage.color = new Color(brightnessImage.color.r, brightnessImage.color.g, brightnessImage.color.b, newAlpha);
            yield return null;
        }

        // 确保最终值准确
        brightnessImage.color = new Color(brightnessImage.color.r, brightnessImage.color.g, brightnessImage.color.b, targetAlpha);
        currentTransition = null;
    }

    /// <summary>
    /// 协程：将亮度从1渐变到0.2
    /// </summary>
    private IEnumerator LightOff(float currentLight)
    {
        float elapsedTime = 0f;
        float startAlpha = brightnessImage.color.a; // 亮度从1变为0.2，相当于遮罩的Alpha从0变为0.8


        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / transitionDuration);
            brightnessImage.color = new Color(brightnessImage.color.r, brightnessImage.color.g, brightnessImage.color.b, newAlpha);
            yield return null;
        }

        // 确保最终值准确
        brightnessImage.color = new Color(brightnessImage.color.r, brightnessImage.color.g, brightnessImage.color.b, targetAlpha);
        currentTransition = null;
    }
}