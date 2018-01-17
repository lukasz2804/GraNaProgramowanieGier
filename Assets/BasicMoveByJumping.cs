using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMoveByJumping : MonoBehaviour
{
    Rigidbody2D _rigidbody2D;
    BoxCollider2D _boxCollider2D;
    float _inputHorizontalAxis;
    float _inputJumpAxis;
    float _jumpMultiplier;
    Vector2 _velocityVec2;
    bool _isAlreadyJumping;
    IEnumerator _jumpDelayCour;

    public float PowerOfJump;
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

        if (Mathf.Abs(_inputHorizontalAxis) > 0.3 && !_boxCollider2D.IsTouchingLayers())
        {
            _rigidbody2D.AddForce(Vector2.right * PowerOfWindSteer * _inputHorizontalAxis);
        }

        if (Mathf.Abs(_inputJumpAxis) > 0.3 && _boxCollider2D.IsTouchingLayers() && !_isAlreadyJumping)
        {
            _isAlreadyJumping = true;
            StartCoroutine(JumpDelay());
            _velocityVec2 = Vector2.up * PowerOfJump * _inputJumpAxis * _jumpMultiplier;
            _velocityVec2.x = PowerOfWindSteer * _inputHorizontalAxis * PowerOfJumpSteer;
            _rigidbody2D.AddForce(_velocityVec2);
        }






    }
}
