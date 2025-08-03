using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    public Rigidbody rb;
    private Vector2 _moveDirection;
    public Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public InputActionReference move;
    public InputActionReference dash;
    public InputActionReference interact;

    public float moveSpeed;
    public float dashTime;
    public float dashSpeed;
    public float timeBetweenDashes;
    public AudioClip dashClip;
    public AudioClip[] footsteps;
    public bool dashWithEssence;

    private AudioSource _audioSource;
    private bool _isDashing;
    private float _currentDashTime;
    private float _timeSinceLastDash;
    private bool _isWalking;


    void Start()
    {
        _isDashing = false;
        _audioSource = GetComponent<AudioSource>();
        StartCoroutine(HandleStepsAudio());
    }

    // Update is called once per frame
    public virtual void Update()
    {
        _moveDirection = move.action.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        if (_isDashing)
        {
           HandleDash();
        }
        else
        {
            if(_moveDirection != Vector2.zero)
            {
                _isWalking = true;
            }
            else { 
                _isWalking = false; 
            }
            _timeSinceLastDash += Time.fixedDeltaTime;
           HandleMovement();
        }
    }

    private void OnEnable()
    {
        dash.action.started += OnDash;
    }

    private void OnDisable()
    {
        dash.action.started -= OnDash;
    }

    private void HandleMovement()
    {
        float angle = -45f * Mathf.Deg2Rad;
        float cos = Mathf.Cos(angle);
        float sin = Mathf.Sin(angle);

        Vector3 isoDirection = new Vector3(
            _moveDirection.x * cos - _moveDirection.y * sin,
            0,
            _moveDirection.x * sin + _moveDirection.y * cos
        ).normalized;

        rb.MovePosition(transform.position + isoDirection * Time.fixedDeltaTime * moveSpeed);

        // Animation blend logic
        float blendValue = Mathf.Clamp01(_moveDirection.magnitude);
        animator.SetFloat("Blend", blendValue);

        if (isoDirection != Vector3.zero) {
            transform.rotation = Quaternion.LookRotation(isoDirection);
        }
    }

    private void OnDash(InputAction.CallbackContext obj)
    {
        if(CanDash())
        {
            _audioSource.PlayOneShot(dashClip);
            _isDashing = true;
            _currentDashTime = 0;
            if (dashWithEssence)
            {
                GameManager.Instance.Essence--;
            }
        }

    }

    public bool CanDash()
    {
        bool dashCDSatisfied = _timeSinceLastDash > timeBetweenDashes;
        bool essenceDashSatisfied = !dashWithEssence || GameManager.Instance.Essence > 0;
        return dashCDSatisfied && essenceDashSatisfied;
    }
    private void HandleDash()
    {
        rb.linearVelocity = transform.forward * dashSpeed;
        _currentDashTime += Time.fixedDeltaTime;
        if (_currentDashTime > dashTime)
        {
            _isDashing = false;
            rb.linearVelocity = Vector3.zero;
            _timeSinceLastDash = 0;
        }
    }

    private IEnumerator HandleStepsAudio()
    {
        int footstepsIndex = 0;
        while (true)
        {
            if (_isWalking)
            {
                _audioSource.PlayOneShot(footsteps[footstepsIndex]);
                footstepsIndex++;
                footstepsIndex%=footsteps.Length;
            }
            yield return new WaitForSeconds(0.3f);
        }

    }


}
