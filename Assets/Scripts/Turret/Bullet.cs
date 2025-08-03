using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public float lifetime = 2f;
    public int damage = 1;

    private Transform target;

    public void Seek(Transform _target)
    {
        target = _target;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);

        lifetime -= Time.deltaTime;
        if (lifetime <= 0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            HitTarget();
        }
    }

    void HitTarget()
    {
        if (target.CompareTag("Enemy"))
        {
            // Calculate stagger direction
            Vector3 staggerDir = (target.position - transform.position).normalized;

            // Example: If your enemy has a script with a TakeDamage(int amount, Vector3 staggerDir) method
            var enemy = target.GetComponent<EmenyController>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage, staggerDir);
            }
        }
        Destroy(gameObject);
    }
}
