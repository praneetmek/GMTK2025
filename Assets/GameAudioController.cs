using UnityEngine;

public class GameAudioController : MonoBehaviour
{
    private AudioSource audioSource;
    public GameObject mainUI;

    public AudioClip MenuClip;
    public AudioClip GameClip;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();  
        audioSource.clip = MenuClip;
    }

    // Update is called once per frame
    void Update()
    {
        if (mainUI.activeSelf && audioSource.clip == GameClip)
        {
            audioSource.clip = MenuClip;
        }
        else if (!mainUI.activeSelf && audioSource.clip == MenuClip)
        {
            audioSource.clip = GameClip;
            audioSource.Play();
        }
    }
}
