using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class CameraControl : MonoBehaviour
{
    //上から回転させるカメラ、速度、フラグ
    [SerializeField] private Camera camera;
    [SerializeField] private float rotSpeed;
    private bool rotFlag;

    //上からズーム倍率のデフォルト値とズーム倍率
    private float defView = 60f;
    [SerializeField] private float minView;
    private float zoom;


    // Start is called before the first frame update
    void Start()
    {
        defView = camera.fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            rotFlag = true;
        }
        CameraRotation();

    }

    private void CameraRotation()
    {
        camera.fieldOfView = defView + zoom;

        if (!rotFlag) { return; }

        camera.transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 180f, 0), rotSpeed);

        if (defView != minView)
        {
            zoom = -1;
        }
    }
}
