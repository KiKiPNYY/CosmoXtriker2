using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class CrosshairController : MonoBehaviour
{
    //最大射角
    [SerializeField]
    float MaxDegreeX;
    [SerializeField]
    float MinDegreeX;
    [SerializeField]
    float MaxDegreeY;
    [SerializeField]
    float MinDegreeY;


    private Vector3 _angle = Vector3.zero; //ジョイコン入力
    private Vector3 _angle2 = Vector3.zero; //これは保存用
    [SerializeField] private float Sensitivity = 0.1f; //感度
    void Start()
    {
        //オイラー角は 0から360で表現するらしいので、負の角度は360から天引きとなる（例えば -30° は 330° となる）そのための計算。
        MinDegreeX += 360;
        MinDegreeY += 360;
    }

    void Update()
    {
        //射角制限に引っかかった時にロードするため、前フレームの角度を取得
        _angle2 = this.gameObject.transform.localEulerAngles;

        //Axis取得
        _angle.x = Input.GetAxis("Right_Horizontal") * Sensitivity;
        _angle.y = Input.GetAxis("Right_Vertical") * Sensitivity;

        //クロスヘアの角度をジョイコンの入力で += する
        this.gameObject.transform.localEulerAngles += _angle;

        //射角制限
        if (this.gameObject.transform.localEulerAngles.x <= MinDegreeX && this.gameObject.transform.localEulerAngles.x >= MinDegreeX - 10)
        {
            this.gameObject.transform.localEulerAngles = _angle2;
        }
        else if (this.gameObject.transform.localEulerAngles.x >= MaxDegreeX && this.gameObject.transform.localEulerAngles.x <= MaxDegreeX + 10)
        {
            this.gameObject.transform.localEulerAngles = _angle2;
        }
        else if (this.gameObject.transform.localEulerAngles.y <= MinDegreeY && this.gameObject.transform.localEulerAngles.y >= MinDegreeY - 10)
        {
            this.gameObject.transform.localEulerAngles = _angle2;
        }
        else if (this.gameObject.transform.localEulerAngles.y >= MaxDegreeY && this.gameObject.transform.localEulerAngles.y <= MaxDegreeY + 10)
        {
            this.gameObject.transform.localEulerAngles = _angle2;
        }

        //弾がレティクルの中心に飛ぶようにするにはカメラの位置と、Crosshairの座標が等しい必要がある。
        //理由は弾道のベクトル計算にメインカメラの座標を用いているためである。
        //VR化する際は、Updateの中で CameraRig座標 == Crosshair座標 とすればいいと思われる。
        //もしくは、カメラの位置をいじるときはCameraControllerの位置をいじる（上の二つをposition(0,0,0)にする）。   ←今はこっち
    }
}
