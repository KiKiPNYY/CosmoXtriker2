using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigherSpown : MonoBehaviour
{
    [SerializeField]
    GameObject figher;

    // Start is called before the first frame update
    void Start(){
        StartCoroutine(Spown());
    }

    // Update is called once per frame
    void Update(){
        
    }

    bool isRunning = false;
    IEnumerator Spown() {
        if (isRunning) { yield break; }
        isRunning = true;
        bool check = EnemyFighterControll.Instance.CheckFighter();
        if ( check ) {
            var instanceFighter = Instantiate(figher, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(2.0f);
            EnemyFighterControll.Instance.AddFighter(instanceFighter.GetComponent<Fighter>());
        }
        yield return new WaitForSeconds(5.0f);
        StartCoroutine(Spown());
    }

}
