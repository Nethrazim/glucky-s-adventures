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
    public float hue = 0f;
    public Vector3 rotationSpeed = new Vector3(0, 0, 720);
    private Material material;
    private
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        material = GetComponent<Renderer>().material;
       
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

        //InvokeRepeating("ChangeColor", 0f, 0.1f);
    }

    void ChangeColor()
    {
        hue += speed * 0.01f;
        if (hue >= 1f) hue = 0f;

        Color rainbowColor = Color.HSVToRGB(hue, 1f, 1f);
        material.SetColor("_Color", rainbowColor);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);
        if (transform.position.x > initialPosition.x + flyDistance)
        {
            DestroyBullet();
        }

        if (transform.position.x < (initialPosition.x + flyDistance) / 2)
        {
            material.SetFloat("_GlowAmount", material.GetFloat("_GlowAmount") + (transform.position.x - initialPosition.x) * 0.25f);
        }


        if (transform.position.x > (initialPosition.x + flyDistance) / 2)
        {
            material.SetFloat("_GlowAmount", material.GetFloat("_GlowAmount") - (transform.position.x - initialPosition.x) * 0.25f);
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
