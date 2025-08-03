using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class MotherTurtle : MonoBehaviour
{
    [Header("Turtle Settings")]
    public GameObject turtlePrefab;
    public int maxTurtles = 10;
    public int turtlesToSpawn = 3;
    public LoopScript loop;
    public List<Transform> spawnPositions = new List<Transform>();

    private List<GameObject> spawnedTurtles = new List<GameObject>();
    private HashSet<int> occupiedPositions = new HashSet<int>();

    public UIAnimations uIAnimations;

    public Transform PlayerTransform;

    public GameObject turtleInstanceHolder; 

    // Add SFX fields
    public List<AudioClip> returnSFX;
    private AudioSource audioSource;

    void Start()
    {
        SpawnTurtles(turtlesToSpawn);
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {

    }

    public void SpawnTurtles(int count)
    {
        int turtlesToSpawn = Mathf.Min(count, maxTurtles - spawnedTurtles.Count);
        List<int> availableIndices = new List<int>();
        for (int i = 0; i < spawnPositions.Count; i++)
        {
            if (!occupiedPositions.Contains(i))
                availableIndices.Add(i);
        }

        for (int i = 0; i < turtlesToSpawn && availableIndices.Count > 0; i++)
        {
            int randIdx = Random.Range(0, availableIndices.Count);
            int posIdx = availableIndices[randIdx];
            availableIndices.RemoveAt(randIdx);

            GameObject turtle = Instantiate(turtlePrefab, spawnPositions[posIdx].position, Quaternion.identity, turtleInstanceHolder.transform);
            spawnedTurtles.Add(turtle);
            occupiedPositions.Add(posIdx);

            Turtle follower = turtle.GetComponent<Turtle>();
            follower.player = PlayerTransform;
            if (follower != null)
            {
                follower.OnStartFollowing += () =>
                {
                    occupiedPositions.Remove(posIdx);
                    spawnedTurtles.Remove(turtle);
                };
            }
        }
    }

    public void OnPlayerReturnedTurtle(int turtlesToSpawn)
    {
        SpawnTurtles(turtlesToSpawn);
        PlayReturnSFX(); // Play SFX when player returns turtles
    }

    // Play a random SFX from the list
    private void PlayReturnSFX()
    {
        if (returnSFX != null && returnSFX.Count > 0 && audioSource != null)
        {
            int index = Random.Range(0, returnSFX.Count);
            audioSource.PlayOneShot(returnSFX[index]);
        }
    }

    // Interaction logic similar to Turtle.cs
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.GetComponent<AdventurerController>().turtlesCollected.Count>0)
        {
            other.GetComponent<AdventurerController>().isOnMotherTurtle = this;
            uIAnimations.ShowUI();
            AdventurerController adventurer = other.GetComponent<AdventurerController>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<AdventurerController>().isOnMotherTurtle = null;
            uIAnimations.HideUI();
            AdventurerController adventurer = other.GetComponent<AdventurerController>();
        }
    }

    public void InteractWithMotherTurtle(AdventurerController adventurer)
    {
        uIAnimations.HideUI();
        if (adventurer == null) return;

        int turtlesFollowing = adventurer.turtlesCollected.Count;
        if (turtlesFollowing > 0)
        {
            SpawnTurtles(turtlesFollowing);
            PlayReturnSFX(); // Play SFX when player returns turtles
            adventurer.ClearFollowingTurtles();
        }
        StartCoroutine(AddTurtlesToLoop(turtlesFollowing));
    }

    private IEnumerator AddTurtlesToLoop(int numTurtles)
    {
        for (int i = 0; i< numTurtles; i++)
        {
            loop.AddTurtle();
            yield return new WaitForSeconds(0.3f);
        }
    }
}
