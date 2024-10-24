using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueMannager : MonoBehaviour
{
    public TMP_FontAsset fontAsset1;
    public TMP_FontAsset fontAsset2;
    public GameObject dialogueBox;//��ʾor���������Ի�����
    public TextMeshProUGUI dialogueText;//������ֺ�����
    public GameObject dialogueTextAsset;

    public GameObject TextTrigger;
    [TextArea(1, 3)]//��ʾ����ʱ����ֻ��ʾһ��
    public string[] dialogueLines;
    [SerializeField] private int currentLine;//����ʵʱ׷�������������

    public int textCount = 0;//�ı�����ִ���
    public RectTransform DBtransform;//�ı����λ��
    public float dialogueRangeX = 0.5f;
    public float dialogueRangeY = 0.5f;

    Vector3 mousePosition;//���λ��
    Vector3 TextTrigger_postion;//������λ��

    public bool isCover = false;//���λ���Ƿ񵽴�ָ������
    bool isScolling; //�Ƿ���� �ж�״̬

    [SerializeField] private float textScollingIntervalTime;//�������
    [SerializeField] private float StartIntervalTime;//��ʼ���

    private DialogueAnim dialogueAnim;

    void Start()
    {
        
        TextTrigger_postion = TextTrigger.transform.position;//��ȡ������λ��
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
        
        MousePosition();//�ж�����Ƿ���봥����Χ
        if (isCover)
        {
            //textCount++;
            //if(textCount > 1)
            //{
            //    textMeshPro.font = fontAsset;
            //}
                dialogueBox.SetActive(true);//��ʾ�Ի���
                if (dialogueBox.activeInHierarchy )//�Ի��򴰿���ʾʱ�ſ��Գ����ı�
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
                            //dialogueBox.SetActive(false);//���ضԻ���
                            //StopCoroutine(ScollingText());//�ر�Э��
                        }
                        currentLine++;
                    }
                }
        }
        else
        {
            dialogueBox.SetActive(false);//���ضԻ���
            StopCoroutine(ScollingText());//�ر�Э��
            currentLine = 0;//�ָ�Ϊ�ӵ�һ�俪ʼ��ʾ
        }
    }

    private IEnumerator ScollingText()
    {
       // yield return new WaitForSeconds(StartIntervalTime);
        isScolling = true;
        dialogueText.text = " ";//��֤��ʼʱ�ı�һ��Ϊ��
        //��ÿ���ַ���ֿ��� ����һ��������
        dialogueAnim.isSpeaking = true;
        foreach(char letter in dialogueLines[currentLine].ToCharArray())
        {
            dialogueText.text += letter;//һ����ĸһ����ĸ��ʾ����
            yield return new WaitForSeconds(textScollingIntervalTime);
            if(isCover == false)
            {
                break;
            }
        }
        dialogueAnim.isSpeaking = false;
        isScolling = false;
    }

    public void MousePosition()//�ж�����Ƿ��ڴ���������
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
