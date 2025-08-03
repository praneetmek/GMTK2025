using UnityEngine;

public class OrbScript : MonoBehaviour
{
    Rigidbody _rb;
    public float floatTime = 1;
    private float _time = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        _time += Time.deltaTime;

    }
    private void FixedUpdate()
    {
        if(_time < floatTime)
        {
            return;
        }
        if (_rb.linearVelocity.magnitude < 20.0)
        {
            _rb.AddForce(new Vector3(0, transform.position.y, 0) - transform.position);
        }
    }
}
