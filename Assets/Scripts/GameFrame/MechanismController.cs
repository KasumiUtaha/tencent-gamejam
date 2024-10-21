using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MechanismController : MonoBehaviour
{
    public static MechanismController instance;
    public GameObject[] gameObjects;
    public List<GameObject> hiddenObjects;
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
        gameObjects = GameObject.FindGameObjectsWithTag("Mechanism");
    }

    public void SetHiddenObjectOn()
    {
        foreach(GameObject go in hiddenObjects)
        {
            go.SetActive(true);
        }
    }

    public void SetHiddenObjectOff()
    {
        foreach (GameObject go in hiddenObjects)
        {
            go.SetActive(false);
        }
    }
    public void SetColliderOn()
    {
        foreach (GameObject go in gameObjects)
        {
            go.GetComponent<Mechanism>().SetColliderOn();
        }
    }

    public void SetColliderOff()
    {
        foreach (GameObject go in gameObjects)
        {
            go.GetComponent<Mechanism>().SetColliderOff();
        }
    }

    public void SetTimePause()
    {
        configReader.time_pause = true;
        if(configReader.player_move == false) 
        {
            charaMove.canMove = false;
            charaMove.GetComponent<Rigidbody2D>().gravityScale = 0;
            charaMove.rb.velocity = Vector2.zero;
            
        }
        foreach (GameObject go in gameObjects)
        {
            go.GetComponent<Mechanism>().TimePause();
        }
    }

    public void SetTimeStart()
    {
        configReader.time_pause = false;
        charaMove.canMove = true;
        charaMove.GetComponent<Rigidbody2D>().gravityScale = 1;
        foreach (GameObject go in gameObjects)
        {
            go.GetComponent<Mechanism>().TimeStart();
        }
    }
}
