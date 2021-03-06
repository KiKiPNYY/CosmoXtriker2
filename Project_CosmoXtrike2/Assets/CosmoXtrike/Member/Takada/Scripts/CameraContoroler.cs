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
    private bool col = false;
    private Animator animator;

    //カメラのアニメーション後に消すタイトルオブジェクト
    [SerializeField] private GameObject titleObject;

    //PivotControler
    [SerializeField] private PivotControler _pivotControler;

    void Start()
    {
        //現在のカメラの倍率をデフォルトの倍率に設定
        defalutView = Camera.main.fieldOfView;
        col = false;
        animFlag = false;
         _pivotControler.transform.gameObject.SetActive(false);
    }

    void Update()
    {
        //ボタンを押したらアニメーションフラグをオンにする
        //if (Input.GetMouseButtonDown(0)) { animFlag = true;}
        if ((Input.GetButtonDown("RightTrigger") || Input.GetButtonDown("LeftTrigger") || Input.GetKeyDown(KeyCode.Space)))
        {
            animFlag = true;
            _pivotControler.transform.gameObject.SetActive(true);
        }

        CameraRote();
        CameraZoomIn();
        CameraZoomOut();
        FlagCange();
    }

    #region カメラの動き
    /// <summary>
    /// 回転
    /// </summary>
    private void CameraRote()
    {
        if (!animFlag) { return; }

        this.transform.DORotate(new Vector3(0, -180f, 0), animSpeed);
    }

    /// <summary>
    /// ズームイン
    /// </summary>
    private void CameraZoomIn()
    {
        if (!animFlag) { return; }

        DOTween.To(() => Camera.main.fieldOfView, fovIn => Camera.main.fieldOfView = fovIn, zoomInValue, animSpeed / 2);
    }

    /// <summary>
    /// ズームアウト
    /// </summary>
    private void CameraZoomOut()
    {
        if (!animFlag) { return; }

        //ズームインしている時間遅延させる
        DOVirtual.DelayedCall(animSpeed / 2, () =>
        {
            DOTween.To(() => Camera.main.fieldOfView,
            fovOut => Camera.main.fieldOfView = fovOut,
            defalutView, animSpeed / 2);
            titleObject.SetActive(false);
        });

    }

    #endregion

    /// <summary>
    /// PivotControlerのフラグをTrueにする
    /// </summary>
    private void FlagCange()
    {
        if (!animFlag) { return; }
        if (col) { return; }
        col = true;
        StartCoroutine("FlagOn");
    }

    /// <summary>
    /// コルーチン
    /// </summary>
    /// <returns></returns>
    private IEnumerator FlagOn()
    {
        yield return new WaitForSeconds(animSpeed + 1f);

        animFlag = false;
        _pivotControler.AnimFlag = true;
        _pivotControler.CallCoroutine();
    }
}
