using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : UIManager
{

    [SerializeField]
    private GameObject menuCanvas;
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
            
            if (menuCanvas.activeInHierarchy)
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
        Time.timeScale = 0f;
    }
    private void TimeReturn()
    {
        Time.timeScale = 1f;
    }
    public void MenuOpen()
    {
        if (timePause)
        {
            TimePause();
        }
        menuCanvas.SetActive(true);
    }
    public void MenuClose()
    {
        TimeReturn();
        menuCanvas.SetActive(false);
    }
}
