using UnityEngine;
using System.Collections.Generic;

using System.Collections;

public class PlayerScript : MonoBehaviour
{
    public LayerMask groundLayer;
    public GameObject rayCastSource;
    public GameObject bulletSpawnPoint;
    private KeyCode lastHitKey;
    private List<GameObject> colliders = new List<GameObject>();
    public float jumpHeight = 1;
    public float moveSpeed = 1;
    //public float speedMult = 1;
    private bool canDoubleJump = false;
    private bool isGrounded = true;
    private Rigidbody2D _rigidBody;
    private Animator _animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        IsGrounded();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump();
        }

        //StartCoroutine(DelayedSetGrounded());

        if (Input.GetKey(KeyCode.D))
        {
            moveRight();
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            stopMovingRight();
        }

        if (Input.GetKey(KeyCode.A))
        {
            moveLeft();
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            stopMovingLeft();
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            
        }

        //if (numberOfJumps >= 1)
        //{
          //  Debug.Log("CE PULA MEA DE " + numberOfJumps);
        //}
    }

    IEnumerator DelayedSetGrounded()
    {
        yield return new WaitForSeconds(0.125f);
        //SetGrounded();
    }

    public void jump()
    {
        Debug.Log("Is Grounded" + isGrounded + "canDoubleJump " + canDoubleJump);
        if (isGrounded)
        {
            _rigidBody.linearVelocity = new Vector2(_rigidBody.linearVelocityX, jumpHeight);
            canDoubleJump = true;
        }
        else if (canDoubleJump)
        {
            _rigidBody.linearVelocity = new Vector2(_rigidBody.linearVelocityX, jumpHeight);
            canDoubleJump = false;
        }
    }
    public void moveRight()
    {
        // Debug.Log("D");
        //_animator.StopPlayback();
        _animator.ResetTrigger("Stop Playing");
        _animator.ResetTrigger("isMovingLeft");
        _animator.SetTrigger("isMovingRight");
        //_rigidBody.AddForce(new Vector2(Input.GetAxis("Horizontal") * Time.deltaTime * speedMult, 0));
        _rigidBody.linearVelocity = new Vector2(moveSpeed, _rigidBody.linearVelocityY);
        lastHitKey = KeyCode.D;
    }

    public void stopMovingRight()
    {
        _animator.ResetTrigger("isMovingRight");
        _animator.SetTrigger("Stop Playing");
    }
    public void moveLeft()
    {
        //Debug.Log("A");
        //_animator.StopPlayback();
        _animator.ResetTrigger("Stop Playing");
        _animator.ResetTrigger("isMovingRight");
        _animator.SetTrigger("isMovingLeft");
        //Debug.Log(-(Input.GetAxis("Horizontal") * Time.deltaTime * speedMult));
        //_rigidBody.AddForce(new Vector2(-(Input.GetAxis("Horizontal") * Time.deltaTime * speedMult), 0));
        _rigidBody.linearVelocity = new Vector2(-moveSpeed, _rigidBody.linearVelocityY);
        lastHitKey = KeyCode.A;
    }

    public void stopMovingLeft()
    {
        _animator.ResetTrigger("isMovingLeft");
        _animator.SetTrigger("Stop Playing");
    }
    void IsGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(rayCastSource.transform.position, 0.001f, groundLayer);
    }
}
