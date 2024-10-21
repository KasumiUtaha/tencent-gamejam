using System.Collections;
using System.Collections.Generic;
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
        foreach (GameObject go in gameObjects)
        {
            go.GetComponent<Mechanism>().TimePause();
        }
    }

    public void SetTimeStart()
    {
        foreach (GameObject go in gameObjects)
        {
            go.GetComponent<Mechanism>().TimeStart();
        }
    }
}
