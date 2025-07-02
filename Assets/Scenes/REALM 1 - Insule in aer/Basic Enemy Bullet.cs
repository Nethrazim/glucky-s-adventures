using UnityEngine;

public class BasicEnemyBullet : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Vector3 initialPosition;
    
    public float flyDistance = 20;
    public float speed;
    
    public bool isFlyingRight = false;
    public bool isRotating = false;

    public bool aimForThePlayer = false;
    public float Damage = 10;
    public float rotationSpeed = 2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialPosition = transform.position;
        _rb = gameObject.GetComponent<Rigidbody2D>();
        
        if (!aimForThePlayer)
        {
            if (isFlyingRight)
            {
                _rb.linearVelocityX = speed;
            }
            else if (!isFlyingRight)
            {
                _rb.linearVelocityX = -speed;
            }
        }

        if (aimForThePlayer)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                Vector3 direction = (player.transform.position - initialPosition).normalized;
                _rb.linearVelocity = direction * speed;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                transform.rotation = Quaternion.Euler(0, 0, angle + 180f);
            }
            
        }

        /*if (isRotating) {
            _rb.rotation = rotationSpeed;
        }*/
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(rotationSpeed * Time.deltaTime);
        if (transform.position.x > initialPosition.x + flyDistance)
        {
            DestroyBullet();
        }
    }
    

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "enemies")
        {
            DestroyBullet();
        }
    }
    private void DestroyBullet()
    {

        Debug.Log("DESTROYED BULLET");
        Destroy(gameObject);
    }
}
