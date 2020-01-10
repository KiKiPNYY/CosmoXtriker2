using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : Enemy{


    bool turnMode = false;
    //割り振られたnumber。
    public int number = 0;
    //自機を狙っているか
    bool target;

    public bool Target{
        get { return target; }
        set { target = value; }
    }

    protected override void Move(){
        base.Move();
        Turn(180, 3.0f);
        if (!turnMode){ StartCoroutine( TurnStayCoroutine() );
        }

    }

    float timer = 0;
    private void Turn(float angle,float time) {
        if (!turnMode) { Debug.Log(timer); return; }
        timer += Time.deltaTime;
        this.transform.Rotate(0, (angle / time) * Time.deltaTime, 0);
        if ( timer >= time){
            Debug.Log("turn End");
            turnMode = false;
            timer = 0;
            return;
        }
    }

    bool isRunning = false;
    IEnumerator TurnStayCoroutine() {
        if (isRunning) yield break;
        isRunning = true;
        yield return new WaitForSeconds(3.0f);
        turnMode = true;
        isRunning = false;
    }
    
}
