using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Formation : MonoBehaviour
{

    private List<Enemy> planes = new List<Enemy>();
    private bool Flagshipalive = true;
    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
            
    }

    //機体数をチェックする
    void CheckFormation(){
        if(planes.Count != this.transform.childCount) {
            LlstUpdate();
            if (CheckFlagShip()) {
                Flagshipalive = false;
            }
        }
    }
    //フラグシップを確認する
    bool CheckFlagShip(){
        for(int i = 0; i < planes.Count; i++){
            if (planes[i].FlagShip){ return true; }
        }
        return false;
    }

    void LlstUpdate() {
        planes.Clear();
        var childTransform = GameObject.Find("RootObject").GetComponentsInChildren<Transform>();
        foreach (Transform child in childTransform){
            planes.Add(child.gameObject.GetComponent<Enemy>());
        }
    }

}
