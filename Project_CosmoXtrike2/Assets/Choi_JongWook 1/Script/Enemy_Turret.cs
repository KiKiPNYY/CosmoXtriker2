using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Turret : MonoBehaviour
{
    private Transform target;

    [Header("Attributes")]
    public float range = 1000f;
    public float fireRate = 1f;
    public float fireCountdown = 0f;

    [Header("Unit Setup Field")]
    public string enemyTag = "Ally";
    public Transform partToRotate;
    public float turnSpeed = 10f;
    public Transform partToYRotate;
    public float YturnSpeed = 5f;
    public float maxAngle = 60;
    public float minAngle = 60;

    [Header("Bulltet Setup Field")]
    public GameObject bulletPrefab;
    public Transform firePoint;

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
        partToRotate.transform.rotation = Quaternion.Euler(0f,rotation.y,0f);

        Vector3 targetpositionY = new Vector3(target.position.x, target.position.y, transform.position.z);
        Vector3 dirY = targetpositionY - transform.position;
        Quaternion lookRotationZ = Quaternion.LookRotation(dirY);
        Vector3 rotationY = Quaternion.Lerp(partToYRotate.rotation, lookRotationZ, Time.deltaTime * YturnSpeed).eulerAngles;
        partToYRotate.transform.parent = baseparent.transform;
        partToYRotate.transform.localRotation = Quaternion.Euler(rotationY.x, 0f, 0f);

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Enemy_Bullet bullet = bulletGO.GetComponent<Enemy_Bullet>();

        if (bullet != null)
            bullet.Seek(target);
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
