using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueAnim : MonoBehaviour
{
    private DialogueMannager mannager;
    bool isMouseInTrigger = false;

    public float image2AppearTime = 0.2f;
    public float image2ApperaDis = -0.1f;
    public GameObject Image1;
    public GameObject Image2;

    private void Start()
    {
        mannager  = GetComponent<DialogueMannager>();
    }

    private void Update()
    {
        if (mannager.isCover)
        {
            if(isMouseInTrigger == false)
            {
                Image1.SetActive(false);
                Image2.SetActive(true);
                StartCoroutine(Image2Appear());
                isMouseInTrigger=true;
            }     
        }
        else
        {
            if (Image2.activeInHierarchy && isMouseInTrigger)
            {
                StartCoroutine(Image2Disappear());
                isMouseInTrigger = false;
            }
        }
    }
    
    IEnumerator Image2Appear()
    {
        float t = 0;
        Vector3 target = Image2.transform.position;
        Vector3 start = Image2.transform.position;
        target.x += image2ApperaDis;
        yield return new WaitForSeconds(0.1f);
        while(t < image2AppearTime)
        {
            t += Time.deltaTime;
            Image2.transform.position = Vector3.MoveTowards(target, start, image2ApperaDis / image2AppearTime);
            yield return null;
        }
    }

    IEnumerator Image2Disappear()
    {
        float t = 0;
        Vector3 target = Image2.transform.position;
        Vector3 start = Image2.transform.position;
        target.x -= image2ApperaDis;
        while (t < image2AppearTime)
        {
            t += Time.deltaTime;
            Image2.transform.position = Vector3.MoveTowards(target, start, image2ApperaDis / image2AppearTime);
            yield return null;
        }
        Image2.SetActive(false);
        Image1.SetActive(true);
    }
}
