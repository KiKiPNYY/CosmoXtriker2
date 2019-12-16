using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class CameraControl : MonoBehaviour
{
    //上から回転させるカメラ、回転速度、ズーム距離、ズーム速度、アニメーションフラグ
    [SerializeField] private GameObject cameraObject;
    [SerializeField] private float rotSpeed;
    [SerializeField] private float zoomDistance;
    [SerializeField] private float zoomSpeed;
    private bool animFlag;

    //タイトルキャンバス
    [SerializeField] private GameObject titleCanvas;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            animFlag = true;

        CameraRotation(rotSpeed);
        CameraZoom(zoomSpeed);
    }

    private void CameraRotation(float Speed)
    {
        if (!animFlag) { return; }

        iTween.RotateTo(cameraObject, iTween.Hash("y", -180f, "time", Speed));
        Destroy(titleCanvas);
        //camera.transform.rotation = Quaternion.RotateTowards(camera.transform.rotation, Quaternion.Euler(0, 180f, 0), rotSpeed);
    }

    private void CameraZoom(float Speed)
    {
        if (!animFlag) { return; }



        animFlag = false;
    }
}
