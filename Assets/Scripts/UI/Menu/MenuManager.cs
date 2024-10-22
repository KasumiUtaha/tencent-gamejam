using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
        MenuClose();
        timePause = true;
    }

    protected override void Update()
    {
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
    public void MenuOpen()
    {

        TimePause();
        menu.SetActive(true);
    }
    public void MenuClose()
    {
        TimeReturn();
        menu.SetActive(false);
    }
}
