using DG.Tweening.Plugins.Core.PathCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ConfigReader : MonoBehaviour
{
    public TextAsset textAsset;
    string path;
    private DateTime lastModified;
    bool time_pause = false;
    bool player_collider = true;
    bool ui_collider = false;
    bool hidden_object = false;


    private void Awake()
    {
        path = Application.dataPath + "/Configuration/" + textAsset.name + ".txt";
        lastModified = File.GetLastWriteTime(path);
        ReadConfig();
    }

    private void Update()
    {
        DateTime currentModified = File.GetLastWriteTime(path);
        Debug.Log(currentModified + "   " + lastModified + "   " + path);
        if (currentModified != lastModified)
        {
            Debug.Log("Modified");
            ReadConfig();
            lastModified = currentModified;
        }
    }

    void ReadConfig()
    {
        string[] lineText = textAsset.text.Split('\n');
        foreach(string line in lineText)
        {
            Parse(line);
        }
    }

    void Parse(string lineText)
    {
        if (lineText.Contains("time_pause"))
        {
            if (lineText.Contains("true"))
            {
                if(time_pause == false) MechanismController.instance.SetTimePause();
                time_pause = true;
            }
            else if (lineText.Contains("false"))
            {
                if (time_pause == true) MechanismController.instance.SetTimeStart();
                time_pause = false;
            }
        }
        else if(lineText.Contains("hidden_object"))
        {
            if (lineText.Contains("true"))
            {
                if(hidden_object == false) MechanismController.instance.SetHiddenObjectOn();
                hidden_object = true;
            }
            else if (lineText.Contains("false"))
            {
                if (hidden_object == true) MechanismController.instance.SetHiddenObjectOff();
                hidden_object = false;
            }
        }
        else if(lineText.Contains("ui_collider"))
        {
            if (lineText.Contains("true"))
            {
                if (ui_collider == false) MechanismController.instance.SetTimePause();
                ui_collider = true;
            }
            else if (lineText.Contains("false"))
            {
                if (ui_collider == true) MechanismController.instance.SetTimeStart();
                ui_collider = false;
            }
        }
        else if(lineText.Contains("player_collider"))
        {
            if (lineText.Contains("true"))
            {
                if (player_collider == false) MechanismController.instance.SetColliderOn();
                player_collider = true;
            }
            else if (lineText.Contains("false"))
            {
                if (player_collider == true) MechanismController.instance.SetColliderOff();
                player_collider = false;
            }
        }
    }

}
