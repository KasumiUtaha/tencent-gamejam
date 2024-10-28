using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChange : MonoBehaviour
{
    [SerializeField]
    private GameObject sceneManager;
    [SerializeField]private ChangeSceneManager changeSceneManager;
    private void Start()
    {
        changeSceneManager = sceneManager.GetComponent<ChangeSceneManager>();
    }
    private void OnTriggerEnter2D()
    {
        if (sceneManager != null)
        {
            Debug.Log("ChangeScene");
            changeSceneManager.ChangeScene();
        }
    }
}
