using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class PivotControler : MonoBehaviour
{
    [SerializeField] private float roteTime;    //Pivotを回転させるのにかける時間
    [SerializeField] private float roteValue;   //回転させる値
    [HideInInspector] public bool AnimFlag;     //アニメーションに使うフラグ
    private bool roteFlag;                      //カメラの回転に使うフラグ

    public GameObject panel1;                       //パネル1
    public Animator panelAnim;                      //パネル1のアニメーション
    [SerializeField] private GameObject panel2;     //パネル2
    [SerializeField] private Animator panelAnim2;   //パネル2のアニメーション

    //public GameObject panel1Letter;                     //パネル1に表示する文字
    //public Animator panel1LetterAnim;                   //パネル1に表示する文字のアニメーション
    //[SerializeField]private GameObject panel2Letter;    //パネル2に表示する文字
    //[SerializeField]private Animator panel2LetterAnim;  //パネル2に表示する文字のアニメーション

    //プレイヤーの番号
    public static int playerNum;

    // Start is called before the first frame update
    void Start()
    {
        //フラグなどの初期化
        playerNum = 0;
        AnimFlag = false;
        roteFlag = true;
    }

    void Update()
    {
        PivotRote();
        PlayerSelect();
    }

    private void PivotRote()
    {
        if (!AnimFlag) { return; }

        //Pivotの回転が終わったらパネルをアニメーション表示
        if (roteFlag && Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //パネル2を表示
            this.gameObject.transform.DORotate(new Vector3(0f, roteValue * -1), roteTime);
            panel1.SetActive(false);
            DOVirtual.DelayedCall(roteTime,()=> { panel2.SetActive(true); panelAnim2.Play("PanelOpen");});

            roteFlag = false;
        }
        else if (!roteFlag && Input.GetKeyDown(KeyCode.RightArrow))
        {
            //パネル１を表示
            this.gameObject.transform.DORotate(new Vector3(0f, roteValue - roteValue), roteTime);
            panel2.SetActive(false);
            DOVirtual.DelayedCall(roteTime,()=>{ panel1.SetActive(true); panelAnim.Play("PanelOpen");});

            roteFlag = true;
        }
    }

    //自機選択
    private void PlayerSelect()
    {
        //ボタンを押したらメインゲームへ、その際にプレイヤーを決定する
        if (roteFlag && Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("Game");
        }
        else if(!roteFlag && Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("Game");
        }
    }

}
