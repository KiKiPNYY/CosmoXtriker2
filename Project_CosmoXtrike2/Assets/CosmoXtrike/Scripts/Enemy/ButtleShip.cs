using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtleShip : MonoBehaviour
{

    //弾の発射位置
    [SerializeField]
    protected GameObject aim;
    //弾の種類
    [SerializeField]
    protected Bullet bullet;
    //発射方向
    [SerializeField]
    private GameObject target;
    //発射時間
    [SerializeField]
    float Time = 5.0f;

    bool coolTime = true;

    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        if (coolTime){
            Attack();
            StartCoroutine(CoolTimeCoroutine(Time));
        }
    }

    public ThisType ReturnMyType(){
        return ThisType.Enemy;
    }

    public void Attack(){
        var targetAim = target.transform.position - aim.transform.position;
        BulletManager.Instnce.Fire(bullet, aim.transform.position, targetAim, ReturnMyType());
    }

    IEnumerator CoolTimeCoroutine(float time){
        coolTime = false;
        yield return new WaitForSeconds(time);
        coolTime = true;
    }

}
