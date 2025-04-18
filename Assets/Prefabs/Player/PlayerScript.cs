using UnityEngine;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using UnityEditor;
using Unity.VisualScripting;

public class PlayerScript : MonoBehaviour
{
    public LayerMask groundLayer;
    public GameObject rayCastSource;
    public GameObject bullet;
    public GameObject mainCamera;
    public Rigidbody2D mainCameraRb;
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
    private float playerCameraDistance;
    public bool keyboardInput = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        mainCameraRb = mainCamera.GetComponent<Rigidbody2D>();
        playerCameraDistance = gameObject.transform.position.x - mainCamera.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        IsGrounded();
        if (!keyboardInput)
        {
            return;
        }

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

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Fire();
        }
        
        //if(!isGrounded)
           // _rigidBody.linearVelocity += Vector2.down * new Vector2(0f, 9.81f) * Time.deltaTime;
        //if (numberOfJumps >= 1)
        //{
        //  Debug.Log("CE PULA MEA DE " + numberOfJumps);
        //}
    }
    public void jump()
    {
        Debug.Log("Is Grounded" + isGrounded + "canDoubleJump " + canDoubleJump);
        if (isGrounded)
        {
            _rigidBody.linearVelocityY = jumpHeight;
            canDoubleJump = true;
        }
        else if (canDoubleJump)
        {
            _rigidBody.linearVelocityY = jumpHeight;
            canDoubleJump = false;
        }
    }
    public void moveRight()
    {
        moveCamera(true);
        // Debug.Log("D");
        //_animator.StopPlayback();
        _animator.ResetTrigger("Stop Playing");
        _animator.ResetTrigger("isMovingLeft");
        _animator.SetTrigger("isMovingRight");
        //_rigidBody.AddForce(new Vector2(Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed, 0));
        _rigidBody.linearVelocityX = moveSpeed;
        lastHitKey = KeyCode.D;
    }

    public void stopMovingRight()
    {
        stopMovingCamera();
        _animator.ResetTrigger("isMovingRight");
        _animator.SetTrigger("Stop Playing");
        _rigidBody.linearVelocityX = 0;
    }
    public void moveLeft()
    {
        moveCamera(false);
        //Debug.Log("A");
        //_animator.StopPlayback();
        _animator.ResetTrigger("Stop Playing");
        _animator.ResetTrigger("isMovingRight");
        _animator.SetTrigger("isMovingLeft");
        //Debug.Log(-(Input.GetAxis("Horizontal") * Time.deltaTime * speedMult));
        //_rigidBody.AddForce(new Vector2(-(Input.GetAxis("Horizontal") * Time.deltaTime * speedMult), 0));
        _rigidBody.linearVelocityX = -moveSpeed;
        lastHitKey = KeyCode.A;
    }

    public void moveCamera(bool right)
    {
        Vector3 targetPosition = new Vector3(gameObject.transform.position.x - playerCameraDistance, mainCamera.transform.position.y, mainCamera.transform.position.z);
        //mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPosition, moveSpeed);

        mainCamera.transform.position = targetPosition;
        //if (right)
        //{
            
        //}
       // else {
           // mainCameraRb.linearVelocityX = -moveSpeed;
        //}
    }

    public void stopMovingCamera()
    {
        //mainCameraRb.linearVelocityX = 0;
    }

    public void Fire()
    {
        Debug.Log("FIRE");
        Instantiate(bullet, bulletSpawnPoint.transform.position, Quaternion.identity);
    }

    public void stopMovingLeft()
    {
        stopMovingCamera();
        _animator.ResetTrigger("isMovingLeft");
        _animator.SetTrigger("Stop Playing");
        _rigidBody.linearVelocityX = 0;
    }
    void IsGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(rayCastSource.transform.position, 0.001f, groundLayer);
    }
}
