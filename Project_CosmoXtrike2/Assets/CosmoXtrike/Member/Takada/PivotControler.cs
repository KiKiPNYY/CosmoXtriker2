using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PivotControler : MonoBehaviour
{
    [SerializeField] private float roteTime;    //Pivotを回転させるのにかける時間
    private bool roteflag;                      //回転させる際に使うフラグ

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PivotRote();
    }

    //Pivotを回転させる
    private void PivotRote()
    {
        //
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {

        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {

        }
    }

}
