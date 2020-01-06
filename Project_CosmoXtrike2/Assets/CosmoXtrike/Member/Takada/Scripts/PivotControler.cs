using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PivotControler : MonoBehaviour
{
    [SerializeField] private float roteTime;    //Pivotを回転させるのにかける時間
    [SerializeField] private float roteValue;   //回転させる値
    private bool roteFlag;                      //回転させる際に使うフラグ

    public Animator PanelAnim;   //パネルのアニメーション

    // Start is called before the first frame update
    void Start()
    {
        roteFlag = true;
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
        if (roteFlag == true && Input.GetKeyDown(KeyCode.LeftArrow))
        {
            this.gameObject.transform.DORotate(new Vector3(0f, roteValue * -1), roteTime);
            PanelAnim.SetBool("CloseFlag", false);
            PanelAnim.SetBool("OpenFlag",true);
            roteFlag = false;
        }
        else if (roteFlag == false && Input.GetKeyDown(KeyCode.RightArrow))
        {
            this.gameObject.transform.DORotate(new Vector3(0f, roteValue - roteValue), roteTime);
            PanelAnim.SetBool("OpenFlag", false);
            PanelAnim.SetBool("CloseFlag", true);
            roteFlag = true;
        }
    }

}
