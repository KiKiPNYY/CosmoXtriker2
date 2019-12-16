using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : Enemy{

    bool fireWait = false;
    
    
    protected override void Move(){
        Vector3 target = GameObject.FindGameObjectWithTag("Player").transform.position;
        target.y = this.transform.position.y;
        this.transform.rotation = Quaternion.LookRotation(target);
    }

    protected override void EnemyUpdate(){
        base.EnemyUpdate();
        if (!fireWait){ StartCoroutine(AttackCoroutine()); }
    }

    IEnumerator AttackCoroutine(){
        fireWait = true;
        yield return new WaitForSeconds(2.0f);
        Attack();
        fireWait = false;
    }
     

}
