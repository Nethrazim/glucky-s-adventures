using UnityEngine;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using UnityEditor;
using Unity.VisualScripting;
using System;
using UnityEngine.InputSystem.Processors;

public class PlayerScript : MonoBehaviour
{
    public GameObject playerDead;
    public LayerMask groundLayer;
    public GameObject rayCastSource;
    public GameObject bullet;
    public GameObject mainCamera;
    public Rigidbody2D mainCameraRb;
    public GameObject bulletSpawnPoint;
    public GameObject bulletSpawnPointLeft;
    public float MaxHealth = 100;
    private float CurrentHealth;
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
    private bool isFacingRight = true;
    private HealthBarScript HealthBarScript;
    private bool isDead = false;
    private HashSet<GameObject> processed = new HashSet<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CurrentHealth = MaxHealth;
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        mainCameraRb = mainCamera.GetComponent<Rigidbody2D>();
        playerCameraDistance = gameObject.transform.position.x - mainCamera.transform.position.x;
        HealthBarScript = GameObject.FindWithTag("HealthBar").GetComponent<HealthBarScript>();
        HealthBarScript.SetMaxHealth(MaxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        IsGrounded();
        if (!keyboardInput)
        {
            return;
        }

        try
        {
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
        }
        catch (Exception ex)
        {
            
        }

        if (isDead == false && CurrentHealth <= 0)
        {
            isDead = true;
            Instantiate(playerDead, gameObject.transform.position, Quaternion.identity);
            GameObject.FindWithTag("Player").GetComponent<Renderer>().enabled = false;
        }
    }


    public void GainLife() 
    {
        CurrentHealth = MaxHealth;
        HealthBarScript.FillHealth();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (CurrentHealth > 0)
        {

            float damageTaken = 0.0f;

            if (collision.gameObject.tag == "bullets" && !processed.Contains(collision.gameObject))
            {
                damageTaken = collision.gameObject.GetComponent<BasicEnemyBullet>().Damage;
                CurrentHealth -= damageTaken;
                HealthBarScript.SetHealth(damageTaken);
                processed.Add(collision.gameObject);
            }
        }
    }
    public void jump()
    {
        if (CurrentHealth > 0)
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
    }
    public void moveRight()
    {
        if (CurrentHealth > 0)
        {
            isFacingRight = true;
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
    }

    public void stopMovingRight()
    {
        if (CurrentHealth > 0)
        {
            stopMovingCamera();
            _animator.ResetTrigger("isMovingRight");
            _animator.SetTrigger("Stop Playing");
            _rigidBody.linearVelocityX = 0;

        }
    }
    public void moveLeft()
    {

        if (CurrentHealth > 0)
        {
            isFacingRight = false;
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
        if (CurrentHealth > 0)
        {
            Debug.Log("FIRE");
            GameObject iBullet = null;

            if (isFacingRight)
            {
                iBullet = Instantiate(bullet, bulletSpawnPoint.transform.position, Quaternion.identity);
                iBullet.GetComponent<Bullet1Script>().isFlyingRight = true;
            }
            else
            {
                iBullet = Instantiate(bullet, bulletSpawnPointLeft.transform.position, Quaternion.identity);
                iBullet.GetComponent<Bullet1Script>().isFlyingRight = false;
            }
        }
    }

    public void stopMovingLeft()
    {
        if (CurrentHealth > 0)
        {
            stopMovingCamera();
            _animator.ResetTrigger("isMovingLeft");
            _animator.SetTrigger("Stop Playing");
            _rigidBody.linearVelocityX = 0;
        }
    }
    void IsGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(rayCastSource.transform.position, 1.5f, groundLayer);
    }
}
