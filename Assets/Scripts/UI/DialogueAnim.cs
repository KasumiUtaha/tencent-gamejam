using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueAnim : MonoBehaviour
{
    private DialogueMannager mannager;
    bool isMouseInTrigger = false;

    public float image2AppearTime = 0.2f;
    public float image2ApperaDis = -0.1f;
    public float speakDeltaTime = 0.1f;
    public bool isSpeaking = false;
    public GameObject Image1;
    public GameObject Image2;
    public GameObject Image3;
    public GameObject Image4;
    private Vector3 image2OriginPosition;

    private void Start()
    {
        mannager  = GetComponent<DialogueMannager>();
        image2OriginPosition = Image2.transform.position;
    }

    private void Update()
    {
        if (mannager.isCover)
        {
            if (isMouseInTrigger == false)
            {
                Image1.SetActive(false);
                Image2.SetActive(true);
                StartCoroutine(Image2Appear());
                isMouseInTrigger = true;
            }
            else if (Input.GetMouseButtonDown(0) && !Image3.activeInHierarchy)
            {
                Image2.SetActive(false);
                Image3.SetActive(true);
                Image4.SetActive(true);
                StartCoroutine(Speaking());
            }
        }
        else
        {
            if (Image2.activeInHierarchy && isMouseInTrigger)
            {
                StartCoroutine(Image2Disappear());
                isMouseInTrigger = false;
            }
            else
            {
                Image3.SetActive(false);
                Image4.SetActive(false);
                Image1.SetActive(true);
                isMouseInTrigger = false;
                StopAllCoroutines();
                Image2.transform.position = image2OriginPosition;
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

    IEnumerator Speaking()
    {
        float t = 0;
        Debug.Log("Start");
        while (true)
        {
            while (!isSpeaking)
            {
                t = 0;
                Image3.SetActive(true);
                yield return null;
            }
            t += Time.deltaTime;
            if(t > speakDeltaTime)
            {
                Image3.SetActive(!Image3.activeInHierarchy);
                t = 0;
            }
            yield return null;
        }

    }
}
