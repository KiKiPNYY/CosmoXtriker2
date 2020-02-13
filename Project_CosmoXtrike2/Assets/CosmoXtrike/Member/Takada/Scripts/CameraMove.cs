using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraMove : MonoBehaviour
{
    private bool gameover;
    [SerializeField] private GameObject cameraObject;   //カメラ
    [SerializeField] private Vector3 cameraVector;      //カメラを移動させる距離
    [SerializeField] private float moveSpeed;            //カメラを移動させる時間

    // Update is called once per frame
    void Update()
    {
        if (!gameover) { return; }

        CameraSecession(cameraVector);
    }

    /// <summary>
    /// カメラの移動
    /// </summary>
    private void CameraSecession(Vector3 vtr)
    {
        cameraObject.transform.DOMove(new Vector3(vtr.x, vtr.y, vtr.z), moveSpeed);
    }

}
