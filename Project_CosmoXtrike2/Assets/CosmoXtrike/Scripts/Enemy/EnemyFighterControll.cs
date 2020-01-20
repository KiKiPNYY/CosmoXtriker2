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

    private List<Fighter> fighters = new List<Fighter>();

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
        fighters.Add(add);
        add.number = fighters.Count - 1;
    }
    /// <summary>
    /// リストから削除する
    /// </summary>
    public void OutFighter(Fighter Out) {
        fighters.RemoveAt(Out.number);
        int i = 0;
        foreach (Fighter fighter in fighters){
            fighter.number = i;
            i++;
        }
    }

    /// <summary>
    /// リストの中からtargetキャラを一機選ぶ
    /// </summary>
    public void SelectTargetFighter(){
        int i = Random.Range(0, fighters.Count);
        fighters[i].Target = true;
    }


    /// <summary>
    /// リストの要素数が０か調べる
    /// </summary>
    public bool CheckFighter(){
        if(fighters.Count == 0) { return true; }
        else { return false; }
    }

    private void Spown(){
        for(int i = 0;i < SpownPoints.Length; i++){
            var spownFighter = Instantiate(fighter, SpownPoints[i]);
            Fighter fighterScript = spownFighter.GetComponent<Fighter>();
            AddFighter(fighterScript);
            fighterScript.FirstPoint = StartPoints[i];
        }
        SelectTargetFighter();
    }

    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        if( CheckFighter()) {
            Spown();
        }
    }


}
