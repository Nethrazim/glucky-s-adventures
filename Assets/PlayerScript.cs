using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private KeyCode lastHitKey;

    public float jumpHeight = 1;
    public float moveSpeed = 1;
    //public float speedMult = 1;
    private short numberOfJumps = 0;
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
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
            Debug.Log("Jump " + numberOfJumps );

            if (numberOfJumps < 2)
            {
                _rigidBody.linearVelocity = new Vector2(_rigidBody.linearVelocityX, jumpHeight);
                numberOfJumps++;
            }
            else
            {
                
            }
        }

            
        if (Input.GetKey(KeyCode.D))
        {
            Debug.Log("D");
            //_animator.StopPlayback();
            _animator.ResetTrigger("Stop Playing");
            _animator.ResetTrigger("isMovingLeft");
            _animator.SetTrigger("isMovingRight");
            //_rigidBody.AddForce(new Vector2(Input.GetAxis("Horizontal") * Time.deltaTime * speedMult, 0));
            _rigidBody.linearVelocity = new Vector2(moveSpeed, _rigidBody.linearVelocityY);
            lastHitKey = KeyCode.D;
        }

        if(Input.GetKeyUp(KeyCode.D))
        {
            //_animator.StopPlayback();
            _animator.ResetTrigger("isMovingRight");
            _animator.SetTrigger("Stop Playing");
        }


        if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("A");
            //_animator.StopPlayback();
            _animator.ResetTrigger("Stop Playing");
            _animator.ResetTrigger("isMovingRight");
            _animator.SetTrigger("isMovingLeft");
            //Debug.Log(-(Input.GetAxis("Horizontal") * Time.deltaTime * speedMult));
            //_rigidBody.AddForce(new Vector2(-(Input.GetAxis("Horizontal") * Time.deltaTime * speedMult), 0));
            _rigidBody.linearVelocity = new Vector2(-moveSpeed, _rigidBody.linearVelocityY);
            lastHitKey = KeyCode.A;
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            //_animator.StopPlayback();
            _animator.ResetTrigger("isMovingLeft");
            _animator.SetTrigger("Stop Playing");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collision.gameObject.tag == "platform")
        {
            //If the GameObject's name matches the one you suggest, output this message in the console
            Debug.Log("Do something here");
            numberOfJumps = 0;
        }
    }
}
