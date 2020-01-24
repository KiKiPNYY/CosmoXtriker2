﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraContoroler : MonoBehaviour
{
     private float defalutView;                 //カメラの有効視野
    [SerializeField] private float zoomInValue; //ズームインさせる値
    [SerializeField] private float animSpeed;   //カメラを回転させる時間

    //アニメーションフラグ
    private bool animFlag = false;
    private Animator animator;

    //カメラのアニメーション後に消すタイトルオブジェクト
    [SerializeField] private GameObject titleObject;

    [SerializeField] private PivotControler _pivotControler;

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
        this.transform.DORotate(new Vector3(0, -180f, 0), animSpeed);
    }

    //カメラをズームイン
    private void CameraZoomIn()
    {
        if (!animFlag) { return; }
        DOTween.To(() => Camera.main.fieldOfView, fovIn => Camera.main.fieldOfView = fovIn, zoomInValue, animSpeed / 2);
    }

    //ズームインしている間遅延させ、再び元の倍率にズームアウト
    private void CameraZoomOut()
    {
        if(!animFlag) { return; }
        DOVirtual.DelayedCall(animSpeed / 2,()=> { DOTween.To(() => Camera.main.fieldOfView, fovOut => Camera.main.fieldOfView = fovOut, defalutView, animSpeed / 2); Destroy(titleObject); });
        DOVirtual.DelayedCall(animSpeed / 2,()=> { _pivotControler.Panel1.SetActive(true); _pivotControler.PanelAnim.Play("PanelOpen"); _pivotControler.AnimFlag = true; animFlag = false; });
    }
}