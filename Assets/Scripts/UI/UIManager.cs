using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    protected virtual void Start()
    {
        //UI开始时状态
    }
    protected virtual void Update()
    {
        //UI更新状态
    }
    protected virtual void OnOpen()
    {
        //UI启动之后逻辑
    }
    protected virtual void OnClose()
    {
        //UI关闭之后逻辑
    }

    protected virtual void GetPlayerInput()
    {

    }
}
