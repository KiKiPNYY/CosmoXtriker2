using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraContoroler : MonoBehaviour
{
     private float defalutView;                 //カメラの有効視野
    [SerializeField] private float zoomInValue; //ズームインさせる値
    [SerializeField] private float zoomSpeed;   //ズームイン＆アウトさせるのにかける時間
    [SerializeField] private float roteSpeed;   //カメラを回転させる時間

    //アニメーションフラグ
    private bool animFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        defalutView = Camera.main.fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        //ボタンを押したらアニメーションフラグをオンにする
        if (Input.GetMouseButtonDown(0)) { animFlag = true;}
        CameraRote();
        CameraZoomIn();
        CameraZoomOut();
    }

    //カメラを回転
    private void CameraRote()
    {
        if (!animFlag) { return; }
        this.transform.DORotate(new Vector3(0, -180f, 0), roteSpeed);
    }

    //カメラをズームイン
    private void CameraZoomIn()
    {
        if (!animFlag) { return; }
        DOTween.To(() => Camera.main.fieldOfView, fovIn => Camera.main.fieldOfView = fovIn, zoomInValue, zoomSpeed);
    }

    //ズームインしている間遅延させ、再び元の倍率にズームアウト
    private void CameraZoomOut()
    {
        if(!animFlag) { return; }
        DOVirtual.DelayedCall(zoomSpeed,()=> { DOTween.To(() => Camera.main.fieldOfView, fovOut => Camera.main.fieldOfView = fovOut, defalutView, zoomSpeed + 1f); });
        animFlag = false;
    }
}
