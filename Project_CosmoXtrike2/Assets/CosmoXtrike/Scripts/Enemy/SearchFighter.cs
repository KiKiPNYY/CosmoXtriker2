using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchFighter : MonoBehaviour
{
    Fighter thisFighter;

    // Start is called before the first frame update
    void Start(){
        var parent = transform.root.gameObject;
        Debug.Log("parent:" + parent.name);
        thisFighter = parent.GetComponent<Fighter>();
        Debug.Log(thisFighter.Target);
    }

    // Update is called once per frame
    void Update(){
        
    }

    private void OnTriggerEnter(Collider other){
        if (thisFighter.Target) { Debug.Log("もうターゲット指定してるよ"); return;  }
        if(other.gameObject.tag != "Player") { Debug.Log("Playerじゃないよ"); return; }
        if(EnemyFighterControll.Instance.CheckTarget()){
            thisFighter.Target = true;
            Debug.Log("索敵したよ");
        }
    }



}
