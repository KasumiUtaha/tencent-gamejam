using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueMannager : MonoBehaviour
{
    public TMP_FontAsset fontAsset;
    public GameObject dialogueBox;//显示or隐藏整个对话窗口
    public TextMeshProUGUI dialogueText, nameText;//输出文字和名字
    public GameObject dialogueTextAsset;
    private TextMeshPro textMeshPro;
    public GameObject TextTrigger;
    [TextArea(1, 3)]//显示文字时不会只显示一行
    public string[] dialogueLines;
    [SerializeField] private int currentLine;//用于实时追踪文字内容输出

    public int textCount = 0;//文本框出现次数
    public RectTransform DBtransform;//文本框的位置

    Vector3 mousePosition;//鼠标位置
    Vector3 TextTrigger_postion;//触发器位置

    bool isCover = false;//鼠标位置是否到达指定区域
    bool isScolling; //是否滚动 判断状态

    [SerializeField] private float textScollingIntervalTime;//滚动间隔
    [SerializeField] private float StartIntervalTime;//初始间隔

    void Start()
    {
        TextTrigger = GameObject.FindWithTag("TextTrigger");
        TextTrigger_postion = TextTrigger.transform.position;//获取触发器位置
        dialogueText.text = dialogueLines[currentLine];
        textMeshPro = dialogueTextAsset.GetComponent<TextMeshPro>();

    }

    // Update is called once per frame
    void Update()
    {  
        MousePosition();//判断鼠标是否进入触发范围

        if (isCover)
        {
            textCount++;
            if(textCount > 1)
            {
                dialogueTextAsset.font = fontAsset;
            }
            dialogueBox.SetActive(true);//显示对话框
                if (dialogueBox.activeInHierarchy)//对话框窗口显示时才可以出现文本
                {
                    if (isScolling == false)
                    {
                        currentLine++;
                        if (currentLine < dialogueLines.Length)
                        {
                            //dialogueText.text = dialogueLines[currentLine];
                            StartCoroutine(ScollingText());
                        }
                        else
                        {
                            dialogueBox.SetActive(false);//隐藏对话框
                            StopCoroutine(ScollingText());//关闭协程
                        }
                    }
                }
        }
        else
        {
            dialogueBox.SetActive(false);//隐藏对话框
            StopCoroutine(ScollingText());//关闭协程
            currentLine = 0;//恢复为从第一句开始显示
        }
    }

    private IEnumerator ScollingText()
    {
       // yield return new WaitForSeconds(StartIntervalTime);
        isScolling = true;
        dialogueText.text = " ";//保证开始时文本一定为空
        //将每个字符拆分开来 存在一个数组中
        foreach(char letter in dialogueLines[currentLine].ToCharArray())
        {
            dialogueText.text += letter;//一个字母一个字母显示出来
            yield return new WaitForSeconds(textScollingIntervalTime);
            if(isCover == false)
            {
                break;
            }
        }
        isScolling = false;
    }

    private void MousePosition()//判断鼠标是否在触发器里面
    {
        isCover = false;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Debug.Log(mousePosition);
        if (mousePosition.x < (TextTrigger_postion.x + 0.5) && mousePosition.x > (TextTrigger_postion.x - 0.5))
        {
            if(mousePosition.y < (TextTrigger_postion.y + 0.5) && mousePosition.y > (TextTrigger_postion.y - 0.5))
                isCover = true;
            else
                isCover = false;
        }
        else
            isCover = false;
    }

}
