using System.Collections;
using UnityEngine;

public class BagScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float damageTime = 0.1f;
    public Material damageMaterial;
    public Material essenceMaterial;
    public LoopScript loop;

    private Material _defaultMaterial;
    public MeshRenderer _mr;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
            GameManager.Instance.ChangeHP(-10);
            StartCoroutine(Stun());
            Destroy(other.gameObject);

        }

        if(other.gameObject.tag == "Essence")
        {
            loop.AddEssence();

            StartCoroutine(AbsorbEssence());
            Destroy(other.gameObject);
        }

    }


    IEnumerator Stun()
    {

        _mr.material = damageMaterial;
        yield return new WaitForSeconds(damageTime);
        _mr.material = _defaultMaterial;
    }
    IEnumerator AbsorbEssence()
    {

        _mr.material = essenceMaterial;
        yield return new WaitForSeconds(damageTime/2);
        _mr.material = _defaultMaterial;
    }
}
