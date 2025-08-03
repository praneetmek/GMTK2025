using UnityEngine;
using UnityEngine.AI;
using System;
using System.Collections.Generic; // Add this

public class Turtle : MonoBehaviour
{
    public Transform player;            
    public float followDistance = 2f;    
    public bool isFollowing = false;    

    public UIAnimations uIAnimations;

    private NavMeshAgent agent;

    // Add this event for when the turtle starts following
    public event Action OnStartFollowing;

    // Add a list of SFX clips
    public List<AudioClip> followSFX;
    private AudioSource audioSource;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
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

    public void StartFollowing()
    {
        if (!isFollowing)
        {
            isFollowing = true;
            PlayFollowSFX(); // Play SFX when starting to follow
            OnStartFollowing?.Invoke();
        }
    }

    // Play a random SFX from the list
    private void PlayFollowSFX()
    {
        if (followSFX != null && followSFX.Count > 0 && audioSource != null)
        {
            int index = UnityEngine.Random.Range(0, followSFX.Count);
            audioSource.PlayOneShot(followSFX[index]);
        }
    }
}
