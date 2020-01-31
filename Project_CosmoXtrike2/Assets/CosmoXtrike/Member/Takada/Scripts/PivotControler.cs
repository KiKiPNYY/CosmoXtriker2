using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class PivotControler : MonoBehaviour
{
    [SerializeField] private float roteTime;    //Pivotを回転させるのにかける時間
    [SerializeField] private float roteValue;   //回転させる値
    [HideInInspector] public bool AnimFlag;     //アニメーションに使うフラグ
    private bool roteFlag;                      //Pivotの回転に使うフラグ

    public GameObject panel1;                       //パネル1
    public Animator panelAnim;                      //パネル1のアニメーション
    [SerializeField] private GameObject panel2;     //パネル2
    [SerializeField] private Animator panelAnim2;   //パネル2のアニメーション

    [SerializeField] private GameObject cameraObject;   //カメラ
    [SerializeField] private float animSpeed;           //カメラのアニメーションにかける時間

    
    //[SerializeField] private Transform jiki1Pos;        
    //[SerializeField] private Transform jiki2Pos;        

    public GameObject panel1Letter;                     //パネル1に表示する文字
    public Animator panel1LetterAnim;                   //パネル1に表示する文字のアニメーション
    [SerializeField]private GameObject panel2Letter;    //パネル2に表示する文字
    [SerializeField]private Animator panel2LetterAnim;  //パネル2に表示する文字のアニメーション

    private Sequence sequence;


    void Start()
    {

        //フラグの初期化
        AnimFlag = false;
        roteFlag = true;

        //Sequenceの生成
        //sequence = DOTween.Sequence();

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

        float x = Input.GetAxis("Right_Vertical");

        if (x > 1) { x = 1; }
        if (x < 0) { x = 0; }

        //Pivotの回転が終わったらパネルをアニメーション表示
        if (roteFlag && x == 1)
        {
            //パネル2を表示
            panel1Letter.SetActive(false);
            panel1.SetActive(false);
            this.gameObject.transform.DORotate(new Vector3(0f, roteValue * -1), roteTime);
            DOVirtual.DelayedCall(roteTime, () => { panel2.SetActive(true); panelAnim2.Play("PanelOpen"); });

            roteFlag = false;

        }
        else if (!roteFlag && x == 0)
        {
            //パネル１を表示
            panel2.SetActive(false);
            panel2.SetActive(false);
            this.gameObject.transform.DORotate(new Vector3(0f, roteValue - roteValue), roteTime);
            DOVirtual.DelayedCall(roteTime, () => { panel1.SetActive(true); panelAnim.Play("PanelOpen"); });

            roteFlag = true;

        }

        Debug.Log(x);

    }

    #endregion

    #region 自機決定
    /// <summary>
    /// PC操作版
    /// </summary>
    private void PlayerSelect_PC()
    {
        //ボタンを押したらメインゲームへ、その際にプレイヤーを決定する
        if (roteFlag && Input.GetKeyDown(KeyCode.Return))
        {
            //cameraObject.transform.DOLocalMove(new Vector3(),animSpeed);
            //cameraObject.transform.DORotate(new Vector3(0f, 180f, 0f), animSpeed);
            DOVirtual.DelayedCall(animSpeed, () => { SceneLoadManager.Instnce.LoadScene("Game"); });
        }
        else if(!roteFlag && Input.GetKeyDown(KeyCode.Return))
        {
            //cameraObject.transform.DOLocalMove(new Vector3(),animSpeed);
            //cameraObject.transform.DORotate(new Vector3(0f, 180f, 0f), animSpeed);
            DOVirtual.DelayedCall(animSpeed, () => { SceneLoadManager.Instnce.LoadScene("Game"); });
        }
    }

    /// <summary>
    /// VR操作版
    /// </summary>
    private void PlayerSelect_VR()
    {
        //ボタンを押したらメインゲームへ、その際にプレイヤーを決定する
        if (roteFlag && Input.GetButtonDown("RightTrigger") || roteFlag && Input.GetButtonDown("LeftTrigger"))
        {
            //cameraObject.transform.DOLocalMove(new Vector3(), animSpeed);
            //cameraObject.transform.DORotate(new Vector3(0f, 180f, 0f), animSpeed);
            DOVirtual.DelayedCall(animSpeed, () => { SceneLoadManager.Instnce.LoadScene("Game"); });
        }
        else if (!roteFlag && Input.GetButtonDown("RightTrigger") || !roteFlag && Input.GetButtonDown("LeftTrigger"))
        {
            //cameraObject.transform.DOLocalMove(new Vector3(), animSpeed);
            //cameraObject.transform.DORotate(new Vector3(0f, 180f, 0f), animSpeed);
            DOVirtual.DelayedCall(animSpeed, () => { SceneLoadManager.Instnce.LoadScene("Game"); });
        }
    }
    #endregion

}
