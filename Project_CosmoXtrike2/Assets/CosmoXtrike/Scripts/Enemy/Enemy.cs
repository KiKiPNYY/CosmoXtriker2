using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



//エネミーの基底クラス
abstract public class Enemy : MonoBehaviour, CommonProcessing{
    [SerializeField] private Effect effect;
    //Flag機かどうか
    [SerializeField]
    protected bool flagShip;
    [HideInInspector]
    public bool FlagShip{
        get { return flagShip; }
    }
    //エネミーのステータス
    [SerializeField]
    protected EnemyDate parameter;
    //エネミーのライフ
    protected int enemyHp;
    //エネミーの弾の種類
    [SerializeField]
    protected Bullet bullet;
    //砲弾の向きの位置
    [SerializeField]
    protected GameObject aim;

    //フラグ機が落とされたか
    protected bool flagshipCrash = false;
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

    [SerializeField]
    public int formationNum = 0;

    [SerializeField] private Vector3 m_offset = Vector3.zero;

    public Vector3 OffSet => m_offset;

    protected void Start(){
     //   Debug.Log(parameter.HP);
        enemyHp = parameter.HP;
        BulletManager.Instnce.AddBullet( bullet );
        EnemyStart();
    }

    protected void FixedUpdate(){

        EnemyUpdate();
    }

    ///<summary>
    ///継承先のStartを書くところ
    /// </summary>
    virtual protected void EnemyStart(){


    }

    /// <summary>
    /// 継承先のUpdateを書くところ　
    /// </summary>
    virtual protected void EnemyUpdate(){
        Move();
    }

    //Enemyの移動
    virtual protected void Move() {
        
        transform.Translate(0f, 0f, parameter.Speed*Time.deltaTime);
        
    }

    //Enemyのダメージ。インターフェースで実装
    virtual public void Damege(int add){
        EffectManager.Instnce.EffectPlay(effect, this.transform);
        enemyHp -= add;
        if (enemyHp <= 0)
        {
            
            //var formationScript = this.gameObject.GetComponentInParent<Formation>();
            //if(formationScript != null){
            //    formationScript.CheckFormation();
            //}
            this.transform.parent = null;
            this.gameObject.SetActive(false);
            MainGameController.Instnce.EnemyDestroyAdd();
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
        var target = GameObject.FindGameObjectWithTag("Player");
        var targetAim = target.transform.position - aim.transform.position;
        BulletManager.Instnce.Fire(bullet,aim.transform.position,targetAim,ReturnMyType());
    }


    //散開。引数は散開する角度
    public void Spread(Vector3 angle){
        this.transform.Rotate(angle);
    }
}
