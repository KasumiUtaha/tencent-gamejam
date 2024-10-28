using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public float loopDeltaTime = 12f;

    private void Start()
    {
        AudioManager.instance.Play("Level 3", 1);
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        float t = 0;
        while (true)
        {
            t += Time.deltaTime;
            if(t > loopDeltaTime)
            {
                t = 0;
                AudioManager.instance.Play("Level 3", 1);
            }
            yield return null;  
        }
    }
}
