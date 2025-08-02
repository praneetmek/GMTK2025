using UnityEngine;
using System.Collections;

public class BagController : MonoBehaviour
{
    public int HP = 100;
    public float damageTime = 0.1f;

    private Color _defaultColor;
    private MeshRenderer _mr;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _mr = gameObject.GetComponent<MeshRenderer>();
        _defaultColor = _mr.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger detected");
        if (other.gameObject.tag == "Enemy")
        {
            HP--;
            StartCoroutine(Stun());

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision Detected");    }

    IEnumerator Stun()
    {

        _mr.material.color = Color.red;
        yield return new WaitForSeconds(damageTime);
        _mr.material.color = _defaultColor;
    }
}
