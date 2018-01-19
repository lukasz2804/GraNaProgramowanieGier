using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMoveByDoubleJump : BasicMove
{
    Rigidbody2D _rigidbody2D;
    BoxCollider2D _boxCollider2D;
    float _inputHorizontalAxis;
    public float _inputJumpAxis;
    public float _inputVerticalAxis;
    float _jumpMultiplier;
    Vector2 _velocityVec2;
    bool _isAlreadyJumping;
    bool _isDJumpBlockerOn;
    bool _doubleJumpAlreadyUsed;
    bool _boxCollIsTouchingLayers;

    public float PowerOfJump;
    public float PowerOfSecondJump;
    public float PowerOfWindSteer;
    public float PowerOfJumpSteer;
    // Use this for initialization
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _jumpMultiplier = 2.5f;

        if (_rigidbody2D == null || _boxCollider2D == null)
        {
            Debug.LogError("wrong GetComponent()");
        }
    }

    IEnumerator JumpDelay()
    {
        yield return new WaitForSeconds(1.2f);
        _isAlreadyJumping = false;
    }


    // Update is called once per frame
    void Update()
    {
        _inputHorizontalAxis = Input.GetAxis("Horizontal");
        _inputJumpAxis = Input.GetAxis("Jump");
        _inputVerticalAxis = Input.GetAxis("Vertical");
        _boxCollIsTouchingLayers = _boxCollider2D.IsTouchingLayers();

        if (Mathf.Abs(_inputHorizontalAxis) > 0.3 && !_boxCollIsTouchingLayers )
        {
            Debug.Log("dsada");
            _rigidbody2D.AddForce(Vector2.right * PowerOfWindSteer * _inputHorizontalAxis);
           
        }

        if (Mathf.Abs(_inputJumpAxis) > 0.3 && _boxCollIsTouchingLayers && !_isAlreadyJumping)
        {
            Debug.Log("jump");
            _isDJumpBlockerOn = true;
            _isAlreadyJumping = true;
            StartCoroutine(JumpDelay());
            _velocityVec2 = Vector2.up * PowerOfJump * _inputJumpAxis * _jumpMultiplier;
            _velocityVec2.x = PowerOfWindSteer * _inputHorizontalAxis * PowerOfJumpSteer;
            _rigidbody2D.AddForce(_velocityVec2);
        }

        if(Mathf.Abs(_inputVerticalAxis) > 0.1 && !_boxCollIsTouchingLayers && !_doubleJumpAlreadyUsed  &&  !_isDJumpBlockerOn)
        {
            _doubleJumpAlreadyUsed = true;
            _velocityVec2.y = PowerOfSecondJump * _inputVerticalAxis * 8.0f * _jumpMultiplier;
            _velocityVec2.x = PowerOfSecondJump * _inputHorizontalAxis * 0.2f * _jumpMultiplier;
            _rigidbody2D.AddForce(_velocityVec2);
            Debug.Log("double Jump");
        }
      //  Debug.Log("update");
        if (_boxCollIsTouchingLayers)
        {
            _doubleJumpAlreadyUsed = false;
        }

        if(Mathf.Abs(_inputJumpAxis) < 0.1)
        {
            _isDJumpBlockerOn = false;
        }
        






    }
}
