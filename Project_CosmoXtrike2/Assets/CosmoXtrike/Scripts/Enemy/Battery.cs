using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : Enemy{

    bool fireWait = false;
    
    protected override void Move(){
        Vector3 target = GameObject.FindGameObjectWithTag("Player").transform.position;
        target.y = this.transform.position.y;
        this.transform.LookAt(target);
    }

    protected override void EnemyUpdate(){
        base.EnemyUpdate();
        if (!fireWait){ StartCoroutine(AttackCoroutine()); }
    }

    IEnumerator AttackCoroutine(){
        if (!fireWait) { Attack();}
        fireWait = true;
        yield return new WaitForSeconds(2.0f);
        fireWait = false;
    }

    bool AngleRestriction(){
        
        return true;
    }
     

}
