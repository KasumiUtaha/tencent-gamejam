using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueMannager : MonoBehaviour
{
    public TMP_FontAsset fontAsset;
    public GameObject dialogueBox;//��ʾor���������Ի�����
    public TextMeshProUGUI dialogueText, nameText;//������ֺ�����
    public GameObject dialogueTextAsset;
    private TextMeshPro textMeshPro;
    public GameObject TextTrigger;
    [TextArea(1, 3)]//��ʾ����ʱ����ֻ��ʾһ��
    public string[] dialogueLines;
    [SerializeField] private int currentLine;//����ʵʱ׷�������������

    public int textCount = 0;//�ı�����ִ���
    public RectTransform DBtransform;//�ı����λ��
    public float dialogueRange = 0.5f;

    Vector3 mousePosition;//���λ��
    Vector3 TextTrigger_postion;//������λ��

    bool isCover = false;//���λ���Ƿ񵽴�ָ������
    bool isScolling; //�Ƿ���� �ж�״̬

    [SerializeField] private float textScollingIntervalTime;//�������
    [SerializeField] private float StartIntervalTime;//��ʼ���

    void Start()
    {
        
        TextTrigger_postion = TextTrigger.transform.position;//��ȡ������λ��
        dialogueText.text = dialogueLines[currentLine];
        textMeshPro = dialogueTextAsset.GetComponent<TextMeshPro>();
        Debug.Log(TextTrigger_postion);

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
                if (dialogueBox.activeInHierarchy)//�Ի��򴰿���ʾʱ�ſ��Գ����ı�
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
                            dialogueBox.SetActive(false);//���ضԻ���
                            StopCoroutine(ScollingText());//�ر�Э��
                        }
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
        foreach(char letter in dialogueLines[currentLine].ToCharArray())
        {
            dialogueText.text += letter;//һ����ĸһ����ĸ��ʾ����
            yield return new WaitForSeconds(textScollingIntervalTime);
            if(isCover == false)
            {
                break;
            }
        }
        isScolling = false;
    }

    private void MousePosition()//�ж�����Ƿ��ڴ���������
    {
        isCover = false;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log(mousePosition + "     " + TextTrigger_postion);
        if (mousePosition.x < (TextTrigger_postion.x + dialogueRange) && mousePosition.x > (TextTrigger_postion.x - dialogueRange))
        {
            if(mousePosition.y < (TextTrigger_postion.y + dialogueRange) && mousePosition.y > (TextTrigger_postion.y - dialogueRange))
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
        Gizmos.DrawWireCube(TextTrigger.transform.position, Vector3.one * dialogueRange);
    }
}
