using UnityEngine;

public class Health : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.ChangeHP(20);
            Destroy(gameObject);
        }   
    }

}
