using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFighterControll : MonoBehaviour
{

    //シングルトン
    private static EnemyFighterControll _enemyFighterControll;

    public static EnemyFighterControll Instance{

        get {
            if(_enemyFighterControll == null) { Debug.LogError("エネミー管理のオブジェクトがありません"); }
            return _enemyFighterControll;
        }
    }

    private void NewInstance(){
        if(_enemyFighterControll== null) {
            _enemyFighterControll = this;
        }
        else if(_enemyFighterControll != null) {
            Destroy(this.gameObject);
        }
    }

    private void Awake(){
        NewInstance();
    }

    private List<GameObject> fighters = new List<GameObject>();
    private List<Fighter> fightersScript = new List<Fighter>();

    [SerializeField]
    private GameObject fighter;
    
    //スポーンするポイント
    [SerializeField]
    private Transform[] SpownPoints;
    //スポーンしてから移動するポイント
    [SerializeField]
    private Transform[] StartPoints;
    

    /// <summary>
    /// リストに追加する
    /// </summary>
    /// <param name="add"></param>
    public void AddFighter(Fighter add){
        fightersScript.Add(add);
        add.number = fightersScript.Count - 1;
    }
    /// <summary>
    /// リストから削除する
    /// </summary>
    public void OutFighter(Fighter Out) {
        fightersScript.RemoveAt(Out.number);
        int i = 0;
        foreach (Fighter fighter in fightersScript){
            fighter.number = i;
            i++;
        }
    }

    /// <summary>
    /// リストの中からtargetキャラを一機選ぶ
    /// </summary>
    public void SelectTargetFighter(){
        int beforeTarget = 9;
        for(int i = 0;i < fightersScript.Count; i++){
            if (fightersScript[i].Target){
                beforeTarget = i;
                fightersScript[i].Target = false;
            }
        }
         while(true){
            int x = Random.Range(0, fightersScript.Count);
            if(x != beforeTarget) {
                fightersScript[x].Target = true;
                return;
            }
            
        }
            
    }
        
    


    /// <summary>
    /// リストの要素数が０か調べる
    /// </summary>
    public bool CheckFighter(){
        foreach(GameObject i in fighters){
            if (i.activeInHierarchy) { return false; }
        }
        return true;
    }

    //最初のスポーン
    private void Spown(){
        for(int i = 0;i < SpownPoints.Length; i++){
            var spownFighter = Instantiate(fighter, SpownPoints[i]);
            Fighter fighterScript = spownFighter.GetComponent<Fighter>();
            fighters[i] = spownFighter;
            AddFighter(fighterScript);
            fighterScript.FirstPoint = StartPoints[i];
        }
        SelectTargetFighter();
    }

    //全てのfighterが停止したら復活する
    private void RespownFighter() {
        if( CheckFighter()) { return; }
        for (int i = 0; i < fighters.Count; i++) {
            fighters[i].SetActive(true);
            fighters[i].transform.position = SpownPoints[i].position;
            fighters[i].transform.rotation = SpownPoints[i].rotation;
        }
    }
    
    // Start is called before the first frame update
    void Start(){
        Spown();
    }

    // Update is called once per frame
    void Update(){
        if ( CheckFighter() ) {
            RespownFighter();
        }

    }


}
