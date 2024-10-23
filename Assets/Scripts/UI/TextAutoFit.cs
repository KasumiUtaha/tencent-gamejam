using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextAutoFit : MonoBehaviour
{

    public TextMeshProUGUI text;
    public GameObject plane;

    public int LineMaxNum;//每一行最多字数
    public float LineMinWidth;//一行最小长度
    public float LineMinHeight;//一行最小高度
    public float LineDertaWidth;//每个字增加的长度
    public float LineDertaHeight;//每一行增加的高度

    public float len;//当前字数
    public float zoom;//非汉字的比例

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

