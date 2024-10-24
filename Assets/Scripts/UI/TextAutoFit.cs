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

    public GameObject image1;
    public GameObject image2;
    public GameObject image3;

    public void Update()
    {
        len = GetLength(text.text);
        if(len <= 2*LineMaxNum)
        {
            image1.SetActive(true);
            image2.SetActive(false);
            image3.SetActive(false);  
        }
        else if(len <= 4*LineMaxNum)
        {
            image1.SetActive(false);
            image2.SetActive(true);
            image3.SetActive(false);
        }
        else
        {
            image1.SetActive(false);
            image2.SetActive(false);
            image3.SetActive(true);
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

