
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_MainBattery : MonoBehaviour
{
    private Transform target;

    [Header("Attributes")]
    public float range = 1000f;
    public float fireRate = 1f;
    public float fireCountdown = 0f;

    [Header("Unit Setup Field")]
    public string enemyTag = "Ally";
    public Transform partToRotate;
    public float turnSpeed = 2f;

    [Header("Bulltet Setup Field")]
    public GameObject bulletPrefab;
    public Transform firePoint1;
    public Transform firePoint2;

    [Header("Bulltet Setup Field")]
    public GameObject bulletPrefab2;
    public Transform subPoint1;
    public Transform subPoint2;

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
        Vector3 targetposition = new Vector3(target.position.x, target.position.y, target.position.z);
        Vector3 dir = targetposition - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation,Time.deltaTime*turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(rotation.x,rotation.y,0f);

        if(fireCountdown <= 0f)
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
        Enemy_Bullet bullet = bulletGO.GetComponent<Enemy_Bullet>();
        Enemy_Bullet bullet2 = bulletGO2.GetComponent<Enemy_Bullet>();

        GameObject Subbullet = (GameObject)Instantiate(bulletPrefab2, subPoint1.position, subPoint1.rotation);
        GameObject Subbullet2 = (GameObject)Instantiate(bulletPrefab2, subPoint2.position, subPoint2.rotation);
        Enemy_Bullet Sbullet = Subbullet.GetComponent<Enemy_Bullet>();
        Enemy_Bullet Sbullet2 = Subbullet2.GetComponent<Enemy_Bullet>();

        if (bullet != null)
            bullet.Seek(target);

        if (bullet2 != null)
            bullet2.Seek(target);
        if (Sbullet != null)
            Sbullet.Seek(target);
        if (Sbullet2 != null)
            Sbullet2.Seek(target);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
