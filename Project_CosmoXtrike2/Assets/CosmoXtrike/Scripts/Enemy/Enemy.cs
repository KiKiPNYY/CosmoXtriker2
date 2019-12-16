using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "CreateScriptable/Create EnemyDate")]
public class EnemyDate : ScriptableObject{
    [SerializeField]
    [Header("初期HP")]
    int hitpoint;
    [SerializeField]
    [Header("移動速度")]
    float speed = 1.0f;
    [SerializeField]
    [Header("攻撃力")]
    int attack;

    public int HP { get { return hitpoint; } }
    public float Speed { get { return speed; } }
    public int Attack { get { return attack; } }
}

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
    EnemyDate parameter;
    //エネミーのライフ
    protected int enemyHp;
    //エネミーの弾の種類
    [SerializeField]
    protected GameObject bullet;
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

    protected void Start(){
        enemyHp = parameter.HP;
    }

    protected void FixedUpdate(){

        EnemyUpdate();
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
    public void Damege(int add){
        EffectManager.Instnce.EffectPlay(effect, this.transform);
        enemyHp -= add;
        if (enemyHp <= 0)
        {

            this.transform.parent = null;
            var formationScript = this.gameObject.GetComponentInParent<Formation>();
            if(formationScript != null){
                formationScript.CheckFormation();
            }
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
        var target = GameObject.FindGameObjectWithTag("Player");
        aim.transform.LookAt(target.transform.position);
        Instantiate(bullet, aim.transform.position, aim.transform.rotation);
    }


    //散開。引数は散開する角度
    public void Spread(Vector3 angle){
        this.transform.Rotate(angle);
    }
}
