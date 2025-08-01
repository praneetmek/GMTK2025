using System.Collections;
using UnityEngine;

public class EmenyController : MonoBehaviour
{
    public float HP = 100;
    public float stunTime = 0.1f;

    private Color _defaultColor;

    private MeshRenderer _mr;
    private Rigidbody _rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _mr = GetComponent<MeshRenderer>();
        _rb = GetComponent<Rigidbody>();
        _defaultColor = _mr.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage, Vector3 staggerDir)
    {
        HP -= damage;
        if(HP < 0)
        {
            Destroy(gameObject);
        }
        StartCoroutine("Stun");
        StartCoroutine(Stagger(staggerDir));
    }

    IEnumerator Stun()
    {
        _mr.material.color = Color.white;
        yield return new WaitForSeconds(stunTime);
        _mr.material.color = _defaultColor;
    }

    IEnumerator Stagger(Vector3 staggerDir)
    {
        _rb.linearVelocity = staggerDir*4;
        yield return new WaitForSeconds(stunTime);
        _rb.linearVelocity = Vector3.zero;
    }
}
