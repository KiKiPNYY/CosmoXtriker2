using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    #region グローバル変数
    [SerializeField]
    private Transform spawnPos;
    [SerializeField]
    private GameObject ship;
    [SerializeField, Header("移動速度")]
    private Vector2 movePower = new Vector2 (1,1);
    [SerializeField, Header("速度限界")]
    private Vector2 maxSpeed = new Vector2(100, 50);
    [SerializeField, Header("移動限界座標")]
    private Vector2 movementLimit = new Vector2(100, 100);
    [SerializeField, Header("ブレーキ力")]
    private Vector2 brakePower = new Vector2(0.95f, 0.9f);
    [SerializeField, Header("傾き")]
    private Vector2 angle = new Vector2(11, 20);
    #endregion


    void Start()
    {
        
    }
    
    void Update()
    {

    }
}
