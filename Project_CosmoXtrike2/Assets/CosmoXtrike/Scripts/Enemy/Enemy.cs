﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//エネミーの基底クラス
abstract public class Enemy : MonoBehaviour, CommonProcessing
{
    [SerializeField] private Effect effect;
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
    public void Damege(int add){
        EffectManager.Instnce.EffectPlay(effect, this.transform);
        enemyHp -= add;
        if (enemyHp <= 0)
        {

            this.transform.parent = null;
            this.gameObject.SetActive(false);
        }
    }

    public int MeteoriteDamege()
    {
        return 0;
    }

    public ThisType ReturnMyType()
    {
        return ThisType.Enemy;
    }


    //Enemyの攻撃
    virtual public void Attack() {

    }


    //散開。引数は散開する角度
    public void Spread(Vector3 angle){
        this.transform.Rotate(angle);
    }
}