using System.Collections;
using UnityEngine;

public class EmenyController : MonoBehaviour
{

    public float HP = 100;
    public float stunTime = 0.1f;
    public float staggerSpeed = 10;
    public Material DamageMaterial;

    public GameObject Target;
    public GameObject OrbPrefab;
    private Material _defaultMaterial;

    private MeshRenderer _mr;
    private Rigidbody _rb;

    private bool _isStunned;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _mr = GetComponent<MeshRenderer>();
        _rb = GetComponent<Rigidbody>();
        _defaultMaterial = _mr.material;
        _isStunned = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (_isStunned)
        {
            return;
        }

         Vector3 targetPosition = new Vector3(Target.transform.position.x, transform.position.y, Target.transform.position.z);
         transform.LookAt(targetPosition);
        _rb.linearVelocity = transform.forward;
    }

    public void TakeDamage(float damage, Vector3 staggerDir)
    {
        HP -= damage;
        if(HP <= 0)
        {
            Instantiate(OrbPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        StartCoroutine("Stun");
        StartCoroutine(Stagger(staggerDir));
    }

    IEnumerator Stun()
    {
        _isStunned = true;
        _rb.linearVelocity = Vector3.zero;
        _mr.material = DamageMaterial;
        yield return new WaitForSeconds(stunTime);
        _mr.material = _defaultMaterial;
        _isStunned = false;
    }

    IEnumerator Stagger(Vector3 staggerDir)
    {
        _rb.linearVelocity = staggerDir*4;
        yield return new WaitForSeconds(stunTime/2);
        _rb.linearVelocity = Vector3.zero;
    }
}
