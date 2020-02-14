using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameOver : MonoBehaviour
{
    private bool gameover;
    [SerializeField] private Transform cameraPos;       //カメラを移動させる距離
    [SerializeField] private float moveX, moveY, moveZ; //カメラをx,y,z方向にどれだけ飛ばすか
    [SerializeField] private float moveSpeed;           //カメラを移動させる時間

    [SerializeField] private GameObject text;           //表示するテキスト
    [SerializeField] private float textDisplayTime;     //テキストを何秒後に表示するか

    [SerializeField] private Image fadeImage;           //透明化させるパネル
    [SerializeField] private float fadeSpeed;           //フェードアウト
    private float red, green, blue, alpha;              //パネルの色、不透明度
    private bool fadeOutStart;
    
    private void Start()
    {
        text.SetActive(false);

        red = fadeImage.color.r;
        green = fadeImage.color.g;
        blue = fadeImage.color.b;
        alpha = fadeImage.color.a;

        alpha = 0;

        gameover = false;
        fadeOutStart = false;

    }

    // Update is called once per frame
    private void Update()
    {
        //プレイヤーがゲームオーバーになったらこの先の処理を通す
        if (!gameover) { return; }

        CameraSecession();
        Invoke("TextDisplay", textDisplayTime);
        Invoke("StartFadeOut", textDisplayTime + 7f);

        //if(Input.GetButtonDown("RightTrigger") || Input.GetButtonDown("LeftTrigger") || Input.GetMouseButtonDown(0)){ fadeOutStart = true; }

        //StartFadeOut();
    }

    /// <summary>
    /// カメラの移動
    /// </summary>
    /// <param name="vtr"></param>
    private void CameraSecession()
    {
        cameraPos.DOMove(new Vector3(cameraPos.position.x + moveX,cameraPos.position.y + moveY,cameraPos.position.z + moveZ), moveSpeed);
    }

    /// <summary>
    /// テキストの表示
    /// </summary>
    /// <param name="time"></param>
    private void TextDisplay()
    {
        text.SetActive(true);
    }

    /// <summary>
    /// フェードアウト
    /// </summary>
    /// <param name="speed"></param>
    private void StartFadeOut()
    {
        //if (!fadeOutStart) { return; }

        fadeImage.enabled = true;
        alpha += fadeSpeed;
        SetAlpha();

        if (alpha >= 1)
        {
            SceneLoadManager.Instnce.LoadScene("Title");
        } 
       
    }

    /// <summary>
    /// 透明度の反映
    /// </summary>
    private void SetAlpha()
    {
        fadeImage.color = new Color(red, green, blue, alpha);
    }

}
