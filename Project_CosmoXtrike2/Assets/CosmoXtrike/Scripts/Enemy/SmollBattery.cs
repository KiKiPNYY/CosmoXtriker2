using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmollBattery : MonoBehaviour
{
    [SerializeField]
    GameObject[] SmollBatterys;
    
    List<Quaternion> startSmollBatterysAngle;

    // Start is called before the first frame update
    void Start(){
        foreach(GameObject i in SmollBatterys) {
            startSmollBatterysAngle.Add(i.transform.rotation);
        }
    }

    // Update is called once per frame
    void Update(){
        
    }

    bool ClockRotate = false;
    private void BatteryRotation(){
        foreach(GameObject i in SmollBatterys) {
            if (ClockRotate) { i.transform.Rotate(0, 1, 0); }
            else if (ClockRotate) { i.transform.Rotate(0, -1, 0); }
        }
    }

    private void ResetBatteryRotate() {
        for(int i =0;i< SmollBatterys.Length; i++) {
            SmollBatterys[i].transform.rotation = startSmollBatterysAngle[i];
        }
        ClockRotate = false;
    }

    private void OnTriggerStay(Collider other){
        if(other.tag == "Player") {
            //ここにダメージ処理を書く

            BatteryRotation();
        }
    }

    private void OnTriggerExit(Collider other){
        if(other.tag == "Player") {
            ResetBatteryRotate();
        }
    }

}
