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
        Moving_2nd();
        //Moving();
        //Rotating();
    }

    void Moving_2nd()
    {
        //  入力値の取得
        float moveX = Input.GetAxis("Left_Horizontal");
        float moveY = Input.GetAxis("Left_Vertical");

        //  入力がある場合
        if (moveX != 0 || moveY != 0)
        {
            //  移動
            rig.AddForce(moveX * movePower.x, moveY * movePower.y, 0);
            if (Mathf.Abs(rig.velocity.x) > maxSpeed.x)
            {
                if (rig.velocity.x > 0)
                    rig.velocity = new Vector3(Mathf.Min(rig.velocity.x, maxSpeed.x), rig.velocity.y, 0);
                if (rig.velocity.x < 0)
                    rig.velocity = new Vector3(Mathf.Max(rig.velocity.x, -maxSpeed.x), rig.velocity.y, 0);
            }
            if (Mathf.Abs(rig.velocity.y) > maxSpeed.y)
            {
                if (rig.velocity.y > 0)
                    rig.velocity = new Vector3(rig.velocity.x, Mathf.Min(rig.velocity.y, maxSpeed.y), 0);
                if (rig.velocity.y < 0)
                    rig.velocity = new Vector3(rig.velocity.x, Mathf.Max(rig.velocity.y, -maxSpeed.y), 0);
            }
            //  回転 押しているかの判定だけ
            if (moveX != 0)
            {
                freezeTimeX = 0;
                rotateTimeX += Time.deltaTime;
                rotateTimeX = Mathf.Min(rotateTimeX, 10.0f);
            }
            if (moveY != 0)
            {
                freezeTimeY = 0;
                rotateTimeY += Time.deltaTime;
                rotateTimeY = Mathf.Min(rotateTimeY, 3.0f);
            }
        }
        //  入力がない場合、移動を止めるブレーキ処理
        if (moveX == 0 && moveY == 0)
        {
            //  一定値未満で切り捨て 移動
            if (rig.velocity.x < 0.1f && rig.velocity.x > -0.1f)
                rig.velocity = new Vector2(0, rig.velocity.y);
            if (rig.velocity.y < 0.1f && rig.velocity.y > -0.1f)
                rig.velocity = new Vector2(rig.velocity.x, 0);
            //  減速
            rig.velocity = new Vector3(rig.velocity.x * brakePower.x, rig.velocity.y * brakePower.y);
        }
        //  回転減少
        if (moveX == 0)
        {
            freezeTimeX += Time.deltaTime;
            if (freezeTimeX > 0.15f)
                rotateTimeX = 0f;
            rotateTimeX -= Time.deltaTime;
            rotateTimeX = Mathf.Max(0, rotateTimeX);
        }
        if (moveY == 0)
        {
            freezeTimeY += Time.deltaTime;
            if (freezeTimeY > 0.15f)
                rotateTimeY = 0f;
            rotateTimeY -= Time.deltaTime;
            rotateTimeY = Mathf.Max(0, rotateTimeY);
        }

        Debug.Log("X = " + rotateTimeX + " : " + "Y = " + rotateTimeY);
        //  回転 割合
        rig.rotation = Quaternion.Euler(moveY * -angle.y * Mathf.Lerp(0f,1f, rotateTimeY / 5.0f), 0, moveX * -angle.x * Mathf.Lerp(0f, 1f, rotateTimeX / 10.0f));

    }

    private float rotateTimeX = 0;
    private float rotateTimeY = 0;
    private float freezeTimeX = 0;
    private float freezeTimeY = 0;

    void Moving()
    {
        //  移動処理
        float moveX = Input.GetAxisRaw("Left_Horizontal") * Time.deltaTime * movePower.x;
        float moveY = Input.GetAxisRaw("Left_Vertical") * Time.deltaTime * movePower.y;
        //  移動の入力があったら
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

    private void Rotating()
    {
        float rotateX = Input.GetAxis("Left_Horizontal");
        float rotateY = Input.GetAxis("Left_Vertical");
        //if (rotateX <= )
        Debug.Log("RotateX = " + rotateX + " : RotateY = " + rotateY);
        //rig.rotation = Quaternion.Euler(Mathf.Lerp(rotateY, 0, Time.deltaTime), 0, Mathf.Lerp(rotateX, 0, Time.deltaTime));
        rig.rotation = Quaternion.Euler(rotateY * -angle.y, 0, rotateX * -angle.x);
    }

    void Braking()
    {

    }
}
