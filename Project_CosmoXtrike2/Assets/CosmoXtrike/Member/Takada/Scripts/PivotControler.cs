using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PivotControler : MonoBehaviour
{
    [SerializeField] private float roteTime;    //Pivotを回転させるのにかける時間
    [SerializeField] private float roteValue;   //回転させる値
    private bool roteFlag;                      //回転させる際に使うフラグ

    //パネルのアニメーション
    public Animator PanelAnim;
    public Animator PanelAnim2;

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
        if (roteFlag && Input.GetKeyDown(KeyCode.LeftArrow))
        {
            this.gameObject.transform.DORotate(new Vector3(0f, roteValue * -1), roteTime);

            roteFlag = false;
        }
        else if (!roteFlag && Input.GetKeyDown(KeyCode.RightArrow))
        {
            this.gameObject.transform.DORotate(new Vector3(0f, roteValue - roteValue), roteTime);

            roteFlag = true;
        }
    }

    private IEnumerator Panel1Anim()
    {
        PanelAnim2.SetBool("ClosePanel", false);

        yield return new WaitForSeconds(roteTime);

        PanelAnim.SetBool("OpenPanel", true);

    }

    private IEnumerator Panel2Anim()
    {
        PanelAnim.SetBool("ClosePanel", true);

        yield return new WaitForSeconds(roteTime);

        PanelAnim2.SetBool("OpenPanel", false);
    }

}
