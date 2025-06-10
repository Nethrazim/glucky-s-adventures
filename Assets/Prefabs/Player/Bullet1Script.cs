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
    public float glowAmountStep = 0.25f;
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
        float parsedDistance = Vector2.Distance(transform.position, initialPosition);

        if (parsedDistance > flyDistance)
        {
            DestroyBullet();
        }

        if (parsedDistance < flyDistance / 2)
        {
            Debug.Log("Adding glow amount");
            //material.SetFloat("_GlowAmount", material.GetFloat("_GlowAmount") + glowAmountStep);
        }


        if (parsedDistance > flyDistance / 2)
        {
            Debug.Log("Substracting glow amount");
            //material.SetFloat("_GlowAmount", material.GetFloat("_GlowAmount") - glowAmountStep);
        }

        //Debug.Log(material.GetFloat("_GlowAmount"));
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
