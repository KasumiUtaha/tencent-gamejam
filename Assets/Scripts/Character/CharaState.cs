using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaState : MonoBehaviour
{ 

    [HideInInspector]
    public CharaMove charaMove;

    public bool isHiding = false;

    //[HideInInspector]

    // Start is called before the first frame update
    void Start()
    {
        charaMove = GetComponent<CharaMove>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

   
}
