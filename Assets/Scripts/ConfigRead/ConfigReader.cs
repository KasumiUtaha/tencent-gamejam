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
    string time_pause = "time_pause";
    string hidden_object = "hidden_object";
    string player_collider = "player_collider";
    string ui_collider = "ui_collider";
    Dictionary<string, bool> diction = new Dictionary<string, bool>();


    private void Awake()
    {
        diction.Add("time_pause", false);
        diction.Add("hidden_object", false);
        diction.Add("player_collider", true);
        diction.Add("ui_collider", false);
        diction.Add("true", true);
        diction.Add("false", false);
        path = Application.dataPath + "/Configuration/" + textAsset.name;
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
        string[] strings = lineText.Split('=');
        if(strings.Length < 2)
        {
            return;
        }
        Debug.Log(strings[0] + "    " + strings[1]);
        diction[strings[0]] = diction[strings[1]];
    }

    void Apply()
    {

    }
}
