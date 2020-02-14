
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet2 : MonoBehaviour
{
    private Transform target;

    public float speed = 100;
    public GameObject impactEffect;
    [SerializeField] private Effect m_effect = null;

    private void Start()
    {
        Destroy(gameObject, 2);
    }
    public void Seek(Transform _target)
    {
        target = _target;
    }
    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        //if(dir.magnitude <= distanceThisFrame)
        //{
        //    HitTarget();
        //    return;
        //}
        //transform.Translate(dir.normalized * distanceThisFrame,Space.World);
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.AddForce(transform.forward.normalized * speed,ForceMode.Force);
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("EnemyShip"))
        {
            HitTarget();
            return;
        }
        else if (col.CompareTag("EnemyFlight"))
        {
            HitTarget();
            return;
        }
    }

    void HitTarget()
    {
        //GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        //Destroy(effectIns, 2f);
        EffectManager.Instnce.EffectPlay(m_effect, this.transform);
        Destroy(gameObject,2);
    }
}
