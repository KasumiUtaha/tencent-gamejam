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
    public bool time_pause = false;
    public bool player_move = false;
    public bool player_collider = true;
    public bool ui_collider = false;
    public bool hidden_object = false;
    [SerializeField] private UIColliderGenerator uIColliderGenerator;
    [SerializeField] private CharaMove charaMove;
    [SerializeField] private BrightnessManager brightnessManager;
    public List<string> originFileText = new List<string>();
    private DateTime currentModified = DateTime.MinValue;



    private void Awake()
    {
        path = Application.dataPath + "/Gameplay/" + textAsset.name + ".txt";
        
        string allText = "";
        foreach (string originFile in originFileText)
        {
            allText += originFile;
            allText += "\n";
        }
        File.WriteAllText(path, allText);
        lastModified = File.GetLastWriteTime(path);
        ReadConfig();
    }

 
    private void Update()
    {

        // Debug.Log(currentModified + "   " + lastModified + "   " + path);
         currentModified = File.GetLastWriteTime(path);
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
        if (lineText.Contains("player_move"))
        {
            Debug.Log("Trigger Player_Move" + "  " + lineText);
            if (lineText.Contains("true"))
            {
                player_move = true;
                charaMove.canMove = true;
                charaMove.GetComponent<Rigidbody2D>().gravityScale = 1;
            }
            else if (lineText.Contains("false"))
            {
                player_move = false;
                if(time_pause) charaMove.canMove = false;
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
                Debug.Log("Trigger" + ui_collider);   
                if (ui_collider == false) uIColliderGenerator.SetUiColliderOn();
                ui_collider = true;
            }
            else if (lineText.Contains("false"))
            {
                if (ui_collider == true) uIColliderGenerator.SetUiColliderOff();
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
        else if(lineText.Contains("brightness"))
        {
            Debug.Log("call SetLight");
            string[] st = lineText.Split('=');
            if (st.Length < 2) return;
            float brightness = float.Parse(st[1]);
            if (brightness > 1f) brightness = 1f;
            else if(brightness < 0f) brightness = 0f;
            Debug.Log("brightness: " + brightness);
            brightnessManager.SetLight(brightness);
        }
    }

}
