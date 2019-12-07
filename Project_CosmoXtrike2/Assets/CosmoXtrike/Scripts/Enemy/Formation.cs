using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Formation : MonoBehaviour
{

    private List<Enemy> planes = new List<Enemy>();
    private Vector3[] discrete = new Vector3[4] 
    { new Vector3(30, 5, 0),
      new Vector3(30, 15, 0),
      new Vector3(30, 30, 0),
      new Vector3(30, 45, 0)
    };
    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        //デバッグ用
        if (Input.GetKeyDown(KeyCode.P)) { CheckFormation(); }
    }

    //機体数をチェックする
    void CheckFormation(){
        bool flagship = true;
        if (planes.Count != this.transform.childCount) {
            LlstUpdate();
            flagship = CheckFlagShip();
        }
        if (!flagship) {
            int i = 0;
            foreach(Enemy enemy in planes){
                enemy.FlagshipCrash = true;
                enemy.Spread(discrete[i]);
                i++;
            }
        }
    }

    //フラグシップを確認する
    bool CheckFlagShip(){
        for(int i = 0; i < planes.Count-1; i++){
            if (planes[i].FlagShip){ return true; }
        }
        return false;
    }
    //リストをアップデートする
    void LlstUpdate() {
        planes.Clear();
        var childTransform = GetComponentsInChildren<Transform>();
        foreach (Transform child in childTransform){
            if(this.name != child.gameObject.name){
                planes.Add(child.gameObject.GetComponent<Enemy>());
            }
        
        }

    }

    

}
