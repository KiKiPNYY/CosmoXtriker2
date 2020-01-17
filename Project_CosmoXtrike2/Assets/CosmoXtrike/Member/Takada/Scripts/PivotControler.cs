using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class PivotControler : MonoBehaviour
{
    [SerializeField] private float roteTime;    //Pivotを回転させるのにかける時間
    [SerializeField] private float roteValue;   //回転させる値
    [HideInInspector] public bool AnimFlag;    
    private bool roteFlag;

    //パネルとパネルのアニメーション
    public GameObject Panel1;
    public Animator PanelAnim;
    [SerializeField] private GameObject Panel2;
    [SerializeField] private Animator PanelAnim2;

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

    // Update is called once per frame
    void Update()
    {
        PivotRote();
        PlayerSelect();
    }

    //Pivotを回転させる
    private void PivotRote()
    {
        if (!AnimFlag) { return; }

        //Pivotの回転が終わったらパネルをアニメーション表示
        if (roteFlag && Input.GetKeyDown(KeyCode.LeftArrow))
        {
            this.gameObject.transform.DORotate(new Vector3(0f, roteValue * -1), roteTime);
            Panel1.SetActive(false);
            DOVirtual.DelayedCall(roteTime,()=> { Panel2.SetActive(true); PanelAnim2.Play("PanelOpen"); /*panel2Flag = false;*/});

            roteFlag = false;
        }
        else if (!roteFlag && Input.GetKeyDown(KeyCode.RightArrow))
        {
            this.gameObject.transform.DORotate(new Vector3(0f, roteValue - roteValue), roteTime);
            Panel2.SetActive(false);
            DOVirtual.DelayedCall(roteTime,()=>{ Panel1.SetActive(true); PanelAnim.Play("PanelOpen"); /*panelFlag = false;*/ });

            roteFlag = true;
        }
    }

    //自機選択
    private void PlayerSelect()
    {
        //ボタンを押したらメインゲームへ
        if (roteFlag && Input.GetKeyDown(KeyCode.Return))
        {
            playerNum = 1;
            SceneManager.LoadScene("Game");
        }
        else if(!roteFlag && Input.GetKeyDown(KeyCode.Return))
        {
            playerNum = 2;
            SceneManager.LoadScene("Game");
        }
    }

}
