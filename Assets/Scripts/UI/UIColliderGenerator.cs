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
    private List<Collider2D> cols;
    void Start()
    {
        buttons = new List<Button>();
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
    public void ButtonToCollider()
    {
        foreach(var col in cols)
        {
            col.enabled = !col.enabled;
        }
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
        cols.Add(boxCollider);
        Vector2 adjustedSize = size * screenToWorldRatio;
        if (boxCollider != null)
        {
            boxCollider.size = adjustedSize;
        }
        else
        {
            Debug.LogWarning("colliderPrefab ȱ�� BoxCollider2D ���");
        }
    }
}
