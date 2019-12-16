using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : Enemy{

    bool fireWait = false;
    Vector3 playerPoint {
        get => new Vector3(
            0,GameObject.FindGameObjectWithTag("Player").transform.position.y,0
            ) 
            ;
    }
    
    protected override void Move(){
        this.transform.LookAt(playerPoint);
    }

    protected override void EnemyUpdate()
    {
        base.EnemyUpdate();
        
    }

    IEnumerator AttackCoroutine(){
        fireWait = true;
        yield return new WaitForSeconds(2.0f);
        Attack();
        fireWait = false;
    }
     

}
