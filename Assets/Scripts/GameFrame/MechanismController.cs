using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MechanismController : MonoBehaviour
{
    public static MechanismController instance;
    public GameObject[] gameObjects;
    public GameObject[] hiddenObjects;
    [SerializeField] private ConfigReader configReader;
    public CharaMove charaMove;

    private void Awake()
    {
        if (instance)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
    }

    public void Update()
    {
        gameObjects = GameObject.FindGameObjectsWithTag("Mechanism");
        hiddenObjects = GameObject.FindGameObjectsWithTag("HiddenObject");
        charaMove = GameObject.FindGameObjectWithTag("Player").GetComponent<CharaMove>();
        configReader = GameObject.Find("Scene").GetComponentInChildren<ConfigReader>();
    }

    public void SetHiddenObjectOn()
    {
        foreach(GameObject go in hiddenObjects)
        {
            if(go != null)
            go.SetActive(true);
        }
    }

    public void SetHiddenObjectOff()
    {
        foreach (GameObject go in hiddenObjects)
        {
            if (go != null)
                go.SetActive(false);
        }
    }
    public void SetColliderOn()
    {
        foreach (GameObject go in gameObjects)
        {
            if (go != null)
                go.GetComponent<Mechanism>().SetColliderOn();
        }
    }

    public void SetColliderOff()
    {
        foreach (GameObject go in gameObjects)
        {
            if (go != null)
                go.GetComponent<Mechanism>().SetColliderOff();
        }
    }

    public void SetTimePause()
    {
        configReader.time_pause = true;
        Debug.Log("SetTimePause");
        if(configReader.player_move == false) 
        {
            charaMove.canMove = false;
            charaMove.GetComponent<Rigidbody2D>().gravityScale = 0;
            charaMove.rb.velocity = Vector2.zero;
            
        }
        foreach (GameObject go in gameObjects)
        {
            if (go != null)
                go.GetComponent<Mechanism>().TimePause();
        }
    }

    public void SetTimeStart()
    {
        configReader.time_pause = false;
        if (charaMove)
        {
            charaMove.canMove = true;

            charaMove.GetComponent<Rigidbody2D>().gravityScale = 1;
        }

        foreach (GameObject go in gameObjects)
        {
            if (go != null)
                go.GetComponent<Mechanism>().TimeStart();
        }
    }
}
