using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static GameObject LoadPrefab(string name)
    {
        GameObject prefab = Resources.Load<GameObject>("Prefabs/" + name);
        return prefab;
    }

    public static AudioClip LoadAudio(string name)
    {
        AudioClip data = Resources.Load<AudioClip>("Audios/" + name);
        return data;
    }
}
