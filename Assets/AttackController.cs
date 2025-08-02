using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

public class AttackController : MonoBehaviour
{
    public AudioClip[] attackHitClips;
    public AudioClip[] attackMissClips;
    public InputActionReference attack;
    public float attackMissStunDuration = 0.5f;

    public float attackRadius;


    private AudioSource _audioSource;
    private int _nextAttackHitClip = 0;
    private int _nextAttackMissClip = 0;
    private float _timeSinceLastAttack = 0;
    private float _timeSinceLastMiss = 0;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        _timeSinceLastAttack += Time.deltaTime;
        _timeSinceLastMiss += Time.deltaTime;
    }

    private void OnEnable()
    {
        attack.action.started += OnAttack;

    }

    private void OnDisable()
    {
        attack.action.started -= OnAttack;
    }


    private void OnAttack(InputAction.CallbackContext obj)
    {
        if (_timeSinceLastMiss < attackMissStunDuration)
        {
            return;
        }
        List<EmenyController> enemies = GetAttackCollisions();

        bool isHit = enemies.Count > 0;
        if (!isHit)
        {
            _timeSinceLastMiss = 0;
        }

        foreach (EmenyController enemy in enemies)
        {
            enemy.TakeDamage(20, transform.forward);
        }
        PlayAudio(isHit);
    }

    private List<EmenyController> GetAttackCollisions()
    {
        List<EmenyController> enemies = new List<EmenyController>();

        Collider[] colliders = Physics.OverlapSphere(transform.position + transform.forward, attackRadius);
        foreach (var collider in colliders)
        {
            if (collider.gameObject.tag == "Enemy")
            {
                enemies.Add(collider.gameObject.GetComponent<EmenyController>());
            }
        }
        return enemies;
    }

    private void PlayAudio(bool hit)
    {
        if (_timeSinceLastAttack > 0.7f)
        {
            _nextAttackHitClip = 0;
            _nextAttackMissClip = 0;
        }
        _timeSinceLastAttack = 0;
        if (!hit)
        {
            _audioSource.pitch = 1;
            _audioSource.PlayOneShot(attackMissClips[_nextAttackMissClip]);
            _nextAttackMissClip = (_nextAttackMissClip + 1) % attackMissClips.Length;


        }
        else
        {
            _audioSource.pitch = 1;
            _audioSource.PlayOneShot(attackHitClips[_nextAttackHitClip]);
            _nextAttackHitClip = (_nextAttackHitClip + 1) % attackHitClips.Length;


        }
    }
}
