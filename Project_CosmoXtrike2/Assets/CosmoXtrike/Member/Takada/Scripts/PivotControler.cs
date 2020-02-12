using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class PivotControler : MonoBehaviour
{
    [SerializeField] private float roteTime;            //Pivotを回転させるのにかける時間
    [SerializeField] private float roteValue;           //回転させる値
    [HideInInspector] public bool AnimFlag;             //アニメーションに使うフラグ
    private bool roteFlag;                              //Pivotの回転に使うフラグ

    public GameObject panel1;                           //パネル1
    public Animator panelAnim;                          //パネル1のアニメーション
    [SerializeField] private GameObject panel2;         //パネル2
    [SerializeField] private Animator panelAnim2;       //パネル2のアニメーション

    [SerializeField] private GameObject cameraObject;   //カメラ
    [SerializeField] private float animSpeed;           //カメラのアニメーションにかける時間 

    [SerializeField] private GameObject panel1Letter;   //パネル1に表示する文字
    [SerializeField] private Animator panel1LetterAnim; //パネル1に表示する文字のアニメーション
    [SerializeField] private GameObject panel2Letter;   //パネル2に表示する文字
    [SerializeField] private Animator panel2LetterAnim; //パネル2に表示する文字のアニメーション

    void Start()
    {

        //フラグの初期化
        AnimFlag = false;
        roteFlag = true;

    }

    void Update()
    {
        //PivotRote_PC();
        //PlayerSelect_PC();
        PivotRote_VR();
        PlayerSelect_VR();
    }

    #region 自機選択
    /// <summary>
    /// PC操作版
    /// </summary>
    private void PivotRote_PC()
    {
        if (!AnimFlag) { return; }

        //Pivotの回転が終わったらパネルをアニメーション表示
        if (roteFlag && Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //パネル2を表示
            this.gameObject.transform.DORotate(new Vector3(0f, roteValue * -1), roteTime);
            panel1.SetActive(false);
            panel1Letter.SetActive(false);
            DOVirtual.DelayedCall(roteTime,()=> { panel2.SetActive(true); panelAnim2.Play("PanelOpen");});
            DOVirtual.DelayedCall(roteTime + 1f, () => { panel2Letter.SetActive(true); panel2LetterAnim.Play("PanelLetterOpen"); });

            roteFlag = false;

        }
        else if (!roteFlag && Input.GetKeyDown(KeyCode.RightArrow))
        {
            //パネル１を表示
            this.gameObject.transform.DORotate(new Vector3(0f, roteValue - roteValue), roteTime);
            panel2.SetActive(false);
            panel2Letter.SetActive(false);
            DOVirtual.DelayedCall(roteTime,()=>{ panel1.SetActive(true); panelAnim.Play("PanelOpen");});
            DOVirtual.DelayedCall(roteTime + 1f, () => { panel1Letter.SetActive(true); panel1LetterAnim.Play("PanelLetterOpen");});

            roteFlag = true;

        }
    }

    /// <summary>
    /// VR操作版
    /// </summary>
    private void PivotRote_VR()
    {
        if (!AnimFlag) { return; }

        if (roteFlag && Input.GetButtonDown("RightTrigger")) { roteFlag = false; }
        else if (!roteFlag && Input.GetButtonDown("RightTrigger")) { roteFlag = true; }

        //Pivotの回転が終わったらパネルのアニメーションを再生し表示する
        if (roteFlag)
        {
            panelAnim2.speed = -1;
            panel2LetterAnim.speed = -1;
            panelAnim2.Play("PanelOpen");
            panel2LetterAnim.Play("PanelLetterOpen");

            this.gameObject.transform.DORotate(new Vector3(0f, roteValue - roteValue), roteTime);
            StartCoroutine("StartPanelAnim");
        }
        else
        {
            //パネル2を表示
            panelAnim.speed = -1;
            panel1LetterAnim.speed = -1;
            panelAnim.Play("PanelOpen");
            panel1LetterAnim.Play("PanelOpen");

            this.gameObject.transform.DORotate(new Vector3(0f, roteValue * -1), roteTime);
            StartCoroutine("StartPanelAnim");
        }

    }

    #endregion

    #region 自機決定
    /// <summary>
    /// PC操作版
    /// </summary>
    private void PlayerSelect_PC()
    {
        if (!AnimFlag) { return; }

        //ボタンを押したらメインゲームへ、その際にプレイヤーを決定する
        if (roteFlag && Input.GetKeyDown(KeyCode.Return))
        {
            DOVirtual.DelayedCall(animSpeed, () => { SceneLoadManager.Instnce.LoadScene("Game"); });
        }
        else if(!roteFlag && Input.GetKeyDown(KeyCode.Return))
        {
            DOVirtual.DelayedCall(animSpeed, () => { SceneLoadManager.Instnce.LoadScene("Game"); });
        }
    }

    /// <summary>
    /// VR操作版
    /// </summary>
    private void PlayerSelect_VR()
    {
        if (!AnimFlag) { return; }

        //ボタンを押したらメインゲームへ、その際にプレイヤーを決定する
        if (roteFlag && Input.GetButtonDown("LeftTrigger"))
        {
            //プレイヤー1に決定
            //   SceneLoadManager.Instnce.LoadScene("Game");
            CosmoXtrikerController.PlayerUseShip = UseShip.normal;
            TitleController.Instnce.ChangeScene();

        }
        else if (!roteFlag && Input.GetButtonDown("LeftTrigger"))
        {
            //プレイヤー2に決定
            //  SceneLoadManager.Instnce.LoadScene("Game");
            CosmoXtrikerController.PlayerUseShip = UseShip.missile;
            TitleController.Instnce.ChangeScene();
        }
    }
    #endregion
    
    /// <summary>
    /// コルーチン
    /// </summary>
    /// <returns></returns>
    private IEnumerator StartPanelAnim()
    {
        yield return new WaitForSeconds(roteTime);

        //パネルのアニメーション再生
        if (roteFlag)
        {
            panelAnim.speed = 1;
            panel1.SetActive(true);
            panelAnim.Play("PanelOpen");

        } 
        else
        {
            panelAnim2.speed = 1;
            panel2.SetActive(true);
            panelAnim2.Play("PanelOpen");

        }

        yield return new WaitForSeconds(1f);

        //文字のアニメーション再生
        if (roteFlag)
        {
            panel1LetterAnim.speed = 1;
            panel1LetterAnim.Play("PanelLetterOpen");

        }
        else
        {
            panel2LetterAnim.speed = 1;
            panel2LetterAnim.Play("PanelLetterOpen");

        }
    }
}
