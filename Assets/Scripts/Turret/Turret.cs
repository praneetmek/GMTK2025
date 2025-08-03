using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("Turret Settings")]
    public float range = 10f;
    public float fireRate = 1f;
    public Transform partToRotate;
    public GameObject bulletPrefab;
    public Transform firePoint;

    private float fireCountdown = 0f;
    private Transform target;

    void Start()
    {
        
    }

    void Update()
    {
        UpdateTarget();

        if (target == null)
            return;

        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        partToRotate.rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * 10f);

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void UpdateTarget()
    {
        // If we already have a target, check if it's still valid
        if (target != null)
        {
            // Check if target is still in range and active
            float distanceToTarget = Vector3.Distance(transform.position, target.position);
            if (distanceToTarget <= range && target.gameObject.activeInHierarchy)
            {
                return; // Keep current target
            }
            else
            {
                target = null; // Lost target
            }
        }

        // Find new target if needed
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    void Shoot()
    {
        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        if (bullet != null)
        {
            bullet.Seek(target);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
