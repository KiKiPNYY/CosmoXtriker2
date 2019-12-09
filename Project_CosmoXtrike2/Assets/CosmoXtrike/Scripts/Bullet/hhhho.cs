using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hhhho : MonoBehaviour
{
    [SerializeField] Bullet bullet;
    void Start()
    {
        BulletManager.Instnce.AddBullet(bullet);
    }

    // Update is called once per frame
    void Update()
    {
        BulletManager.Instnce.Fire(bullet, this.transform.position + this.transform.forward * 5, ((this.transform.position + this.transform.forward) - this.transform.position).normalized, ThisType.Enemy);
    }
}
