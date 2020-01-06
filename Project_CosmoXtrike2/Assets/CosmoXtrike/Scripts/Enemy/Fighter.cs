using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : Enemy{




    protected override void Move(){
        base.Move();

    }


    private void Turn(float angle,float time) {
        this.transform.Rotate(0, (angle/time)*Time.deltaTime , 0);
    }


}
