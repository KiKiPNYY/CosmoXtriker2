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
    bool target = false;

    public bool Target{
        get { return target; }
        set { target = value; }
    }

    protected override void Move(){
        base.Move();
        if (!target){
            Debug.Log("旋回中");
            FrightTurn();
        }
        //Turn(180, 3.0f);
        //if (!turnMode){ StartCoroutine( TurnStayCoroutine() ); }

    }

    float timer = 0;
    bool turnMode = true;
    private void Turn(float angle,float time) {
        if (!turnMode) {
            Debug.Log("回ってないよ");
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

    bool clockWise = true;
    //∞型に旋回する。通常の旋回状態
    void FrightTurn() {
        if (target) { return; }
        if (clockWise){
            Turn(360, TurnTime);
            Debug.Log("右回転");
        }
        else if (!clockWise) {
            Turn(-360, TurnTime);
            Debug.Log("左回転");
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
    
}
