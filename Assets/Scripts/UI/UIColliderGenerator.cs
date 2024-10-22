using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class UIColliderGenerator : MonoBehaviour
{
       
    public Camera mainCamera; // �������
    public GameObject colliderPrefab; // ����������ײ���Ԥ�Ƽ�
    private List<Button> buttons;
    [SerializeField]
    private Dictionary<Button,BoxCollider2D> buttonToCol;
    public bool ui_collider;//��¶���ֶ�
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
            Debug.LogError("��Ϊ colliderPrefab ��ֵһ������ BoxCollider2D ��Ԥ�Ƽ���");
            return;
        }

        // �������е� Button ���
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
        // ��ȡ��ť�� RectTransform
        RectTransform rectTransform = uiButton.GetComponent<RectTransform>();
        RectTransform canvasRect = rectTransform.root.GetComponent<RectTransform>();
        if (rectTransform == null)
        {
            Debug.LogWarning("UI Ԫ��ȱ�� RectTransform ���");
            return;
        }
        float screenToWorldRatio = mainCamera.orthographicSize * 2 / canvasRect.rect.height;

        // ��ȡ��ť����Ļ�ռ��е��ĸ��ǵ�λ��
        Vector3[] worldCorners = new Vector3[4];
        rectTransform.GetWorldCorners(worldCorners);

        // �������ĵ�λ�úͳߴ�
        Vector3 bottomLeft = worldCorners[0];
        Vector3 topRight = worldCorners[2];

        // �������ĵ�λ�úʹ�С��ת�����������꣩
        Vector3 centerPosition = (bottomLeft + topRight) / 2f;
        Vector2 size = new Vector2(topRight.x - bottomLeft.x, topRight.y - bottomLeft.y);
        

        // ����Ļλ��ת��Ϊ����λ��
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(centerPosition);
        worldPosition.z = 0; // ȷ�����ɵ���ײ��λ�� 2D ƽ����

        // ������ײ�����
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
            Debug.LogWarning("colliderPrefab ȱ�� BoxCollider2D ���");
        }
    }
}
