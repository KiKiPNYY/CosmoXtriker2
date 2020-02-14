


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DualPurpose_gun : MonoBehaviour
{
    private Transform target;

    [Header("Attributes")]
    public float range = 1000f;
    public float fireRate = 1f;
    public float fireCountdown = 0f;

    [Header("Unit Setup Field")]
    public string enemyTag = "EnemyFlight";
    public Transform partToRotate;
    public float turnSpeed = 10f;
    public Transform partToYRotate;
    public float YturnSpeed = 5f;

    [Header("Bulltet Setup Field")]
    public GameObject bulletPrefab;
    public Transform firePoint1;
    public Transform firePoint2;

    public GameObject parent;
    public GameObject baseparent;



    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    // Update is called once per frame
    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach(GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position,enemy.transform.position);
            if(distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if(nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }else
        {
            target = null;
        }
    }

    void Update()
    {
        if(target == null)
        return;
        Vector3 targetposition = new Vector3(target.position.x, transform.position.y, target.position.z);
        Vector3 dir = targetposition - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation,Time.deltaTime*turnSpeed).eulerAngles;
        partToRotate.transform.parent = parent.transform;
        partToRotate.transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        Vector3 targetpositionY = new Vector3(target.position.x, target.position.y, transform.position.z);
        Vector3 dirY = targetpositionY - transform.position;
        Quaternion lookRotationZ = Quaternion.LookRotation(dirY);
        Vector3 rotationY = Quaternion.Lerp(partToYRotate.rotation, lookRotationZ, Time.deltaTime * YturnSpeed).eulerAngles;
        partToYRotate.transform.parent = baseparent.transform;
        partToYRotate.transform.localRotation = Quaternion.Euler(rotationY.x, 0f, 0f);
        //Quaternion lockRotation = Quaternion.LookRotation(dirY, Vector3.up);
        //lockRotation.x = 90;
        //lockRotation.y = 0;
        //partToYRotate.transform.rotation = Quaternion.Lerp(transform.rotation, lockRotation, Time.deltaTime * YturnSpeed);

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint1.position, firePoint1.rotation);
        GameObject bulletGO2 = (GameObject)Instantiate(bulletPrefab, firePoint2.position, firePoint2.rotation);
        Bullet2 bullet = bulletGO.GetComponent<Bullet2>();
        Bullet2 bullet2 = bulletGO2.GetComponent<Bullet2>();

        if (bullet != null)
            bullet.Seek(target);
        if (bullet2 != null)
            bullet2.Seek(target);
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
