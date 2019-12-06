using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//エネミーの基底クラス
abstract public class Enemy : MonoBehaviour
{
    //Flag機かどうか
    [SerializeField]
    protected bool flagShip;
    [HideInInspector]
    public bool FlagShip{
        get { return flagShip; }
    }
    //エネミーのライフ
    protected int enemyHp;
    //エネミーの移動速度
    protected float speed = 1.0f;
    //エネミーの攻撃力
    protected int attack;
    //エネミーの弾の種類
    protected GameObject bullet;

    //フラグ機が落とされたか
    protected bool flagshipCrash;
    [HideInInspector]
    public bool FlagshipCrash{
        get { return flagshipCrash; }
        set { flagshipCrash = value; }
    }
    
    //散開時の角度
    protected float spreadAngle;
    [HideInInspector]
    public float SpreadAngle{
        get { return spreadAngle; }
        set { spreadAngle = value; }
    }


    protected void FixedUpdate(){

        Move();

    }

    //Enemyの移動
    virtual protected void Move() {
        
        transform.Translate(0f, 0f, speed*Time.deltaTime);
        
    }

    //Enemyのダメージ。インターフェースで実装

    //Enemyの攻撃
    virtual public void Attack() {

    }


    //散開。引数は散開する角度
    public void Spread(Vector3 angle){
        this.transform.Rotate(angle);
    }
}
