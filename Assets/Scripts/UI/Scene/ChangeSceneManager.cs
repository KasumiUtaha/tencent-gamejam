using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ChangeSceneManager : UIManager
{
    [SerializeField]
    private GameObject fadeCanvas; // �ӳ����л�ȡ FadeCanvas
    public float targetAlpha = 0f;
    [SerializeField]
    private float transitionDuration = 2f;
    [SerializeField]
    private float sceneTransTime = 1f;
    [SerializeField]
    private Image fadeImage;
    [SerializeField]
    private float blackContinueTime = 1f;
    [SerializeField]
    private TextMeshProUGUI title;
    [SerializeField]
    private GameObject titleGameObject;
    [SerializeField]
    private float blackChangeTime;
    private Coroutine currentTransition;
    private string currentSceneName;
    private string nextSceneName;
    // ���������ڴ��� OnClose �ӵ����õ�ЭԼ
    private Coroutine openCloseCoroutine;

    [SerializeField]
    private List<string> levelNames; // ���ڴ洢�ؿ����Ƶ��б�
    protected override void Start()
    {
        base.Start();
        title = titleGameObject.GetComponentInChildren<TextMeshProUGUI>();
        SetLightOn();
    }
    // ÿ֡����һ��
    protected override void Update()
    {
        base.Update();
    }

    protected override void OnOpen()
    {
        base.OnOpen();
        SetLightOff();
        // �� sceneTransTime ������ OnClose
        StartOpenCloseSequence();
    }

    protected override void OnClose()
    {
        base.OnClose();
        // ��ѡ����� openCloseCoroutine �������У�ֹͣ��
        if (openCloseCoroutine != null)
        {
            StopCoroutine(openCloseCoroutine);
            openCloseCoroutine = null;
        }
        // UI �رպ�������߼��������������

        // ���ó����л�����
        StartCoroutine(LoadNextLevel());
    }

    public void ChangeScene()
    {
        OnOpen();
    }

    public void SetLightOn()
    {
        // ��������ڽ��еĹ��ɣ���ֹͣ��
        if (currentTransition != null)
        {
            StopCoroutine(currentTransition);
        }

        // ��ʼ�����ƹ�Ĺ���
        currentTransition = StartCoroutine(LightOn());
    }

    public void SetLightOff()
    {
        // ��������ڽ��еĹ��ɣ���ֹͣ��
        if (currentTransition != null)
        {
            StopCoroutine(currentTransition);
        }

        // ��ʼ�رյƹ�Ĺ���
        currentTransition = StartCoroutine(LightOff());
    }

    private IEnumerator LightOn()
    {
        float elapsedTime = 0f;
        float startAlpha = 0f;
        targetAlpha = 1f;

        // ��һ���裺���⽥��
        while (elapsedTime < blackChangeTime)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / blackChangeTime);
            fadeImage.color = Color.black;
            title.color = new Color(title.color.r, title.color.g, title.color.b, newAlpha);
            yield return null;
        }

        // ȷ��������ȫ����
        title.color = new Color(title.color.r, title.color.g, title.color.b, targetAlpha);

        // �ڶ����裺���Ᵽ��ʵ�壬��������ȫ��
        elapsedTime = 0f;
        while (elapsedTime < blackContinueTime)
        {
            elapsedTime += Time.deltaTime;
            fadeImage.color = Color.black;
            yield return null;
        }

        // �������裺�����ͱ���һ�𵭳���ʧ
        elapsedTime = 0f;
        startAlpha = 1f;
        targetAlpha = 0f;
        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / transitionDuration);
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, newAlpha);
            title.color = new Color(title.color.r, title.color.g, title.color.b, newAlpha);
            yield return null;
        }

        // ȷ�������ͱ�����ȫ��ʧ
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, targetAlpha);
        title.color = new Color(title.color.r, title.color.g, title.color.b, targetAlpha);

        currentTransition = null;
    }

    private IEnumerator LightOff()
    {
        float elapsedTime = 0f;
        float startAlpha = fadeImage.color.a; // ���ȴ�1��Ϊ0.2���൱�����ֵ�Alpha��0��Ϊ0.8
        targetAlpha = 1f;
        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / transitionDuration);
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, newAlpha);
            yield return null;
        }

        // ȷ������ֵ׼ȷ
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, targetAlpha);
        currentTransition = null;
    }

    /// <summary>
    /// ������ sceneTransTime ������ OnClose ��ЭԼ
    /// </summary>
    private void StartOpenCloseSequence()
    {
        // ����Ѿ���һ�� open-close ЭԼ�����У���ֹͣ��
        if (openCloseCoroutine != null)
        {
            StopCoroutine(openCloseCoroutine);
        }

        // �����µ� open-close ЭԼ
        openCloseCoroutine = StartCoroutine(OpenCloseCoroutine());
    }

    /// <summary>
    /// ЭԼ���ȴ� sceneTransTime ������ OnClose
    /// </summary>
    private IEnumerator OpenCloseCoroutine()
    {
        // �ȴ�ָ����ת��ʱ��
        yield return new WaitForSeconds(sceneTransTime);

        // ���� OnClose �������ر�����
        OnClose();

        // ���ЭԼ����
        openCloseCoroutine = null;
    }

    /// <summary>
    /// ת������һ���ؿ�����
    /// </summary>
    private IEnumerator LoadNextLevel()
    {
        
        // ��ȡ��ǰ�����ı��
        currentSceneName = SceneManager.GetActiveScene().name;
        if(!levelNames.Contains(currentSceneName))
        {
            Debug.Log("��ǰ����������ʽ�ؿ�");
        }
        else
        {
            for (int i = 0; i < levelNames.Count; i++)
            {
                if (levelNames[i] == currentSceneName)
                {
                    nextSceneName = levelNames[(i + 1) % levelNames.Count];
                }
            }
            // ȷ����һ����������

            SceneManager.LoadScene(nextSceneName);

            // �ȴ�����������ɺ�ȷ������ fadeCanvas
            yield return new WaitUntil(() => SceneManager.GetActiveScene().name == nextSceneName);
            EnsureFadeCanvasExists();
        }

    }

    /// <summary>
    /// ȷ�������д��� fadeCanvas
    /// </summary>
    private void EnsureFadeCanvasExists()
    {
        if (fadeCanvas == null)
        {
            GameObject existingFadeCanvas = GameObject.Find("FadeCanvas");
            if (existingFadeCanvas != null)
            {
                fadeCanvas = existingFadeCanvas;
                fadeImage = fadeCanvas.GetComponentInChildren<Image>();
            }
            else
            {
                // ���������û���ҵ� FadeCanvas����ʹ�ù����ڴ˶����ϵ�Ԥ���� FadeCanvas
                fadeCanvas = Instantiate(fadeCanvas, transform);
                fadeCanvas.name = "FadeCanvas";
                fadeImage = fadeCanvas.GetComponentInChildren<Image>();
            }
        }
    }
}
