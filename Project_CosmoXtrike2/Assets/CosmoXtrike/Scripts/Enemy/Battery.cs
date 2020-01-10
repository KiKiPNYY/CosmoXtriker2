using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : Enemy{

    bool fireWait = false;
    bool lockOn = false;
    Quaternion StartQuaternion;

    [SerializeField]
    int rightRotate = 45;
    [SerializeField]
    int LeftRotate = 45;

    protected override void EnemyStart(){
        //最初の回転を保存
        StartQuaternion = this.transform.rotation;

    }

    protected override void EnemyUpdate(){
        if (!PlayerDistance() ) { return; }
        Move();
        if (lockOn&&!fireWait){ StartCoroutine(AttackCoroutine()); }
    }

    protected override void Move(){
        Vector3 target = GameObject.FindGameObjectWithTag("Player").transform.position;
        target.y = this.transform.position.y;
        this.transform.LookAt(target);
        if(this.transform.rotation.y > StartQuaternion.y + rightRotate){
            this.transform.rotation = new Quaternion(0, rightRotate, 0, 0);
            lockOn = false;
        }else if(this.transform.rotation.y < StartQuaternion.y - LeftRotate) {
            this.transform.rotation = new Quaternion(0, -LeftRotate, 0, 0);
            lockOn = false;
        }else {
            lockOn = true;
        }
    }
    
    IEnumerator AttackCoroutine(){
        if (!fireWait) { Attack();}
        fireWait = true;
        yield return new WaitForSeconds(2.0f);
        fireWait = false;
    }

    bool PlayerDistance() {
        Vector3 target = GameObject.FindGameObjectWithTag("Player").transform.position;
        float answer = Vector3.Distance(target, this.transform.position);
        //if(answer > 10000) { return true; }
        //return false;
        return true;
    }

    bool AngleRestriction(){
        
        return true;
    }
     

}
