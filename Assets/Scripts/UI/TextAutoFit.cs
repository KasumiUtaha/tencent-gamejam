using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextAutoFit : MonoBehaviour
{

    public TextMeshProUGUI text;
    public GameObject plane;

    public int LineMaxNum;//ÿһ���������
    public float LineMinWidth;//һ����С����
    public float LineMinHeight;//һ����С�߶�
    public float LineDertaWidth;//ÿ�������ӵĳ���
    public float LineDertaHeight;//ÿһ�����ӵĸ߶�

    public float len;//��ǰ����
    public float zoom;//�Ǻ��ֵı���

    public void Update()
    {
        len = GetLength(text.text);
        if (len <= LineMaxNum)
        {
            plane.GetComponent<RectTransform>().sizeDelta =
            new Vector2(LineMinWidth + LineDertaWidth * len, LineMinHeight);
        }
        else
        {
            plane.GetComponent<RectTransform>().sizeDelta =
            new Vector2(LineMinWidth + LineDertaWidth * LineMaxNum, LineMinHeight + LineDertaHeight * (int)(len / LineMaxNum));
        }
    }

    public float GetLength(string str)
    {
        float res = 0;
        for (int i = 0; i < str.Length; i++)
        {
            if (str[i] < 127)
            {
                res += zoom;
            }
            else
            {
                res += 1f;
            }
        }
        return res;
    }
}

