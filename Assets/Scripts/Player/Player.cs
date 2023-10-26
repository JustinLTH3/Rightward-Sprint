using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    //The only Player character apear in the scene
    public static Player Instance { get; private set; }
    public bool IsAlive { get => Health > 0; }
    public int Health { get; private set; }
    public const int MaxHealth = 3;
    public UnityEvent DamageEvent { get; private set; }

    float _leftRightInput;
    [SerializeField] private float _speed;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private float _jumpHeight;
    bool _isGrounded = false;

    void Start()
    {
        if (Instance != this && Instance) Destroy(this);
        else Instance = this;
        Health = MaxHealth;
        DamageEvent ??= new();
        InputManager.InputMap.GamePlay.Jump.performed += Jump;
    }
    void Update()
    {
        _leftRightInput = InputManager.InputMap.GamePlay.LeftRight.ReadValue<float>();
        Movement();
    }
    private void OnCollisionStay2D( Collision2D collision )
    {
        _isGrounded = true;
    }
    private void OnCollisionExit2D( Collision2D collision )
    {
        _isGrounded = false;
    }
    private void Jump( InputAction.CallbackContext callbackContext )
    {
        if (_isGrounded)
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, Mathf.Sqrt(-2 * _jumpHeight * Physics2D.gravity.y));
        }
    }
    private void Movement()
    {
        if (IsAlive)
        {
            _rigidbody2D.velocity = new(_leftRightInput * _speed, _rigidbody2D.velocity.y);
        }
    }
    public void Damage( int damage )
    {
        DamageEvent.Invoke();
        Health -= damage;
        if (!IsAlive)
        {
            _rigidbody2D.Sleep();
        }
    }
}
