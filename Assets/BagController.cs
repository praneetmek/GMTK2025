using System.Collections;
using UnityEngine;

public class BagScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float damageTime = 0.1f;
    public Material damageMaterial;

    private Material _defaultMaterial;
    private MeshRenderer _mr;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _mr = gameObject.GetComponent<MeshRenderer>();
        _defaultMaterial = _mr.material;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            GameManager.Instance.ChangeHP(-1);
            StartCoroutine(Stun());
            Destroy(other.gameObject);

        }
    }


    IEnumerator Stun()
    {

        _mr.material = damageMaterial;
        yield return new WaitForSeconds(damageTime);
        _mr.material = _defaultMaterial;
    }
}
