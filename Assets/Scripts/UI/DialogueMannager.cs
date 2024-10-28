using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueMannager : MonoBehaviour
{
    public TMP_FontAsset fontAsset1;
    public TMP_FontAsset fontAsset2;
    public GameObject dialogueBox;//显示or隐藏整个对话窗口
    public TextMeshProUGUI dialogueText;//输出文字和名字
    public GameObject dialogueTextAsset;

    public GameObject TextTrigger;
    [TextArea(1, 3)]//显示文字时不会只显示一行
    public string[] dialogueLines;
    [SerializeField] private int currentLine;//用于实时追踪文字内容输出

    public int textCount = 0;//文本框出现次数
    public RectTransform DBtransform;//文本框的位置
    public float dialogueRangeX = 0.5f;
    public float dialogueRangeY = 0.5f;

    Vector3 mousePosition;//鼠标位置
    Vector3 TextTrigger_postion;//触发器位置

    public bool isCover = false;//鼠标位置是否到达指定区域
    bool isScolling; //是否滚动 判断状态

    [SerializeField] private float textScollingIntervalTime;//滚动间隔
    [SerializeField] private float StartIntervalTime;//初始间隔

    private DialogueAnim dialogueAnim;

    void Start()
    {
        
        TextTrigger_postion = TextTrigger.transform.position;//获取触发器位置
        dialogueAnim = GetComponent<DialogueAnim>();

    }

    public void ChangeFont1()
    {
        dialogueText.font = fontAsset1;
    }

    public void ChangeFont2()
    {
        dialogueText.font = fontAsset2;
    }

    // Update is called once per frame
    void Update()
    {
        
        MousePosition();//判断鼠标是否进入触发范围
        if (isCover)
        {
            //textCount++;
            //if(textCount > 1)
            //{
            //    textMeshPro.font = fontAsset;
            //}
                dialogueBox.SetActive(true);//显示对话框
                if (dialogueBox.activeInHierarchy )//对话框窗口显示时才可以出现文本
                {
                    if (isScolling == false && (Input.GetMouseButton(0) || currentLine == 0))
                    {
                        
                        if (currentLine < dialogueLines.Length)
                        {
                            //dialogueText.text = dialogueLines[currentLine];
                            StartCoroutine(ScollingText());
                        }
                        else
                        {
                            //dialogueBox.SetActive(false);//隐藏对话框
                            //StopCoroutine(ScollingText());//关闭协程
                        }
                        currentLine++;
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
        dialogueAnim.isSpeaking = true;
        foreach(char letter in dialogueLines[currentLine].ToCharArray())
        {
            dialogueText.text += letter;//一个字母一个字母显示出来
            yield return new WaitForSeconds(textScollingIntervalTime);
            if(isCover == false)
            {
                break;
            }
        }
        dialogueAnim.isSpeaking = false;
        isScolling = false;
    }

    public void MousePosition()//判断鼠标是否在触发器里面
    {
        isCover = false;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (mousePosition.x < (TextTrigger_postion.x + dialogueRangeX) && mousePosition.x > (TextTrigger_postion.x - dialogueRangeX))
        {
            if(mousePosition.y < (TextTrigger_postion.y + dialogueRangeY) && mousePosition.y > (TextTrigger_postion.y - dialogueRangeY))
                isCover = true;
            else
                isCover = false;
        }
        else
            isCover = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Vector3 vector3 = new Vector3(dialogueRangeX * 2f, dialogueRangeY * 2f, 1);
        Gizmos.DrawWireCube(TextTrigger.transform.position, vector3);
    }
}
