using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    protected float speed = 0.1f;
    //エネミーの攻撃力
    protected int attack;
    //エネミーの弾の種類
    protected GameObject bullet;

    //Enemyの移動
    virtual protected void Move() {
        transform.Translate(0f, 0f,speed);
    }

    //Enemyのダメージ。インターフェースで実装

    //Enemyの攻撃
    virtual public void Attack() {

    }

}
