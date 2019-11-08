using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipController : MonoBehaviour
{
    #region グローバル変数
    [SerializeField]
    private Transform spawnPos;
    [SerializeField]
    private GameObject ship;
    [SerializeField, Header("移動速度")]
    private Vector2 movePower = new Vector2(1, 1);
    [SerializeField, Header("速度限界")]
    private Vector2 maxSpeed = new Vector2(100, 50);
    [SerializeField, Header("移動限界座標")]
    private Vector2 movementLimit = new Vector2(100, 100);
    [SerializeField, Header("ブレーキ力")]
    private Vector2 brakePower = new Vector2(0.95f, 0.9f);
    [SerializeField, Header("傾き")]
    private Vector2 angle = new Vector2(11, 20);
    #endregion

    Rigidbody rig;

    [SerializeField]
    private Text velocityText;

    void Start()
    {
        rig = ship.GetComponent<Rigidbody>();
    }
    
    void Update()
    {

    }

    void FixedUpdate()
    {
        Moving();
        Rotating();
    }

    void Moving()
    {
        //  移動処理
        float moveX = Input.GetAxisRaw("Left_Horizontal") * Time.deltaTime * movePower.x;
        float moveY = Input.GetAxisRaw("Left_Vertical") * Time.deltaTime * movePower.y;
        if (moveX != 0 || moveY != 0)
        {
            rig.AddForce(moveX, moveY, 0);
            if (Mathf.Abs(rig.velocity.x) > maxSpeed.x)
            {
                if (rig.velocity.x > 0)
                {
                    rig.velocity = new Vector3(Mathf.Min(rig.velocity.x, maxSpeed.x), rig.velocity.y, 0);
                }
                if (rig.velocity.x < 0)
                {
                    rig.velocity = new Vector3(Mathf.Max(rig.velocity.x, maxSpeed.x), rig.velocity.y, 0);
                }
            }
            if (Mathf.Abs(rig.velocity.y) > maxSpeed.y)
            {
                if (rig.velocity.y > 0)
                {
                    rig.velocity = new Vector3(rig.velocity.x, Mathf.Min(rig.velocity.y, maxSpeed.y), 0);
                }
                if (rig.velocity.y < 0)
                {
                    rig.velocity = new Vector3(rig.velocity.x, Mathf.Max(rig.velocity.y, maxSpeed.y), 0);
                }
            }
        }
        velocityText.text = rig.velocity.ToString("F2");

        //  ブレーキ処理
        if (moveX == 0 && moveY == 0)
        {
            //  一定値未満で切り捨て
            if (rig.velocity.x < 0.1f && rig.velocity.x > -0.1f)
                rig.velocity = new Vector2(0, rig.velocity.y);
            if (rig.velocity.y < 0.1f && rig.velocity.y > -0.1f)
                rig.velocity = new Vector2(rig.velocity.x, 0);
            //  減速
            rig.velocity = new Vector3(rig.velocity.x * brakePower.x, rig.velocity.y * brakePower.y);
        }

    }

    private float rotateTime = 0;

    private void Rotating()
    {
        float rotateX = Input.GetAxis("Right_Horizontal");
        float rotateY = Input.GetAxis("Right_Vertical");
        //if (rotateX <= )
        Debug.Log("RotateX = " + rotateX + " : RotateY = " + rotateY);
        //rig.rotation = Quaternion.Euler(Mathf.Lerp(rotateY, 0, Time.deltaTime), 0, Mathf.Lerp(rotateX, 0, Time.deltaTime));
        rig.rotation = Quaternion.Euler(rotateY * -angle.y, 0, rotateX * -angle.x);
    }
}
