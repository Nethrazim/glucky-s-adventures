using Unity.Mathematics.Geometry;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Bullet1Script : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Vector3 initialPosition;
    public float flyDistance = 20;
    public float speed;
    public bool isFlyingRight = true;

    public Vector3 rotationSpeed = new Vector3(0, 0, 720);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialPosition = transform.position;
        _rb = gameObject.GetComponent<Rigidbody2D>();
        if (isFlyingRight)
        {
            _rb.linearVelocityX = speed;
        }
        else 
        {
            _rb.linearVelocityX = -speed;
        }

        _rb.rotation = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);
        if (transform.position.x > initialPosition.x + flyDistance)
        {
            DestroyBullet();
        }
    }

    private void DestroyBullet()
    {

        Debug.Log("DESTROYED BULLET");
        Destroy(gameObject);
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        DestroyBullet();
        return;
        if (collision.gameObject.tag == "enemies")
        {
            DestroyBullet();
        }
        else if (collision.gameObject.tag == "platform")
        {
            DestroyBullet();
        }
    }
}
