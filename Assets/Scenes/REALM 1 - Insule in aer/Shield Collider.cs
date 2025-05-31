using UnityEngine;

public class ShieldCollider : MonoBehaviour
{
    public ParticleSystem shieldSparkles;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "bullets")
        {
            Vector3 direction = (collision.gameObject.transform.position - transform.position).normalized;
            
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            //transform.rotation = Quaternion.Euler(0, 0, angle + 180f);

            Vector2 contactPoint = collision.transform.position;
            
            if (contactPoint.x > transform.position.x)
            {
                contactPoint.x -= 0.5f;
                //angle += 90;
            }
            else
            {
                contactPoint.x += 0.5f;
                angle -= 45;
            }

            Instantiate(shieldSparkles, contactPoint, Quaternion.Euler(0, 0, angle));
        }
    }
}
