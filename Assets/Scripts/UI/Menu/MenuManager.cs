using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
public class MenuManager : UIManager
{
    [SerializeField]
    private GameObject menu;
    private bool timePause;

    public void SetTimePause(bool flag)
    {
        timePause = flag;
    }
    protected override void Start()
    {
        base.Start();
        if (menu == null)
        {
            Debug.LogError("menu is not assigned in the inspector!");
        }
        MenuClose();
        timePause = true;
    }

    protected override void Update()
    {
        if (menu == null)
        {
            Debug.LogError("menu is not assigned in the inspector!");
        }
        base.Update();
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            if (menu.activeInHierarchy)
            {
                OnClose();
            }
            else
            {
                OnOpen();
            }
        }
    }
    protected override void OnOpen()
    {
        base.OnOpen();
        MenuOpen();
    }

    protected override void OnClose()
    {
        base.OnClose();
        MenuClose();
    }

    private void TimePause()
    {
        MechanismController.instance.SetTimePause();
    }
    private void TimeReturn()
    {
        MechanismController.instance.SetTimeStart();
    }
    private void MenuOpen()
    {

        TimePause();
        menu.SetActive(true);
    }

    public void MenuClose()
    {
        TimeReturn();
        if (menu != null)
        {
            menu.SetActive(false);
            Debug.Log("menu Close");
        }
        else
        {
            Debug.Log("NO menu");
        }
    }
}
