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
        //
        for (int i = 0;i < fightersScript.Count; i++){
            if (!fightersScript[i].Target){
                fightersScript[i].Target = true;
                return;
            }
        }
        //すべてのターゲットモードがtrueになっているなら全てを
        //falseにして一番目をtrueにする
        foreach(Fighter i in fightersScript){
            i.Target = false;
        }
        fightersScript[0].Target = true;

        /*旧仕様。無限ループに突入する
        List<int> beforeTarget = new List<int>();
        for(int i = 0;i < fightersScript.Count; i++){
            if (fightersScript[i].Target){
                beforeTarget.Add(i);
            }
        }
        int j = 0;
         while(true){
            int x = Random.Range(0, fightersScript.Count);
            if (beforeTarget.Count <= 0){
                fightersScript[x].Target = true;
                break;
            }
            bool check = false;
            foreach(int i in beforeTarget){
                if(x == i) {
                    check = true;
                }
            }
            if (!check){
                fightersScript[x].Target = true;
                break;
            }
            j++;
            if(j >= 100){
                Debug.LogError("無限ループに突入してます");
                break;
            }
        }
          */  
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
            spownFighter.transform.parent = null;
            Fighter fighterScript = spownFighter.GetComponent<Fighter>();
            fighters.Add(spownFighter);
            AddFighter(fighterScript);
            fighterScript.FirstPoint = StartPoints[i];
            fighterScript.Sorite();
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
            fightersScript[i].Target = false;
            fightersScript[i].Sorite();
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
