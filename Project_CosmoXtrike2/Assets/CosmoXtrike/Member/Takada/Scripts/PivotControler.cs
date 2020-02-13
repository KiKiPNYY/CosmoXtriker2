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


    [SerializeField] private float m_minLeverVal = 0;
    private float m_timer = 0;
    private bool m_triggerPermission = false;
    private bool m_coroutineUpdate = false;

    public void CallCoroutine()
    {
        m_coroutineUpdate = true;
        m_triggerPermission = true;
        StartCoroutine("StartPanelAnim");
    }

    void Start()
    {

        //フラグの初期化
        AnimFlag = false;
        roteFlag = true;
        m_timer = 0;
        m_triggerPermission = true;
    }

    void Update()
    {
        //PivotRote_PC();
        //PlayerSelect_PC();
        LeverTriggerUpdate();
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
            DOVirtual.DelayedCall(roteTime, () => { panel2.SetActive(true); panelAnim2.Play("PanelOpen"); });
            DOVirtual.DelayedCall(roteTime + 1f, () => { panel2Letter.SetActive(true); panel2LetterAnim.Play("PanelLetterOpen"); });

            roteFlag = false;

        }
        else if (!roteFlag && Input.GetKeyDown(KeyCode.RightArrow))
        {
            //パネル１を表示
            this.gameObject.transform.DORotate(new Vector3(0f, roteValue - roteValue), roteTime);
            panel2.SetActive(false);
            panel2Letter.SetActive(false);
            DOVirtual.DelayedCall(roteTime, () => { panel1.SetActive(true); panelAnim.Play("PanelOpen"); });
            DOVirtual.DelayedCall(roteTime + 1f, () => { panel1Letter.SetActive(true); panel1LetterAnim.Play("PanelLetterOpen"); });

            roteFlag = true;

        }
    }

    /// <summary>
    /// VR操作版
    /// </summary>
    private void PivotRote_VR()
    {
        if (!AnimFlag) { return; }
        if (m_triggerPermission) { return; }
        if (m_coroutineUpdate) { return; }
 
        //if (roteFlag && Input.GetButtonDown("RightTrigger")) { roteFlag = false; }
        //else if (!roteFlag && Input.GetButtonDown("RightTrigger")) { roteFlag = true; }
        m_timer += Time.deltaTime;
        //Pivotの回転が終わったらパネルのアニメーションを再生し表示する
        if (roteFlag)
        {

            //パネル1を取得
            panel2.SetActive(false);
            panel2Letter.SetActive(false);

            this.gameObject.transform.DORotate(new Vector3(0f, roteValue - roteValue), roteTime);
            //StartCoroutine("StartPanelAnim");
        }
        else
        {

            //パネル2を表示
            panel1.SetActive(false);
            panel1Letter.SetActive(false);

            this.gameObject.transform.DORotate(new Vector3(0f, roteValue * -1), roteTime);
            //StartCoroutine("StartPanelAnim");
        }

        if (m_timer < roteTime) { return; }
        m_coroutineUpdate = true;
        m_timer = 0;
        StartCoroutine("StartPanelAnim");
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
        else if (!roteFlag && Input.GetKeyDown(KeyCode.Return))
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
        if (roteFlag && (Input.GetButtonDown("LeftTrigger") || Input.GetButtonDown("RightTrigger")))
        {
            //プレイヤー1に決定
            //   SceneLoadManager.Instnce.LoadScene("Game");
            CosmoXtrikerController.PlayerUseShip = UseShip.normal;
            TitleController.Instnce.ChangeScene();

        }
        else if (!roteFlag && (Input.GetButtonDown("LeftTrigger") || Input.GetButtonDown("RightTrigger")))
        {
            //プレイヤー2に決定
            //  SceneLoadManager.Instnce.LoadScene("Game");
            CosmoXtrikerController.PlayerUseShip = UseShip.missile;
            TitleController.Instnce.ChangeScene();
        }
    }
    #endregion

    /// <summary>
    /// レバー入力のUpdate
    /// </summary>
    private void LeverTriggerUpdate()
    {
        if (!AnimFlag) { return; }
        if (!m_triggerPermission) { return; }
        Debug.Log(Input.GetAxis("Right_Horizontal"));
        float x = Input.GetAxis("Right_Horizontal");
        //x += Input.GetKeyDown(KeyCode.LeftArrow) == true ? 1 : 0;
        //x += Input.GetKeyDown(KeyCode.RightArrow) == true ? -1 : 0;
        Debug.Log(x);
        if (Mathf.Abs(x) < m_minLeverVal) { return; }

        if (roteFlag)
        {
            //パネル2を表示
            panelAnim2.Play("PanelOpen", 0, -1);
            panel2LetterAnim.Play("PanelLetterOpen", 0, -1);
            //Debug.Log(panelAnim.speed);
            //panelAnim.speed = -1;
            //panel1LetterAnim.speed = -1;
            
            roteFlag = false;
        }
        else if (!roteFlag)
        {
            panelAnim.Play("PanelOpen", 0, -1);
            panel1LetterAnim.Play("PanelOpen", 0, -1);
            //panelAnim2.speed = -1;
            //panel2LetterAnim.speed = -1;
           

            roteFlag = true;
        }

        m_triggerPermission = false;
    }

    /// <summary>
    /// コルーチン
    /// </summary>
    /// <returns></returns>
    private IEnumerator StartPanelAnim()
    {
        //yield return new WaitForSeconds(roteTime);

        //パネルのアニメーション再生
        if (roteFlag)
        {

            panel1.SetActive(true);
            panelAnim.Play("PanelOpen");

            panel2.SetActive(false);
        }
        else
        {

            panel2.SetActive(true);
            panelAnim2.Play("PanelOpen");

            panel1.SetActive(false);
        }

        yield return new WaitForSeconds(1f);

        //文字のアニメーション再生
        if (roteFlag)
        {

            panel1Letter.SetActive(true);

            panel1LetterAnim.Play("PanelLetterOpen");

        }
        else
        {

            panel2Letter.SetActive(true);
            panel2LetterAnim.Play("PanelLetterOpen");
        }

        m_coroutineUpdate = false;
        m_timer = 0;
        m_triggerPermission = true;
    }
}
