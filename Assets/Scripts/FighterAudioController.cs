using UnityEngine;
using UnityEngine.InputSystem;

public class FighterAudioController : MonoBehaviour
{

    public AudioSource audioSource;

    public AudioClip dashClip;

    public InputActionReference dash;

    private CharacterController _characterController;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }


    private void OnEnable()
    {
        dash.action.started += PlayDash;
    }

    private void OnDisable()
    {
        dash.action.started -= PlayDash;

    }


    void PlayDash(InputAction.CallbackContext obj)
    {
        if (_characterController.CanDash())
        {
            audioSource.pitch = 1;
            audioSource.PlayOneShot(dashClip);
        }

    }
}
