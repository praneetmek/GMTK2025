using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    public Rigidbody rb;
    private Vector2 _moveDirection;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameManager.PlayerType playerType;
    public InputActionReference move;
    public InputActionReference dash;
    public InputActionReference attack;

    public float moveSpeed;
    public float dashTime;
    public float dashSpeed;
    public float timeBetweenDashes;
    public float attackRadius;

    private bool _isDashing;
    private float _currentDashTime;
    private float _timeSinceLastDash;

    void Start()
    {
        _isDashing = false;
    }

    // Update is called once per frame
    void Update()
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
            _timeSinceLastDash += Time.fixedDeltaTime;
           HandleMovement();
        }
    }

    private void OnEnable()
    {
        dash.action.started += OnDash;
        attack.action.started += OnAttack;

    }

    private void OnDisable()
    {
        dash.action.started -= OnDash;
        attack.action.started -= OnAttack;
    }

    private void HandleMovement()
    {
        rb.MovePosition(transform.position + new Vector3(_moveDirection.x, 0, _moveDirection.y) * Time.fixedDeltaTime * moveSpeed);
        if (_moveDirection.x != 0 || _moveDirection.y != 0) {
            transform.rotation = Quaternion.LookRotation(new Vector3(_moveDirection.x, 0, _moveDirection.y));
        }
    }

    private void OnDash(InputAction.CallbackContext obj)
    {
        if(_timeSinceLastDash > timeBetweenDashes)
        {
            _isDashing = true;
            _currentDashTime = 0;
        }

    }

    private void HandleDash()
    {
        rb.linearVelocity = transform.forward * dashSpeed;
        _currentDashTime += Time.fixedDeltaTime;
        if(_currentDashTime > dashTime)
        {
            _isDashing = false;
            rb.linearVelocity = Vector3.zero;
            _timeSinceLastDash = 0;
        }
    }

    private void OnAttack(InputAction.CallbackContext obj)
    {

        Collider[] colliders = Physics.OverlapSphere(transform.position + transform.forward, attackRadius);
        foreach (var collider in colliders)
        {
            if(collider.gameObject.tag == "Enemy")
            {
                collider.gameObject.GetComponent<EmenyController>().TakeDamage(20, transform.forward);
                Debug.Log("HIT ENEMY");
            }
            else
            {
                Debug.Log(collider.tag);
            }
        }
    }
}
