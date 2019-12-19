using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class CameraControl : MonoBehaviour
{
    //上から回転させるカメラ、回転速度
    [SerializeField] private GameObject cameraObject;
    [SerializeField] private float rotSpeed;

    //上からズームさせるカメラ、デフォルト値、ズーム最小値、ズーム速度
    [SerializeField] private Camera camera;
    [SerializeField] private float defView;
    [SerializeField] private float minView;
    [SerializeField] private float zoomSpeed;

    private float view;

    //アニメーションフラグ
    private bool animFlag;

    //タイトルキャンバス
    [SerializeField] private GameObject titleCanvas;


    // Start is called before the first frame update
    void Start()
    {
        view = camera.fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        view = camera.fieldOfView;

        if (Input.GetMouseButtonDown(0)) { animFlag = true; }

        CameraRotation();
        CameraZoom();
    }

    private void CameraRotation()
    {
        if (!animFlag) { return; }

        iTween.RotateTo(cameraObject, iTween.Hash("y", -180f, "time", rotSpeed));
        Destroy(titleCanvas);
        //camera.transform.rotation = Quaternion.RotateTowards(camera.transform.rotation, Quaternion.Euler(0, 180f, 0), rotSpeed);
    }

    private void CameraZoom()
    {
        if (!animFlag) { return; }

        iTween.ValueTo(camera.gameObject, iTween.Hash("from", view,"to", minView,"time", zoomSpeed));
        //iTween.ValueTo(camera.gameObject, iTween.Hash("from", view,"to", defView,"time", zoomSpeed,"delay",zoomSpeed));

        camera.fieldOfView = Mathf.Clamp(value: view, min: minView, max: defView);

        animFlag = false;
    }
}
