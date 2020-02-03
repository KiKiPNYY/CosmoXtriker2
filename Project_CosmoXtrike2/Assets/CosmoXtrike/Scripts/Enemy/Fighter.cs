using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : Enemy{

    

    //割り振られたnumber。
    public int number = 0;
    //旋回の時間
    [SerializeField]
    float TurnTime = 5.0f;

    //自機を狙っているか
    [SerializeField]
    bool target = false;

    //playerのtransform
    Transform lockOnTransform;

    //ショットを撃った後か
    bool coolTimeMode = false;

    public bool Target{
        get { return target; }
        set { target = value; }
    }

    //生成されたばかりか
    bool sorite;
    //最初に向かうポイント
    Transform firstPoint;
    //最初のポイントに向かっているか
    bool goFirstPoint;
    //ぶつかりそうになったか
    bool avoidanced;
    //回避する時間
    float avoidTime = 0.5f;

    public Transform FirstPoint{
        get { return firstPoint; }
        set { firstPoint = value; }
    }


    protected override void EnemyStart(){
        base.EnemyStart();
        lockOnTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    protected override void EnemyUpdate(){
        base.EnemyUpdate();
        Shot();
    }

    public override void Damege(int add)
    {
        base.Damege(add);
        if (enemyHp <= 0&&target){ EnemyFighterControll.Instance.SelectTargetFighter(); }
    }

    protected override void Move(){
        bool avoidance = Avoidance();
        float speed = parameter.Speed;
        if (avoidance) {transform.Translate(0f, 0f, speed/2 * Time.deltaTime); }
        else if (!avoidance) { transform.Translate(0f, 0f, speed * Time.deltaTime); }
        if (sorite) { return; }
        if(avoidance && !avoidanced){ StartCoroutine(AvoidanceTime()); }

        if (target){
            LockOnPlayer();
            //Debug.Log("追跡中");
        }else if(avoidanced){
            timer = 0;
            Turn(90, avoidTime);
            return;
        }else if(!target){
            //Debug.Log("旋回中");
            FrightTurn();
        }
        //Turn(180, 3.0f);
        //if (!turnMode){ StartCoroutine( TurnStayCoroutine() ); }

    }

    void Shot(){
        if (coolTimeMode){ return; }
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit, 2000.0f)) {
            if(hit.transform.tag == "Player"){
                Attack();
                //Debug.Log("ショット！！");
                StartCoroutine( CoolTime() );
            }
        }
    }

    bool Avoidance(){
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit, 1000.0f)) {
            
                return true;
            
        }
        return false;
    }


    IEnumerator CoolTime(){
        if (coolTimeMode) { yield break; }
        coolTimeMode = true;
        yield return new WaitForSeconds(2.0f);
        coolTimeMode = false;

    }

    float timer = 0;
    bool turnMode = true;
    /// <summary>
    ///　旋回する関数
    /// </summary>
    /// <param name="angle"></param>
    /// <param name="time"></param>
    private void Turn(float angle,float time) {
        if (!turnMode) {
            //Debug.Log("回ってないよ");
            return;
        }
        timer += Time.deltaTime;
        this.transform.Rotate(0, (angle / time) * Time.deltaTime, 0, Space.World);
        if ( timer >= time){
            Debug.Log("turn End");
            //turnMode = false;
            //timer = 0;
            return;
        }
    }

    float swingTimer = 0;
    bool swingMode = true;
    /// <summary>
    /// 上下する関数
    /// </summary>
    /// <param name="angle"></param>
    /// <param name="time"></param>
    void Swing(float angle,float time) {
        if (!swingMode) { return; }
        swingTimer += Time.deltaTime;
        if (swingTimer > time) {
            //swingMode = false;
            swingTimer = 0;
        }
        else if(swingTimer >= time/2) {
            this.transform.Rotate(-(angle / (time/2) ) * Time.deltaTime, 0, 0, Space.World);
        }
        else {
            this.transform.Rotate((angle / (time/2) ) * Time.deltaTime, 0, 0, Space.World);
        }
        
    }

    /// <summary>
    /// playerの方を向く関数
    /// </summary>
    /// <param name="time"></param>
    void LockOnPlayer(){

        Quaternion targetRotation = Quaternion.LookRotation(lockOnTransform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);


    }

    void LockOnFirstPlayer(){
        Quaternion targetRotation = Quaternion.LookRotation(firstPoint.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);
    }

    bool clockWise = true;
    //∞型に旋回する。通常の旋回状態
    void FrightTurn() {
        if (target) { return; }
        if (clockWise){
            Turn(360, TurnTime);
            Swing(30, TurnTime);
            //Debug.Log("右回転");
        }
        else if (!clockWise) {
            Turn(-360, TurnTime);
            Swing(30, TurnTime);
            //Debug.Log("左回転");
        }
        if(timer >= TurnTime) {
            turnMode = true;
            clockWise = !clockWise;
            timer = 0;
        }

    }

    bool turnIsRunning = false;
    IEnumerator TurnStayCoroutine(float Time) {
        if (turnIsRunning) yield break;
        turnIsRunning = true;
        yield return new WaitForSeconds(Time);
        turnMode = true;
        turnIsRunning = false;
    }

    public void Sorite() {
        StartCoroutine(SoriteCoroutine());
    }

    IEnumerator SoriteCoroutine(){
        sorite = true;
        yield return new WaitForSeconds(3.0f);
        sorite = false;
        goFirstPoint = true;
    }

    IEnumerator AvoidanceTime() {
        if (avoidanced) { yield break; }
        avoidanced = true;
        yield return new WaitForSeconds(avoidTime);
        avoidanced = false;
    }
    
}

