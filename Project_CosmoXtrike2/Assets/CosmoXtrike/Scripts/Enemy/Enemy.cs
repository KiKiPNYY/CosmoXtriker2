﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//エネミーの基底クラス
abstract public class Enemy : MonoBehaviour, CommonProcessing
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
    [SerializeField]
    protected GameObject bullet;
    //砲弾の向き
    [SerializeField]
    protected GameObject aim;

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
    public void Damege(int add){
        enemyHp =- add;
        if(enemyHp <= 0){
            this.transform.parent = null;
            var formationScript = this.gameObject.GetComponentInParent<Formation>();
            if(formationScript != null){
                formationScript.CheckFormation();
            }
            this.gameObject.SetActive(false);
        }
    }

    public ThisType ReturnMyType()
    {
        return ThisType.Enemy;
    }


    //Enemyの攻撃
    virtual public void Attack() {
        var target = GameObject.FindGameObjectWithTag("Player");
        aim.transform.LookAt(target.transform.position);
        Instantiate(bullet, aim.transform.position, aim.transform.rotation);
    }


    //散開。引数は散開する角度
    public void Spread(Vector3 angle){
        this.transform.Rotate(angle);
    }
}
