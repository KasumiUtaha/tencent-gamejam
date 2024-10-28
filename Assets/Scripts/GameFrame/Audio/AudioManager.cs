using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private GameObject audioPlayerPrefab;

    private void Awake()
    {
        if(instance)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            audioPlayerPrefab = ResourceManager.LoadPrefab("AudioPlayer");
            DontDestroyOnLoad(gameObject);
        }
    }

    public void Play(string name, float volume = 0.5f, float pitch = 1.0f)
    {
        var ap  = Instantiate(audioPlayerPrefab,transform).GetComponent<AudioPlayer>();
        if (ap == null) Debug.Log("error");
        ap.audioSource.clip = ResourceManager.LoadAudio(name);
        ap.audioSource.pitch = pitch;
        if(!ap.audioSource.clip)
        {
            Debug.LogWarning(name + "");
        }
        ap.gameObject.name = name;
        ap.audioSource.volume = volume;
        ap.audioSource.Play();
        Destroy(ap.gameObject, ap.audioSource.clip.length);
    }
    public AudioPlayer GetAudioPlayer(string name)
    {
        AudioPlayer ap = Instantiate(audioPlayerPrefab, transform).GetComponent<AudioPlayer>();
        ap.audioSource.clip = ResourceManager.LoadAudio(name);
        if (!ap.audioSource.clip)
        {
            Debug.LogWarning(name + "");
        }
        return ap;
    }
}
