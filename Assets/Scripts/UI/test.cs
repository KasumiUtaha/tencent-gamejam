using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class UIColliderGenerator1 : MonoBehaviour
{
    public Camera mainCamera; // �������
    public GameObject colliderPrefab; // ����������ײ���Ԥ�Ƽ�
    private List<Button> buttons;
    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        if (colliderPrefab == null)
        {
            Debug.LogError("��Ϊ colliderPrefab ��ֵһ������ BoxCollider2D ��Ԥ�Ƽ���");
            return;
        }

        // �������е� Button ���
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
        // ��ȡ��ť�� RectTransform
        RectTransform rectTransform = uiButton.GetComponent<RectTransform>();
        if (rectTransform == null)
        {
            Debug.LogWarning("UI Ԫ��ȱ�� RectTransform ���");
            return;
        }

        // ��ȡ��ť�Ĵ�С����Ի����ı���
        RectTransform canvasRect = rectTransform.root.GetComponent<RectTransform>();
        if (canvasRect == null)
        {
            Debug.LogWarning("δ�ҵ��������� RectTransform ���");
            return;
        }

        // ���㰴ť�ڻ����е����λ�úʹ�С
        Vector2 size = new Vector2(rectTransform.rect.width * rectTransform.lossyScale.x, rectTransform.rect.height * rectTransform.lossyScale.y);
        Vector3 relativePosition = rectTransform.localPosition;

        // ������Ļ��������������֮��ı���
        float screenToWorldRatio = mainCamera.orthographicSize * 2 / canvasRect.rect.height;

        // �����λ��ת��Ϊ����������ռ�λ��
        Vector3 worldPosition = canvasRect.TransformPoint(relativePosition);
        worldPosition.z = 0; // ȷ�����ɵ���ײ��λ�� 2D ƽ����

        // ������ײ��Ĵ�С������Ļ�ͻ����ı�����������
        Vector2 adjustedSize = size * screenToWorldRatio;

        // ������ײ�����
        GameObject newColliderObject = Instantiate(colliderPrefab, worldPosition, Quaternion.identity);
        BoxCollider2D boxCollider = newColliderObject.GetComponent<BoxCollider2D>();

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