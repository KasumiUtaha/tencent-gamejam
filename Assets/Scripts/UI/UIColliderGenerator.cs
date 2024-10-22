using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class UIColliderGenerator : MonoBehaviour
{
       
    public Camera mainCamera; // 主摄像机
    public GameObject colliderPrefab; // 用于生成碰撞体的预制件
    private List<Button> buttons;
    [SerializeField]
    private Dictionary<Button,BoxCollider2D> buttonToCol;
    public bool ui_collider;//暴露的字段
    private bool pre_ui_collider;
    void Start()
    {
        ui_collider = false;
        pre_ui_collider = ui_collider;
        buttons = new List<Button>();
        buttonToCol = new Dictionary<Button,BoxCollider2D>();
        mainCamera ??= Camera.main;
        if (colliderPrefab == null)
        {
            Debug.LogError("请为 colliderPrefab 赋值一个带有 BoxCollider2D 的预制件。");
            return;
        }

        // 查找所有的 Button 组件
        GameObject[] allGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (GameObject go in allGameObjects)
        {
            buttons.AddRange(go.GetComponentsInChildren<Button>(true));
        }
        foreach (Button button in buttons)
        {
            CreateColliderForUIButton(button);
        }
    }
    private void Update()
    {
        if(ui_collider != pre_ui_collider)
        {
            
            foreach(var but in buttonToCol.Keys)
            {
                buttonToCol[but].enabled = ui_collider;
                if (!but.isActiveAndEnabled)
                {
                    buttonToCol[but].enabled = but.gameObject.activeInHierarchy;
                }
            }
            pre_ui_collider = ui_collider;
        }
    }

    public void SetUiColliderOn()
    {
        ui_collider=true;
    }
    public void SetUiColliderOff()
    {
        ui_collider = false;
    }
    void CreateColliderForUIButton(Button uiButton)
    {
        // 获取按钮的 RectTransform
        RectTransform rectTransform = uiButton.GetComponent<RectTransform>();
        RectTransform canvasRect = rectTransform.root.GetComponent<RectTransform>();
        if (rectTransform == null)
        {
            Debug.LogWarning("UI 元素缺少 RectTransform 组件");
            return;
        }
        float screenToWorldRatio = mainCamera.orthographicSize * 2 / canvasRect.rect.height;

        // 获取按钮在屏幕空间中的四个角的位置
        Vector3[] worldCorners = new Vector3[4];
        rectTransform.GetWorldCorners(worldCorners);

        // 计算中心点位置和尺寸
        Vector3 bottomLeft = worldCorners[0];
        Vector3 topRight = worldCorners[2];

        // 计算中心点位置和大小（转换到世界坐标）
        Vector3 centerPosition = (bottomLeft + topRight) / 2f;
        Vector2 size = new Vector2(topRight.x - bottomLeft.x, topRight.y - bottomLeft.y);
        

        // 将屏幕位置转换为世界位置
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(centerPosition);
        worldPosition.z = 0; // 确保生成的碰撞体位于 2D 平面上

        // 创建碰撞体对象
        GameObject newColliderObject = Instantiate(colliderPrefab, worldPosition, Quaternion.identity);
        BoxCollider2D boxCollider = newColliderObject.GetComponent<BoxCollider2D>();
        Vector2 adjustedSize = size * screenToWorldRatio;
        if (boxCollider != null)
        {
            boxCollider.size = adjustedSize;
            boxCollider.enabled = false;
            buttonToCol[uiButton] = boxCollider;
            
        }
        else
        {
            Debug.LogWarning("colliderPrefab 缺少 BoxCollider2D 组件");
        }
    }
}
