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
            Vector2 contactPoint = collision.transform.position;
            Instantiate(shieldSparkles, contactPoint, Quaternion.Euler(0,0,120));
        }
    }
}
