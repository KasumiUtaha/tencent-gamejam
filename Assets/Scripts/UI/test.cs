using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class UIColliderGenerator1 : MonoBehaviour
{
    public Camera mainCamera; // 主摄像机
    public GameObject colliderPrefab; // 用于生成碰撞体的预制件
    private List<Button> buttons;
    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        if (colliderPrefab == null)
        {
            Debug.LogError("请为 colliderPrefab 赋值一个带有 BoxCollider2D 的预制件。");
            return;
        }

        // 查找所有的 Button 组件
        buttons = new List<Button>();
        GameObject[] allGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (GameObject go in allGameObjects)
        {
            buttons.AddRange(go.GetComponentsInChildren<Button>(true));
        }


    }
    public void ButtonToCollider()
    {
        foreach (Button button in buttons)
        {
            CreateColliderForUIButton(button);
        }
    }
    void CreateColliderForUIButton(Button uiButton)
    {
        // 获取按钮的 RectTransform
        RectTransform rectTransform = uiButton.GetComponent<RectTransform>();
        if (rectTransform == null)
        {
            Debug.LogWarning("UI 元素缺少 RectTransform 组件");
            return;
        }

        // 获取按钮的大小和相对画布的比例
        RectTransform canvasRect = rectTransform.root.GetComponent<RectTransform>();
        if (canvasRect == null)
        {
            Debug.LogWarning("未找到根画布的 RectTransform 组件");
            return;
        }

        // 计算按钮在画布中的相对位置和大小
        Vector2 size = new Vector2(rectTransform.rect.width * rectTransform.lossyScale.x, rectTransform.rect.height * rectTransform.lossyScale.y);
        Vector3 relativePosition = rectTransform.localPosition;

        // 计算屏幕坐标与世界坐标之间的比例
        float screenToWorldRatio = mainCamera.orthographicSize * 2 / canvasRect.rect.height;

        // 将相对位置转换为画布的世界空间位置
        Vector3 worldPosition = canvasRect.TransformPoint(relativePosition);
        worldPosition.z = 0; // 确保生成的碰撞体位于 2D 平面上

        // 调整碰撞体的大小根据屏幕和画布的比例进行缩放
        Vector2 adjustedSize = size * screenToWorldRatio;

        // 创建碰撞体对象
        GameObject newColliderObject = Instantiate(colliderPrefab, worldPosition, Quaternion.identity);
        BoxCollider2D boxCollider = newColliderObject.GetComponent<BoxCollider2D>();

        if (boxCollider != null)
        {
            boxCollider.size = adjustedSize;
        }
        else
        {
            Debug.LogWarning("colliderPrefab 缺少 BoxCollider2D 组件");
        }
    }
}