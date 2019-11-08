using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputTest : MonoBehaviour
{

    [SerializeField]
    private Text inputText, inputText2;

    void Start()
    {

    }

    void Awake()
    {

    }

    void Update()
    {
        DownKeyCheck();
    }


    void DownKeyCheck()
    {
        inputText.text = "R_Horizontal=" + Input.GetAxis("Right_Horizontal").ToString("F2") + " : R_Vertical" + Input.GetAxis("Right_Vertical").ToString("F2");
        inputText2.text = "L_Horizontal=" + Input.GetAxis("Left_Horizontal").ToString("F2") + " : L_Vertical" + Input.GetAxis("Left_Vertical").ToString("F2");
        

        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Fire1");
        }

        if (Input.GetButtonDown("Fire2"))
        {
            Debug.Log("Fire2");
        }

        //if (Input.anyKeyDown)
        //{
        //    foreach (KeyCode code in Enum.GetValues(typeof(KeyCode)))
        //    {
        //        if (Input.GetKeyDown(code))
        //        {
        //            //処理を書く
        //            Debug.Log(code);
        //            break;
        //        }
        //    }
        //}
    }


}