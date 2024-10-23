using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ChangeSceneManager : UIManager
{
    [SerializeField]
    private GameObject fadeCanvas; // 从场景中获取 FadeCanvas
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
    // 新增：用于处理 OnClose 延调调用的协约
    private Coroutine openCloseCoroutine;

    [SerializeField]
    private List<string> levelNames; // 用于存储关卡名称的列表
    protected override void Start()
    {
        base.Start();
        title = titleGameObject.GetComponentInChildren<TextMeshProUGUI>();
        SetLightOn();
    }
    // 每帧调用一次
    protected override void Update()
    {
        base.Update();
    }

    protected override void OnOpen()
    {
        base.OnOpen();
        SetLightOff();
        // 在 sceneTransTime 秒后调用 OnClose
        StartOpenCloseSequence();
    }

    protected override void OnClose()
    {
        base.OnClose();
        // 可选：如果 openCloseCoroutine 仍在运行，停止它
        if (openCloseCoroutine != null)
        {
            StopCoroutine(openCloseCoroutine);
            openCloseCoroutine = null;
        }
        // UI 关闭后的其他逻辑可以在这里添加

        // 调用场景切换方法
        StartCoroutine(LoadNextLevel());
    }

    public void ChangeScene()
    {
        OnOpen();
    }

    public void SetLightOn()
    {
        // 如果有正在进行的过渡，先停止它
        if (currentTransition != null)
        {
            StopCoroutine(currentTransition);
        }

        // 开始开启灯光的过渡
        currentTransition = StartCoroutine(LightOn());
    }

    public void SetLightOff()
    {
        // 如果有正在进行的过渡，先停止它
        if (currentTransition != null)
        {
            StopCoroutine(currentTransition);
        }

        // 开始关闭灯光的过渡
        currentTransition = StartCoroutine(LightOff());
    }

    private IEnumerator LightOn()
    {
        float elapsedTime = 0f;
        float startAlpha = 0f;
        targetAlpha = 1f;

        // 第一步骤：标题渐显
        while (elapsedTime < blackChangeTime)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / blackChangeTime);
            fadeImage.color = Color.black;
            title.color = new Color(title.color.r, title.color.g, title.color.b, newAlpha);
            yield return null;
        }

        // 确保标题完全显现
        title.color = new Color(title.color.r, title.color.g, title.color.b, targetAlpha);

        // 第二步骤：标题保持实体，背景保持全黑
        elapsedTime = 0f;
        while (elapsedTime < blackContinueTime)
        {
            elapsedTime += Time.deltaTime;
            fadeImage.color = Color.black;
            yield return null;
        }

        // 第三步骤：背景和标题一起淡出消失
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

        // 确保背景和标题完全消失
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, targetAlpha);
        title.color = new Color(title.color.r, title.color.g, title.color.b, targetAlpha);

        currentTransition = null;
    }

    private IEnumerator LightOff()
    {
        float elapsedTime = 0f;
        float startAlpha = fadeImage.color.a; // 亮度从1变为0.2，相当于遮罩的Alpha从0变为0.8
        targetAlpha = 1f;
        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / transitionDuration);
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, newAlpha);
            yield return null;
        }

        // 确保最终值准确
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, targetAlpha);
        currentTransition = null;
    }

    /// <summary>
    /// 启动在 sceneTransTime 秒后调用 OnClose 的协约
    /// </summary>
    private void StartOpenCloseSequence()
    {
        // 如果已经有一个 open-close 协约在运行，先停止它
        if (openCloseCoroutine != null)
        {
            StopCoroutine(openCloseCoroutine);
        }

        // 启动新的 open-close 协约
        openCloseCoroutine = StartCoroutine(OpenCloseCoroutine());
    }

    /// <summary>
    /// 协约：等待 sceneTransTime 秒后调用 OnClose
    /// </summary>
    private IEnumerator OpenCloseCoroutine()
    {
        // 等待指定的转换时间
        yield return new WaitForSeconds(sceneTransTime);

        // 调用 OnClose 以启动关闭序列
        OnClose();

        // 清除协约引用
        openCloseCoroutine = null;
    }

    /// <summary>
    /// 转换到下一个关卡场景
    /// </summary>
    private IEnumerator LoadNextLevel()
    {
        
        // 获取当前场景的编号
        currentSceneName = SceneManager.GetActiveScene().name;
        if(!levelNames.Contains(currentSceneName))
        {
            Debug.Log("当前场景不是正式关卡");
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
            // 确定下一个场景名称

            SceneManager.LoadScene(nextSceneName);

            // 等待场景加载完成后，确保存在 fadeCanvas
            yield return new WaitUntil(() => SceneManager.GetActiveScene().name == nextSceneName);
            EnsureFadeCanvasExists();
        }

    }

    /// <summary>
    /// 确保场景中存在 fadeCanvas
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
                // 如果场景中没有找到 FadeCanvas，则使用挂载在此对象上的预定义 FadeCanvas
                fadeCanvas = Instantiate(fadeCanvas, transform);
                fadeCanvas.name = "FadeCanvas";
                fadeImage = fadeCanvas.GetComponentInChildren<Image>();
            }
        }
    }
}
