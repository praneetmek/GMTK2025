using UnityEngine;
using UnityEngine.AI;

public class Turtle : MonoBehaviour
{
    public Transform player;            
    public float followDistance = 2f;    
    public bool isFollowing = false;    

    public UIAnimations uIAnimations;

    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (isFollowing)
        {
            float distance = Vector3.Distance(transform.position, player.position);

            if (distance > followDistance)
            {
                agent.SetDestination(player.position);
            }
            else
            {
                agent.ResetPath();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isFollowing)
        {
            uIAnimations.ShowUI();
            other.GetComponent<AdventurerController>().currentTurtle = this;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            uIAnimations.HideUI();
            other.GetComponent<AdventurerController>().currentTurtle = null;
        }
    }
}
