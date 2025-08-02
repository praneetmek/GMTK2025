using UnityEngine;
using UnityEngine.InputSystem;

public class FighterAudioController : MonoBehaviour
{

    public AudioSource audioSource;
    public AudioClip[] audioClips;

    public InputActionReference attack;
    private int _nextAttackClip = 0;

    private float _timeSinceLastAttack = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _timeSinceLastAttack += Time.deltaTime;
    }

    private void OnEnable()
    {
        attack.action.started += PlayAttack;
    }

    private void OnDisable()
    {
        attack.action.started -= PlayAttack;

    }
    void PlayAttack(InputAction.CallbackContext obj)
    {
        if(_timeSinceLastAttack > 0.7f)
        {
            _nextAttackClip = 0;
        }
        _timeSinceLastAttack = 0;
        audioSource.PlayOneShot(audioClips[_nextAttackClip]);
        _nextAttackClip = (_nextAttackClip + 1) % audioClips.Length;
    }
}
