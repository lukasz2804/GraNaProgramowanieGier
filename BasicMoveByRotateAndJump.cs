using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMoveByRotateAndJump : MonoBehaviour
{
    Rigidbody2D _rigidbody2D;
    BoxCollider2D _boxCollider2D;
    float _inputHorizontalAxis;
    float _inputJumpAxis;
    float _jumpMultiplier;
    Vector2 _velocityVec2;
    bool _isAlreadyJumping;
    public Vector3[] points;
    public float torquePower;
    private Vector2 src;
    private Vector2 trg;

    public float PowerOfJump;
    public float PowerOfWindSteer;
    public float PowerOfJumpSteer;
    // Use this for initialization
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(src, trg);
    }


    void Start()
    {
        points = new Vector3[4];
        src = Vector2.zero;
        trg = Vector2.zero;
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

        float top = _boxCollider2D.offset.y + (_boxCollider2D.size.y / 3f);
        float btm = _boxCollider2D.offset.y - (_boxCollider2D.size.y / 3f);
        float left = _boxCollider2D.offset.x - (_boxCollider2D.size.x / 3f);
        float right = _boxCollider2D.offset.x + (_boxCollider2D.size.x / 3f);

        points[0] = transform.TransformPoint(new Vector3(left, top, 0f));
        points[1] = transform.TransformPoint(new Vector3(right, top, 0f));
        points[2] = transform.TransformPoint(new Vector3(left, btm, 0f));
        points[3] = transform.TransformPoint(new Vector3(right, btm, 0f));

        _inputHorizontalAxis = Input.GetAxis("Horizontal");
        _inputJumpAxis = Input.GetAxis("Jump");

        float xRel;
        xRel = (_inputHorizontalAxis > 0.3) ? 3000 : -3000;
        Vector2 maxPoint;
        maxPoint = points[0];
        for (int i = 0; i < 4; i++)
        {
            if (points[i].y > maxPoint.y)
                maxPoint = points[i];
            else
            if (points[i].y == maxPoint.y)
            {
                if ((_inputHorizontalAxis > 0.3) ? (points[i].x < xRel) : (points[i].x > xRel))
                {
                    maxPoint = points[i];
                }
            }

        }

        if (Mathf.Abs(_inputHorizontalAxis) > 0.3 )
        {
            src = maxPoint;
            trg = src + Vector2.right * torquePower * _inputHorizontalAxis;
            _rigidbody2D.AddForceAtPosition(Vector2.right * torquePower * _inputHorizontalAxis, maxPoint);
        }


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
