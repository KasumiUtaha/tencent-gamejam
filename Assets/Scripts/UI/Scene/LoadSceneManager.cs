using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneManager : UIManager
{
    [SerializeField]
     
    // Start is called before the first frame update
    protected override void  Start()
    {
        base.Start();
        OnOpen();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base .Update();
    }

    protected override void OnOpen()
    {
        base.OnOpen();
        //UI����֮���߼�
    }
    protected override void OnClose()
    {
        base.OnClose();
        //UI�ر�֮���߼�
    }

    protected override void GetPlayerInput()
    {

    }
}
